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
    public class ProdutoService : BaseService, IProdutoService
    {
        //QUERY STRING: são as variáveis que ficam dentro da URL. Sempre com interrogação na frente. Sempre utilizada para pesquisas.
        public async Task Insert(ProdutoDTO produto)
        {
            //List<Error> errors = new List<Error>();

            #region VALIDAÇÃO DESCRIÇÃO PRODUTO
            if (string.IsNullOrWhiteSpace(produto.Descricao))
            {
                base.AddError("A descrição do produto deve ser informada.", "Descricao");
                //errors.Add(new Error() { Message = "A descrição do produto deve ser informada.", FieldName = "Descricao" });
            }
            else
            {
                produto.Descricao = produto.Descricao.Trim();
                if (produto.Descricao.Length < 5 || produto.Descricao.Length > 40)
                {
                    base.AddError("A descrição do produto deve conter entre 5 e 40 caracteres.", "Descricao");
                    //errors.Add(new Error() { Message = "A descrição do produto deve conter entre 5 e 40 caracteres.", FieldName = "Descricao" });
                }
            }
            #endregion

            #region VALIDAÇÃO PREÇO
            if (produto.Preco <= 0)
            {
                base.AddError("O preço deve ser informado.", "Preco");
                //errors.Add(new Error() { Message = "O preço deve ser informado.", FieldName = "Preco" });
            }
            #endregion

            #region VERIFICAÇÃO DE ERROS
            ////Após validar todos os campos, verifique se possuímos erros.
            //if (errors.Count > 0)
            //{
            //    throw new NecoException(errors);
            //}

            base.CheckErrors();
            try
            {
                //é como descartar
                using (SSContext context = new SSContext())
                {
                    context.Produtos.Add(produto);
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

        public async Task Update(ProdutoDTO produto)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(ProdutoDTO produto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProdutoDTO>> GetProducts(int page, int size)
        {
            try
            {
                using (SSContext context = new SSContext())
                {
                    return await context.Produtos.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate do administrador.");
            }
        }

        public async Task<ProdutoDTO> GetProductByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}