using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace ViaCep.Tests
{ 
    public class EndecoTests
    {
        /// <summary>
        /// Valida a pesquisa por endereço completo.
        /// </summary>
        [Fact]
        public void ValidaEnderecoCompleto()
        {
            var fixtureResults = ResultadoTest.GetSampleResults();
            var clientMock = new Mock<IViaCepCliente>();
            clientMock.Setup(c => c.Search(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(fixtureResults);

            var results = clientMock.Object.Search(
                fixtureResults.First().Estado,
                fixtureResults.First().Cidade,
                fixtureResults.First().Rua
            );
            Assert.NotNull(results);

            var list = results.ToList();
            Assert.True(list.Any());
            Assert.Contains(
                list,
                r => r.Cep.Equals("12345-678", StringComparison.InvariantCultureIgnoreCase)
            );
            Assert.Contains(
                list,
                r => r.Cep.Equals("98765-432", StringComparison.InvariantCultureIgnoreCase)
            );
        }

        /// <summary>
        /// Valida a pesquisa por endereço completo.
        /// </summary>
        [Fact]
        public async Task ValidaEnderecoCompletoAsync()
        {
            var fixtureResults = ResultadoTest.GetSampleResults();
            var clientMock = new Mock<IViaCepCliente>();
            clientMock
                .Setup(c =>
                    c.SearchAsync(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(fixtureResults);

            var results = await clientMock.Object.SearchAsync(
                fixtureResults.First().Estado,
                fixtureResults.First().Cidade,
                fixtureResults.First().Rua,
                CancellationToken.None
            );
            Assert.NotNull(results);

            var list = results.ToList();
            Assert.True(list.Any());
            Assert.Contains(
                list,
                r => r.Cep.Equals("12345-678", StringComparison.InvariantCultureIgnoreCase)
            );
            Assert.Contains(
                list,
                r => r.Cep.Equals("98765-432", StringComparison.InvariantCultureIgnoreCase)
            );
        }
    }
}
