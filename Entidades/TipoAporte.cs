using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GestionPersonas.Entidades
{
   public  class TipoAporte
    {
        [Key]
        public int TipoAporteId { get; set; }
        public string Descripcion { get; set; }
        public float BaseMonto { get; set; }
        public float MetaMonto { get; set; }
    }
}
