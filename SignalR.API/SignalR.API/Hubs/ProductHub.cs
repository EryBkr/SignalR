using Microsoft.AspNetCore.SignalR;
using SignalR.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.API.Hubs
{
    //Implemente ettik.Metot isimlerimizi magic stringler ile yazmak hataya sebebiyet verebilir
    public class ProductHub : Hub<IProductHub>
    {
        public async Task SendProduct(Product product)
        {
            await Clients.All.ReceiveProduct(product);
        }
    }
}
