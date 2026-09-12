using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosDeUso.Usuario;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.Usuario
{
    public class LoginCU : ILogin
    {
        private IRepositorioUsuario _repositorio;

        public LoginCU(IRepositorioUsuario repo)
        {
            _repositorio = repo;
        }

        public UsuarioDTO Login(string username, string password)
        {
            LogicaNegocio.Entidades.Usuario usuario = _repositorio.Login(username, password);
            if (usuario == null)
            {
                throw new UsuarioException("Credenciales invalidas.");
            }
            return UsuarioMapper.ToDTO(usuario);
        }
    }
}
