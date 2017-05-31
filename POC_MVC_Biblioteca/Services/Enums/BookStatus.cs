using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.Services
{
    public enum BookStatus
    {
        [Description("Disponível")]
        Available = 1,
        [Description("Locado")]
        Located,
        [Description("Em posse do usuário")]
        OnReaderHands,
        [Description("Reservado")]
        Reserved,
        [Description("Em atraso")]
        Overdue
    }
}