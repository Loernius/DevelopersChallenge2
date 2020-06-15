using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Domain.Seletores.Base
{
    public enum OrderBy { ASC, DESC }

    public class SeletorBase
    {
        public SeletorBase()
        {
            
            this.Pagina = 1;
            this.RegistroPorPagina = 10;
        }

        public int Pagina { get; set; }

        public int RegistroPorPagina { get; set; }

        public string OrderBy { get; set; }

        public OrderBy OrderByOrder { get; set; }
    }
}
