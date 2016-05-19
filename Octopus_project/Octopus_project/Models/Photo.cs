using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Octopus_project.Models
{
    public class Photo
    {
        public int PhotoId { set; get; }
        public string PublisherName { set; get; }
        public string PublisherSurname { set; get; }
        public string Path { set; get; }
        public int Likes { set; get; }
    }
}