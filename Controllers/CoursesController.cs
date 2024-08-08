using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online__Smart_Learning_System.Data;
using Online__Smart_Learning_System.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Online__Smart_Learning_System.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly CourseBusinessLogic courseBusinessLogic;
        private readonly GoogleDriveService googleDriveService;

        public CoursesController(CourseBusinessLogic courseBusinessLogic,GoogleDriveService googleDriveService)
        {
            this.courseBusinessLogic = courseBusinessLogic;
            this.googleDriveService = googleDriveService;
        }
        public IActionResult CoursesList()
        {
            List<CourseWithInstructor> courses =courseBusinessLogic.GetAllCoursesWithInstructor();
            return View(courses);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Course course, IFormFile pdfFile)
        {
            if (pdfFile != null && pdfFile.Length > 0)
            {
                // Upload to Google Drive
                var fileUrl = await UploadFileToGoogleDrive(pdfFile);

                if (fileUrl != null)
                {
                    // Set URL and file name in the course model
                    course.PdfFileName = pdfFile.FileName;
                    course.PdfUrl = fileUrl; // Assuming PdfUrl is a new property in your Course model

                    // Save course to the database
                    courseBusinessLogic.CreateCourse(course);
                    await courseBusinessLogic.SaveChangesAsync();

                    return RedirectToAction(nameof(CoursesList));
                }
                else
                {
                    ModelState.AddModelError("", "Failed to upload PDF to Google Drive.");
                }
            }
            return RedirectToAction(nameof(CoursesList));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Course course=courseBusinessLogic.GetCourseById(id);
            return View(course);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Course course,IFormFile pdfFile)
        {
            if (pdfFile != null && pdfFile.Length > 0)
            {
                var fileUrl = await UploadFileToGoogleDrive(pdfFile);

                if (fileUrl != null)
                {
                    // Set URL and file name in the course model
                    course.PdfFileName = pdfFile.FileName;
                    course.PdfUrl = fileUrl; // Assuming PdfUrl is a new property in your Course model

                    // Save course to the database
                    courseBusinessLogic.UpdateCourse(course);
                    await courseBusinessLogic.SaveChangesAsync();

                    return RedirectToAction(nameof(CoursesList));
                }
                else
                {
                    courseBusinessLogic.UpdateCourse(course);
                    ModelState.AddModelError("", "Failed to upload PDF to Google Drive.");
                }
            }
            else
            {
                 courseBusinessLogic.UpdateCourse(course);
                 return RedirectToAction("CoursesList");
            }
            return View();
        }
        public IActionResult RemoveCourse(int id)
        {
            Course course = courseBusinessLogic.GetCourseById(id);
            Console.WriteLine(course.Title);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        [HttpPost]
        public IActionResult Remove(int courseId)
        {
            try
            {
                courseBusinessLogic.DeleteCourse(courseId);
                TempData["DeleteSuccessMsg"] = "Course Deleted Successfully!!";
                return RedirectToAction("CoursesList");
            }
            catch (Exception ex)
            {
                // Log the exception and show an error message
                Console.WriteLine($"An error occurred while deleting the course: {ex.Message}");
                // Optionally, you can pass an error message to the view
                return View("Error");
            }
        }
        public IActionResult CourseDetails(int id)
        {
            CourseWithInstructor courseWithInstructor=courseBusinessLogic.GetCourseWithInstructor(id);
            return View(courseWithInstructor);
        }
        private async Task<string> UploadFileToGoogleDrive(IFormFile file)
        {
            var fileStream = file.OpenReadStream();
            var mimeType = file.ContentType;
            var fileName = file.FileName;

            // Upload the file to Google Drive
            var fileId = await googleDriveService.UploadFileAsync(file);

            // Get the file URL
            var fileUrl = await googleDriveService.GetFileUrl(fileId);

            return fileUrl;
        }
    }
}
