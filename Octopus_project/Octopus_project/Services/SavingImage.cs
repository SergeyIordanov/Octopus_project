using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.IO;

namespace Octopus_project.Services
{
    public class SavingImage
    {
        public static void SaveImage(HttpPostedFileBase file, string filePath)
        {
            Image img = ImageResizer.ResizeToBitmap(file, 400, 400);
            try
            {        
                file.SaveAs(filePath);
            }
            catch (Exception)
            {
            }
        }
    }
}