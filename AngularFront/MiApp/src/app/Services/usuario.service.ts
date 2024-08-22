import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { appsettings } from '../Settings/appSettings';
import { Usuario } from '../Models/Usuario';
import { ResponseApi } from '../Models/ResponseApi';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private http = inject(HttpClient);
  private apiUrl:string =appsettings.apiUrl + "Usuario"; 
  constructor() { }



  lista(){
    return this.http.get<Usuario[]>(this.apiUrl);
  }

  obtener(id:number){
    return this.http.get<Usuario>(`${this.apiUrl}/${id}`);
  }

  crear(objeto:Usuario){
    return this.http.post<ResponseApi>(this.apiUrl,objeto);
  }  

  editar(objeto:Usuario){
    return this.http.put<ResponseApi>(this.apiUrl,objeto);
  }  
  eliminar(id:number){
    return this.http.delete<ResponseApi>(`${this.apiUrl}/${id}`); 
  }  

}
