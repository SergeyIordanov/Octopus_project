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
            ViewBag.Error = "";
        }

        public ActionResult Index()
        {
            ViewBag.Likes = "";
            if (Session["Likes"] != null)
                ViewBag.Likes = Session["Likes"];
            return View(db.Photos.ToList());
        }

        [HttpPost]
        public ActionResult Index(string publisherName, string publisherSurname, HttpPostedFileBase file)
        {
            if (file == null || publisherName.Trim() == "" || publisherSurname.Trim() == "")
            {
                ViewBag.Error = "Fill all fieds at the form";
                return View();
            }
            if (file.ContentType != "image/jpeg" && file.ContentType != "image/png")
            {
                ViewBag.Error = "The file must be .jpeg or .png";
                return View();
            }
            string fileName = file.FileName;

            string filePath = HttpContext.Server.MapPath(
                    "../" + ConfigurationManager.AppSettings["ImagesPath"] + fileName);

            SavingImage.SaveImage(file, filePath);

            db.Photos.Add(
                new Photo
                {
                    PublisherName = publisherName,
                    PublisherSurname = publisherSurname,
                    Path = fileName
                });
            db.SaveChanges();
            return RedirectToAction("Index");
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

        public ActionResult Like(int? photoId)
        {
            if (photoId != null)
            {
                if (Session["Likes"] != null)
                    if (SessionValuesParser.FindIdFromLikesString(Session["Likes"].ToString(), (int)photoId))
                    {
                        return RedirectToAction("Index");
                    }
                foreach (Photo photo in db.Photos)
                    if (photoId == photo.PhotoId)
                    {
                        if (Session["Likes"] != null)
                        {
                            Session["Likes"] = Session["Likes"].ToString() + "|" + photoId;
                        }
                        else
                            Session["Likes"] = photoId.ToString();
                        photo.Likes++;
                    }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult ClearDB()
        {
            foreach (Photo photo in db.Photos)
                db.Photos.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
}