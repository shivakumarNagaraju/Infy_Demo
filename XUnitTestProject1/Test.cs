using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using DemoInfiy.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Threading.Tasks;
using DemoInfiy.Controllers;

namespace XUnitTestProject1
{
    public class Test
    {
        private HttpClient Client;
        private ValueController _controller;

        [Fact]
        public void TestCase()
        {
            var controoler = new ValueController(null);
            int[] num = { 3, 2, 1 };
            int[] actualResult= { 1,2,3};
            bool isequal=true;
            var result = controoler.GetSortedArrayValues(num);
            for(int i=0;i< actualResult.Length; i++)
            {
                if(actualResult[i] != result[i])
                {
                    isequal = false;
                    break;
                    
                }
            }
            Assert.Equal(true, isequal);
        }
    }
}

