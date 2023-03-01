using System.Net;
using System.Collections.Generic;
using System.Linq;
using MoonlightShadow.Models;
using Microsoft.AspNetCore.Http;
using Extension.Web;
using MoonlightShadow.Services;
using Newtonsoft.Json;

namespace MoonlightShadow.Services
{
    public class SessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public T GetComplexData<T>(string key)
        {
            var data = _httpContextAccessor.HttpContext.Session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public void SetComplexData(string key, object value)
        {
            _httpContextAccessor.HttpContext.Session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public string GetString(string key)
        {
            return _httpContextAccessor.HttpContext.Session.GetString(key);
        }

        public void SetString(string key, string value)
        {
            _httpContextAccessor.HttpContext.Session.SetString(key, value);
        }

        public void Remove(string key)
        {
            _httpContextAccessor.HttpContext.Session.Remove(key);
        }
    }
}