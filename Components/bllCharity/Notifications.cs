using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bllCharity
{
    public class Notifications : ErrorManager
    {
        public enum NotificationType
        {
            NewUser = 1,
            ResetPassword = 2
        }

        private static void Insert(NotificationType type, CharityUser user)
        {
            DatabaseParameters parameters = new DatabaseParameters();
            parameters.Add("@userId", user.Id);
            parameters.Add("@notificationType", (int)type);
            Database.Instance.NonQuery("sp_Notification_Insert", parameters);
        }

        private static void EmailPassword(CharityUser user)
        {
            string emailBody = "Your password is " + user.Password;
            Utility.Email.Send(user.Email, user.DisplayName, "Your Password", emailBody);
        }

        public static void NewUser(CharityUser user)
        {
            EmailPassword(user);
            Insert(NotificationType.NewUser, user);            
        }

        public static void ResetPassword(CharityUser user)
        {
            EmailPassword(user);
            Insert(NotificationType.ResetPassword, user);            
        }
    }
}
