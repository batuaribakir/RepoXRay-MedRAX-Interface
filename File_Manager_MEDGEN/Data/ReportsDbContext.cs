using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_MEDGEN.Data
{
    public class ReportsDbContext : DbContext
    {
        public DbSet<UploadedFile> UploadedFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQLite connection
            optionsBuilder.UseSqlite("Data Source=ReportsDb.sqlite");
        }
    }

    public class UploadedFile
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
    }
}
