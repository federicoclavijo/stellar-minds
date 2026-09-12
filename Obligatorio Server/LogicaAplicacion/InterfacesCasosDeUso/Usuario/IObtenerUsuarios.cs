using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.InterfacesCasosDeUso.Usuario
{
    public interface IObtenerUsuarios
    {
        public List<UsuarioDTO> Ejecutar();
    }
}
