using System.Collections.Generic;

namespace ViaCep.Tests
{   

    internal static class ResultadoTest
    {
        /// <summary>
        /// Obtém os resultados da amostra.
        /// </summary>
        /// <returns>ICollection&lt;ViaCepResult&gt;.</returns>
        public static ICollection<ViaCepResultado> GetSampleResults() =>
            new List<ViaCepResultado>
            {
                new ViaCepResultado
                {
                    Unidade = "Any",
                    Cidade = "São Paulo",
                    Complemento = "",
                    CodigoGIA = 1,
                    CodigoIBGE = 1,
                    Bairro = "Centro",
                    Estado = "SP",
                    Rua = "Rua Direita",
                    Cep = "12345-678"
                },
                new ViaCepResultado
                {
                    Unidade = "",
                    Cidade = "São Paulo",
                    Complemento = "",
                    CodigoGIA = null,
                    CodigoIBGE = 1,
                    Bairro = "Centro",
                    Estado = "SP",
                    Rua = "Rua Direita",
                    Cep = "45632-870"
                },
                new ViaCepResultado
                {
                    Unidade = "",
                    Cidade = "São Paulo",
                    Complemento = "",
                    CodigoGIA = 12,
                    CodigoIBGE = 123,
                    Bairro = "Centro",
                    Estado = "SP",
                    Rua = "Rua Direita",
                    Cep = "98765-432"
                }
            };
    }
}
