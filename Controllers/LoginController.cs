using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using WebMVCApplication1.Models;

namespace WebMVCApplication1.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
        };


        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("check")]
        public async Task<IActionResult> Check(string email, string password)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("/users");

            string content = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(content))
            {
                List<User> users = JsonConvert.DeserializeObject<List<User>>(content);

                //return users.Where(x => x.email.Equals(email)).Any();

                User user = null;

                try
                {
                    user = users.Where(x => x.email.Equals(email)).FirstOrDefault();
                }
                catch (Exception)
                {
                    user = null;
                }

                if (user != null)
                {
                    HttpContext.Session.SetString("username", user.username);

                 
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Message = string.Format("아이디 또는 비밀번호를 다시 확인하세요. 등록되지 않은 아이디이거나 아이디 또는 비밀번호를 잘못 입력하셨습니다.");

                return View("Index");
            }

            ViewBag.Message = "시스템 오류 발생";

            return View("Index");
        }

        // POST: LoginController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
