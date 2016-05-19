using System;
using System.Drawing;
using System.Web;

namespace Octopus_project.Services
{
    public static class ImageResizer
    {
        public static byte[] Resize(HttpPostedFileBase file, uint width, uint height)
        {
            if (file != null && file.ContentLength != 0 && file.ContentLength <= 307200)
            {
                using (Bitmap originalPic = new Bitmap(file.InputStream, false))
                {
                    int originalWidth = originalPic.Width;
                    int originalHeight = originalPic.Height;
                    int widthDiff = (originalWidth - (int)width);
                    int heightDiff = (originalHeight - (int)height);

                    bool doWidthResize = (width > 0 && originalWidth > width && widthDiff > -1 && widthDiff > heightDiff);
                    bool doHeightResize = (height > 0 && originalHeight > height && heightDiff > -1 && heightDiff > widthDiff);

                    if (doWidthResize || doHeightResize || (originalWidth.Equals(originalHeight) && widthDiff.Equals(heightDiff)))
                    {
                        int iStart;
                        decimal divider;
                        if (doWidthResize)
                        {
                            iStart = originalWidth;
                            divider = Math.Abs((decimal)iStart / width);
                            originalWidth = (int)width;
                            originalHeight = (int)Math.Round((originalHeight / divider));
                        }
                        else
                        {
                            iStart = originalHeight;
                            divider = Math.Abs((decimal)iStart / height);
                            originalHeight = (int)height;
                            originalWidth = (int)Math.Round((originalWidth / divider));
                        }
                    }

                    using (Bitmap newBMP = new Bitmap(originalPic, originalWidth, originalHeight))
                    {
                        ImageConverter converter = new ImageConverter();
                        return (byte[])converter.ConvertTo(newBMP, typeof(byte[]));
                    }
                }
            }
            return null;
        }

        public static Bitmap ResizeToBitmap(HttpPostedFileBase file, uint width, uint height)
        {
            if (file != null && file.ContentLength != 0 && file.ContentLength <= 307200)
            {
                using (Bitmap originalPic = new Bitmap(file.InputStream, false))
                {
                    int originalWidth = originalPic.Width;
                    int originalHeight = originalPic.Height;
                    int widthDiff = (originalWidth - (int)width);
                    int heightDiff = (originalHeight - (int)height);

                    bool doWidthResize = (width > 0 && originalWidth > width && widthDiff > -1 && widthDiff > heightDiff);
                    bool doHeightResize = (height > 0 && originalHeight > height && heightDiff > -1 && heightDiff > widthDiff);

                    if (doWidthResize || doHeightResize || (originalWidth.Equals(originalHeight) && widthDiff.Equals(heightDiff)))
                    {
                        int iStart;
                        decimal divider;
                        if (doWidthResize)
                        {
                            iStart = originalWidth;
                            divider = Math.Abs((decimal)iStart / width);
                            originalWidth = (int)width;
                            originalHeight = (int)Math.Round((originalHeight / divider));
                        }
                        else
                        {
                            iStart = originalHeight;
                            divider = Math.Abs((decimal)iStart / height);
                            originalHeight = (int)height;
                            originalWidth = (int)Math.Round((originalWidth / divider));
                        }
                    }

                    using (Bitmap newBMP = new Bitmap(originalPic, originalWidth, originalHeight))
                    {
                        return newBMP;
                    }
                }
            }
            return null;
        }
    }
}