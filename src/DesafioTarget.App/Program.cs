using DesafioTarget.App.Comissao;
using DesafioTarget.App.Estoque;
using System.Text.Json;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var opcoes = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

string caminhoVendas = Path.Combine(AppContext.BaseDirectory, "Dados", "vendas.json");
string jsonVendas = File.ReadAllText(caminhoVendas);
ArquivoDeVendas? arquivoVendas = JsonSerializer.Deserialize<ArquivoDeVendas>(jsonVendas, opcoes);


string caminhoEstoque = Path.Combine(AppContext.BaseDirectory, "Dados", "estoque.json");
string jsonEstoque = File.ReadAllText(caminhoEstoque);
ArquivoDeEstoque? arquivoEstoque = JsonSerializer.Deserialize<ArquivoDeEstoque>(jsonEstoque, opcoes);

if (arquivoVendas is null)
{
    Console.WriteLine("Json de vendas inválido");
    return;

}
if (arquivoEstoque is null)
{
    Console.WriteLine("Json de estoque inválido");
    return;
}

Console.WriteLine($"Vendas lidas: {arquivoVendas.Vendas.Count}");


var vendasPorVendedor = arquivoVendas.Vendas.GroupBy(v => v.Vendedor);

foreach (var grupo in vendasPorVendedor)
{
    string vendedor = grupo.Key;
    int quantidade = grupo.Count();
    decimal totalVendido = grupo.Sum(v => v.Valor);
    decimal totalComissao = grupo.Sum(v => CalculadoraComissao.Calcular(v.Valor));

    Console.WriteLine($"{vendedor}: {quantidade} vendas, total {totalVendido:C}, comissão {totalComissao:C}");
}

Console.WriteLine($"\nProdutos em estoque: {arquivoEstoque.Estoque.Count}");

foreach (var produto in arquivoEstoque.Estoque)
{
    Console.WriteLine($"Produto {produto.CodigoProduto} - {produto.DescricaoProduto}: {produto.Estoque} unidades em estoque");
}

var servicoEstoque = new ServicoEstoque(arquivoEstoque.Estoque);

var movimentacao1 = servicoEstoque.Movimentar(101, TipoMovimentacao.Entrada, 50, "Reabastecimento de produto 1");
Console.WriteLine($"\nMovimentação {movimentacao1.Id}: estoque final {movimentacao1.EstoqueFinal}");

var movimentacao2 = servicoEstoque.Movimentar(101, TipoMovimentacao.Saida, 20, "Venda de produto 2");
Console.WriteLine($"Movimentação {movimentacao2.Id}: estoque final {movimentacao2.EstoqueFinal}");

var movimentacao3 = servicoEstoque.Movimentar(103, TipoMovimentacao.Saida, 500, "Venda grande");
Console.WriteLine($"Movimentação {movimentacao3.Id}: estoque final {movimentacao3.EstoqueFinal}");