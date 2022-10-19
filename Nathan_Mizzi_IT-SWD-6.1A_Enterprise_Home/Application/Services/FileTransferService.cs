using Application.Interfaces;
using Application.ViewModels;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services
{
    public class FileTransferService : IFileTransferService
    {
        private IFileTransferRepository fileTransferRepo;

        public FileTransferService(IFileTransferRepository _fileTransferRepo)
        {
            fileTransferRepo = _fileTransferRepo;
        }

        public void AddFileTransfer(AddFileTransferViewModel model)
        {
            fileTransferRepo.AddTransfer(new Domain.Models.FileTransfer()
            {
                RecipientEmail = model.RecipientEmail,
                SenderEmail = model.SenderEmail,
                Title = model.Title,
                Message = model.Message,
                Password = model.Password,
                FilePath = model.FilePath
            });
        }

        public IQueryable getUsersFileTransfers(string userEmail) {

            var listOfTransfers = from f in fileTransferRepo.getTransfers()
                                  where f.SenderEmail == userEmail || f.RecipientEmail == userEmail
                                  orderby f.Id descending
                                  select new FileTransferViewModel()
                                  {
                                      SenderEmail = f.SenderEmail,
                                      RecipientEmail = f.RecipientEmail,
                                      Title = f.Title,
                                      Message = f.Message,
                                      FilePath = f.FilePath
                                  };
                            
            return listOfTransfers;
        }
    }
}
