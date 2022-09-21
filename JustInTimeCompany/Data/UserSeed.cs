using JustInTimeCompany.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JustInTimeCompany.Data
{
    public class UserSeed
    {
        private static readonly Guid AdminId = new("319a3971-483c-4ed9-a0cc-2bec19a07b06");
        private static readonly Guid Pilot1Id = new("821f98b0-ff1e-458a-b15b-a9ea85750e45");
        private static readonly Guid Pilot2Id = new("3ac33a35-d3d6-4a04-a79b-aded19a205db");
        private static readonly Guid Pilot3Id = new("efb6c53f-48d0-4981-a3c7-ef714fa2b508");
        private static readonly Guid User1Id = new("bb76dfd3-10d3-4224-b4b3-712aafa7ccaa");
        private static readonly Guid User2Id = new("51d08f0c-0c50-4e47-8e8b-55f7125019bc");
        private static readonly Guid User3Id = new("4b40d3d6-bd20-43f2-a5b8-082009c13682");

        private static readonly Guid AdminRoleId = new("4e50f06a-e14c-4657-ae21-5ba760cd3a51");
        private static readonly Guid PilotRoleId = new("4635ec8f-2c34-444e-9bb8-fb646e7bd1da");
        private static readonly Guid UserRoleId = new("570245bc-2723-4392-9213-68202d71dad4");

        public static void SeedRoles(ModelBuilder modelBuilder)
        {
            var adminRole = new
            {
                Id = AdminRoleId.ToString(),
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            };

            var pilotRole = new
            {
                Id = PilotRoleId.ToString(),
                Name = "Pilot",
                NormalizedName = "PILOT"
            };

            var userRole = new
            {
                Id = UserRoleId.ToString(),
                Name = "User",
                NormalizedName = "USER"
            };
            modelBuilder.Entity<IdentityRole>().HasData(adminRole, pilotRole, userRole);
        }

        public static void SeedUsers(ModelBuilder builder)
        {
            var admin = new ApplicationUser()
            {
                Id = AdminId.ToString(),
                FirstName = "Mo",
                LastName = "Ney",
                UserName = "money",
                Email = "M.Ney@jitc.com",
                Gender = Gender.Female,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                TwoFactorEnabled = false,
                NormalizedUserName = "MONEY"
            };
            var pilot1 = new ApplicationUser()
            {
                Id = Pilot1Id.ToString(),
                FirstName = "Danièle",
                LastName = "Balav",
                UserName = "danbal",
                Email = "D.Balav@jitc.com",
                Gender = Gender.Female,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                TwoFactorEnabled = false,
                NormalizedUserName = "DANBAL"
            };
            var pilot2 = new ApplicationUser()
            {
                Id = Pilot2Id.ToString(),
                FirstName = "Thierry",
                LastName = "Sabine",
                UserName = "thisab",
                Email = "T.Sabine@jitc.com",
                Gender = Gender.Male,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                TwoFactorEnabled = false,
                NormalizedUserName = "THISAB"
            };
            var pilot3 = new ApplicationUser()
            {
                Id = Pilot3Id.ToString(),
                FirstName = "Eli",
                LastName = "Coptère",
                UserName = "elicop",
                Email = "E.Coptère@jitc.com",
                Gender = Gender.Male,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                TwoFactorEnabled = false,
                NormalizedUserName = "ELICOP"
            };
            var user1 = new ApplicationUser()
            {
                Id = User1Id.ToString(),
                FirstName = "Maxime",
                LastName = "Gaber",
                UserName = "maxgab",
                Email = "m.gaber@jitc.com",
                Gender = Gender.Dragon,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                TwoFactorEnabled = false,
                NormalizedUserName = "MAXGAB"
            };
            var user2 = new ApplicationUser()
            {
                Id = User2Id.ToString(),
                FirstName = "Florence",
                LastName = "Gaber",
                UserName = "flogab",
                Email = "f.gaber@jitc.com",
                Gender = Gender.Female,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                TwoFactorEnabled = false,
                NormalizedUserName = "FLOGAB"

            };
            var user3 = new ApplicationUser()
            {
                Id = User3Id.ToString(),
                FirstName = "Christian",
                LastName = "Gaber",
                UserName = "chrgab",
                Email = "c.gaber@jitc.com",
                Gender = Gender.Male,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                TwoFactorEnabled = false,
                NormalizedUserName = "CHRGAB"
            };
            admin.PasswordHash = PassGenerate(admin);
            pilot1.PasswordHash = PassGenerate(pilot1);
            pilot2.PasswordHash = PassGenerate(pilot2);
            pilot3.PasswordHash = PassGenerate(pilot3);
            user1.PasswordHash = PassGenerate(user1);
            user2.PasswordHash = PassGenerate(user2);
            user3.PasswordHash = PassGenerate(user3);
            builder.Entity<ApplicationUser>().HasData(admin, pilot1, pilot2, pilot3, user1, user2, user3);
        }

        public static string PassGenerate(ApplicationUser user)
        {
            PasswordHasher<ApplicationUser> passHash = new PasswordHasher<ApplicationUser>();
            return passHash.HashPassword(user, "Test1234/");
        }

        public static void SeedUserRoles(ModelBuilder builder)
        {
            var admin = new { RoleId = AdminRoleId.ToString(), UserId = AdminId.ToString() };
            var p1 = new { RoleId = PilotRoleId.ToString(), UserId = Pilot1Id.ToString() };
            var p2 = new { RoleId = PilotRoleId.ToString(), UserId = Pilot2Id.ToString() };
            var p3 = new { RoleId = PilotRoleId.ToString(), UserId = Pilot3Id.ToString() };
            var u1 = new { RoleId = UserRoleId.ToString(), UserId = User1Id.ToString() };
            var u2 = new { RoleId = UserRoleId.ToString(), UserId = User2Id.ToString() };
            var u3 = new { RoleId = UserRoleId.ToString(), UserId = User3Id.ToString() };

            builder.Entity<IdentityUserRole<string>>()
                .HasData(admin, p1, p2, p3, u1, u2, u3);
        }


    }
}



