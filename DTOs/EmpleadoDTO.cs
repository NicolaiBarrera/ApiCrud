namespace BackEndAPI.DTOs
{
    public class EmpleadoDTO
    {
        public int EmpleadoId { get; set; }

        public int? PersonaId { get; set; }

        public string? NombrePersona { get; set; }

        public string? Cargo { get; set; }

        public string? trabajaActual { get; set; } //V2

        public string? FechaContrato { get; set; }
    }
}
