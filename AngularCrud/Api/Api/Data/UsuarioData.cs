using Api.Models;
using System.Data;
using System.Data.SqlClient;
namespace Api.Data
{
    public class UsuarioData
    {
        private readonly string conexion;

        public UsuarioData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<Usuario>> Lista()
        {
            List<Usuario> lista = new List<Usuario>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("listaUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Usuario
                        {
                            IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            Documento = reader["Documento"].ToString(),
                            Correo = reader["Correo"].ToString(),
                            FechaNacimiento = reader["FechaNacimiento"].ToString(),
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<Usuario> Obtener(int Id)
        {
            Usuario objeto = new Usuario();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("obtenerUsuario", con);
                cmd.Parameters.AddWithValue("@IdUsuario",Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Usuario
                        {
                            IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            Documento = reader["Documento"].ToString(),
                            Correo = reader["Correo"].ToString(),
                            FechaNacimiento = reader["FechaNacimiento"].ToString(),
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Crear(Usuario objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("crearUsuario", con);
                cmd.Parameters.AddWithValue("@NombreCompleto", objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@Documento", objeto.Documento);
                cmd.Parameters.AddWithValue("@Correo", objeto.Correo);  
                cmd.Parameters.AddWithValue("@FechaNacimiento", objeto.FechaNacimiento);  
                cmd.CommandType = CommandType.StoredProcedure;

                try 
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true:false; 
                }
                catch
                {
                    respuesta = false;
                }
                
            }
            return respuesta;
        }

        public async Task<bool> Editar(Usuario objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("editarUsuario", con);
                cmd.Parameters.AddWithValue("@IdUsuario", objeto.IdUsuario);
                cmd.Parameters.AddWithValue("@NombreCompleto", objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@Documento", objeto.Documento);
                cmd.Parameters.AddWithValue("@Correo", objeto.Correo);
                cmd.Parameters.AddWithValue("@FechaNacimiento", objeto.FechaNacimiento);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }

            }
            return respuesta;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("eliminarUsuario", con);
                cmd.Parameters.AddWithValue("@IdUsuario", id);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }

            }
            return respuesta;
        }
    }
}
