using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GHAInmobiliaria.Models
{
    public class RepositorioPropietario
    {
        private readonly IConfiguration config;
        private readonly string connectionString;

        public RepositorioPropietario(IConfiguration config)
        {
            this.config = config;
            this.connectionString = config["ConnectionStrings:DefaultConnection"];
        }

        public List<Propietario> ObtenerTodos()
        {
            List<Propietario> listaPropietarios = new List<Propietario>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT Id, Nombre, Apellido, Dni, Telefono, Email, Clave" +
                    $" FROM Propietarios";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Propietario propietario = new Propietario
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),
                            Clave = reader.GetString(6),
                        };
                        listaPropietarios.Add(propietario);
                    }
                    connection.Close();
                }
            }
            return listaPropietarios;
        }

        public int Alta(Propietario propietario)
        {
            int resultado = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Propietarios (Nombre, Apellido, Dni, Telefono, Email, Clave) " +
                    $"VALUES (@nombre, @apellido, @dni, @telefono, @email, @clave);" +
                    "SELECT SCOPE_IDENTITY();";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@nombre", propietario.Nombre);
                    command.Parameters.AddWithValue("@apellido", propietario.Apellido);
                    command.Parameters.AddWithValue("@dni", propietario.Dni);
                    command.Parameters.AddWithValue("@telefono", propietario.Telefono);
                    command.Parameters.AddWithValue("@email", propietario.Email);
                    command.Parameters.AddWithValue("@clave", propietario.Clave);
                    connection.Open();
                    resultado = Convert.ToInt32(command.ExecuteScalar());
                    propietario.Id = resultado;
                    connection.Close();
                }
            }
            return resultado;
        }

        public Propietario ObtenerPorId(int id)
        {
            Propietario propietario = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT Id, Nombre, Apellido, Dni, Telefono, Email, Clave FROM Propietarios" +
                    $" WHERE Id=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        propietario = new Propietario
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),
                            Clave = reader.GetString(6)
                        };
                    }
                    connection.Close();
                }
            }
            return propietario;
        }

        public int Modificacion(Propietario propietario)
        {
            int resultado = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Propietarios SET Nombre=@nombre, Apellido=@apellido, Dni=@dni, Telefono=@telefono, Email=@email, Clave=@clave " +
                    $"WHERE Id = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@nombre", propietario.Nombre);
                    command.Parameters.AddWithValue("@apellido", propietario.Apellido);
                    command.Parameters.AddWithValue("@dni", propietario.Dni);
                    command.Parameters.AddWithValue("@telefono", propietario.Telefono);
                    command.Parameters.AddWithValue("@email", propietario.Email);
                    command.Parameters.AddWithValue("@clave", propietario.Clave);
                    command.Parameters.AddWithValue("@id", propietario.Id);
                    connection.Open();
                    resultado = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return resultado;
        }

        public int Baja(int id)
        {
            int resultado = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"DELETE FROM Propietarios WHERE Id = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    resultado = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return resultado;
        }
    }
}
