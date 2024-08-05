using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online__Smart_Learning_System.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [DisplayName("Course Title")]
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        [DisplayName("Course Description")]
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        [DisplayName("Instructor Id")]
        [Required]
        [Range(1,20)]
        public int InstructorId { get; set; }

        [DisplayName("Course Price")]
        [Required]
        
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
      
        public string? PdfFileName {  get; set; }
      
        public string? PdfUrl { get; set; }
        
        public Course()
        {

        }
        public Course(string title, string description, int instructorId, decimal price, string? pdfFileName, string pdfUrl)
        {
            Title = title;
            Description = description;
            InstructorId = instructorId;
            Price = price;
            PdfFileName = pdfFileName;
            PdfUrl = pdfUrl;
            CreatedDate = DateTime.Now;
        }
        public Users? user;
    }
}
