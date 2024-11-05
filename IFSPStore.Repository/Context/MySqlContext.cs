using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFSPStore.Domain.Entities;

namespace IFSPStore.Repository.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) :
        base(options)
        {
            Database.EnsureCreated();
        }
    
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Cidade> Cidades { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Grupo> Grupos { get; set; }
    public DbSet<Produto> Produtos { get; set; }

    public DbSet<Venda> Vendas { get; set; }
    public DbSet<VendaItem> VendaItens { get; set; }
}
}
