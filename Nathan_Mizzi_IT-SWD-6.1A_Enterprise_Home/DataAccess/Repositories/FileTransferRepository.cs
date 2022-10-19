using Data.Context;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class FileTransferRepository : IFileTransferRepository
    {
        private FileTransferContext context;

        public FileTransferRepository(FileTransferContext _context)
        {
            context = _context;
        }

        public void AddTransfer(FileTransfer f)
        {
            context.FileTranfers.Add(f);
            context.SaveChanges();
        }

        public IQueryable<FileTransfer> getTransfers()
        {
            return context.FileTranfers;
        }
    }
}
