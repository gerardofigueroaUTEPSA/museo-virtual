import { Injectable } from '@angular/core';
import {Marcador} from '../interfaces/marcador.interface';
@Injectable()
export class MapasService {

  marcadores:Marcador[]=[];

  constructor() { 
    let nuevoMarcador:Marcador={
      lat:-17.766491,
      lng:-63.173752,
      titulo:"HackBO",
      draggable:true
    }
    this.marcadores.push(nuevoMarcador);


  }

}
