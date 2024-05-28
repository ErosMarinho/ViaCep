using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ViaCep
{
    public class ViaCepCliente : IViaCepCliente
    {
        #region Private fields

        /// <summary>
        /// O URL base
        /// </summary>
        private const string _baseUrl = "https://viacep.com.br";

        /// <summary>
        /// O cliente HTTP
        /// </summary>
        private readonly HttpClient _httpClient;

        #endregion

        #region ~Ctors

        /// <summary>
        /// Inicializa uma nova instância do <see cref="ViaCepCliente"/> class.
        /// </summary>
        public ViaCepCliente()
        {
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        /// <summary>
        /// Inicializa uma nova instância do <see cref="ViaCepCliente"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        public ViaCepCliente(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Pesquisa o CEP especificado.
        /// </summary>
        /// <param name="zipCode">The zip code.</param>
        /// <returns></returns>
        public ViaCepResultado Search(string codigoCep)
        {
            return SearchAsync(codigoCep, CancellationToken.None).Result;
        }

        /// <summary>
        /// Pesquisa as iniciais de estado especificadas.
        /// </summary>
        /// <param name="inicialEstado">A inciais do Estado.</param>
        /// <param name="cidade">A cidade.</param>
        /// <param name="endereco">O endereco.</param>
        /// <returns></returns>
        public IEnumerable<ViaCepResultado> Search(string inicialEstado, string cidade, string endereco)
        {
            return SearchAsync(inicialEstado, cidade, endereco, CancellationToken.None).Result;
        }

        /// <summary>
        /// Pesquisa o assíncrono.
        /// </summary>
        /// <param name="codigoCep">O codigo do Cep.</param>
        /// <param name="cancelamentoToken">P token.</param>
        /// <returns></returns>
        public async Task<ViaCepResultado> SearchAsync(
            string codigoCep,
            CancellationToken cancelamentoToken
        )
        {
            var response = await _httpClient
                .GetAsync($"/ws/{codigoCep}/json", cancelamentoToken)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response
                .Content.ReadAsAsync<ViaCepResultado>(cancelamentoToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Pesquisa o assíncrono.
        /// </summary>
        /// <param name="inicialEstado">A inciais do Estado.</param>
        /// <param name="cidade">A cidade.</param>
        /// <param name="endereco">O endereco.</param>
        /// <param name="cancelamentoToken">O cancelamento Token.</param>
        /// <returns></returns>
        public async Task<IEnumerable<ViaCepResultado>> SearchAsync(
            string inicialEstado,
            string cidade,
            string endereco,
            CancellationToken cancelamentoToken
        )
        {
            var response = await _httpClient
                .GetAsync($"/ws/{inicialEstado}/{cidade}/{endereco}/json", cancelamentoToken)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response
                .Content.ReadAsAsync<List<ViaCepResultado>>(cancelamentoToken)
                .ConfigureAwait(false);
        }

        #endregion
    }
}
