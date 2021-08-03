import { Component, OnInit } from '@angular/core';
import { CovidServiceService } from './services/covid-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'CovidMapAngular';

  //Chart coloumn names was created
  coloumnNames = ["Tarih", "İstanul", "Ankara", "İzmir", "Canakkale", "Antalya"];

  //Chart labelleri alt tarafta gözüksün
  options:any={
    legend:{position:"Bottom"}
  };

  //DI ile servisimi oluşturuyorum covidChartList e UI da erişebilmek için public olarak tanımladım
  //Servis sayesinde api tarafından datalarımı hub aracılığıyla alacağım
  constructor(public covidService: CovidServiceService) {
  }

//Component ilk oluştuğunda execute edilir
  ngOnInit(): void {

    //Bağlantı başlatıldı ve start.invoke aktif edildi.
    this.covidService.startConnection();

    //Hubtan dönecek olan datalar alındı(subscribe olundu)
    this.covidService.startListener();
  }






}
