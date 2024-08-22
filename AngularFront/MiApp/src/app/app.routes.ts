import { Routes } from '@angular/router';
import { InicioComponent } from './Pages/inicio/inicio.component';
import { UsuarioComponent } from './Pages/usuario/usuario.component';

export const routes: Routes = [
    {path: '', redirectTo: '/inicio', pathMatch: 'full' },
    {path:'inicio',component:InicioComponent},
    {path:'usuario/:id',component:UsuarioComponent},

];
