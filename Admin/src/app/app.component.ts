import { Component } from '@angular/core';
import {MapasService} from './services/mapas.service';
import {Marcador} from './interfaces/marcador.interface';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  lat: number = -17.766491;
  lng: number = -63.173752;
  zoom: number = 16; 
  marcadorSel: any = null;


  constructor(public mapaService:MapasService){
    this.mapaService.cargarMarcadores();
  }

clickMapa(evento){
  let nuevoMarcador:Marcador={
    lat:evento.coords.lat,
    lng:evento.coords.lng,
    titulo:"sin titulo",
    draggable:true
  }
  console.log(evento)
  this.mapaService.insertarMarcador(nuevoMarcador);
}
clickMarcador(marcador:Marcador,i:number){
    console.log(marcador,i);
    this.marcadorSel = marcador;
  }
dragEndMarcador(marcador:Marcador,evento){
  console.log(marcador,evento)
  let lat = evento.coords.lat;

  let lng = evento.coords.lng;
  marcador.lat = lat;
  marcador.lng = lng;
  this.mapaService.guardarMarcadores();
}
  

}
