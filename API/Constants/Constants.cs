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
        public const string KibokoFixerFolder = "kibokopropertymanager/";
        public const string KibokoFixerBlogPostFolder = "kibokopropertymanager/blogpost/";
        public const string KibokoFixerProfileFolder = "kibokopropertymanager/profile/";
        public const string KibokoFixerIssueFolder = "kibokopropertymanager/issue/";
    }
}