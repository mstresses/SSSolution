using DAO.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public async Task<UsuarioDTO> Authenticate(string email, string password)
        {
            using (var ctx = new SSContext())
            {
                UsuarioDTO user = await ctx.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Senha == password);
                //PESQUISAR ConfigureAwait(false); *IMPORTANTE*

                if (user == null)
                {
                    throw new Exception("Email e/ou senha inválidos.");
                }
                return user;
            }
        }

        public async Task Create(UsuarioDTO usuario)
        {
            try
            {
                using (var context = new SSContext())
                {
                    context.Usuarios.Add(usuario);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException.Message.Contains("EMAIL"))
                {
                    throw new Exception("O email já foi cadastrado.");
                }
                throw new Exception();
            }
        }

        public async Task<List<UsuarioDTO>> GetUsers()
        {
            using (var context = new SSContext())
            {
                return await context.Usuarios.ToListAsync();
            }
        }
    }
}