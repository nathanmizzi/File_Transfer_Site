using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class FileTransfer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SenderEmail { get; set; }

        [Required]
        public string RecipientEmail { get; set; }

        [Required]
        public string Title { get; set; }

        public string Message { get; set; }

        public string Password { get; set; }

        [Required]
        public string FilePath { get; set; }

    }
}
