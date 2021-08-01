using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalR.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.API.Hubs
{
    //SignalR Hub ı entegre ettik
    //Her bir Client erişiminde bir MyHub instance 'ı oluşur
    public class MyHub : Hub
    {

        //DB ye erişmek için direkt context i DI ile oluşturuyorum
        //Burada ki amacım direkt sonuca ulaşmak
        private readonly MyDbContext _context;

        public MyHub(MyDbContext context)
        {
            _context = context;
        }

        //Her bir instance oluştuğunda dataların kaybolmaması için static oluşturduk
        private static List<string> Names { get; set; } = new List<string>();

        //Sayfada aktif olan kullanıcı sayısı,static tanımlama nedenimiz instance oluştuğu zaman referansı değişmesin içerisinde ki değerleri kaybetmeyelim
        public static int ClientCount { get; set; } = 0;

        //TeamCount sayısına göre farklı bir connection oluşturacağımız için property olarak oluşturuyorum
        //Public tanımlama nedenimiz Controller da erişmek ve static tanımlama nedenimiz ise instance oluştuğunda değerimiz kaybolmasın
        public static int TeamCount { get; set; } = 7;

        //Client ın tetikleyeceği metodu tanımladık
        public async Task SendName(string name)
        {
            if (Names.Count >= TeamCount)
            {
                //Kullanıcı değeri bizim Controller tarafında belirlediğimiz TeamCount değerini aştığı zaman aşan kişinin tarayıcısında ve o kişiye özel uyarı göndereceğiz
                await Clients.Caller.SendAsync("Error", $"Takım sayısı en fazla {TeamCount} kişi olabilir");
            }
            else
            {
                //Her instance oluştuğunda listeye gönderilen ismi ekliyorum
                Names.Add(name);

                //Bu hub a bağlı olan herkes için çalışır
                //Client tarafında çalışacak olan metodu ve parametreyi belirledik
                await Clients.All.SendAsync("ReceiveName", name);
            }

        }

        public async Task GetNames()
        {
            await Clients.All.SendAsync("ReceiveNames", Names);
        }


        //Hub a bağlanıldığında
        public async override Task OnConnectedAsync()
        {
            //Bağlantı yapıldığında kişi sayısını arttırıyoruz
            ClientCount++;
            //Bağlanan kişi sayısı arttığı zaman Client tarafında tetikleme işlemi yapıyoruz
            await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
        }

        //Hub tan bağlantı koparıldığında
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            //Bağlantı kesildiğinde Client sayısını azaltılıyoruz
            ClientCount--;
            //Bağlanan kişi sayısı azaldığı zaman Client tarafında tetikleme işlemi yapıyoruz
            await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
        }

        //Groups
        public async Task SendNameByGroup(string playerName, string teamName)
        {
            //Seçilen Takımın entity sini aldık
            var teamEntity = _context.Teams.FirstOrDefault(i => i.TeamName == teamName);

            if (teamEntity != null)
            {
                //Takım var ise o takıma kişiyi ekliyoruz
                teamEntity.Users.Add(new User { Name = playerName });
            }
            else
            {
                //Takım Adı mevcut değilse oluşturuyoruz
                var newTeam = new Team { TeamName = teamName };
                //Oyuncu adının Yeni Takımımıza ekliyoruz
                newTeam.Users.Add(new User { Name = playerName });
                //DB ye ekledik
                _context.Teams.Add(newTeam);
            }
            //Kayıt edildi
            await _context.SaveChangesAsync();

            //Mesajı Sadece Takım Adına sahip gruba yayınlayacağız
            await Clients.Group(teamName).SendAsync("ReceiveMessageByGroup",playerName,teamName);
        }

        //Client tarafında kişi,hangi gruba kayıt olmak isterse ona ekliyoruz.
        //Groups ConnectionId ye ihtiyaç duyuyor
        //ConnectionId Client hub a her bağlandığında oluşan unique bir değerdir
        public async Task AddToGroup(string teamName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, teamName);
        }

        //Kişiyi Gruptan çıkartacaktır
        //ConnectionId Client hub a her bağlandığında oluşan unique bir değerdir
        public async Task RemoveToGroup(string teamName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, teamName);
        }

        //Tüm Playerlerı gösterek (Client ilk girdiği zaman)
        public async Task GetNamesByGroup()
        {
            //Oyuncularıyla birlikte takımları alıyoruz
            var teams = _context.Teams.Include(i => i.Users).Select(x => new
            {
                teamName=x.TeamName,
                Users=x.Users.ToList()
            });

            //Tüm Takım ve Oyuncu listesini UI da gösterebilmek için Tetikleyeceğiz
            await Clients.All.SendAsync("ReceiveNamesByGroup",teams);
        }

        //Client tarafından Complex Type alıp Tekrar Client tarafına gönderiyoruz
        public async Task SendProduct(Product product)
        {
            await Clients.All.SendAsync("ReceiveProduct",product);
        }
    }
}
