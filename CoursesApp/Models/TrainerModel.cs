using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CoursesApp.Models
{
    public class TrainerModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage ="The email address should be in the form name@exampl.com")]
        public string Email { get; set; }
        [StringLength(250, MinimumLength = 10)]
        public string Description { get; set; }
        [Url(ErrorMessage = "Please write a correct url!")]
        public string Website { get; set; }
        public System.DateTime Creation_Date { get; set; }
        public SelectList MainTrainers { get; set; }
    }
}