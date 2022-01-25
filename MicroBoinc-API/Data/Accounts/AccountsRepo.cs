using MicroBoincAPI.Models.Accounts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Data.Accounts
{
    public class AccountsRepo : IAccountsRepo
    {
        private readonly AppDbContext _context;

        public AccountsRepo(AppDbContext context)
        {
            _context = context;
        }

        //Accounts
        public void CreateAccount(Account account)
        {
            _context.Accounts.Add(account);
        }

        public bool IsDiscordIDLinked(long id)
        {
            return _context.Accounts.Any(x => x.DiscordID == id);
        }

        //Account keys
        public void CreateAccountKey(AccountKey key)
        {
            _context.AccountsKeys.Add(key);
        }

        public AccountKey GetKey(string code, bool noTracking = false)
        {
            var query = _context.AccountsKeys.AsQueryable();
            if (noTracking)
                query = query.AsNoTracking();

            return query
                .Include(x => x.Account)
                .FirstOrDefault(x => x.Identifier == code);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
