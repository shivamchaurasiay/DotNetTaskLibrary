using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetsTasks.Core
{
    public class EmailKeys
    {

        private static IConfigurationSection _configuration;
        public static void Configure(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public static string SmtpServer => _configuration["SmtpServer"];

        public static string SMTPUserName => _configuration["SMTPUserName"];

        public static string SMTPUserPassword => _configuration["SMTPUserPassword"];

        public static string SmtpPort => _configuration["SmtpPort"];
        public static string AdminEmail => _configuration["AdminEmail"];
        public static string Domain => _configuration["Domain"];
        public static string MailBCC => _configuration["MailBCC"];

        public static string MailFromName => _configuration["MailFromName"];
        public static string DefaultFromEmail => _configuration["DefaultFromEmail"];
        public static string DefaultFromName => _configuration["DefaultFromName"];
    }
}
