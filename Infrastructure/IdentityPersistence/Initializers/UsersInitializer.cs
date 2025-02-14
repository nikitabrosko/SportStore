﻿using System.Threading.Tasks;
using Domain.Entities;
using Domain.IdentityEntities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.IdentityPersistence.Initializers
{
    public static class UsersInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager)
        {
            if (await userManager.FindByNameAsync("admin") is null)
            {
                var entity = new User
                {
                    UserName = "admin", 
                    Email = "Nikita.brosko@mail.ru"
                };

                await userManager.CreateAsync(entity, "Admin123");

                await userManager.AddToRoleAsync(entity, "admin");
            }
            else if (!await userManager.IsInRoleAsync(
                await userManager.FindByNameAsync("admin"), "admin"))
            {
                await userManager.AddToRoleAsync(
                    await userManager.FindByNameAsync("admin"), "admin");
            }
        }
    }
}