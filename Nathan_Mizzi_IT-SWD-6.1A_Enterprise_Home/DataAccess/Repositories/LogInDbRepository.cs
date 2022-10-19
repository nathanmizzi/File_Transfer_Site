using System;
using System.Collections.Generic;
using System.Text;
using Data.Context;
using Domain.Interfaces;
using Domain.Models;

namespace Data.Repositories
{
    public class LogInDbRepository : ILogRepository
    {
        private FileTransferContext context;

        public LogInDbRepository(FileTransferContext _context)
        {
            context = _context;
        }

        public void SaveLog(Log l)
        {
            context.logs.Add(l);
        }
    }
}
