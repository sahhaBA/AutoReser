import {HttpHeaders} from "@angular/common/http";

export class MyConfig{
  //static adresaServer:string = "https://localhost:44327";
  static adresaServer:string = "https://p2076.app.fit.ba";
  static httpOpcije = {
    headers: new HttpHeaders({"Content-Type": "application/json"})
  };
}
