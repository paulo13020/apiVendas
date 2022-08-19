using APIGestaoVendas.Data;
using APIGestaoVendas.Model;
using APIGestaoVendas.Repositorio;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace APIGestaoVendas.Controllers
{
    public class GestaoController : Controller
    {
        private readonly APIDbContext _apiContexto;
        private readonly IBuscaInformacoesAPI _respositorioAPI;
        private readonly IConfiguration _configuration;
        public GestaoController(APIDbContext apiContexto, IBuscaInformacoesAPI respositorioAPI, IConfiguration configuration)
        {
            _apiContexto = apiContexto;
            _respositorioAPI = respositorioAPI;
            _configuration = configuration;
        }

        [Route("/api/Vendedor")]
        [HttpPost]
        public JsonResult Vendedor(Vendedor vendedor)
        {
            if (_apiContexto.Vendedores.Any(p => p.Email == vendedor.Email))
            {
                return Json("Email já cadastrado");
            }

            _apiContexto.Vendedores.Add(vendedor);
            _apiContexto.SaveChanges();
            return Json("Vendedor Cadastrado.");
        }

        [Route("/api/Oportunidade")]
        [HttpPost]
        public JsonResult Oportunidade(Oportunidade oportunidade)
        {
            _respositorioAPI.CadastrarOportunidadeVendedor();
            if (oportunidade.CNPJ.Count() < 14)
            {
                return Json("CNPJ inválido");
            }
            var obterInfoAPI = _respositorioAPI.ObterInfoAPIPublica(oportunidade.CNPJ.ToString().Replace(".", "").Replace("/", "").Replace("-", ""));

            _respositorioAPI.CadastrarOportunidade(obterInfoAPI.Result, oportunidade);
            var CadastrarOportunidadeVendedor = _respositorioAPI.CadastrarOportunidadeVendedor();

            if (CadastrarOportunidadeVendedor == "Erro")
            {
                return Json("Oportunidade cadastrada, porém sem vendedores na região.");
            }

            return Json("Oportunidade Cadastrada");
        }


        [Route("/api/BuscarInfoUsuario")]
        [HttpGet]
        public JsonResult BuscarInfoUsuario(int idVendedor)
        {
            var obterInfoUsuario = _apiContexto.Vendedores.Where(p => p.IdVendedor == idVendedor).ToList();
            if (obterInfoUsuario.Count > 0)
            {
                using (var conexaoDB = new SqlConnection(_configuration.GetConnectionString("DB")))
                {

                    var RetornoBD = conexaoDB.Query($"select v.NomeVendedor, v.RegiaoResponsavel, p.CNPJ, p.codigoEstado, p.NomeOportunidade, p.RazaoSocial, p.descricaoAtividade,p.ValorMonetario from OportunidadeVendedor o inner join Vendedor v on v.idVendedor = o.IdVendedor inner join Oportunidade p on p.idOportunidade = o.idOportunidade where v.IdVendedor = {idVendedor} ");

                    return Json(RetornoBD);
                }
            }

            return Json("Usuário inválido !");


        }
    }
}
