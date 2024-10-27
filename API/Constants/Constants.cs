using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Constants
{
    public class MailClientConfigurations
    {
        public const string Server = "mail.hippofixer.co.za";
        public const int Port = 26;
        public const string SenderEmail = "sysinfo@hippofixer.co.za";
        public const string Password = "k&A?zcmFu)%E";
    }

    public class Cloudinary
    {
        public const string KibokoFixerFolder = "kibokopropertymanager/";
        public const string KibokoFixerBlogPostFolder = "kibokopropertymanager/blogpost/";
        public const string KibokoFixerProfileFolder = "kibokopropertymanager/profile/";
        public const string KibokoFixerIssueFolder = "kibokopropertymanager/issue/";
    }

    public class Status
    {
        public const int Pending = 1;
        public const int InProgress = 2;
        public const int Completed = 3;
        public const int Approved = 4;
        public const int Rejected = 5;
    }
}