using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.API.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        //Controller tarafında socket işlemi yapabilmek için ekledik
        //Her zaman hub class ı içerisinde olayları çözemeyebiliriz
        private readonly IHubContext<MyHub> _hubContext;

        public NotificationController(IHubContext<MyHub> hubContext)
        {
            _hubContext = hubContext;
        }


        //Bu Endpoint e istek geldiği zaman Client tarafında tetikleme işlemi yapıyoruz.
        [HttpGet("{teamcount}")]
        //domain.com/api/Notification/11 şeklinde
        public async Task<IActionResult> SetTeamCount(int teamcount)
        {
            //Parametre olarak gelen teamcount MyHub a atanacak ve işlem gerçekleşecek
            MyHub.TeamCount = teamcount;

            //Client tarafında Notify e subscribe oluyoruz ve Notify e subscribe olanlar yapılan değişikliği gözlemleyebiliyor.
            await _hubContext.Clients.All.SendAsync("Notify", $"Takım {teamcount} kişiden oluşacaktır");

            return Ok();
        }
    }
}
