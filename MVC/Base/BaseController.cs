using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MVC.Base
{
    public class BaseController<Entity, Key> : Controller
    {
        private readonly HttpClient httpClient;
        public BaseController()
        {
            URL url = new URL();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(url.GetDevelopment())
            };
        }

        public ViewResult Index() => View();

        public async Task<IActionResult> Get()
        {
            using var response = await httpClient.GetAsync(typeof(Entity).Name);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var entity = JsonConvert.DeserializeObject<ResponseVM<Entity>>(apiResponse);
            return new JsonResult(entity);
        }



        public ViewResult GetById() => View();

        public async Task<IActionResult> GetById(Key key)
        {
            using (var httpClient = new HttpClient())
            {
                using var response = await httpClient.GetAsync(typeof(Entity).Name + "/" + key);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var dataList = JsonConvert.DeserializeObject<ResponseVM<Entity>>(apiResponse);
                    var entity = dataList.Result.SingleOrDefault();
                    return View(entity);
                }
                else
                {
                    ViewBag.StatusCode = response.StatusCode;
                    return View();
                }
            }

        }


        [HttpPost]
        public async Task<IActionResult> Post(Entity entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(typeof(Entity).Name, content);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseVM<Entity>>(apiResponse);
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Key key)
        {
            using var response = await httpClient.DeleteAsync(typeof(Entity).Name + '/' + key);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseVM<Entity>>(apiResponse);
          
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Put(Entity entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(typeof(Entity).Name, content);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseVM<Entity>>(apiResponse);
            return new JsonResult(result);
        }
    }
}