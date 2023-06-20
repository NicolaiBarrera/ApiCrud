using Microsoft.EntityFrameworkCore;
using BackEndAPI.Modelos;
using BackEndAPI.Services.Contrato;

namespace BackEndAPI.Services.Implementación
{
    public class EmpleadoService: IEmpleadoService
    {
        private MasterContext _dbContext;
        public EmpleadoService(MasterContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Empleado>> GetList()
        {
            try { 
                List<Empleado> lista = new List<Empleado>();
                lista = await _dbContext.Empleados.Include(dpt => dpt.Persona).ToListAsync(); // REFERENCIA HACIA LA LLAVE FORANEA
                return lista;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Empleado> Get(int idEmpleado)
        {
            try
            {
                Empleado? encontrado = new Empleado();
                encontrado = await _dbContext.Empleados.Include(dpt => dpt.Persona)
                    .Where (e => e.EmpleadoId == idEmpleado).FirstOrDefaultAsync();
                return encontrado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Empleado> Add(Empleado modelo)
        {
            try
            {
                _dbContext.Empleados.Add(modelo);
                await _dbContext.SaveChangesAsync();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Update(Empleado modelo)
        {
            try
            {
                _dbContext.Empleados.Update(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Delete(Empleado modelo)
        {
            try
            {
                _dbContext.Empleados.Remove(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
