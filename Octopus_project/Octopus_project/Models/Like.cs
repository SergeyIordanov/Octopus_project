using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Octopus_project.Models
{
    public class Like
    {
        public int LikeId { set; get; }
        public string UserId { set; get; }
        public int PhotoId { set; get; }
    }
}