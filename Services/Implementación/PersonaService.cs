using Microsoft.EntityFrameworkCore;
using BackEndAPI.Modelos;
using BackEndAPI.Services.Contrato;

namespace BackEndAPI.Services.Implementación
{
     public class PersonaService :IPersonaService
    {
        private MasterContext _dbContext;

        public PersonaService(MasterContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Persona>> GetList()
        {
            try { 
            List<Persona> lista = new List<Persona>(); 
                lista = await _dbContext.Personas.ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
