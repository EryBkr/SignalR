using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.API.Models
{
    public class Team
    {
        //team.Users.Add() kullanımı için referans oluşturuyoruz
        public Team()
        {
            Users = new List<User>();
        }

        public int Id { get; set; }
        public string TeamName { get; set; }

        //ICollection dememizin sebebi Contains vs... gibi kendine ait metotları olmasıdır.
        //Virtual key in kullanılma nedeni ise Lazy Loading e uygun ortam sağlamaktır
        public virtual ICollection<User> Users { get; set; }
    }
}
