using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameLiterature.Models;

namespace GameLiterature.Data
{
    public class GameLiteratureDbContext : DbContext
    {
        public GameLiteratureDbContext (DbContextOptions<GameLiteratureDbContext> options)
            : base(options)
        {
        }

        public DbSet<GameLiterature.Models.Literature> Literature { get; set; } = default!;
    }
}
