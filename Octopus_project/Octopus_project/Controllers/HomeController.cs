﻿using Microsoft.AspNet.Identity;
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
            //var userId = User.Identity.GetUserId();
            db = new ApplicationDbContext();
            ViewBag.Error = "";
        }

        public ActionResult Index()
        {
            ViewBag.Likes = db.Likes.ToList();
            //if (Session["Likes"] != null)
            //ViewBag.Likes = Session["Likes"];
            List<Photo> model = db.Photos.ToList();
            model.Reverse();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string publisherName, string publisherSurname, HttpPostedFileBase file)
        {
            if (file == null)
            {
                return RedirectToAction("Index");
            }
            if (file.ContentType != "image/jpeg" && file.ContentType != "image/png")
            {
                return RedirectToAction("Index");
            }
            string fileName = file.FileName;

            string filePath = HttpContext.Server.MapPath(
                    ConfigurationManager.AppSettings["ImagesPath"] + fileName);

            SavingImage.SaveImage(file, filePath);

            if (publisherName == null || publisherSurname == null)
            {
                string userId = User.Identity.GetUserId();
                foreach(var user in db.Users)
                {
                    if(user.Id.Equals(userId))
                    {
                        publisherName = user.Name;
                        publisherSurname = user.Surname;
                        break;
                    }
                }
            }
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

        //public ActionResult Like(int? photoId)
        //{
        //    if (photoId != null)
        //    {
        //        if (Session["Likes"] != null)
        //            if (SessionValuesParser.FindIdFromLikesString(Session["Likes"].ToString(), (int)photoId))
        //            {
        //                return RedirectToAction("Index");
        //            }
        //        foreach (Photo photo in db.Photos)
        //            if (photoId == photo.PhotoId)
        //            {
        //                if (Session["Likes"] != null)
        //                {
        //                    Session["Likes"] = Session["Likes"].ToString() + "|" + photoId;
        //                }
        //                else
        //                    Session["Likes"] = photoId.ToString();
        //                photo.Likes++;
        //            }
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Index");
        //}

        public ActionResult Like(int? photoId)
        {
            if (photoId != null && User.Identity.GetUserId() != null)
            {
                Photo currentPhoto = null;
                foreach (Photo photo in db.Photos)
                {
                    if (photo.PhotoId == photoId)
                        currentPhoto = photo;
                }
                if (currentPhoto != null)
                {
                    bool isliked = false;
                    foreach (Like like in db.Likes)
                    {
                        if (like.PhotoId == currentPhoto.PhotoId && like.UserId.Equals(User.Identity.GetUserId()))
                        {
                            currentPhoto.Likes--;
                            db.Likes.Remove(like);
                            isliked = true;
                        }
                    }
                    if (!isliked)
                    {
                        currentPhoto.Likes++;
                        db.Likes.Add(new Like() { PhotoId = currentPhoto.PhotoId, UserId = User.Identity.GetUserId() });
                    }
                }
            }
            db.SaveChanges();
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