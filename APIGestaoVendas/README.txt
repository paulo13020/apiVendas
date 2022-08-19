API GESTÃO DE VENDAS

Favor, alterar no appsettings.json a "ConnectionStrings" colocando a string de conexão local.

3 END POINTS Criados 

Vendedor: 
Realiza o cadastro de um vendedor cujo e-mail é unico.

Oportunidade:
Cadastra uma oportunidade pegando o CNPJ e indo na API publica e retornando razão social e nome cadastrado no CNPJ.

BuscarInfoUsuario
Retorna todas as oportunidades do usuario pelo ID.


O Repositorio CadastrarOportunidadeVendedor é responsável pelo sistema de Roleta: 

A logica maior é se um vendedor de uma região tiver uma oportunidade cadastrada, ela pula para o proximo da lista.
