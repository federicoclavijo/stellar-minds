using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LogicaNegocio.Enums
{
    public enum EstadoPrestamo
    {
        [Display(Name = "En préstamo")]
        EN_PRESTAMO,
        [Display(Name = "Devuelto")]
        DEVUELTO
    }
}
    