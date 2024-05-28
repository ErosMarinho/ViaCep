using Newtonsoft.Json;

namespace ViaCep
{
    public sealed class ViaCepResultado
    {
        /// <summary>
        /// Obtém ou define o CEP.
        /// </summary>
        /// <value>
        /// O código postal.
        /// </value>
        [JsonProperty("cep")]
        public string Cep { get; set; }

        /// <summary>
        /// Obtém ou define a rua.
        /// </summary>
        /// <value>
        /// A rua.
        /// </value>
        [JsonProperty("logradouro")]
        public string Rua { get; set; }

        /// <summary>
        /// Obtém ou define o complemento.
        /// </summary>
        /// <value>
        /// O complemento.
        /// </value>
        [JsonProperty("complemento")]
        public string Complemento { get; set; }

        /// <summary>
        /// Obtém ou define o bairro.
        /// </summary>
        /// <value>
        /// O bairro.
        /// </value>
        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        /// <summary>
        /// Obtém ou define a cidade.
        /// </summary>
        /// <value>
        /// A cidade
        /// </value>
        [JsonProperty("localidade")]
        public string Cidade { get; set; }

        /// <summary>
        /// Obtém ou define as iniciais do estado.
        /// </summary>
        /// <value>
        /// O estado
        /// </value>
        [JsonProperty("uf")]
        public string Estado { get; set; }

        /// <summary>
        /// Obtém ou define a unidade.
        /// </summary>
        /// <value>
        /// A unidade.
        /// </value>
        [JsonProperty("unidade")]
        public string Unidade { get; set; }

        /// <summary>
        /// Obtém ou define o código ibge.
        /// </summary>
        /// <value>
        /// O código ibge
        /// </value>
        [JsonProperty("ibge")]
        public int CodigoIBGE { get; set; }

        /// <summary>
        /// Obtém ou define o código gia.
        /// </summary>
        /// <value>
        /// Ó código gia
        /// </value>
        [JsonProperty("gia")]
        public int? CodigoGIA { get; set; }
    }
}
