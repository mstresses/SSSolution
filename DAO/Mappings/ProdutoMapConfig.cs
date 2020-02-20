using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Mappings
{
    internal class ProdutoMapConfig : EntityTypeConfiguration<ProdutoDTO>
    {
        public ProdutoMapConfig()
        {
            this.ToTable("PRODUTOS");

            this.Property(p => p.Descricao).HasMaxLength(60);

            this.Property(p => p.Cor).HasMaxLength(30);

            this.Property(p => p.Preco).IsRequired();
        }
    }
}