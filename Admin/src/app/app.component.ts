import { Component } from '@angular/core';
import {MapasService} from './services/mapas.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  lat: number = -17.766491;
  lng: number = -63.173752;
  zoom: number = 16; 

clickMapa(evento){
  console.log(evento)
}



}
