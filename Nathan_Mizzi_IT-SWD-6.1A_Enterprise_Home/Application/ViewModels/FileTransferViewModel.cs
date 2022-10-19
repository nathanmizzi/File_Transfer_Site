using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
    public class FileTransferViewModel
    {
        public string RecipientEmail { get; set; }

        public string SenderEmail { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string FilePath { get; set; }
    }
}
