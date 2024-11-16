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
                new IssueType{Id = 9, Name = "Issue with Car"},
                new IssueType{Id = 10, Name = "Other Services or Repairs"}
            };

            foreach (var issueType in issueTypes)
            {
                context.IssueTypes.Add(issueType);
            }

            var cities = new List<City>
            {
                // Western Cape
                new City{Id = 1, Name = "Bellville, Western Cape"},
                new City{Id = 2, Name = "Cape Town, Western Cape"},
                new City{Id = 3, Name = "Constantia, Western Cape"},
                new City{Id = 4, Name = "George, Western Cape"},
                new City{Id = 5, Name = "Hopefield, Western Cape"},
                new City{Id = 6, Name = "Oudtshoorn, Western Cape"},
                new City{Id = 7, Name = "Paarl, Western Cape"},
                new City{Id = 8, Name = "Simon’s Town, Western Cape"},
                new City{Id = 9, Name = "Stellenbosch, Western Cape"},
                new City{Id = 10, Name = "Swellendam, Western Cape"},
                new City{Id = 11, Name = "Worcester, Western Cape"},

                // Gauteng
                new City{Id = 12, Name = "Benoni, Gauteng"},
                new City{Id = 13, Name = "Boksburg, Gauteng"},
                new City{Id = 14, Name = "Brakpan, Gauteng"},
                new City{Id = 15, Name = "Carletonville, Gauteng"},
                new City{Id = 16, Name = "Germiston, Gauteng"},
                new City{Id = 17, Name = "Johannesburg, Gauteng"},
                new City{Id = 18, Name = "Krugersdorp, Gauteng"},
                new City{Id = 19, Name = "Pretoria, Gauteng"},
                new City{Id = 20, Name = "Randburg, Gauteng"},
                new City{Id = 21, Name = "Randfontein, Gauteng"},
                new City{Id = 22, Name = "Roodepoort, Gauteng"},
                new City{Id = 23, Name = "Soweto, Gauteng"},
                new City{Id = 24, Name = "Springs, Gauteng"},
                new City{Id = 25, Name = "Vanderbijlpark, Gauteng"},
                new City{Id = 26, Name = "Vereeniging, Gauteng"},

                // KwaZulu-Natal
                new City{Id = 27, Name = "Durban, KwaZulu-Natal"},
                new City{Id = 28, Name = "Empangeni, KwaZulu-Natal"},
                new City{Id = 29, Name = "Newcastle, KwaZulu-Natal"},
                new City{Id = 30, Name = "Pietermaritzburg, KwaZulu-Natal"},
                new City{Id = 31, Name = "Pinetown, KwaZulu-Natal"},
                new City{Id = 32, Name = "Ulundi, KwaZulu-Natal"},
                new City{Id = 33, Name = "Umlazi, KwaZulu-Natal"},
                new City{Id = 34, Name = "uMnambithi, KwaZulu-Natal"},

                // Eastern Cape
                new City{Id = 35, Name = "Butterworth, Eastern Cape"},
                new City{Id = 36, Name = "Dikeni, Eastern Cape"},
                new City{Id = 37, Name = "East London, Eastern Cape"},
                new City{Id = 38, Name = "Gqeberha, Eastern Cape"},
                new City{Id = 39, Name = "Graaff-Reinet, Eastern Cape"},
                new City{Id = 40, Name = "Kariega, Eastern Cape"},
                new City{Id = 41, Name = "Komani, Eastern Cape"},
                new City{Id = 42, Name = "Makhanda, Eastern Cape"},
                new City{Id = 43, Name = "Mthatha, Eastern Cape"},
                new City{Id = 44, Name = "Qonce, Eastern Cape"},
                new City{Id = 45, Name = "Zwelitsha, Eastern Cape"},

                // Free State
                new City{Id = 46, Name = "Bethlehem, Free State"},
                new City{Id = 47, Name = "Bloemfontein, Free State"},
                new City{Id = 48, Name = "Jagersfontein, Free State"},
                new City{Id = 49, Name = "Kroonstad, Free State"},
                new City{Id = 50, Name = "Odendaalsrus, Free State"},
                new City{Id = 51, Name = "Parys, Free State"},
                new City{Id = 52, Name = "Phuthaditjhaba, Free State"},
                new City{Id = 53, Name = "Sasolburg, Free State"},
                new City{Id = 54, Name = "Virginia, Free State"},
                new City{Id = 55, Name = "Welkom, Free State"},

                // Limpopo
                new City{Id = 56, Name = "Giyani, Limpopo"},
                new City{Id = 57, Name = "Lebowakgomo, Limpopo"},
                new City{Id = 58, Name = "Musina, Limpopo"},
                new City{Id = 59, Name = "Phalaborwa, Limpopo"},
                new City{Id = 60, Name = "Polokwane, Limpopo"},
                new City{Id = 61, Name = "Seshego, Limpopo"},
                new City{Id = 62, Name = "Sibasa, Limpopo"},
                new City{Id = 63, Name = "Thabazimbi, Limpopo"},

                // Mpumalanga
                new City{Id = 64, Name = "Emalahleni, Mpumalanga"},
                new City{Id = 65, Name = "Mbombela, Mpumalanga"},
                new City{Id = 66, Name = "Secunda, Mpumalanga"},

                // North West
                new City{Id = 67, Name = "Klerksdorp, North West"},
                new City{Id = 68, Name = "Mahikeng, North West"},
                new City{Id = 69, Name = "Mmabatho, North West"},
                new City{Id = 70, Name = "Potchefstroom, North West"},
                new City{Id = 71, Name = "Rustenburg, North West"},

                // Northern Cape
                new City{Id = 72, Name = "Kimberley, Northern Cape"},
                new City{Id = 73, Name = "Kuruman, Northern Cape"},
                new City{Id = 74, Name = "Port Nolloth, Northern Cape"}
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

            if (!result.Succeeded)
            {
                Console.WriteLine(result.Errors);
            }
            await userManager.AddToRoleAsync(admin, "Admin");

        }
    }
}
