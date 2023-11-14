using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AddressBook.Models
{
    public class Account
    {
        private string Admin_account { get; set; } = "AddressBook";

        private string Admin_password { get; set; } = "AddressBookroot";

        public bool CheckAccount(string account, string password)
        {
            if (Admin_account.Equals(account) && Admin_password.Equals(password))
                return true;
            else
                return false;
        }
    }
}