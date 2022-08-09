using Microsoft.AspNetCore.Identity;
using MlVitrine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using MlVitrine.Models;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MlVitrine.Services
{
    public class MLCreate
    {
        private readonly MlVitrineContext _context;

        
        HttpClient client = new HttpClient();

        public MLCreate(MlVitrineContext context)
        {
            _context = context;
        }

        public bool UserHaveTokenMeli(string UserName)
        {
            var SearchToken = _context.MeliSession.Where(p => p.Username == UserName).OrderBy(p => p.CreatedDate).LastOrDefault();
            if (SearchToken.TokenMeli == null)
            {
                return false;
            }else
            {
                if (SearchToken.ExpirationDate < DateTime.UtcNow)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }
        public bool UserHaveCodeMeli(string UserName)
        {
            var SearchCode = _context.MeliSession.Where(p => p.Username == UserName).OrderBy(p => p.CreatedDate).LastOrDefault();
            if (SearchCode == null) { return false; }
            else { return true; }
        }

        public string GetUserCode(string UserName)
        {
            var SearchCode = _context.MeliSession.Where(p => p.Username == UserName).OrderBy(p => p.CreatedDate).LastOrDefault();
            if (SearchCode != null) 
            {
                if (SearchCode.CodeMeli != null) { return SearchCode.CodeMeli; }
                else { return "Usuario nao tem codigo valido"; }
            }
            return "Usuario nao tem codigo valido";

        }
        public long GetCodeStatus(string Code)
        {
            var SearchCode = _context.MeliSession.Where(p => p.CodeMeli == Code).OrderBy(p => p.CreatedDate).LastOrDefault();
            if (SearchCode != null)
            {
                if (SearchCode.CodeMeli != null) { return SearchCode.SessionId; }
                else { return 0; }
            }
            return 0;

        }
        public string GetUserToken(string UserName)
        {
            var SearchCode = _context.MeliSession.Where(p => p.Username == UserName).OrderBy(p => p.CreatedDate).LastOrDefault();
            if (SearchCode != null)
            {
                if (SearchCode.TokenMeli != null) { return SearchCode.TokenMeli; }
                else { return "Usuario nao tem Token valido"; }
            }
            return "Usuario nao tem Token valido";
        }
        public async Task<string> SetUserFirstRequest(int StatusCode, string UserName)
        {
            var app_id = "7003305904370662";
            var app_secret = "63xkU4YX4AXte4ku8pAok6LT728j4Fbg";
            var redirect_url = "https://localhost:7276";
            var InsertMeliSession = new MeliSession
            {
                SessionId = StatusCode,
                Username = UserName,
                CreatedDate = DateTime.UtcNow,
            };
            _context.Add(InsertMeliSession);
            await _context.SaveChangesAsync();
            return $"https://auth.mercadolivre.com.br/authorization?response_type=code&client_id={app_id}&redirect_uri={redirect_url}&state={StatusCode}";
        }

        public async Task<string> SetUserCode(string UserName, string Code, long Session)
        {
            var app_id = "7003305904370662";
            var app_secret = "63xkU4YX4AXte4ku8pAok6LT728j4Fbg";
            var redirect_url = "https://localhost:7276";

            var SearchCode = _context.MeliSession.Where(p => p.Username == UserName).Where(p => p.SessionId == Session).OrderBy(p => p.CreatedDate).LastOrDefault();
            SearchCode.CodeMeli = Code;
            _context.Update(SearchCode);
            await _context.SaveChangesAsync();

            using (var requestMessage =
                        new HttpRequestMessage(HttpMethod.Post, "https://api.mercadolibre.com/oauth/token"))
            {
                requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                requestMessage.Content = new FormUrlEncodedContent(new[]
                {
                                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                                new KeyValuePair<string, string>("client_id", app_id),
                                new KeyValuePair<string, string>("client_secret", app_secret),
                                new KeyValuePair<string, string>("code", Code),
                                new KeyValuePair<string, string>("redirect_uri", redirect_url),
                            });
                var token_access = await client.SendAsync(requestMessage);
                var token = await token_access.Content.ReadAsStringAsync();
                dynamic token_json =  JObject.Parse(token);
                SearchCode.TokenMeli = token_json.access_token;
                SearchCode.ExpirationDate = DateTime.UtcNow.AddHours(6);
                _context.Update(SearchCode);
                await _context.SaveChangesAsync();
                return redirect_url;

            };
        }
    }
}