using Business.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extension
{
    public class Helper
    {
        public static string CreateFile(string rootPath, string folder, IFormFile file)
        {
            if (file.ContentType != "image/png" && file.ContentType != "image/jpeg")
                throw new ImageTypeException("File tipi duzgun deyil");

            if (file.Length > 2097152)
                throw new ImageSizeException("File teyin edilmis olcuden yuxari ola bilmez");

            string fileName= Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string path = rootPath + $@"\{folder}\" + fileName;

            using(FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }

        public static void DeleteFile(string rootPath, string folder, string fileName)
        {
            string path = rootPath + $@"\{folder}\" + fileName;

            if(!File.Exists(path))
                throw new Exceptions.FileNotFoundException("file tapilmadi");  

            File.Delete(path);
        }
    }
}
