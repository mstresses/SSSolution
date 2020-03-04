using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Mappings
{
    internal class UsuarioMapConfig : EntityTypeConfiguration<UsuarioDTO>
    {
        public UsuarioMapConfig()
        {
            this.ToTable("USUARIOS");

            this.Property(u => u.Email).HasMaxLength(100);
            this.HasIndex(u => u.Email).IsUnique(true).HasName("UQ_USUARIO_EMAIL");

            this.Property(u => u.Senha).IsRequired().HasMaxLength(20);
        }
    }
}
