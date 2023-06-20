using BackEndAPI.Modelos;
using System;
using System.Collections.Generic;

namespace BackEndAPI.Modelos;

public partial class Persona
{
    public int PersonaId { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
