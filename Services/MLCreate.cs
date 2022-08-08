using Microsoft.AspNetCore.Identity;
using MlVitrine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using MlVitrine.Models;
using System.Threading.Tasks;

namespace MlVitrine.Services
{
    public class MLCreate
    {

        public static async Task<bool> CreateTestUser(int StatusCode, string CodMeli, string UserName, MlVitrineContext _context)
        {

            var VerifySession = _context.MeliSession.Where(p => p.Username == UserName).Last();
            if (VerifySession != null) {
                if (VerifySession.ExpirationDate <= DateTime.UtcNow)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

    }
}