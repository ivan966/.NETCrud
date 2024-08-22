import { Component, inject, OnInit } from '@angular/core';

import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

import { UsuarioService } from '../../Services/usuario.service';
import { Usuario } from '../../Models/Usuario';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';



@Component({
  selector: 'app-inicio',
  standalone: true,
  imports: [
    MatCardModule,
    MatTableModule,
    MatIconModule,
    MatButtonModule,
    CommonModule
  ],
  templateUrl: './inicio.component.html',
  styleUrl: './inicio.component.css'
})

export class InicioComponent implements OnInit {

  private UsuarioServicio = inject(UsuarioService);
  public ListaUsuarios: Usuario[] = [];
  public displayedColumns: string[] = ['NombreCompleto', 'Documento', 'Correo', 'FechaNacimiento', 'Boton'];

  data$: Observable<any> = new Observable<any>();

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.data$ = this.UsuarioServicio.lista()
  }


  nuevo() {
    this.router.navigate(['/usuario', 0]);
  }

  editar(objeto: Usuario) {
    console.log(objeto.idUsuario);
    this.router.navigate(['/usuario', objeto.idUsuario]);
  }

  eliminar(objeto: Usuario) {
    if (confirm("Desea eliminar el usuario " + objeto.nombreCompleto)) {
      this.UsuarioServicio.eliminar(objeto.idUsuario).subscribe({
        next: (data) => {
          if (data.isSuccess) {
            this.data$ = this.UsuarioServicio.lista()
          }
          else {
            alert("No se puede eliminar")
          }
        },
        error: (err) => {
          console.log(err.message)
        }
      })
    }
  }

}
