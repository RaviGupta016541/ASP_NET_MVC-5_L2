using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentMVC.Models
{
    public class Student
    {
        [Key]
        [Required(ErrorMessage ="Roll number is required")]
        public int Rollno { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Sname { get; set; }

        [Required(ErrorMessage = "Standard is required")]
        [Range(1,12,ErrorMessage ="Standard should be in range 1 to 12")]
        public int Std { get; set; }
    }
}
