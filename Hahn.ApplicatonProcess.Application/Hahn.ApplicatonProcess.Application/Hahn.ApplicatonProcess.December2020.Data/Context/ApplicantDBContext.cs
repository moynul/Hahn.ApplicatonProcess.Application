using Hahn.ApplicatonProcess.December2020.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Context
{
    public class ApplicantDBContext : DbContext
    {
        public ApplicantDBContext(DbContextOptions<ApplicantDBContext> options) : base(options)
        {
        }
        public DbSet<Applicant> Applicants { get; set; }
    }
}
