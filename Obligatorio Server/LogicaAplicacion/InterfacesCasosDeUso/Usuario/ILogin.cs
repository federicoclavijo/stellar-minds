using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.Usuario
{
    public interface ILogin
    {
        public UsuarioDTO Login(string username, string password);
    }
}
