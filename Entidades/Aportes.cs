﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPersonas.Entidades
{
    public class Aportes
    {
        //AporteId,Fecha,PersonaId,Concepto, Monto
        [Key]
        public int AporteId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int PersonaId { get; set; }
        public string Concepto { get; set; }
        public float Monto { get; set; }

        [ForeignKey("TipoAporteId")]
        public TipoAporte TipoAportes { get; set; }

        [ForeignKey("AporteId")]
        public List<AporteDetalle> AporteDetalle { get; set; } = new List<AporteDetalle>();

    }
}
