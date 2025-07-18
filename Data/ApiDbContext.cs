using Microsoft.EntityFrameworkCore;
using ApiExample.Models;
using System.Collections.Generic;

namespace ApiExample.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        // DbSet<Product> representa la tabla 'Products' en la base de datos
        public DbSet<Product> Products { get; set; }
    }
}
