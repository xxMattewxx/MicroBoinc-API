using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MicroBoincAPI.Models.Accounts;

namespace MicroBoincAPI.Utils
{
    public static class ControllerExtensions
    {
        public static AccountKey GetLoggedInKey(this ControllerBase controller)
        {
            return (AccountKey)controller.HttpContext.Items["LoggedInUser"];
        }
    }
}
