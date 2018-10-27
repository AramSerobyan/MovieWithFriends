using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieTestApi.Models
{

    public class MovieContext : DbContext
    {
        MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }

        public DbSet<MovieItem> MovieItems { get; set; }

    }
}
