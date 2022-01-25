using System.Text;
using System.Security.Cryptography;
using MicroBoincAPI.Models.Permissions;
using MicroBoincAPI.Models.Accounts;
using System;
using MicroBoincAPI.Models.Groups;

namespace MicroBoincAPI.Utils
{
    public class Common
    {
        public static readonly RNGCryptoServiceProvider _rng = new();
        public static string GenerateToken(int size)
        {
            var buffer = new byte[size];
            _rng.GetNonZeroBytes(buffer);

            var sb = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                sb.Append(buffer[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static bool IsAuthorized(AccountKey key, Group group, GroupPermission perm, GroupPermissionEnum requested)
        {
            if (key.IsRoot || group.OwnedBy == key.Account)
                return true;

            return perm != null && perm.Permissions.HasFlag(requested);
        }
    }
}
