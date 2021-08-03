using CovidMap.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovidController : ControllerBase
    {
        private readonly CovidService _service;

        public CovidController(CovidService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> SaveCovid(Covid covid)
        {
            //Save işlemi Hub u tekrardan tetikleyecektir.Yeni Eklenen datayı böyleye görebileceğiz
            await _service.SaveCovid(covid);
         
            return Ok(_service.GetCovidMaps());
        }

        [HttpGet("getcovidmaps")]
        public  IActionResult GetCovidMaps()
        {
            //Grafik gösterimine uygun olabilmesi için pivotlanmış halini servisten return ediyorum
            return Ok(_service.GetCovidMaps());
        }

        //Başlangıçta Data yüklemek için ekledik
        [HttpGet]
        public IActionResult InitializeCovid()
        {
            Random random = new Random();
            Enumerable.Range(1, 10).ToList().ForEach(x =>
            {
                foreach (var item in Enum.GetValues(typeof(ECity)))
                {
                    var newCovid = new Covid { City = ((ECity)item), Count = random.Next(100, 1000), CovidDate = DateTime.Now.AddDays(x) };

                    //Foreach içerisinde await kabul edilmez
                    _service.SaveCovid(newCovid).Wait();

                    //1 Sn Bekliyoruz ki data değişimini rahat gözlemleyelim
                    System.Threading.Thread.Sleep(1000);
                }
            });

            return Ok("Covid 19 Dataları Db ye kayıt edildi");
        }
    }
}
