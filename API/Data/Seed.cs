using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, 
            RoleManager<AppRole> roleManager, DataContext context)
        {
            if (await userManager.Users.AnyAsync()) return;

            var roles = new List<AppRole>
            {
                new AppRole{Name = "SuperAdmin"},
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Client"},
                new AppRole{Name = "Contractor"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            var workOrderStatuses = new List<WorkOrderStatus>
            {
                new WorkOrderStatus { Id = 1, Identifier = "Pending", Description = "Work or request is pending approval or assignment" },
                new WorkOrderStatus { Id = 2, Identifier = "InProgress", Description = "Work or request is currently being handled" },
                new WorkOrderStatus { Id = 3, Identifier = "Completed", Description = "Work or request has been completed" },
                new WorkOrderStatus { Id = 4, Identifier = "Approved", Description = "Issue request has been approved" },
                new WorkOrderStatus { Id = 5, Identifier = "Rejected", Description = "Issue request has been rejected" }
            };

            foreach (var workOrderStatus in workOrderStatuses)
            {
                context.WorkOrderStatuses.Add(workOrderStatus);
            }

            var genders = new List<Gender>
            {                
                new Gender{Id = 1, Name = "Male"},
                new Gender{Id = 2, Name = "Female"}
            };

            foreach (var gender in genders)
            {
                context.Genders.Add(gender);
            }

            var issueTypes = new List<IssueType>
            {                
                new IssueType{Id = 1, Name = "Plumbing System Repairs"},                
                new IssueType{Id = 2, Name = "Electrical System Repairs"},
                new IssueType{Id = 3, Name = "Aircon Installations, Repairs"},
                new IssueType{Id = 4, Name = "Floor And Wall Tiling Service"},
                new IssueType{Id = 5, Name = "Pest Control"},
                new IssueType{Id = 6, Name = "Painting Touch Ups"},
                new IssueType{Id = 7, Name = "Professional Cleaning"},
                new IssueType{Id = 8, Name = "Structural Repairs"},
                new IssueType{Id = 9, Name = "Other Services or Repairs"}
            };

            foreach (var issueType in issueTypes)
            {
                context.IssueTypes.Add(issueType);
            }

            // var provinces = new List<Province>
            // {
            //     new Province{Id = 1, Name = "Western Cape"},
            //     new Province{Id = 2, Name = "Gauteng"},
            //     new Province{Id = 3, Name = "KwaZulu-Natal"},
            //     new Province{Id = 4, Name = "Limpopo"},
            //     new Province{Id = 5, Name = "Mpumalanga"},
            //     new Province{Id = 6, Name = "North West"},
            //     new Province{Id = 7, Name = "Free State"},
            //     new Province{Id = 8, Name = "Eastern Cape"},
            //     new Province{Id = 9, Name = "Northern Cape"},
            // };

            var cities = new List<City>
            {
                new City{Id = 1, Name = "Cape Town"},
                new City{Id = 2, Name = "Johannesburg"},
                new City{Id = 3, Name = "Durban"},
                new City{Id = 4, Name = "Polokwane"},
                new City{Id = 5, Name = "Mbombela"},
                new City{Id = 6, Name = "Kimberley"}
            };

            foreach (var city in cities)
            {
                context.Cities.Add(city);
            }
            
            await context.SaveChangesAsync();

            var admin = new AppUser
            {
                UserName = "admin",
                Email = "admin@kibokofixer.co.za",
                GenderId = 1,
            };

            var result = await userManager.CreateAsync(admin, "KibokoFixer1502@2024");

            if(!result.Succeeded) {
                Console.WriteLine(result.Errors);
            } 
            await userManager.AddToRoleAsync(admin, "Admin"); 

        }
    }
}
