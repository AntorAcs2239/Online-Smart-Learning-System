namespace Online__Smart_Learning_System.Models
{
    public class CourseContent
    {
        public int ContentId { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public byte[] PdfFile { get; set; }
        public string Description { get; set; }
    }
}
