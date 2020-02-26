using BLL.Interfaces;
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
    public class ProdutoService : IProdutoService
    {
        public void Insert(ProdutoDTO produto)
        {
            List<Error> errors = new List<Error>();

            #region VALIDAÇÃO DESCRIÇÃO PRODUTO
            if (string.IsNullOrWhiteSpace(produto.Descricao))
            {
                errors.Add(new Error() { Message = "A descrição do produto deve ser informada.", FieldName = "Descricao" });
            }
            else if (produto.Descricao.Length < 5 || produto.Descricao.Length > 40)
            {
                errors.Add(new Error() { Message = "A descrição do produto deve conter entre 5 e 40 caracteres.", FieldName = "Descricao" });
            }
            #endregion

            #region VALIDAÇÃO COR
            if (string.IsNullOrWhiteSpace(produto.Cor))
            {
                errors.Add(new Error() { Message = "A cor deve ser informada.", FieldName = "Cor" });
            }
            #endregion

            #region VALIDAÇÃO PREÇO
            if (produto.Preco == 0)
            {
                errors.Add(new Error() { Message = "O preço deve ser informado.", FieldName = "Preco" });
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
                    context.Produtos.Add(produto);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate do administrador.");
            }
        }

        public List<ProdutoDTO> GetData()
        {
            try
            {
                using (SSContext context = new SSContext())
                {
                    List<ProdutoDTO> produtos = context.Produtos.ToList(); //igual return context.Produtos.ToList();
                    return produtos;
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