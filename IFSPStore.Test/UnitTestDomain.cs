using IFSPStore.Domain.Entities;
using System.Diagnostics;
using System.Text.Json;

namespace IFSPStore.Test
{
    [TestClass]
    public class UnitTestDomain
    {
        [TestMethod]
        public void TestCidade()
        {
            Cidade cidade = new Cidade(1, "Birigui", "SP");
            Debug.Write(JsonSerializer.Serialize(cidade));
            Assert.AreEqual(cidade.Nome, "Birigui");
            Assert.AreEqual(cidade.Estado, "SP");
        }

        [TestMethod]
        public void TestUsuario()
        {
            Usuario usuario = new Usuario(1, "Guilherme", "12345678", "Guizim", "guitlaureano@gmail.com", DateTime.Today, DateTime.Today, true);
            Debug.Write(JsonSerializer.Serialize(usuario));
            Assert.AreEqual(usuario.Nome, "Guilherme");
            Assert.AreEqual(usuario.Senha, "12345678");
            Assert.AreEqual(usuario.Login, "Guizim");
            Assert.AreEqual(usuario.Email, "guitlaureano@gmail.com");
            Assert.AreEqual(usuario.DataCadastro, DateTime.Today);
            Assert.AreEqual(usuario.DataLogin, DateTime.Today);
            Assert.AreEqual(usuario.Ativo, true);
        }

        [TestMethod]
        public void TestVenda()
        {

        }
    }
}