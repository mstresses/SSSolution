using Common;
using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Impl
{
    public class ClienteService : BaseService, IClienteService
    {
        public async Task Insert(ClienteDTO cliente)
        {
            #region VALIDAÇÃO NOME
            if (string.IsNullOrWhiteSpace(cliente.Nome))
            {
                base.AddError("O nome deve ser informado.", "Nome");
                //errors.Add(new Error() { Message = "O nome deve ser informado.", FieldName = "Nome" });
            }
            else 
            {
                cliente.Nome = cliente.Nome.Trim();
                if (cliente.Nome.Length < 5 || cliente.Nome.Length > 40)
                {
                    base.AddError("O nome deve conter entre 5 e 40 caracteres.", "Nome");
                    //errors.Add(new Error() { Message = "O nome deve conter entre 5 e 40 caracteres.", FieldName = "Nome" });
                }
            }
            #endregion

            #region VALIDAÇÃO CPF
            if (string.IsNullOrWhiteSpace(cliente.CPF))
            {
                base.AddError("O CPF deve ser informado.", "CPF");
            }
            else
            {
                cliente.CPF = cliente.CPF.Trim();
                if (cliente.CPF.Length != 14)
                {
                    base.AddError("O CPF deve conter 14 caracteres.", "CPF");
                }
            }
            #endregion

            #region VALIDAÇÃO EMAIL
            if (string.IsNullOrWhiteSpace(cliente.Email))
            {
                base.AddError("O email deve ser informado.", "Email");
            }
            else
            {
                cliente.Email = cliente.Email.Trim();
            }
            #endregion

            #region VALIDAÇÃO SENHA
            if (string.IsNullOrWhiteSpace(cliente.Senha))
            {
                base.AddError("A senha deve ser informada.", "Senha");
            }
            else 
            {
                cliente.Senha = cliente.Senha.Trim();
                if (cliente.Senha.Length < 6 || cliente.Senha.Length > 40)
                {
                    base.AddError("A senha deve conter entre 6 e 40 caracteres.", "Senha" );
                }
            }
            #endregion

            base.CheckErrors();

            #region VALIDAÇÃO ERROS
            try
            {
                //é como descartar
                using (SSContext context = new SSContext())
                {
                    context.Clientes.Add(cliente);
                    await context.SaveChangesAsync();
                }//context.Dispose();

            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate do administrador.");
            }
            #endregion
        }

        public async Task Update(ClienteDTO cliente)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(ClienteDTO cliente)
        {
            throw new NotImplementedException();
        }
       
        public async Task<List<ClienteDTO>> GetData()
        {
            try
            {
                using (SSContext context = new SSContext())
                {
                    //List<ClienteDTO> clientes = context.Clientes.ToListAsync();
                    //return clientes;

                    return await context.Clientes.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate do administrador.");
            }
        }

        public async Task<ClienteDTO> GetCostumerByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}