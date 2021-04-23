using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GHAInmobiliaria.Models
{
    public class RepositorioContrato
    {
        private readonly IConfiguration config;
        private readonly string connectionString;

        public RepositorioContrato(IConfiguration config)
        {
            this.config = config;
            this.connectionString = config["ConnectionStrings:DefaultConnection"];
        }

		public int Alta(Contrato contrato)
		{
			int resultado = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"INSERT INTO Contratos (FechaDesde, FechaHasta, MontoMensual, InmuebleId, InquilinoId) " +
					"VALUES (@fechaDesde, @fechaHasta, @montoMensual, @inmuebleId, @inquilinoId);" +
					"SELECT SCOPE_IDENTITY();";

				using (var command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@fechaDesde", contrato.FechaDesde);
					command.Parameters.AddWithValue("@fechaHasta", contrato.FechaHasta);
					command.Parameters.AddWithValue("@montoMensual", contrato.MontoMensual);
					command.Parameters.AddWithValue("@inmuebleId", contrato.InmuebleId);
					command.Parameters.AddWithValue("@inquilinoId", contrato.InquilinoId);
					
					connection.Open();
					resultado = Convert.ToInt32(command.ExecuteScalar());
					contrato.Id = resultado;
					connection.Close();
				}
			}
			return resultado;
		}

		public List<Contrato> ObtenerTodos()
		{
			List<Contrato> resultado = new List<Contrato>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = "SELECT c.Id, c.FechaDesde, c.FechaHasta, c.MontoMensual, c.InmuebleId, c.InquilinoId, " +
					" inm.Direccion, inm.Precio," +
					" inq.Nombre, inq.Apellido" +
					" FROM Contratos c" +
					" INNER JOIN Inmuebles inm ON c.InmuebleId = inm.Id" +
					" INNER JOIN Inquilinos inq ON c.InquilinoId = Inq.Id";

				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Contrato contrato = new Contrato
						{
							Id = reader.GetInt32(0),
							FechaDesde = reader.GetDateTime(1),
							FechaHasta = reader.GetDateTime(2),
							MontoMensual = reader.GetDecimal(3),
							InmuebleId = reader.GetInt32(4),
							InquilinoId = reader.GetInt32(5),

							Inmueble = new Inmueble
							{
								Id = reader.GetInt32(4),
								Direccion = reader.GetString(6),
								Precio = reader.GetDecimal(7),
							}, 

							Inquilino = new Inquilino
                            {
								Id = reader.GetInt32(5),
								Nombre = reader.GetString(8),
								Apellido = reader.GetString(9)
                            }
						};

						resultado.Add(contrato);
					}
					connection.Close();
				}
			}
			return resultado;
		}

		public int Modificacion(Contrato contrato)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = " UPDATE Contratos SET" +
				" FechaDesde=@fechaDesde, FechaHasta=@fechaHasta, MontoMensual=@montoMensual, InmuebleId=@inmuebleId, InquilinoId=@inquilinoId" +
				" WHERE Id=@id";

				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@fechaDesde", contrato.FechaDesde);
					command.Parameters.AddWithValue("@fechaHasta", contrato.FechaHasta);
					command.Parameters.AddWithValue("@montoMensual", contrato.MontoMensual);
					command.Parameters.AddWithValue("@inmuebleId", contrato.InmuebleId);
					command.Parameters.AddWithValue("@inquilinoId", contrato.InquilinoId);
					command.Parameters.AddWithValue("@id", contrato.Id);
					command.CommandType = CommandType.Text;
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public int Baja(int id)
		{
			int resultado = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"DELETE FROM Contratos WHERE Id = @id";
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

		public Contrato ObtenerPorId(int id)
		{
			Contrato contrato = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = "SELECT c.Id, c.FechaDesde, c.FechaHasta, c.MontoMensual, c.InmuebleId, c.InquilinoId, " +
					" inm.Direccion, inm.Precio," +
					" inq.Nombre, inq.Apellido" +
					" FROM Contratos c" +
					" INNER JOIN Inmuebles inm ON c.InmuebleId = inm.Id" +
					" INNER JOIN Inquilinos inq ON c.InquilinoId = Inq.Id" +
					" WHERE c.Id = @id";

				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@id", id);
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						contrato = new Contrato
						{
							Id = reader.GetInt32(0),
							FechaDesde = reader.GetDateTime(1),
							FechaHasta = reader.GetDateTime(2),
							MontoMensual = reader.GetDecimal(3),
							InmuebleId = reader.GetInt32(4),
							InquilinoId = reader.GetInt32(5),

							Inmueble = new Inmueble
							{
								Id = reader.GetInt32(4),
								Direccion = reader.GetString(6),
								Precio = reader.GetDecimal(7),
							},

							Inquilino = new Inquilino
							{
								Id = reader.GetInt32(5),
								Nombre = reader.GetString(8),
								Apellido = reader.GetString(9)
							}
						};
					}
					connection.Close();
				}
			}
			return contrato;
		}
	}
}
