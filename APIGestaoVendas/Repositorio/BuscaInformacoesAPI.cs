using APIGestaoVendas.Data;
using APIGestaoVendas.Model;
using APIGestaoVendas.Service;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIGestaoVendas.Repositorio
{
    public class BuscaInformacoesAPI : IBuscaInformacoesAPI
    {
        private readonly APIDbContext _apiContexto;
        private readonly IConfiguration _configuration;
        public BuscaInformacoesAPI(APIDbContext apiContexto, IConfiguration configuration)
        {
            _apiContexto = apiContexto;
            _configuration = configuration;
        }
        public async Task<Oportunidade> ObterInfoAPIPublica(string CNPJ)
        {
            try
            {
                Oportunidade listaOportunidade = new Oportunidade();
                var uri = "https://publica.cnpj.ws/cnpj/";

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(uri + CNPJ);
                var obterRazaoSocial = new
                {
                    razao_social = "",

                    estabelecimento = new
                    {
                        atividade_principal = new
                        {
                            descricao = ""
                        },
                        estado = new
                        {
                            ibge_id = ""
                        }
                    },

                };
                string ResponseBody = await response.Content.ReadAsStringAsync();
                var retornoAPI = JsonConvert.DeserializeAnonymousType(ResponseBody, obterRazaoSocial);
                listaOportunidade = new Oportunidade
                {
                    RazaoSocial = retornoAPI.razao_social,
                    descricaoAtividade = retornoAPI.estabelecimento.atividade_principal.descricao.ToString(),
                    codigoEstado = int.Parse(retornoAPI.estabelecimento.estado.ibge_id.Remove(retornoAPI.estabelecimento.estado.ibge_id.Length - 1))
                };

                return listaOportunidade;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void CadastrarOportunidade(Oportunidade responseAPI, Oportunidade oportunidade)
        {
            try
            {
                Oportunidade cadastrarOportunidade = new Oportunidade
                {
                    CNPJ = oportunidade.CNPJ,
                    codigoEstado = responseAPI.codigoEstado,
                    descricaoAtividade = responseAPI.descricaoAtividade,
                    NomeOportunidade = oportunidade.NomeOportunidade,
                    RazaoSocial = responseAPI.RazaoSocial,
                    ValorMonetario = oportunidade.ValorMonetario,
                };

                _apiContexto.Oportunidades.Add(cadastrarOportunidade);
                _apiContexto.SaveChanges();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }

        public string CadastrarOportunidadeVendedor()
        {

            var listaVendedores = _apiContexto.Vendedores.ToList();
            var listaRegiaoOportunidade = _apiContexto.Oportunidades.ToList();


            for (int i = 0; i < listaRegiaoOportunidade.Count(); i++)
            {
                for (int ii = 0; ii < listaVendedores.Count(); ii++)
                {
                    var obterVendedor = listaVendedores.ElementAt(ii);
                    var obterOportunidade = listaRegiaoOportunidade.ElementAt(i);

                    if ((int)obterVendedor.RegiaoResponsavel == (int)obterOportunidade.codigoEstado && _apiContexto.VendedorOportunidades.Where(p => p.IdVendedor == obterVendedor.IdVendedor).Count() == 0)
                    {
                        var VerificarOportunidade = _apiContexto.VendedorOportunidades.Where(p => p.idOportunidade == obterOportunidade.idOportunidade).FirstOrDefault();
                        if (VerificarOportunidade == null)
                        {
                            using (var conexaoDB = new SqlConnection(_configuration.GetConnectionString("DB")))
                            {
                                conexaoDB.Execute("INSERT INTO OportunidadeVendedor VALUES (@idOportunidade, @IdVendedor)", new
                                {
                                    idOportunidade = obterOportunidade.idOportunidade,
                                    IdVendedor = obterVendedor.IdVendedor,
                                });
                            }
                            return "Cadastrado";
                        }

                    }
                }
            }
            return "Erro";
        }



    }
}
