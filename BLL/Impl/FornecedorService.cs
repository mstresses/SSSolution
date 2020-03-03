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
using Common.Extensions;
using DAO.Interfaces;

namespace BLL.Impl
{
    public class FornecedorService : BaseService, IFornecedorService //BASESERVICE -> NA AMBEV SE UTILIZA *ABSTRACT VALIDATOR*
    {
        public async Task Create(FornecedorDTO fornecedor)
        {
            #region VALIDAÇÃO NOME FORNECEDOR
            if (string.IsNullOrWhiteSpace(fornecedor.Fornecedor))
            {
                base.AddError("O nome do fornecedor deve ser informado.", "Fornecedor");
            }
            else
            {
                fornecedor.Fornecedor = fornecedor.Fornecedor.Trim();
                if (fornecedor.Fornecedor.Length < 5 || fornecedor.Fornecedor.Length > 40)
                {
                    base.AddError("O nome do fornecedor deve conter entre 5 e 40 caracteres.", "Descricao");
                }
            }
            #endregion

            #region VALIDAÇÃO CNPJ

            string resposta = fornecedor.CNPJ.IsValidCnpj();
            if (resposta != "") base.AddError("CNPJ", resposta); //IF EM UMA ÚNICA LINHA (SEM CHAVES)

            #endregion

            #region VALIDAÇÃO EMAIL
            if (string.IsNullOrWhiteSpace(fornecedor.Email))
            {
                base.AddError("O email deve ser informado.", "Email");
            }
            else
            {
                fornecedor.Email = fornecedor.Email.Trim();
            }
            #endregion

            #region VALIDAÇÃO ENDEREÇO
            List<Error> errors = new EnderecoValidator().Validate(fornecedor.Endereco);
            foreach (var erroDoEndereco in errors) //ADICIONA OS ERROS DO ENDEREÇO DO FORNECEDOR (CASO ENCONTRE)
            {
                base.AddError(erroDoEndereco.FieldName, erroDoEndereco.Message);
            }
            #endregion
            
            #region VERIFICAÇÃO DE ERROS
            base.CheckErrors();
            try
            {
                FornecedorRepository repository = new FornecedorRepository();
                await repository.Create(fornecedor);
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
        public async Task<List<FornecedorDTO>> GetSuppliers(int page, int size)
        {
            try
            {
                using (SSContext context = new SSContext())
                {
                    return await context.Fornecedores.ToListAsync();
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