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
    public class CategoriaService : BaseService, ICategoriaService
    {
        public async Task Insert(CategoriaDTO categoria)
        {
            #region VALIDAÇÃO NOME
            if (string.IsNullOrWhiteSpace(categoria.Categoria))
            {
                base.AddError("A categoria deve ser informada.", "Categoria");
            }
            else
            {
                categoria.Categoria = categoria.Categoria.Trim();
                if (categoria.Categoria.Length < 5 || categoria.Categoria.Length > 40)
                {
                    base.AddError("O nome da categoria deve conter entre 5 e 40 caracteres.", "Categoria");
                }
            }
            #endregion

            base.CheckErrors();

            //MODELO DE PREVENÇÃO 
            #region VALIDAÇÃO ERROS
            try
            {
                using (SSContext context = new SSContext())
                {
                    //SELECT NO BANCO (busca registros com o mesmo nome)
                    CategoriaDTO categoriaJaCadastrada = await context.Categorias.FirstOrDefaultAsync(c => c.Categoria == categoria.Categoria); 
                    if (categoriaJaCadastrada != null)
                    {
                        //SE ENTROU AQUI, CATEGORIA JÁ CADASTRADA. NÃO VAI RODAR O SAVECHANGES.
                        throw new NecoException("Essa categoria já foi cadastrada!");
                    }
                    
                    context.Categorias.Add(categoria);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (ex is NecoException)
                {
                    throw ex;
                }
                File.WriteAllText("log.txt", ex.Message + " " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate o administrador.");
            }
            #endregion
        }

        public async Task Update(CategoriaDTO categoria)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(CategoriaDTO categoria)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoriaDTO>> GetCategories()
        {
            try
            {
                using (SSContext context = new SSContext())
                {
                    //return await context.Categorias.Skip(page*size).Take(size).ToListAsync();
                    return await context.Categorias.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate do administrador.");
            }
        }

        public async Task<CategoriaDTO> GetCategorieByID(int id)
        {
            try
            {
                using (SSContext context = new SSContext())
                {
                    return await context.Categorias.FindAsync();
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