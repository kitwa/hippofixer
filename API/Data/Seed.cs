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
                new AppRole{Name = "PropertyManager"},
                new AppRole{Name = "Tenant"},
                new AppRole{Name = "Contractor"},
                new AppRole{Name = "Owner"},
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
                new IssueType{Id = 3, Name = "Pest Infestations"},
                new IssueType{Id = 4, Name = "Pest Control"},
                new IssueType{Id = 5, Name = "Painting Touch Ups"},
                new IssueType{Id = 6, Name = "Professional Cleaning"},
                new IssueType{Id = 7, Name = "Structural Repairs"},
                new IssueType{Id = 8, Name = "Other Repair"}

            };

            foreach (var issueType in issueTypes)
            {
                context.IssueTypes.Add(issueType);
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
