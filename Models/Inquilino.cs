using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GHAInmobiliaria.Models
{
    public class Inquilino
    {
		[Key]
		[Display(Name = "Código")]
		public int Id { get; set; }
		[Required]
		public string Nombre { get; set; }
		[Required]
		public string Apellido { get; set; }
		[Required]
		public string Dni { get; set; }
		[Required]
		public string Telefono { get; set; }
		[Required, EmailAddress]
		public string Email { get; set; }
		[Required]
		public string DireccionTrabajo { get; set; }
		[Required]
        public string DniGarante { get; set; }
		[Required]
        public string NombreGarante { get; set; }
		[Required]
        public string  ApellidoGarante { get; set; }

		public override string ToString()
        {
			return $"{Nombre} {Apellido}, ({Id})";
        }
    }

}
