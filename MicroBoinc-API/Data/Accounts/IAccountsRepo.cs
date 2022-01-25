using MicroBoincAPI.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Data.Accounts
{
    public interface IAccountsRepo
    {
        //Accounts
        public void CreateAccount(Account account);
        public bool IsDiscordIDLinked(long id);

        //Account keys
        public void CreateAccountKey(AccountKey key);
        public AccountKey GetKey(string code, bool noTracking = false);

        public bool SaveChanges();
    }
}
