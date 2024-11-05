using IFSPStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static IFSPStore.Test.UnitTestRepository;

namespace IFSPStore.Test
{
    [TestClass]
    public class UnitTestRepository
    {
        public partial class MyDbContext : DbContext
        {
            public DbSet<Usuario> Usuarios { get; set; }
            public DbSet<Cidade> Cidades { get; set; }
            public DbSet<Cliente> Clientes { get; set; }
            public DbSet<Grupo> Grupos { get; set; }
            public DbSet<Produto> Produtos { get; set; }

            public DbSet<Venda> Vendas { get; set; }
            public DbSet<VendaItem> VendaItens { get; set; }

            public MyDbContext()
            {
                Database.EnsureCreated();
            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                var server = "localhost";
                var port = "3307";
                var database = "IFSPStore";
                var username = "root";
                var password = "";
                var strCon = $"Server={server}; Port={port}; Database={database}; " +
                $"Uid={username}; Pwd={password}";
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseMySql(strCon, ServerVersion.AutoDetect(strCon));
                }
            }
        }

        [TestMethod]
        public void TestInsertCidade()
        {
            using (var context = new MyDbContext())
            {
                var cidade = new Cidade
                {
                    Nome = "Birigui",
                    Estado = "SP"
                };
                context.Cidades.Add(cidade);

                cidade = new Cidade
                {
                    Nome = "Araçatuba",
                    Estado = "SP"
                };
                context.Cidades.Add(cidade);
                context.SaveChanges();
            }
        }
        [TestMethod]
        public void TestListCidades()
        {
            using (var context = new MyDbContext())
            {
                foreach (var cidade in context.Cidades)
                {
                    Console.WriteLine(JsonSerializer.Serialize(cidade));
                }
            }
        }
        [TestMethod]
        public void TestInsertClientes()
        {

            using (var context = new MyDbContext())
            {
                var cidade = context.Cidades.FirstOrDefault(c => c.Id == 1);
                var cliente = new Cliente
                {
                    Nome = "Guilherme Laureano",
                    Cidade = cidade,
                    Documento = "123.123.123.45",
                    Endereco = "Rua Doralice Boteon, 405",
                    Bairro = "Monte Libano"
                };
                context.Clientes.Add(cliente);

                cidade = context.Cidades.FirstOrDefault(c => c.Id == 2);
                cliente = new Cliente
                {
                    Nome = "Leivas Kaue",
                    Cidade = cidade,
                    Documento = "234.234.234.56",
                    Endereco = "Mario Gerald, 506",
                    Bairro = "Residencial Muita Agua"
                };
                context.Clientes.Add(cliente);
                context.SaveChanges();
            }
        }
        [TestMethod]
        public void TestInsertGrupos()
        {
            using (var context = new MyDbContext())
            {
                var grupo = new Grupo
                {
                    Nome = "Grupo 1"
                };
                context.Grupos.Add(grupo);

                grupo = new Grupo
                {
                    Nome = "Grupo2"
                };
                context.Grupos.Add(grupo);

                context.SaveChanges();
            }
        }

        [TestMethod]
        public void TestInsertProdutos()
        {
            using (var context = new MyDbContext())
            {
                var grupo = context.Grupos.FirstOrDefault(g => g.Id == 1);
                var produto = new Produto
                {
                    Nome = "Arroz",
                    Preco = 50,
                    Quantidade = 3,
                    DataCompra = DateTime.Today,
                    UnidadeVenda = "Kg",
                    Grupo = grupo
                };
                context.Produtos.Add(produto);

                grupo = context.Grupos.FirstOrDefault(g => g.Id == 2);
                produto = new Produto
                {
                    Nome = "Feijão",
                    Preco = 10,
                    Quantidade = 3,
                    DataCompra = DateTime.Today,
                    UnidadeVenda = "Kg",
                    Grupo = grupo
                };
                context.Produtos.Add(produto);

                context.SaveChanges();
            }
        }

        [TestMethod]
        public void TestInsertUsuarios()
        {
            using (var context = new MyDbContext())
            {
                var usuario = new Usuario
                {
                    Nome = "Guilherme",
                    Senha = "123",
                    Login = "Guilherme",
                    Email = "guitlaureano@gmail.com",
                    DataCadastro = DateTime.Today,
                    DataLogin = DateTime.Today,
                    Ativo = true
                };
                context.Usuarios.Add(usuario);

                usuario = new Usuario
                {
                    Nome = "Kauê",
                    Senha = "123",
                    Login = "Kauê",
                    Email = "extremodemolidor@gmail.com",
                    DataCadastro = DateTime.Today,
                    DataLogin = DateTime.Today,
                    Ativo = true
                };
                context.Usuarios.Add(usuario);

                context.SaveChanges();
            }
        }
        [TestMethod]
        public void TestInsertVenda()
        {
            using (var context = new MyDbContext())
            {
                var cliente = context.Clientes.FirstOrDefault(x => x.Id == 1);
                var usuario = context.Usuarios.FirstOrDefault(x => x.Id == 1);
                var P1 = context.Produtos.FirstOrDefault(x => x.Id == 1);
                var P2 = context.Produtos.FirstOrDefault(x => x.Id == 2);

                // Verificação de nulos
                if (cliente == null || usuario == null || P1 == null || P2 == null)
                {
                    throw new InvalidOperationException("Cliente, Usuario ou Produto não encontrado no banco de dados.");
                }

                var venda = new Venda
                {
                    Data = DateTime.Today,
                    Usuario = usuario,
                    Cliente = cliente
                };

                var Lista = new List<VendaItem>
        {
            new VendaItem
            {
                Produto = P1,
                Quantidade = 3,
                ValorUnitario = (float)P1.Preco,
                ValorTotal = (float)(3 * P1.Preco),
                Venda = venda
            },
            new VendaItem
            {
                Produto = P2,
                Quantidade = 7,
                ValorUnitario = (float)P2.Preco,
                ValorTotal = (float)(7 * P2.Preco),
                Venda = venda
            }
        };

                // Calcula o valor total
                float VT = Lista.Sum(v => v.ValorTotal);

                venda.Items = Lista;
                venda.ValorTotal = VT;

                context.Vendas.Add(venda);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void TestListVenda()
        {
            using (var context = new MyDbContext())
            {
                foreach (var venda in context.Vendas)
                {
                    Console.WriteLine(JsonSerializer.Serialize(venda));
                }
            }
        }
    }
}
    



