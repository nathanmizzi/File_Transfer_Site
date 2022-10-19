using Application.Interfaces;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class LogInDbService : ILogService
    {
        private ILogRepository logRepo;

        public LogInDbService(ILogRepository _logRepo)
        {
            logRepo = _logRepo;
        }

        public void AddLog(string _message, bool _isProblematic, string _user)
        {
            logRepo.SaveLog(new Log()
            {
                Message = _message,
                IsProblematic = _isProblematic,
                User = _user
            });
        }
    }
}
