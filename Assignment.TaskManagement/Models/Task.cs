using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment.TaskManagement.Models
{
    public class Task
    {
        public Guid id { get; set; }
        [Required(ErrorMessage = "Task Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Task Description is required")]
        public string Description { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Created Date")]
        public DateTime? DateCreated { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Modified Date")]
        public DateTime? DateUpdated { get; set; }
    }
}