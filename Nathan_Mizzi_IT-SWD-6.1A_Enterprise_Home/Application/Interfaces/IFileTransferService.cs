using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Interfaces
{
    public interface IFileTransferService
    {
        public void AddFileTransfer(AddFileTransferViewModel Model);

        public IQueryable getUsersFileTransfers(string userEmail);
    }
}
