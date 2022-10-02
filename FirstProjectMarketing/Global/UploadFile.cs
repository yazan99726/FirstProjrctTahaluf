using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace FirstProjectMarketing.Global
{
    public class UploadFile
    {
        private readonly IWebHostEnvironment hosting;

        public UploadFile(IWebHostEnvironment hosting)
        {
            this.hosting = hosting;
        }

        public string imageFile(IFormFile file, string folderName)
        {
            if (file != null)
            {
                string root = Path.Combine(hosting.WebRootPath, "Images"+ folderName);
                string imgPath =  file.FileName;
                string fullPath = Path.Combine(root, imgPath);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));
                return imgPath;
            }
            return null;
        }
        public string imageFile(IFormFile file, string oldFile, string folderName)
        {
            if (file != null)
            {
                string root = Path.Combine(hosting.WebRootPath, "Images"+ folderName);
                string imgPath =  file.FileName;
                string newpath = Path.Combine(root,imgPath);
                
                if (oldFile != null)
                {
                    string OldPath = Path.Combine(root, oldFile);
                    File.Delete(OldPath);
                }
                file.CopyTo(new FileStream(newpath, FileMode.Create));
                return imgPath;
            }
            return oldFile;
        }
        public void DeleteFile(string oldFile, string folderName)
        {
            string root = Path.Combine(hosting.WebRootPath, "Images" + folderName);
            string OldPath = Path.Combine(root, oldFile);
            if (OldPath != null)
            {
                File.Delete(OldPath);
            }
        }
    }

}
