import { Component, inject, Input, OnInit } from '@angular/core';

import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {FormBuilder,FormGroup,ReactiveFormsModule} from '@angular/forms';
import { UsuarioService } from '../../Services/usuario.service';
import { Router } from '@angular/router';
import { Usuario } from '../../Models/Usuario';

@Component({
  selector: 'app-usuario',
  standalone: true,
  imports: [MatFormFieldModule,MatInputModule,MatButtonModule,ReactiveFormsModule],
  templateUrl: './usuario.component.html',
  styleUrl: './usuario.component.css'
})
export class UsuarioComponent implements OnInit{

  @Input('id') idUsuario! : number;
  private UsuarioServicio = inject(UsuarioService);
  public formBuild = inject(FormBuilder);

  public formUsuario:FormGroup = this.formBuild.group({
    nombreCompleto:[''],
    documento:[''],
    correo:[''],
    fechaNacimiento:['']
  });

  constructor(private router:Router){}

  ngOnInit(): void{
    if(this.idUsuario != 0){
      this.UsuarioServicio.obtener(this.idUsuario).subscribe({
        next:(data) =>{
          this.formUsuario.patchValue({
            nombreCompleto: data.nombreCompleto,
            documento:data.documento,
            correo: data.correo,
            fechaNacimiento: data.fechaNacimiento
          })
        },
        error:(err)=>{
          console.log(err.message)
        }
      })
    }
  }

  guardar(){
    const objeto : Usuario = {
      idUsuario : this.idUsuario,
      nombreCompleto :  this.formUsuario.value.nombreCompleto,
      documento : this.formUsuario.value.documento,
      correo : this.formUsuario.value.correo,
      fechaNacimiento : this.formUsuario.value.fechaNacimiento
    }

    if(this,this.idUsuario == 0){
      this.UsuarioServicio.crear(objeto).subscribe({
        next:(data)=>{
          if(data.isSuccess){
            this.router.navigate(["/"]);
          }
          else{
            alert("Error al Crear")
          }
        },
        error:(err)=>{
          console.log(err.message)
        }
      })
    }
    else{
      this.UsuarioServicio.editar(objeto).subscribe({
        next:(data)=>{
          if(data.isSuccess){
            this.router.navigate(["/"]);
          }
          else{
            alert("Error al Editar")
          }
        },
        error:(err)=>{
          console.log(err.message)
        }
      })
    }
  }
  volver(){
    this.router.navigate(["/"]);
  }
}
