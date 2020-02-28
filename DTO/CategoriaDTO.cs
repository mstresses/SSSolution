﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CategoriaDTO
    {
        public int ID { get; set; }
        public string Categoria { get; set; }

        public virtual ICollection<ProdutoDTO> Produtos { get; set; }

        public CategoriaDTO()
        {
            this.Produtos = new List<ProdutoDTO>();
        }
    }
}