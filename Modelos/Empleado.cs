using BackEndAPI.Modelos;
using System;
using System.Collections.Generic;

namespace BackEndAPI.Modelos;

public partial class Empleado
{
    public int EmpleadoId { get; set; }

    public int? PersonaId { get; set; }

    public string? Cargo { get; set; }

    public string? trabajaActual { get; set; } //V2

    public DateTime? FechaContrato { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Persona? Persona { get; set; }
}
