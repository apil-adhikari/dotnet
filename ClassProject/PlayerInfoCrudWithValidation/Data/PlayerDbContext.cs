﻿using Microsoft.EntityFrameworkCore;
using PlayerInfoCrudWithValidation.Models;

namespace PlayerInfoCrudWithValidation.Data
{
    public class PlayerDbContext : DbContext
    {
        public PlayerDbContext(DbContextOptions<PlayerDbContext> options) : base(options) { }
        public DbSet<Player> Players { get; set; }
    }
}
