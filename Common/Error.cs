using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Error
    {
        public string Message { get; set; } //Mensagem de erro.
        public string FieldName { get; set; } //O nome da propriedade que está errada.
    }
}
