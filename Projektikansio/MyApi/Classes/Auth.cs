using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApi.Controllers;
using MyApi.Data;

namespace SharedLib
{
    public class Auth
    {
        //public static matkaajaDTO CurrentUser { get; set; }
        //private readonly MydbContext _context;
        //public static async Task<matkaajaDTO> Test(string? email, string? pass)
        //{

        //    using (var _context = new MydbContext())
        //    {
        //        var response = await _context.Matkaajas.Where(p => p.Email.Equals(email) && p.Password.Equals(pass)).FirstOrDefaultAsync();

        //        if (response != null)
        //        {
        //            CurrentUser = response.toMatkaajaDTO();
        //            return CurrentUser;
        //        }
        //        return null;

        //    }

        //}
    }
}
