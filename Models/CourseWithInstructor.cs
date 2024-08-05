using Microsoft.AspNetCore.Mvc;

namespace Online__Smart_Learning_System.Models
{
	public class CourseWithInstructor
	{ 
		public int CourseId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int InstructorId { get; set; }
		public decimal Price { get; set; }
		public DateTime CreatedDate { get; set; }
		public string UserName {  get; set; }
		public string? PdfFileName {  get; set; }
		public string? PdfUrl {  get; set; }
		public CourseWithInstructor() { }
		public CourseWithInstructor(int courseId, string title, string description, decimal price,
			DateTime createdDate,int instructorId, string userName,string pdfFileName, string pdfUrl)
		{
			CourseId = courseId;
			Title = title;
			Description = description;
			InstructorId = instructorId;
			Price = price;
			CreatedDate = createdDate;
			UserName = userName;
			PdfFileName = pdfFileName;
            PdfUrl = pdfUrl;
		}
        public CourseWithInstructor(int courseId, string title, string description, decimal price,
            DateTime createdDate, int instructorId, string userName)
        {
            CourseId = courseId;
            Title = title;
            Description = description;
            InstructorId = instructorId;
            Price = price;
            CreatedDate = createdDate;
            UserName = userName;
        }
    }
}
