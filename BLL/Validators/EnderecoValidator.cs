using Common;
using DTO.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    internal class EnderecoValidator //Tentar faze com AbstractValidator
    {
        public List<Error> Validate(Endereco endereco)
        {
            List<Error> errors = new List<Error>();
            if (string.IsNullOrWhiteSpace(endereco.Bairro))
            {
                errors.Add(new Error() { FieldName = "Bairro", Message = "Bairro deve ser informado." });
            }
            return errors;
        }
    }
}