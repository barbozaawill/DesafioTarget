using DesafioTarget.App.Comissao;
using System.Text.Json;

Console.OutputEncoding = System.Text.Encoding.UTF8;

string caminho = Path.Combine(AppContext.BaseDirectory, "Dados" ,"vendas.json");
string json = File.ReadAllText(caminho);

var opcoes = new JsonSerializerOptions {  PropertyNameCaseInsensitive = true };
ArquivoDeVendas? arquivo = JsonSerializer.Deserialize<ArquivoDeVendas>(json, opcoes);

if (arquivo is null)
{
    Console.WriteLine("Json inválido");
    return;
}

Console.WriteLine($"Vendas lidas: {arquivo.Vendas.Count}");


var vendasPorVendedor = arquivo.Vendas.GroupBy(v => v.Vendedor);

foreach (var grupo in vendasPorVendedor)
{
    string vendedor = grupo.Key;
    int quantidade = grupo.Count();
    decimal totalVendido = grupo.Sum(v => v.Valor);
    decimal totalComissao = grupo.Sum(v => CalculadoraComissao.Calcular(v.Valor));

    Console.WriteLine($"{vendedor}: {quantidade} vendas, total {totalVendido:C}, comissão {totalComissao:C}");
}