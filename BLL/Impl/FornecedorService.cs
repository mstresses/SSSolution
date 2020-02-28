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
    class FornecedorService : BaseService, IFornecedorService
    {
        public async Task Insert(FornecedorDTO fornecedor)
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
            if (string.IsNullOrWhiteSpace(fornecedor.CNPJ))
            {
                base.AddError("O CNPJ deve ser informado.", "CNPJ");
            }
            else
            {
                fornecedor.CNPJ = fornecedor.CNPJ.Trim();
                if (fornecedor.CNPJ.Length != 18)
                {
                    base.AddError("O CNPJ deve conter 18 caracteres.", "CNPJ");
                }
            }
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

            #region VERIFICAÇÃO DE ERROS
            base.CheckErrors();
            try
            {
                using (SSContext context = new SSContext())
                {
                    context.Fornecedores.Add(fornecedor);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate do administrador.");
            }
            #endregion
        }

        public async Task Update(FornecedorDTO fornecedor)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(FornecedorDTO fornecedor)
        {
            throw new NotImplementedException();
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

        public async Task<FornecedorDTO> GetSupplierByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
