using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Mappings
{
    internal class ProdutoMapConfig : EntityTypeConfiguration<ProdutoDTO> //O ProdutoDTO é GENERICS, sempre entre <>
    {
        public ProdutoMapConfig()
        {
            this.ToTable("PRODUTOS");

            //Já foi configurado nas configurações globais para unicode e required.
            this.Property(p => p.Descricao).HasMaxLength(60);

            //Preço e enum já é required por padrão :)
        }
    }
}