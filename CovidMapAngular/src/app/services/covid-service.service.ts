import { Injectable } from '@angular/core';

//"npm install @microsoft/signalr" komutuyla yüklediğimiz kütüphaneyi servise import ettik
//* as komutu kütüphane içerisinde ki herşeyi "signalR" ismmiyle kullanacağımı belirtiyor
import * as signalR from "@microsoft/signalr";
import { Covid } from '../models/covid.model';


//DI işleminin uygulanabileceği anlamına gelir
//ng g service services/covidService komutuyla oluşturduk
@Injectable({
  providedIn: 'root'
})
export class CovidServiceService {

  //apiden gelen datayı charta uygun hale getireceğimiz dizimiz
  covidChartList = new Array();

  //Hub Connection u tanımladık
  private hubConnection: signalR.HubConnection;



  constructor() { }

  //İlk dataları almak için hub a istek atacağımız fonksiyon
  private startInvoke() {
    this.hubConnection.invoke("GetCovidList").catch((err) => {
      console.log(`Dataları alırken alınan hata: ${err}`);
    });
  };


  //Socket bağlantısını oluşturacak metodumuz
  startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:44372/MyHub").build();

    //Bağlantıyı başlatıyoruz
    this.hubConnection.start()
      .then(() => {
        console.log("Soket bağlantısı başarılı");

        //İlk dataları alacak olan fonksiyonumuzu çağırıyoruz
        this.startInvoke();
      })
      .catch((err) => {
        console.log(`Sokete bağlanırken alınan hata : ${err}`);
      });
  };

  //Server a istek attıktan sonra Client tarafında tetiklenecek metodumuza subscribe ile bağlandık
  //Dönen datayı Angular tarafında modelimize cast ettik
  startListener() {
    this.hubConnection.on("ReceiveCovidList", (covidCharts: Covid[]) => {
      //Grafik oluşturmada kullanacağımız dizimizi sıfırlıyoruz ki dublicate datalar oluşmasın yoksa tutarsız sonuçlar alırız
      this.covidChartList = [];

      //Socketten gelen datalarımız foreach ile dönüyorum ve grafik tarafında kullanacağım dizime atıyorum
      covidCharts.forEach((item) => {
        this.covidChartList.push([
          item.date,
          item.totalIstanbulVariant,
          item.totalAnkaraVariant,
          item.totalIzmirVariant,
          item.totalCanakkaleVariant,
          item.totalAntalyaVariant]);
      });

    });
  }

}
