using Hahn.ApplicationProcess.May2020.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.May2020.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {

        }

        public DbSet<Applicant> Applicants { get; set; }

    }
}
