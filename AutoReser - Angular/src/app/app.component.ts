import {Component, OnInit} from '@angular/core';
import {Izvjestaj, IzvjestajiVM} from './IzvjestajiVM';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MyConfig } from "./MyConfig";
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  // @ts-ignore
  izvjestajPrikaz: IzvjestajiVM = null;
  pretraga: string = "";
  // @ts-ignore
  urediIzvjestaj: Izvjestaj;
  prikaziModul: boolean = false;
  brPodatakaPoStranici: number = 10;
  trenutnaStranica: number = 1;

  constructor(private http:HttpClient) {
  }

  ngOnInit(): void {
    this.preuzmiIzvjestaje();
  }

  preuzmiIzvjestaje(){
    let url:string = MyConfig.adresaServer + "/Home/Izvjestaji?trenutnaStranica=" + this.trenutnaStranica + "&q=" + this.pretraga;
    this.http.get<IzvjestajiVM>(url).subscribe((result)=>{
       this.izvjestajPrikaz = result;
    });
  }

  obnoviAuto(i: Izvjestaj){
    let indexOf = this.izvjestajPrikaz.izvjestaji.indexOf(i);
    this.izvjestajPrikaz.izvjestaji.splice(indexOf, 1);

    let url:string = MyConfig.adresaServer + "/Home/ObnoviAuto?izvjestajID=" + i.izvjestajID;
    this.http.get(url).subscribe((result)=>{
      alert(i.opis + " uspješno obnovljen!");
    });
  }

  obrisiIzvjestaj(i: Izvjestaj){
    let indexOf = this.izvjestajPrikaz.izvjestaji.indexOf(i);
    this.izvjestajPrikaz.izvjestaji.splice(indexOf, 1);

    let url:string = MyConfig.adresaServer + "/Home/ObrisiIzvjestaj?izvjestajID=" + i.izvjestajID;
    this.http.get(url).subscribe((result)=>{
      alert("Izvještaj " + i.naziv + " " + i.opis + " uspješno obrisan!");
    });
  }

  getIzvjestaje(): Izvjestaj[]{
    return this.izvjestajPrikaz.izvjestaji;
  }

  getBrojIzvjestaja(){
    return this.izvjestajPrikaz.total;
  }

  getBrojIzvjestajaTrenutno(){
    return this.izvjestajPrikaz.izvjestaji.length;
  }

  uredi(i: Izvjestaj){
    this.prikaziModul = true;
    return this.urediIzvjestaj = i;
  }

  snimiPromjene(){
    let url:string = MyConfig.adresaServer + "/Home/SnimiPromjeneIzvjestaja";

    this.http.post(url, this.urediIzvjestaj, MyConfig.httpOpcije).subscribe((result)=>{
      this.prikaziModul = false;
    });
  }

  pageNumberChanged($event: any) {
     this.preuzmiIzvjestaje();
  }

  pretraziIzvjestaje() {
    this.trenutnaStranica = 1;
    this.preuzmiIzvjestaje();
  }
}
