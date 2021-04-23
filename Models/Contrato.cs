using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GHAInmobiliaria.Models
{
    public class Contrato
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required]
        public DateTime FechaDesde { get; set; }

        [Required]
        public DateTime FechaHasta { get; set; }

        [Required]
        public decimal MontoMensual { get; set; }

        [Required]
        [Display(Name = "Direccion")]
        public int InmuebleId { get; set; }

        [ForeignKey(nameof(InmuebleId))]
        public Inmueble Inmueble { get; set; }

        [Required]
        [Display(Name = "Inquilino")]
        public int InquilinoId { get; set; }

        [ForeignKey(nameof(InquilinoId))]
        public Inquilino Inquilino { get; set; }


        
    }
}
