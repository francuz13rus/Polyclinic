using Microsoft.EntityFrameworkCore;
using Polyclinic.Models;
using System.Collections.Generic;

namespace Polyclinic
{
    public partial class ClinicApiContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
