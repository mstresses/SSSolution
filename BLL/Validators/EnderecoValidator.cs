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

            if (string.IsNullOrWhiteSpace(endereco.Rua))
            {
                errors.Add(new Error() { FieldName = "Rua", Message = "Rua deve ser informada." });
            }
            else if (endereco.Rua.Length > 100)
            {
                errors.Add(new Error() { FieldName = "Rua", Message = "Rua deve conter no máximo 100 caracteres." });
            }
            else
            {
                endereco.Rua.Trim();
            }

            if (string.IsNullOrWhiteSpace(endereco.Numero))
            {
                errors.Add(new Error() { FieldName = "Numero", Message = "Número deve ser informado." });
            }

            if (endereco.Complemento.Length > 200)
            {
                errors.Add(new Error() { FieldName = "Complemento", Message = "Complemento deve ter no máximo 200 caracteres." });
            }

            if (string.IsNullOrWhiteSpace(endereco.Bairro))
            {
                errors.Add(new Error() { FieldName = "Bairro", Message = "Bairro deve ser informado." });
            }

            if (string.IsNullOrWhiteSpace(endereco.Cidade))
            {
                errors.Add(new Error() { FieldName = "Cidade", Message = "Cidade deve ser informado." });
            }

            if (string.IsNullOrWhiteSpace(endereco.UF))
            {
                errors.Add(new Error() { FieldName = "UF", Message = "UF deve ser informado." });
            }
            return errors;
        }
    }
}