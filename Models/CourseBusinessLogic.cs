using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Online__Smart_Learning_System.Data;
using System.Linq.Expressions;

namespace Online__Smart_Learning_System.Models
{
    public class CourseBusinessLogic
    {
        private readonly ApplicationDbContext _context;
        public CourseBusinessLogic(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<CourseWithInstructor> GetAllCoursesWithInstructor()
        {
            var fetchQuery = @"SELECT Course.CourseId, Course.Title, Course.Description, Course.Price, Course.CreatedDate, Course.InstructorId,
                               Users.UserName, Course.PdfFileName, Course.PdfUrl 
                               FROM Course 
                               INNER JOIN Users ON Course.InstructorId = Users.UserId
                               ORDER BY Course.CreatedDate DESC";
            var result = new List<CourseWithInstructor>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = fetchQuery;
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var courseWithInstructor = new CourseWithInstructor(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetDecimal(3),
                            reader.GetDateTime(4),
                            reader.GetInt32(5),
                            reader.GetString(6),
                            reader.IsDBNull(7) ? null : reader.GetString(7),
                            reader.IsDBNull(8) ? null : reader.GetString(8)
                        );
                        result.Add(courseWithInstructor);
                    }
                }
            }
            return result;
        }
        public CourseWithInstructor GetCourseWithInstructor(int id)
        {
            var fetchQuery = @"SELECT Course.CourseId, Course.Title, Course.Description, Course.Price, Course.CreatedDate, Course.InstructorId,
                               Users.UserName, Course.PdfFileName, Course.PdfUrl 
                               FROM Course 
                               INNER JOIN Users ON Course.InstructorId = Users.UserId
                               WHERE Course.CourseId = @p0";
            var result = new CourseWithInstructor();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = fetchQuery;
                command.Parameters.Add(new SqlParameter("p0", id));
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new CourseWithInstructor(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetDecimal(3),
                            reader.GetDateTime(4),
                            reader.GetInt32(5),
                            reader.GetString(6),
                            reader.IsDBNull(7) ? null : reader.GetString(7),
                            reader.IsDBNull(8) ? null : reader.GetString(8)
                        );
                    }
                }
            }
            return result;
        }
        public void CreateCourse(Course course)
        {
             _context.Course.Add(course);
        }
        public Course GetCourseById(int courseId)
        {
            Course course=new Course();
            if (courseId > 0)
            {
                course = _context.Course.Find(courseId);
            }
            return course;
        }
        public void UpdateCourse(Course course)
        {
            try
            {
                var query = @"UPDATE Course SET Title = @p0, Description = @p1, InstructorId = @p2, Price = @p3,PdfUrl=@p4 WHERE CourseId = @p4";
                var query2 = @"UPDATE Course SET Title = @p0, Description = @p1, InstructorId = @p2, Price = @p3 WHERE CourseId = @p4";
                if(course.PdfUrl != null)
                {
                    _context.Database.ExecuteSqlRaw(query, course.Title, course.Description, course.InstructorId, course.Price, course.CourseId, course.PdfUrl);
                }
                else
                {
                    _context.Database.ExecuteSqlRaw(query2, course.Title, course.Description, course.InstructorId, course.Price, course.CourseId);
                }
            }
            catch (Exception ex)
            {
                // Log detailed exception information
                Console.WriteLine($"An error occurred while updating the course: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }
        public void DeleteCourse(int courseId)
        {
            try
            {
                var query = @"Delete from Course where courseId=@p0";
                int rowsAffected = _context.Database.ExecuteSqlRaw(query, courseId);
            }
            catch (Exception ex)
            {
                // Log detailed exception information
                Console.WriteLine($"An error occurred while Deleting the course: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        internal async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
