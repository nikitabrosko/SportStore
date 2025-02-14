﻿using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.IdentityPersistence
{
    public class ApplicationIdentityDbContext : IdentityDbContext<User>, IApplicationIdentityDbContext
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
            : base(options)
        { 
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<ShoppingCart>().Property(s => s.Id).ValueGeneratedNever();
            builder.Entity<Customer>().Property(c => c.Id).ValueGeneratedNever();

            base.OnModelCreating(builder);
        }
    }
}