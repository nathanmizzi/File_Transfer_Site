using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Log
    {
        [Key]
        public int Key { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public bool IsProblematic { get; set; }

        public string User { get; set; }

    }
}
