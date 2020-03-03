using DAO.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class FornecedorRepository : IFornecedorRepository
    {
        public async Task Create(FornecedorDTO fornecedor)
        {
            using (var context = new SSContext())
            {
                context.Fornecedores.Add(fornecedor);
                await context.SaveChangesAsync();
            }
        }

        public Task<List<FornecedorDTO>> GetSuppliers()
        {
            using (var context = new SSContext())
            {
                await context.Fornecedores.ToListAsync();
            }
        }
    }
}