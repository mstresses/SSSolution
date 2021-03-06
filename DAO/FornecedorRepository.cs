﻿using DAO.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class FornecedorRepository : IFornecedorRepository
    {
        public async Task Create(FornecedorDTO fornecedor)
        {
            try
            {
                using (var context = new SSContext())
                {
                    context.Fornecedores.Add(fornecedor);
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

        public async Task<List<FornecedorDTO>> GetSuppliers()
        {
            using (var context = new SSContext())
            {
                return await context.Fornecedores.ToListAsync();
            }
        }
    }
}