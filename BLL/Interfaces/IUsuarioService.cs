using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUsuarioService
    {
        Task Insert(UsuarioDTO cliente);

        Task<UsuarioDTO> Authenticate(string email, string password);

        Task<List<UsuarioDTO>> GetUsers();
    }
}