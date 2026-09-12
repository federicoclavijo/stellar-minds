using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF.Repositorios
{
    public class RepositorioObjetoCeleste : IRepositorioObjetoCeleste
    {
        private ObligatorioContext _context;
        public RepositorioObjetoCeleste(ObligatorioContext context)
        {
            _context = context;
        }

        public void Add(ObjetoCeleste aAgregar)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ObjetoCeleste> FindAll()
        {
            return _context.ObjetosCelestes.AsQueryable().ToList(); ;
        }

        public ObjetoCeleste FindById(int id)
        {
            ObjetoCeleste objeto = _context.ObjetosCelestes
                                    .Where(o => o.Id == id)
                                    .FirstOrDefault();
            return objeto;
        }

        public void Update(ObjetoCeleste aActualizar)
        {
            throw new NotImplementedException();
        }
    }
}
