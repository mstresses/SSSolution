using Common;
using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Impl
{
    public class ClienteService : IClienteService
    {
        public void Insert(ClienteDTO cliente)
        {
            List<Error> errors = new List<Error>();

            #region VALIDAÇÃO NOME
            if (string.IsNullOrWhiteSpace(cliente.Nome))
            {
                errors.Add(new Error() { Message = "O nome deve ser informado.", FieldName = "Nome" });
            }
            else if (cliente.Nome.Length < 5 || cliente.Nome.Length > 40)
            {
                errors.Add(new Error() { Message = "O nome deve conter entre 5 e 40 caracteres.", FieldName = "Nome" });
            }
            #endregion

            #region VALIDAÇÃO CPF
            if (string.IsNullOrWhiteSpace(cliente.CPF))
            {
                errors.Add(new Error() { Message = "O CPF deve ser informado.", FieldName = "CPF" });
            }
            else if (cliente.CPF.Length != 14)
            {
                errors.Add(new Error() { Message = "O CPF deve conter 14 caracteres.", FieldName = "CPF" });
            }
            #endregion

            #region VALIDAÇÃO EMAIL
            if (string.IsNullOrWhiteSpace(cliente.Email))
            {
                errors.Add(new Error() { Message = "O email deve ser informado.", FieldName = "Email" });
            }
            else if (true)
            {

            }
            #endregion

            //Após validar todos os campos, verifique se possuímos erros.
            if (errors.Count > 0)
            {
                throw new NecoException(errors);
            }

            try
            {
                using (SSContext context = new SSContext())
                {
                    context.Clientes.Add(cliente);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate do administrador.");
            }
        }
    }
}