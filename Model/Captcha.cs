using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SITConnect.Model
{
    public class Captcha
    {
    private readonly HttpClient captchaClient;

    public Captcha(HttpClient captchaClient)
    {
        this.captchaClient = captchaClient;
    }

    public async Task<bool> IsValid(string captcha)
    {
        try
        {
            var postTask = await captchaClient
                .PostAsync($"?secret=6LeS91ceAAAAAHl_-MAhctJNSmqJZOxYcGSyKyOJ&response={captcha}", new StringContent(""));
            var result = await postTask.Content.ReadAsStringAsync();
            var resultObject = JObject.Parse(result);
            dynamic success = resultObject["success"];
            return (bool)success;
        }
        catch (Exception)
        {
            return false;
        }
    }
    }
}
