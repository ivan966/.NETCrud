﻿namespace Api.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string? NombreCompleto { get; set; }

        public string? Documento { get; set; }

        public string? Correo { get; set; }

        public string? FechaNacimiento { get; set; }
    }
}
