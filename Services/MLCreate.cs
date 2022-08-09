using Microsoft.AspNetCore.Identity;
using MlVitrine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using MlVitrine.Models;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

namespace MlVitrine.Services
{
    public class MLCreate
    {
        private readonly MlVitrineContext _context;

        public MLCreate(MlVitrineContext context)
        {
            _context = context;
        }

        public async Task<string> CreateTestUser(int StatusCode, string CodMeli, string UserName)
        {

            var app_id = "7003305904370662";
            var app_secret = "63xkU4YX4AXte4ku8pAok6LT728j4Fbg";
            var redirect_url = "https://localhost:7276";
            HttpClient client = new HttpClient();
            var VerifySession = _context.MeliSession.Where(p => p.Username == UserName).OrderByDescending(p => p.ExpirationDate).FirstOrDefault();
            var m = new MeliSession
            {
                SessionId = StatusCode,
                Username = UserName,
                CreatedDate = DateTime.UtcNow

            };

            if (VerifySession == null)
            {
                if (CodMeli == null)
                {
                    _context.Add(m);
                    await _context.SaveChangesAsync();
                    return "https://auth.mercadolivre.com.br/authorization?response_type=code&client_id=7003305904370662&redirect_uri=https://localhost:7276&state=" + StatusCode;
                }
                else
                {
                    using (var requestMessage =
                        new HttpRequestMessage(HttpMethod.Post, "https://api.mercadolibre.com/oauth/token"))
                    {
                        requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        requestMessage.Content = new FormUrlEncodedContent(new[]
                        {
                                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                                new KeyValuePair<string, string>("client_id", app_id),
                                new KeyValuePair<string, string>("client_secret", app_secret),
                                new KeyValuePair<string, string>("code", VerifySession.CodeMeli),
                                new KeyValuePair<string, string>("redirect_uri", redirect_url),
                            });
                        var token_access = await client.SendAsync(requestMessage);
                        var InsertToken = _context.MeliSession.Where(p => p.SessionId == StatusCode).SingleOrDefault();
                        InsertToken.TokenMeli = token_access.ToString();
                        _context.Update(InsertToken);
                        await _context.SaveChangesAsync();
                        return redirect_url;
                    };

                }

            }else
            {
                if (VerifySession.ExpirationDate > DateTime.UtcNow)
                {

                    using (var requestMessage =
                        new HttpRequestMessage(HttpMethod.Post, "https://api.mercadolibre.com/oauth/token"))
                    {
                        requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        requestMessage.Content = new FormUrlEncodedContent(new[]
                        {
                                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                                new KeyValuePair<string, string>("client_id", app_id),
                                new KeyValuePair<string, string>("client_secret", app_secret),
                                new KeyValuePair<string, string>("code", VerifySession.CodeMeli),
                                new KeyValuePair<string, string>("redirect_uri", redirect_url),
                            });
                        var token_access = await client.SendAsync(requestMessage);
                        var InsertToken = _context.MeliSession.Where(p => p.SessionId == StatusCode).SingleOrDefault();
                        InsertToken.TokenMeli = token_access.ToString();
                        _context.Update(InsertToken);
                        await _context.SaveChangesAsync();
                        return redirect_url;
                    };

                }
                else
                {
                    if (CodMeli != null)
                    {
                        using (var requestMessage =
                        new HttpRequestMessage(HttpMethod.Post, "https://api.mercadolibre.com/oauth/token"))
                        {
                            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            requestMessage.Content = new FormUrlEncodedContent(new[]
                            {
                                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                                new KeyValuePair<string, string>("client_id", app_id),
                                new KeyValuePair<string, string>("client_secret", app_secret),
                                new KeyValuePair<string, string>("code", CodMeli),
                                new KeyValuePair<string, string>("redirect_uri", redirect_url),
                            });
                            var token_access = await client.SendAsync(requestMessage);
                            var InsertToken = _context.MeliSession.Where(p => p.SessionId == StatusCode).SingleOrDefault();
                            InsertToken.TokenMeli = token_access.ToString();
                            _context.Update(InsertToken);
                            await _context.SaveChangesAsync();
                            return redirect_url;
                        };

                    }
                    else {
                        _context.Add(m);
                        await _context.SaveChangesAsync();
                        return "https://auth.mercadolivre.com.br/authorization?response_type=code&client_id=7003305904370662&redirect_uri=https://localhost:7276&state=" + StatusCode;
                    }
                    
                }
            }
      
        }




    }
}