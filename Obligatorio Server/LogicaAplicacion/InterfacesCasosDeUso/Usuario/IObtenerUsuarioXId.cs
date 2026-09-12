using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.InterfacesCasosDeUso.Usuario
{
    public interface IObtenerUsuarioXId
    {
        UsuarioDTO Ejecutar(int id);
    }
}
