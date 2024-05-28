using Xunit;

namespace ViaCep.IntegrationTests
{
    public class CepIntTests : IntegracaoTest
    {
        [Fact]
        public async Task PesquisaCEPValidoRetornaResultadoEsperado()
        {
            //Arrange
            var validaCep = "01001-000";

            //Act
            var result = await Client.SearchAsync(validaCep, default);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(validaCep, result.Cep);
            Assert.NotNull(result.Cidade);
            Assert.NotNull(result.Estado);
        }

        [Fact]
        public async Task PesquisaCEPInvalidoGeraExcecaoSolicitacaoHTTP()
        {
            //Arrange
            var invalidoCep = "invalid";

            //Act and Assert
            await Assert.ThrowsAsync<HttpRequestException>(
                () => Client.SearchAsync(invalidoCep, default)
            );
        }

        [Fact]
        public async Task PesquisaTokenCanceladoLancaExcecaoTarefaCancelada()
        {
            //Arrange
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            //Act and Assert
            await Assert.ThrowsAsync<TaskCanceledException>(
                () => Client.SearchAsync("01001-000", cancellationTokenSource.Token)
            );
        }
    }
}
