﻿using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class SSContext : DbContext
    {
        public SSContext():base(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\900192\Desktop\SSdoCelo.mdf;Integrated Security=True;Connect Timeout=30")
        {
        }

        public DbSet<ClienteDTO> Clientes { get; set; }
        public DbSet<ProdutoDTO> Produtos { get; set; }
        public DbSet<FornecedorDTO> Fornecedores { get; set; }
        public DbSet<CategoriaDTO> Categorias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Remove a pluralização das tabelas no banco.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Aplica as configurações de formatação nas tabelas.
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Properties()
                        .Where(c => c.PropertyType == typeof(string))
                        .Configure(c => c.IsRequired().IsUnicode(false));
             
            base.OnModelCreating(modelBuilder);
        }
    }
}