using Microsoft.AspNet.Identity;
using Octopus_project.Models;
using Octopus_project.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Octopus_project.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db;

        public HomeController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            //foreach (var user in db.Users)
            //    db.Users.Remove(user);
            //db.SaveChanges();

            //foreach (Photo photo in db.Photos)
            //{
            //    db.Photos.Remove(photo);
            //}
            //db.SaveChanges();
            
            return View(db.Users.ToList());
        }

        public ActionResult Photos()
        {
            return View(db.Photos.ToList());
        }

        [HttpGet]
        public ActionResult AddPhoto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPhoto(string publisherName, string publisherSurname, HttpPostedFileBase uploadImage)
        {
            if (uploadImage == null || publisherName.Trim() == "" || publisherSurname.Trim() == "")
            {
                ViewBag.Error = "Fill all fieds at the form";
                return View();
            }
            if (uploadImage.ContentType != "image/jpeg" && uploadImage.ContentType != "image/png")
            {
                ViewBag.Error = "The file must be .jpeg or .png";
                return View();
            }
            string fileName = uploadImage.FileName;

            string filePath = HttpContext.Server.MapPath(
                    "../" + ConfigurationManager.AppSettings["ImagesPath"] + fileName);

            //string filePath = ConfigurationManager.AppSettings["ImagesPath"] + fileName;

            SavingImage.SaveImage(uploadImage, filePath);

            db.Photos.Add(
                new Photo
                {
                    PublisherName = publisherName,
                    PublisherSurname = publisherSurname,
                    Path = fileName
                });
            db.SaveChanges();
            return RedirectToAction("Photos");
        }
    }
}