using Data.Context;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Data.Repositories
{
    public class LogInFileRepository : ILogRepository
    {

        private FileTransferContext context;
        private IWebHostEnvironment webHostEnviroment;

        public LogInFileRepository(FileTransferContext _context, IWebHostEnvironment _webHostEnvironment)
        {
            context = _context;
            webHostEnviroment = _webHostEnvironment;
        }

        public void SaveLog(Log l)
        {
            string fileName = "Log-" + Guid.NewGuid().ToString() + ".txt";

            string absolutePath = webHostEnviroment.WebRootPath + "\\logs\\" + fileName;

            using (FileStream fs = new FileStream(absolutePath, FileMode.CreateNew, FileAccess.Write))
            {
                string logToWrite = "Log Message: " + l.Message + " , Is Problematic: " + l.IsProblematic + " , By user: " + l.User;
                byte[] encodedLog = new UTF8Encoding(true).GetBytes(logToWrite);
                fs.Write(encodedLog, 0, encodedLog.Length);

                fs.Close();
            }
        }
    }
}
