using BackEndAPI.Modelos;

namespace BackEndAPI.Services.Contrato
{
    public interface IPersonaService
    {
        Task<List<Persona>> GetList();
    }
}
