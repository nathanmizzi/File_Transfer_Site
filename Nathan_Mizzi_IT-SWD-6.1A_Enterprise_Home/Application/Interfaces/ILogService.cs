using Application.ViewModels;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ILogService
    {
        public void AddLog(string message, bool isProblematic, string user);
    }
}
