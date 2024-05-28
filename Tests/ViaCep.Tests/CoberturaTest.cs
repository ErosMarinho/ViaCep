using System;
using System.Linq;
using System.Net.Http;
using RichardSzalay.MockHttp;
using Xunit;

namespace ViaCep.Tests
{
    public class CoberturaTest
    {
        /// <summary>
        /// O URL base
        /// </summary>
        private readonly Uri _baseUrl = new Uri("https://viacep.com.br/");

        /// <summary>
        /// Valida a criação de nova instância.
        /// </summary>
        [Fact]
        public void ValidaCriaNovaInstancia()
        {
            var client = new ViaCepCliente();
            Assert.NotNull(client);
        }

        /// <summary>
        /// Valida a pesquisa por cobertura de CEP.
        /// </summary>
        [Fact]
        public void ValidaCepCodigoCobertura()
        {
            const string resultData =
                "{'cep': '01001-000','logradouro': 'Praça da Sé','complemento': 'lado ímpar','bairro': 'Sé','localidade': 'São Paulo','uf': 'SP','unidade': '','ibge': '3550308','gia': '1004'}";
            var httpClientMock = new MockHttpMessageHandler();
            httpClientMock.When("https://viacep.com.br/ws/*/json").Respond("application/json", resultData);

            var httpClient = new HttpClient(httpClientMock) { BaseAddress = _baseUrl };
            var client = new ViaCepCliente(httpClient);

            var result = client.Search("01001000");
            Assert.NotNull(result);
            Assert.Equal("01001-000", result.Cep);
            Assert.Equal("Praça da Sé", result.Rua);
            Assert.Equal("lado ímpar", result.Complemento);
            Assert.Equal("Sé", result.Bairro);
            Assert.Equal("São Paulo", result.Cidade);
            Assert.Equal("SP", result.Estado);
            Assert.Equal(string.Empty, result.Unidade);
            Assert.Equal(3550308, result.CodigoIBGE);
            Assert.True(result.CodigoGIA.HasValue);
            Assert.Equal(1004, result.CodigoGIA);
        }

        /// <summary>
        /// Valida a pesquisa por cobertura de endereço.
        /// </summary>
        [Fact]
        public void ValidaCoberturaEndereco()
        {
            const string resultData =
                "[ { 'cep': '91790-072', 'logradouro': 'Rua Domingos José Poli', 'complemento': '', 'bairro': 'Restinga', 'localidade': 'Porto Alegre', 'uf': 'RS', 'unidade': '', 'ibge': '4314902', 'gia': '' }, { 'cep': '91910-420', 'logradouro': 'Rua José Domingos Varella', 'complemento': '', 'bairro': 'Cavalhada', 'localidade': 'Porto Alegre', 'uf': 'RS', 'unidade': '', 'ibge': '4314902', 'gia': '' }, { 'cep': '90420-200', 'logradouro': 'Rua Domingos José de Almeida', 'complemento': '', 'bairro': 'Rio Branco', 'localidade': 'Porto Alegre', 'uf': 'RS', 'unidade': '', 'ibge': '4314902', 'gia': '' } ]";
            var httpClientMock = new MockHttpMessageHandler();
            httpClientMock.When("https://viacep.com.br/ws/*/*/*/json").Respond("application/json", resultData);

            var httpClient = new HttpClient(httpClientMock) { BaseAddress = _baseUrl };
            var client = new ViaCepCliente(httpClient);

            var results = client.Search("RS", "Porto Alegre", "Domingos Jose");
            Assert.NotNull(results);

            var list = results.ToList();
            Assert.Equal(3, list.Count);
            Assert.All(list, result => Assert.False(result.CodigoGIA.HasValue));
            Assert.All(list, result => Assert.Empty(result.Complemento));
            Assert.All(list, result => Assert.Equal("Porto Alegre", result.Cidade));
            Assert.All(list, result => Assert.Equal("RS", result.Estado));
            Assert.All(list, result => Assert.Equal(4314902, result.CodigoIBGE));
        }
    }
}
