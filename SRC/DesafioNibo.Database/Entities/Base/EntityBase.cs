using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Desafio.Database.Entities.Base
{
    public class EntityBase
    {
        public Guid id { get; set; }
        public bool active { get; set; }
    }
}
