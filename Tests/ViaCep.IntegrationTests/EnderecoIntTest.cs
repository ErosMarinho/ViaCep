using Xunit;

namespace ViaCep.IntegrationTests
{
    public class EnderecoIntTest : IntegracaoTest
    {
        [Fact]
        public async Task PesquisaEnderecoValidoRetornaResultadoEsperado()
        {
            //Arrange
            var validaEstado = "SP";
            var validaCidade = "São Paulo";
            var validaEndereco = "Rua Direita";
            var validaCep = "01002-902"; //Corresponde à cidade e estado acima
            var validaSegundoCep = "01002-000"; //Outro CEP válido para a mesma cidade e estado

            //Act
            var results = (
                await Client.SearchAsync(validaEstado, validaCidade, validaEndereco, default)
            ).ToList();

            //Assert
            Assert.NotNull(results);
            Assert.Contains(results, r => r.Cep == validaCep);
            Assert.Contains(results, r => r.Cep == validaSegundoCep);
        }

        [Fact]
        public async Task PesquisaEndereçoInexistenteRetornaResultadosVazios()
        {
            //Arrange
            var enderecoNaoExiste = "Endereco não existe";
            var cidadeNaoExiste = "Cidade não existe";

            //Act
            var results = await Client.SearchAsync(
                "XX",
                cidadeNaoExiste,
                enderecoNaoExiste,
                default
            );

            //Assert
            Assert.Empty(results);
        }

        [Fact]
        public async Task PesquisaTokenCanceladoLancaExcecaoTarefaCancelada()
        {
            //Arrange
            var validaEstado = "SP";
            var validaCidade = "São Paulo";
            var validaEndereco = "Rua Direita";
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            //Act and Assert
            await Assert.ThrowsAsync<TaskCanceledException>(
                () =>
                    Client.SearchAsync(
                        validaEstado,
                        validaCidade,
                        validaEndereco,
                        cancellationTokenSource.Token
                    )
            );
        }
    }
}
