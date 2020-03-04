using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interfaces
{
    public interface IUsuarioRepository
    {
        Task Create(UsuarioDTO usuario);
        Task<UsuarioDTO> Authenticate(string email, string password);
        Task<List<UsuarioDTO>> GetUsers();
    }
}
