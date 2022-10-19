using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class FileTransferContext : IdentityDbContext
    {
        public FileTransferContext(DbContextOptions<FileTransferContext> options) :
        base(options)
        { }

        public DbSet<FileTransfer> FileTranfers { get; set; }

        public DbSet<Log> logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
