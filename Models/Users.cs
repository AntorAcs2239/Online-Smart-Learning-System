using System.ComponentModel.DataAnnotations;

namespace Online__Smart_Learning_System.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string UserName {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Users() { }
        public Users(string userName, string email, string password)
        {
            UserName=email; 
            Email=password;
            Password=password;
        }
        public ICollection<Course>?Courses { get; set; }
    }
}
