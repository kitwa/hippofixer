using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Constants
{
    public class MailClientConfigurations
    {
        public const string Server = "mail.kibokohouse.com";
        public const int Port = 26;
        public const string SenderEmail = "sysinfo@kibokohouse.com";
        public const string Password = "k&A?zcmFu)%E";
    }

    public class Cloudinary
    {
        public const string KibokoPropertyManagerFolder = "kibokopropertymanager/";
        public const string KibokoPropertyManagerBlogPostFolder = "kibokopropertymanager/blogpost/";
        public const string KibokoPropertyManagerProfileFolder = "kibokopropertymanager/profile/";
        public const string KibokoPropertyManagerWorkOrderFolder = "kibokopropertymanager/workorder/";
    }
}