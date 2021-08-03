using CovidMap.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidMap.Models
{
    public class CovidService
    {
        //DB erişimini DI ile ekledik
        private readonly AppDbContext _context;

        //Db ye ekleme işlemi yapıldığında "ReceiveCovidList" metodunu tetikleyebilmek için ekledik
        private readonly IHubContext<CovidHub> _hubContext;

        public CovidService(AppDbContext context, IHubContext<CovidHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IQueryable<Covid> GetList()
        {
            return _context.Covids.AsQueryable();
        }

        public async Task SaveCovid(Covid covid)
        {
            await _context.AddAsync(covid);
            await _context.SaveChangesAsync();

            //Ekleme işleminde sonra socket tekrardan tetiklenecek ve yeni dataları gönderecek
            await _hubContext.Clients.All.SendAsync("ReceiveCovidList",GetCovidMaps());
        }

        //Grafik gösterimine uygun olabilmesi için pivotlanmış halini return ediyorum
        public List<PivottedList> GetCovidMaps()
        {
            //IQueryable tipinde listemi aldım
            var queryableDatas = GetList();

            var pivotCovidMap = queryableDatas.GroupBy(q => q.CovidDate.Date)
                .Select(g => new PivottedList
                {
                    Date = g.Key.ToString("MMMM dd"),
                    TotalIstanbulVariant = g.Where(c => c.City == ECity.Istanbul).Sum(c => c.Count),
                    TotalAnkaraVariant = g.Where(c => c.City == ECity.Ankara).Sum(c => c.Count),
                    TotalIzmirVariant = g.Where(c => c.City == ECity.Izmir).Sum(c => c.Count),
                    TotalCanakkaleVariant = g.Where(c => c.City == ECity.Canakkale).Sum(c => c.Count),
                    TotalAntalyaVariant = g.Where(c => c.City == ECity.Antalya).Sum(c => c.Count)
                }).ToList();

            pivotCovidMap.Reverse();

            return pivotCovidMap.Take(5).ToList();
        }

    }
}
