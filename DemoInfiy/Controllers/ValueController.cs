using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DemoInfiy.Controllers
{
    [EnableCors("AllowCors")]
    [Route("api/[controller]")]
   
    public class ValueController : Controller
    {
        private readonly IConfiguration _configuration;
        private IHostingEnvironment hostingEnv;
       

        public ValueController(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
            this.hostingEnv = env;
        }

        [HttpGet]
        [Route("GetSortedArrayValues")]
        public JsonResult GetSortedArrayValues(int [] arr)
        {
            string data = string.Empty;
            int temp;
            for (int j = 0; j <= arr.Length - 2; j++)
            {
                for (int i = 0; i <= arr.Length - 2; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        temp = arr[i + 1];
                        arr[i + 1] = arr[i];
                        arr[i] = temp;
                    }
                }
            }
            var path = Path.Combine(hostingEnv.ContentRootPath, @"TxtFiles\" + $"{Guid.NewGuid() + Convert.ToString(DateTime.Now.Second)}.txt");
            using (FileStream fs = System.IO.File.Create(path))
            {
                string ids = String.Join(",", arr.Select(p => p.ToString()).ToArray());
                byte[] content = new UTF8Encoding(true).GetBytes(ids);
                fs.Write(content, 0, content.Length);
            }
            return Json(arr);
        }

        [HttpGet]
        [Route("GetLatestFileContents")]
        public JsonResult GetLatestFileContents()
        {
            var directory = new DirectoryInfo("TxtFiles");
            var myFile = (from f in directory.GetFiles()
                          orderby f.LastWriteTime descending
                          select f).First();
            var Filename = @"TxtFiles\" + myFile.Name;
            var fileContents = System.IO.File.ReadAllLines(Filename);
            return Json(new { Status = true, message = "Success", fileContents = fileContents, });
        }
    }
}