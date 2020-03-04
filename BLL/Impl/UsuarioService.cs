using BLL.Interfaces;
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
    public class UsuarioService : BaseService, IUsuarioService
    {
        UsuarioRepository repository = new UsuarioRepository();

        public async Task Insert(UsuarioDTO usuario)
        {
            #region VALIDAÇÃO NOME
            if (string.IsNullOrWhiteSpace(usuario.Nome))
            {
                base.AddError("O nome deve ser informado.", "Nome");
                //errors.Add(new Error() { Message = "O nome deve ser informado.", FieldName = "Nome" });
            }
            else
            {
                usuario.Nome = usuario.Nome.Trim();
                if (usuario.Nome.Length < 5 || usuario.Nome.Length > 40)
                {
                    base.AddError("O nome deve conter entre 5 e 40 caracteres.", "Nome");
                    //errors.Add(new Error() { Message = "O nome deve conter entre 5 e 40 caracteres.", FieldName = "Nome" });
                }
            }
            #endregion

            #region VALIDAÇÃO EMAIL
            if (string.IsNullOrWhiteSpace(usuario.Email))
            {
                base.AddError("O email deve ser informado.", "Email");
            }
            else
            {
                usuario.Email = usuario.Email.Trim();
            }
            #endregion

            #region VALIDAÇÃO SENHA
            if (string.IsNullOrWhiteSpace(usuario.Senha))
            {
                base.AddError("A senha deve ser informada.", "Senha");
            }
            else
            {
                usuario.Senha = usuario.Senha.Trim();
                if (usuario.Senha.Length < 6 || usuario.Senha.Length > 40)
                {
                    base.AddError("A senha deve conter entre 6 e 40 caracteres.", "Senha");
                }
            }
            #endregion

            base.CheckErrors();

            #region VALIDAÇÃO ERROS
            try
            {
                UsuarioRepository repository = new UsuarioRepository();
                await repository.Create(usuario);
            }
            catch (Exception ex)
            {
                //Exemplo de curto circuito, se a primeira expressão for falsa o C# não chega nem a ler a segunda expressão. PERIGOSO!!!
                if (ex.InnerException != null && ex.InnerException.InnerException.Message.Contains("UQ"))
                {
                    List<Error> error = new List<Error>();
                    error.Add(new Error() { FieldName = "CNPJ", Message = "CNPJ já cadastrado!" });
                    throw new NecoException(error);
                }
                //Log.Write("Erro 666" + ex.StackTrace);
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate do administrador.");
            }
            #endregion
        }

        public async Task<List<UsuarioDTO>> GetUsers()
        {
            try
            {
                using (SSContext context = new SSContext())
                {
                    return await context.Usuarios.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate do administrador.");
            }
        }

        public async Task<UsuarioDTO> Authenticate(string email, string password)
        {
            return await repository.Authenticate(email, password);
        }
    }
}