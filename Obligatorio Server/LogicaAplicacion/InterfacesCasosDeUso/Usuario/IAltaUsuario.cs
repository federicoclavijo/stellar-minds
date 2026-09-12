using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.InterfacesCasosDeUso.Usuario
{
    public interface IAltaUsuario
    {
        public void Ejecutar(UsuarioDTO dto);
    }
}
