using APIGestaoVendas.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIGestaoVendas.Repositorio
{
    public interface IBuscaInformacoesAPI
    {
        public Task<Oportunidade> ObterInfoAPIPublica(string CNPJ);

        public void CadastrarOportunidade(Oportunidade responseAPI, Oportunidade oportunidade);
        public string CadastrarOportunidadeVendedor();


    }
}
