using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GHAInmobiliaria.Models
{
    public class RepositorioInmueble
    {
        private readonly IConfiguration config;
        private readonly string connectionString;

        public RepositorioInmueble(IConfiguration config)
        {
            this.config = config;
            this.connectionString = config["ConnectionStrings:DefaultConnection"];
        }

		public int Alta(Inmueble inmueble)
		{
			int resultado = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"INSERT INTO Inmuebles (Direccion, Ambientes, Uso, Tipo, Disponible, Superficie, Precio, PropietarioId) " +
					"VALUES (@direccion, @ambientes, @uso, @tipo, @disponible, @superficie, @precio, @propietarioId);" +
					"SELECT SCOPE_IDENTITY();";
				using (var command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@direccion", inmueble.Direccion);
					command.Parameters.AddWithValue("@ambientes", inmueble.Ambientes);
					command.Parameters.AddWithValue("@uso", inmueble.Uso);
					command.Parameters.AddWithValue("@tipo", inmueble.Tipo);
					command.Parameters.AddWithValue("@disponible", inmueble.Disponible);
					command.Parameters.AddWithValue("@superficie", inmueble.Superficie);
					command.Parameters.AddWithValue("@precio", inmueble.Precio);
					command.Parameters.AddWithValue("@propietarioId", inmueble.PropietarioId);
					connection.Open();
					resultado = Convert.ToInt32(command.ExecuteScalar());
					inmueble.Id = resultado;
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
				string sql = $"DELETE FROM Inmuebles WHERE Id = @id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@id", id);
					command.CommandType = CommandType.Text;
					connection.Open();
					resultado = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return resultado;
		}

		public int Modificacion(Inmueble inmueble)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = "UPDATE Inmuebles SET " +
					"Direccion=@direccion, Ambientes=@ambientes, Uso=@uso, Tipo=@tipo, Disponible=@disponible, Precio=@precio, Superficie=@superficie, PropietarioId=@propietarioId " +
					"WHERE Id = @id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@direccion", inmueble.Direccion);
					command.Parameters.AddWithValue("@ambientes", inmueble.Ambientes);
					command.Parameters.AddWithValue("@uso", inmueble.Uso);
					command.Parameters.AddWithValue("@tipo", inmueble.Tipo);
					command.Parameters.AddWithValue("@disponible", inmueble.Disponible);
					command.Parameters.AddWithValue("@precio", inmueble.Precio);
					command.Parameters.AddWithValue("@superficie", inmueble.Superficie);
					command.Parameters.AddWithValue("@propietarioId", inmueble.PropietarioId);
					command.Parameters.AddWithValue("@id", inmueble.Id);
					command.CommandType = CommandType.Text;
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}
		

		public List<Inmueble> ObtenerTodos()
		{
			List<Inmueble> res = new List<Inmueble>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = "SELECT i.Id, Direccion, Ambientes, Uso, Tipo, Disponible, Superficie, Precio, PropietarioId," +
					" p.Nombre, p.Apellido" +
					" FROM Inmuebles i INNER JOIN Propietarios p ON i.PropietarioId = p.Id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Inmueble entidad = new Inmueble
						{
							Id = reader.GetInt32(0),
							Direccion = reader.GetString(1),
							Ambientes = reader.GetInt32(2),
							Uso = reader.GetString(3),
							Tipo = reader.GetString(4),
							Disponible = reader.GetString(5),
							Superficie = reader.GetString(6),
							Precio = reader.GetDecimal(7),
							PropietarioId = reader.GetInt32(8),

							Propietario = new Propietario
							{
								Id = reader.GetInt32(8),
								Nombre = reader.GetString(9),
								Apellido = reader.GetString(10),
							}
						};
						res.Add(entidad);
					}
					connection.Close();
				}
			}
			return res;
		}

		

		public Inmueble ObtenerPorId(int id)
		{
			Inmueble inmueble = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = "SELECT i.Id, Direccion, Ambientes, Uso, Tipo, Disponible, Precio, Superficie, PropietarioId," +
					" p.Nombre, p.Apellido" +
					" FROM Inmuebles i INNER JOIN Propietarios p ON i.PropietarioId = p.Id" +
					" WHERE i.Id=@id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@id", id);
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						inmueble = new Inmueble
						{
							Id = reader.GetInt32(0),
							Direccion = reader.GetString(1),
							Ambientes = reader.GetInt32(2),
							Uso = reader.GetString(3),
							Tipo = reader.GetString(4),
							Disponible = reader.GetString(5),
							Precio = reader.GetDecimal(6),
							Superficie = reader.GetString(7),
							PropietarioId = reader.GetInt32(8),
							Propietario = new Propietario
							{
								Id = reader.GetInt32(8),
								Nombre = reader.GetString(9),
								Apellido = reader.GetString(10)
							}
						};
					}
					connection.Close();
				}
			}
			return inmueble;
		}

		public List<Inmueble> BuscarPorPropietario(int idPropietario)
		{
			List<Inmueble> result = new List<Inmueble>();
			Inmueble inmueble = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = "SELECT Id, Direccion, Ambientes, Uso, Tipo, Disponible, Precio, Superficie, PropietarioId," +
					" p.Nombre, p.Apellido" +
					" FROM Inmuebles i INNER JOIN Propietarios p ON i.PropietarioId = p.Id" +
					" WHERE IdPropietario=@idPropietario";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@idPropietario", idPropietario);
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						inmueble = new Inmueble
						{
							Id = reader.GetInt32(0),
							Direccion = reader.GetString(1),
							Ambientes = reader.GetInt32(2),
							Uso = reader.GetString(3),
							Tipo = reader.GetString(4),
							Disponible = reader.GetString(5),
							Precio = reader.GetDecimal(6),
							Superficie = reader.GetString(7),
							PropietarioId = reader.GetInt32(8),
							Propietario = new Propietario
							{
								Id = reader.GetInt32(8),
								Nombre = reader.GetString(9),
								Apellido = reader.GetString(10)
							}
						};
						result.Add(inmueble);
					}
					connection.Close();
				}
			}
			return result;
		}
	}
}
