using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MlVitrine.Services
{
    public class MLCreate
    {
       public static long ml_key = 7003305904370662;
       public static string ml_shared = "63xkU4YX4AXte4ku8pAok6LT728j4Fbg";

        static HttpClient client = new HttpClient();

        static async Task<Uri> CreateTestUser()
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer" + ml_shared);
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "https://api.mercadolibre.com/users/test_user", "site_id:" + ml_key);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
    } 
}