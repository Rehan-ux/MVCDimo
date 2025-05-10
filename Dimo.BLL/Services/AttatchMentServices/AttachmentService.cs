using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.BLL.Services.AttatchMentServices
{
    public class AttachmentService : IAttachmentService
    {
        List<string> allowedExtensions = [".png", ".jpg", ".Jepg"];    //syntaxsuger//new List<string>() { ".png", ".jpg", ".Jepg" };  //هنا بعمل check علي Extention
        const int maxSize = 2_997_152;
        public string? Upload(IFormFile file, string folderName)
        {
            //1-check Extention
            var extension= Path.GetExtension(file.FileName);//.png
            if (!allowedExtensions.Contains(extension))return null ; 
            //2- Check size
            if(file.Length == 0 || file.Length > maxSize) return null ;
            //3- Get Located folder path
           // var folderpath = "D:\\taskat الدوره\\MVC\\MVCDimo\\Dimo.PL\\wwwroot\\Files\\Images\\" Local invalid
           //var folderPath= $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{folderName}"; //الطريقه الاولي 
           var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files",folderName);
            //4- Make Attatchment Name Unique اسمه يكون  علشان مش يكون كتطرر 
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";//Uniqe
            //5-File path
            var filepath = Path.Combine(folderPath,fileName);//file location
            //6- Greate File Stream tocopy file [Unmanage ]
           using FileStream fs = new FileStream(filepath, FileMode.Create);
            //7- Use Stream TO copy File 
            file.CopyTo(fs);
            //8- Return file name to store in dsta base
            return fileName;

        }
        public bool Delete(string fileName , string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName, fileName);
            //return bool عايز اقول دا اتمسح ولالا يعني true ,gh false

            if (!File.Exists(filePath)) return false;
            else
            {
                File.Delete(filePath);
                return true;
            }
        }

        
    }
}
