using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;


namespace ViaCep.Tests
{ 
    public class CepCodigoTest
    {
        /// <summary>
        /// Valida a pesquisa por CEP.
        /// </summary>
        [Fact]
        public void ValidaZipCode()
        {
            var fixtureResults = ResultadoTest.GetSampleResults();
            var clientMock = new Mock<IViaCepCliente>();
            clientMock.Setup(c => c.Search(It.IsAny<string>())).Returns(fixtureResults.First(r => r.Cep.Equals("12345-678")));

            var result = clientMock.Object.Search("12345678");
            Assert.NotNull(result);
            Assert.Equal("Any", result.Unidade);
            Assert.Equal("Rua Direita", result.Rua);
            Assert.Equal(string.Empty, result.Complemento);
            Assert.Equal(1, result.CodigoGIA);
            Assert.Equal(1, result.CodigoIBGE);
            Assert.Equal("Centro", result.Bairro);
            Assert.Equal("São Paulo", result.Cidade);
            Assert.Equal("SP", result.Estado);
        }

        /// <summary>
        /// Valida a busca por CEP.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ValidaCepAsync()
        {
            var fixtureResults = ResultadoTest.GetSampleResults();
            var clientMock = new Mock<IViaCepCliente>();
            clientMock.Setup(c => c.SearchAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(fixtureResults.First(r => r.Cep.Equals("12345-678")));

            var result = await clientMock.Object.SearchAsync("12345678", CancellationToken.None);
            Assert.NotNull(result);
            Assert.Equal("Any", result.Unidade);
            Assert.Equal("Rua Direita", result.Rua);
            Assert.Equal(string.Empty, result.Complemento);
            Assert.Equal(1, result.CodigoGIA);
            Assert.Equal(1, result.CodigoIBGE);
            Assert.Equal("Centro", result.Bairro);
            Assert.Equal("São Paulo", result.Cidade);
            Assert.Equal("SP", result.Estado);
        }

        /// <summary>
        /// Valida se a busca por CEP não gera exceção caso o endereço não possua código
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void ValidaCepSemCodigo()
        {
            var fixtureResults = ResultadoTest.GetSampleResults();
            var clientMock = new Mock<IViaCepCliente>();
            clientMock.Setup(c => c.Search(It.IsAny<string>())).Returns(fixtureResults.First(r => !r.CodigoGIA.HasValue));

            var result = clientMock.Object.Search("12345678");
            Assert.NotNull(result);
            Assert.Equal("", result.Unidade);
            Assert.Equal("Rua Direita", result.Rua);
            Assert.Equal(string.Empty, result.Complemento);
            Assert.Null(result.CodigoGIA);
            Assert.Equal(1, result.CodigoIBGE);
            Assert.Equal("Centro", result.Bairro);
            Assert.Equal("São Paulo", result.Cidade);
            Assert.Equal("SP", result.Estado);
        }

        /// <summary>
        /// Valida a busca por CEP sem código gia assíncrono.
        /// </summary>
        [Fact]
        public async Task ValidaCepSemCodigoeAsync()
        {
            var fixtureResults = ResultadoTest.GetSampleResults();
            var clientMock = new Mock<IViaCepCliente>();
            clientMock.Setup(c => c.SearchAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(fixtureResults.First(r => !r.CodigoGIA.HasValue));

            var result = await clientMock.Object.SearchAsync("12345678", CancellationToken.None);
            Assert.NotNull(result);
            Assert.Equal("", result.Unidade);
            Assert.Equal("Rua Direita", result.Rua);
            Assert.Equal(string.Empty, result.Complemento);
            Assert.Null(result.CodigoGIA);
            Assert.Equal(1, result.CodigoIBGE);
            Assert.Equal("São Paulo", result.Cidade);
            Assert.Equal("SP", result.Estado);
        }
    }
}
