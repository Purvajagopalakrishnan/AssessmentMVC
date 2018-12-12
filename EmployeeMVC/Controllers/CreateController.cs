using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EmployeeMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMVC.Controllers
{
    public class CreateController : Controller
    {
        public IActionResult Index()
        {
            var emps = new List<Employees>();
            using (var client = new HttpClient())
            {
                var uri = new Uri("https://localhost:44354/api");
                var responseTask = client.GetAsync(uri);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Employees>>();


                    emps = readTask.Result;

                }


            }
            return View(emps);


           
        }
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Employees employ)
        {
            using (var client = new HttpClient())
            {
               client.BaseAddress= new Uri("http://localhost:44354/api/UpdateDetails");

                //HTTP POST
                // var responseTask = client.GetAsync(uri);
                var postTask = client.PostAsJsonAsync<Employees>("employee", employ);
                    //<Employees>();
                   
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(employ);

        }

    }
}