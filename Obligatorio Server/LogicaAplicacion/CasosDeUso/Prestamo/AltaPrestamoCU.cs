using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosDeUso.Prestamo;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;

namespace LogicaAplicacion.CasosDeUso.Prestamo
{
    public class AltaPrestamoCU : IAltaPrestamo
    {
        private IRepositorioPrestamo _repoP;
        private IRepositorioEquipo _repoE;
        private IRepositorioUsuario _repoU;
        private IRepositorioAuditoria _repoA;

        public AltaPrestamoCU(IRepositorioPrestamo repoP, IRepositorioEquipo repoE, IRepositorioUsuario repoU, IRepositorioAuditoria repoA)
        {
            _repoP = repoP;
            _repoE = repoE;
            _repoU = repoU;
            _repoA = repoA;
        }
        public void Ejecutar(PrestamoDTO dto, int coordinadorId)
        {
            if (!dto.UsuarioId.HasValue) throw new PrestamoException("Debe seleccionar un socio.");
            if (!dto.TelescopioId.HasValue) throw new PrestamoException("Debe seleccionar un telescopio.");
            if (!dto.MonturaId.HasValue) throw new PrestamoException("Debe seleccionar una montura.");

            LogicaNegocio.Entidades.Prestamo entidad = PrestamoMapper.FromDto(dto);

            entidad.Telescopio = _repoE.FindById((int)entidad.TelescopioId) as Telescopio;
            entidad.Montura = _repoE.FindById((int)entidad.MonturaId) as Montura;
            entidad.Usuario = _repoU.FindById((int)entidad.UsuarioId);

            if (dto.CamaraId.HasValue) 
            { 
                entidad.Camara = _repoE.FindById((int)dto.CamaraId) as Camara; 
            }
            if (dto.OcularId.HasValue) 
            { 
                entidad.Ocular = _repoE.FindById((int)dto.OcularId) as Ocular;
            }
            entidad.Estado = EstadoPrestamo.EN_PRESTAMO;
            entidad.FechaInicio = DateTime.Now;
            entidad.Validar();
            entidad.Prestar();
            _repoP.Add(entidad);



            LogicaNegocio.Entidades.Usuario? coordinador = _repoU.FindById(coordinadorId);
            if (coordinador == null) throw new UsuarioException("No se encontró al coordinador.");

            LogicaNegocio.Entidades.Auditoria auditoria = new LogicaNegocio.Entidades.Auditoria();
            auditoria.Usuario = coordinador;
            auditoria.Prestamo = entidad;
            auditoria.Accion = TipoAccion.Alta;
            auditoria.Fecha = entidad.FechaInicio;
            auditoria.Validar();
            _repoA.Add(auditoria);
        }

    }
}