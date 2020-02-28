using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IFornecedorService
    {
        Task Insert(FornecedorDTO fornecedor);
        Task Update(FornecedorDTO fornecedor);
        Task Delete(FornecedorDTO fornecedor);
        Task<List<FornecedorDTO>> GetSuppliers(int page, int size);
        Task<FornecedorDTO> GetSupplierByID(int id);
    }
}
