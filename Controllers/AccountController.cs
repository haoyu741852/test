using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Carts.Models;

namespace Carts.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model, string returnUrl)
        {
            if (this.ModelState.IsValid)
            {
                using (Models.TESTEntities db = new Models.TESTEntities())
                {
                    var check = (from user in db.UserEFs where user.UserName == model.UserName select user).FirstOrDefault();

                    if (check != default(Models.UserEF))
                    {
                        ViewBag.ReturnUrl = returnUrl;
                        ViewBag.ResultMessage = String.Format("使用者暱稱[{0}]已存在，請使用別的暱稱!", model.UserName);
                        return View(model); 
                    }
                    else
                    {
                        var data = new UserEF
                        {
                            Email = model.Email,
                            PwHash = model.Pw,
                            UserName = model.UserName,
                            LoginDateTime = DateTime.Now,
                            //LogoutDateTime = DateTime.Now
                        };
                        db.UserEFs.Add(data);
                        db.SaveChanges();

                        //TempData["ResultMessage"] = String.Format("使用者[{0}]成功註冊", data.UserName);
                        Session["User"] = data.UserName;
                        return Redirect(returnUrl ?? "~/");
                    }
                }
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (this.ModelState.IsValid)
            {
                using (Models.TESTEntities db = new Models.TESTEntities())
                {
                    var check = (from user in db.UserEFs where user.UserName == model.UserName select user).FirstOrDefault();

                    if (check != default(Models.UserEF))
                    {
                        if (check.PwHash != model.Pw)
                        {
                            ViewBag.ReturnUrl = returnUrl;
                            ViewBag.ResultMessage = String.Format("密碼[{0}]錯誤!", model.Pw);
                            return View(model);
                        }
                        else
                        {
                            check.LoginDateTime = DateTime.Now;
                            db.SaveChanges();

                            //TempData["ResultMessage"] = String.Format("使用者[{0}]成功註冊", data.UserName);
                            Session["User"] = check.UserName;
                            return Redirect(returnUrl ?? "~/");
                        }
                    }
                    else
                    {
                        ViewBag.ResultMessage = String.Format("使用者暱稱[{0}]不存在，請先註冊!", model.UserName);
                        return View();
                    }
                }
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }
        }

        public ActionResult Manage(string returnUrl)
        {
            var username = Session["User"];
            if (username != null)
            {
                using (Models.TESTEntities db = new Models.TESTEntities())
                {
                    var check = (from user in db.UserEFs where user.UserName == username.ToString() select user).FirstOrDefault();

                    if (check != default(Models.UserEF))
                    {
                        ViewBag.ReturnUrl = returnUrl;
                        return View(check);
                    }
                    else
                    {
                        ViewBag.ReturnUrl = returnUrl;
                        ViewBag.ResultMessage = "使用者資料不存在，請先註冊";
                        return RedirectToAction("Register");
                    }
                }
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.ResultMessage = "使用者資料不存在，請先登入";
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult Manage(UserEF data, string returnUrl)
        {
            if (this.ModelState.IsValid)
            {
                var username = Session["User"];
                if (username != null)
                {
                    using (Models.TESTEntities db = new Models.TESTEntities())
                    {
                        var check = (from user in db.UserEFs where user.UserName == username.ToString() select user).FirstOrDefault();

                        if (check != default(Models.UserEF))
                        {
                            check.Email = data.Email;
                            check.PwHash = data.PwHash;
                            db.SaveChanges();

                            return Redirect(returnUrl ?? "~/");
                        }
                        else
                        {
                            ViewBag.ReturnUrl = returnUrl;
                            ViewBag.ResultMessage = "使用者資料不存在，請先註冊";
                            return RedirectToAction("Register");
                        }
                    }
                }
                else
                {
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.ResultMessage = "使用者資料不存在，請先登入";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult Logout(string returnUrl)
        {
            var username = Session["User"];
            if (username != null)
            {
                using (Models.TESTEntities db = new Models.TESTEntities())
                {
                    var check = (from user in db.UserEFs where user.UserName == username.ToString() select user).FirstOrDefault();

                    if (check != default(Models.UserEF))
                    {
                        check.LogoutDateTime = DateTime.Now;
                        db.SaveChanges();

                        Session.Clear();
                        return Redirect(returnUrl ?? "~/");
                    }
                    else
                    {
                        ViewBag.ReturnUrl = returnUrl;
                        ViewBag.ResultMessage = "使用者資料不存在，請先註冊";
                        return RedirectToAction("Register");
                    }
                }
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.ResultMessage = "使用者資料不存在，請先登入";
                return RedirectToAction("Login");
            }
        }
    }
}