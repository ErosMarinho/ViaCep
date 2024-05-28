using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ViaCep
{
    /// <summary>
    /// A interface do cliente ViaCEP.
    /// </summary>
    public interface IViaCepCliente
    {
        /// <summary>
        /// Pesquisa o CEP especificado.
        /// </summary>
        /// <param name="codigoCep">O código postal.</param>
        /// <returns></returns>
        ViaCepResultado Search(string codigoCep);

        /// <summary>
        /// Searches the specified address by state initials (UF), city and address name.
        /// </summary>
        /// <param name="inicialEstado">As iniciais do estado.</param>
        /// <param name="cidade">A cidade.</param>
        /// <param name="endereco">O endereco.</param>
        /// <returns></returns>
        IEnumerable<ViaCepResultado> Search(string inicialEstado, string cidade, string endereco);

        /// <summary>
        /// Pesquisa o CEP especificado de forma assíncrona.
        /// </summary>
        /// <param name="codigoCep">O codigo Cep.</param>
        /// <param name="cancelamentoToken">O token de cancelamento.</param>
        /// <returns></returns>
        Task<ViaCepResultado> SearchAsync(string codigoCep, CancellationToken cancellationToken);

        /// <summary>
        /// Pesquisa o endereço especificado por iniciais de estado (UF), cidade e nome do endereço de forma assíncrona.
        /// </summary>
        /// <param name="inicialEstado">A iniciais do Estado.</param>
        /// <param name="cidade">A cidade.</param>
        /// <param name="endereco">O endereco.</param>
        /// <param name="cancelamentoToken">Cancelamento Token.</param>
        /// <returns></returns>
        Task<IEnumerable<ViaCepResultado>> SearchAsync(
            string inicialEstado,
            string cidade,
            string endereco,
            CancellationToken cancelamentoToken
        );
    }
}
