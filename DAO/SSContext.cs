﻿using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class SSContext : DbContext
    {
        public SSContext():base(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\900192\Desktop\SSNeco.mdf;Integrated Security=True;Connect Timeout=30")
        {
        }

        public DbSet<ClienteDTO> Clientes { get; set; }
        public DbSet<ProdutoDTO> Produtos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Properties()
                        .Where(c => c.PropertyType == typeof(string))
                        .Configure(c => c.IsRequired().IsUnicode(false));
             
            base.OnModelCreating(modelBuilder);
        }
    }
}