/*
 public static void SeedRoles(RoleManager<IdentityRole> roleManager)
   {
   if (!roleManager.RoleExistsAsync("Administrator").Result)
   {
   IdentityResult role = roleManager.CreateAsync(new IdentityRole("Administrator")).Result;
   }
   if (!roleManager.RoleExistsAsync("Pilot").Result)
   {
   IdentityResult role = roleManager.CreateAsync(new IdentityRole("Pilot")).Result;
   }
   if (!roleManager.RoleExistsAsync("User").Result)
   {
   IdentityResult role = roleManager.CreateAsync(new IdentityRole("User")).Result;
   }
   }
   
   
   
   public static void SeedUsers(UserManager<ApplicationUser> userManager)
   {
   
   ApplicationUser user = userManager.FindByNameAsync("neymo").Result;
   if (user == null)
   {
   var admin = new ApplicationUser()
   {
   Id = AdminId.ToString(),
   FirstName = "Mo",
   LastName = "Ney",
   UserName = "money",
   Email = "M.Ney@jitc.com",
   Gender = Gender.Female,
   };
   var pilot1 = new ApplicationUser()
   {
   Id = Pilot1Id.ToString(),
   FirstName = "Danièle",
   LastName = "Balav",
   UserName = "danbal",
   Email = "D.Balav@jitc.com",
   Gender = Gender.Female,
   };
   var pilot2 = new ApplicationUser()
   {
   Id = Pilot2Id.ToString(),
   FirstName = "Thierry",
   LastName = "Sabine",
   UserName = "thisab",
   Email = "T.Sabine@jitc.com",
   Gender = Gender.Male,
   };
   var pilot3 = new ApplicationUser()
   {
   Id = Pilot3Id.ToString(),
   FirstName = "Eli",
   LastName = "Coptère",
   UserName = "elicop",
   Email = "E.Coptère@jitc.com",
   Gender = Gender.Male,
   };
   var user1 = new ApplicationUser()
   {
   Id = User1Id.ToString(),
   FirstName = "Maxime",
   LastName = "Gaber",
   UserName = "maxgab",
   Email = "m.gaber@jitc.com",
   Gender = Gender.Dragon,
   };
   var user2 = new ApplicationUser()
   {
   Id = User2Id.ToString(),
   FirstName = "Florence",
   LastName = "Gaber",
   UserName = "flogab",
   Email = "f.gaber@jitc.com",
   Gender = Gender.Female,
   };
   var user3 = new ApplicationUser()
   {
   Id = User3Id.ToString(),
   FirstName = "Christian",
   LastName = "Gaber",
   UserName = "chrgab",
   Email = "c.gaber@jitc.com",
   Gender = Gender.Male,
   };
   
   if (userManager.FindByNameAsync(admin.UserName).Result == null)
   {
   var seedPassword = "Test1234/";
   IdentityResult result = userManager.CreateAsync(admin, seedPassword).Result;
   if (result.Succeeded)
   {
   userManager.AddToRoleAsync(admin, "Administrator").Wait();
   }
   
   result = userManager.CreateAsync(pilot1, seedPassword).Result;
   if (result.Succeeded)
   {
   userManager.AddToRoleAsync(pilot1, "Pilot").Wait();
   }
   result = userManager.CreateAsync(pilot2, seedPassword).Result;
   if (result.Succeeded)
   {
   userManager.AddToRoleAsync(pilot2, "Pilot").Wait();
   }
   result = userManager.CreateAsync(pilot3, seedPassword).Result;
   if (result.Succeeded)
   {
   userManager.AddToRoleAsync(pilot3, "Pilot").Wait();
   }
   result = userManager.CreateAsync(user1, seedPassword).Result;
   if (result.Succeeded)
   {
   userManager.AddToRoleAsync(user1, "User").Wait();
   }
   result = userManager.CreateAsync(user2, seedPassword).Result;
   if (result.Succeeded)
   {
   userManager.AddToRoleAsync(user2, "User").Wait();
   }
   result = userManager.CreateAsync(user3, seedPassword).Result;
   if (result.Succeeded)
   {
   userManager.AddToRoleAsync(user3, "User").Wait();
   }
   }
   }
   }
 */
