using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class EscritosTexto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public DateTime Fecha { get; set; }
        public bool? Last { get; set; }
    }
}
