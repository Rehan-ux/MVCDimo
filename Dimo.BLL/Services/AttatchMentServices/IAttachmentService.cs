﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.BLL.Services.AttatchMentServices
{
    public interface IAttachmentService
    {
        //Upload
        public string? Upload(IFormFile file, string folderName);
        //Delete
        bool Delete(string fileName ,string folderName);
    }
}
