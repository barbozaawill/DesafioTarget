using DesafioTarget.App.Comissao;
using DesafioTarget.App.Estoque;
using DesafioTarget.App.Juros;
using System.Text.Json;
using System.Globalization; // tive problema utilizando o parser sem globalization por que o decimal estava com vírgula e não ponto..

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

var servicoEstoque = new ServicoEstoque(arquivoEstoque.Estoque);

while (true)
{
    Console.WriteLine("\nDesafio Target");
    Console.WriteLine("1. Comissão de vendedores");
    Console.WriteLine("2. Movimentação de estoque");
    Console.WriteLine("3. Cálculo de juros");
    Console.WriteLine("0. Sair");
    Console.Write("Escolha uma opção:\n");
    string? opcao = Console.ReadLine();
    Console.WriteLine("");

    switch (opcao)
    {
        case "1":
            ExecutarComissao(arquivoVendas.Vendas);
            break;
        case "2":
            ExecutarEstoque(servicoEstoque, arquivoEstoque.Estoque);
            break;
        case "3":
            ExecutarJuros();
            break;
        case "0":
            Console.WriteLine("Saindo...");
            return;
        default:
            Console.WriteLine("Opção inválida.");
            break;
    }
}

void ExecutarComissao(List<Venda> vendas)
{
    Console.WriteLine($"Vendas lidas: {vendas.Count}");
    var vendasPorVendedor = vendas.GroupBy(v => v.Vendedor);
    foreach (var grupo in vendasPorVendedor)
    {
        string vendedor = grupo.Key;
        int quantidade = grupo.Count();
        decimal totalVendido = grupo.Sum(v => v.Valor);
        decimal totalComissao = grupo.Sum(v => CalculadoraComissao.Calcular(v.Valor));
        Console.WriteLine($"{vendedor}: {quantidade} vendas, total {totalVendido:C}, comissão {totalComissao:C}");
    }
}

void ExecutarEstoque(ServicoEstoque servico, List<Produto> produtos)
{
    Console.WriteLine($"Produtos em estoque: {produtos.Count}");
    foreach (var produto in produtos)
    {
        Console.WriteLine($"Produto {produto.CodigoProduto} - {produto.DescricaoProduto}: {produto.Estoque} unidades em estoque");
    }

    Console.WriteLine("\nNova movimentação");
    int codigo = LerInteiro("Código do produto: ");

    Console.Write("Tipo (E = Entrada / S = Saída): ");
    string tipoDigitado = Console.ReadLine()?.ToUpper() ?? "";

    TipoMovimentacao tipo;
    if (tipoDigitado == "E")
    {
        tipo = TipoMovimentacao.Entrada;
    }
    else if (tipoDigitado == "S")
    {
        tipo = TipoMovimentacao.Saida;
    }
    else
    {
        Console.WriteLine("Tipo inválido. Use E para entrada ou S para saída.");
        return;
    }

    int quantidadeMov = LerInteiro("Quantidade: ");

    Console.Write("Descrição: ");
    string descricao = Console.ReadLine() ?? "";

    try
    {
        var mov = servico.Movimentar(codigo, tipo, quantidadeMov, descricao);
        Console.WriteLine($"\nMovimentação {mov.Id} registrada. Estoque final: {mov.EstoqueFinal}");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Erro ao movimentar estoque: {ex.Message}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Dados inválidos: {ex.Message}");
    }
}

void ExecutarJuros()
{
    Console.WriteLine("\nCálculo de juros");
    decimal valorOriginal = LerDecimal("Valor original: ");
    DateOnly vencimento = LerData("Data de vencimento (dd/MM/yyyy): ");
    DateOnly hoje = DateOnly.FromDateTime(DateTime.Now);
    try
    {
        decimal juros = CalculadoraJuros.Calcular(valorOriginal, vencimento, hoje);
        Console.WriteLine($"Valor original: {valorOriginal:C}");
        Console.WriteLine($"Vencimento: {vencimento:dd/MM/yyyy}");
        Console.WriteLine($"Data do cálculo: {hoje:dd/MM/yyyy}");
        Console.WriteLine($"Juros (2,5%/dia): {juros:C}");
        Console.WriteLine($"Valor atualizado: {valorOriginal + juros:C}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Erro no cálculo de juros: {ex.Message}");
    }
}

int LerInteiro(string pergunta)
{
    while (true)
    {
        Console.Write(pergunta);
        string? texto = Console.ReadLine();

        if (int.TryParse(texto, out int numero))
        {
            return numero;
        }

        Console.WriteLine("Digite um número inteiro válido: ");
    }
}

decimal LerDecimal(string pergunta)
{
    while (true)
    {
        Console.Write(pergunta);
        string? texto = Console.ReadLine();

        if (decimal.TryParse(texto, NumberStyles.Number, new CultureInfo("pt-BR"), out decimal valor))
        {
            return valor;
        }
        Console.WriteLine("Valor inválido. Use vírgula para os centavos");
    }
}

DateOnly LerData(string pergunta)
{
    while (true)
    {
        Console.Write(pergunta);
        string? texto = Console.ReadLine();

        if (DateOnly.TryParseExact(texto, "dd/MM/yyyy", out DateOnly data))
        {
            return data;
        }
        Console.WriteLine("Digite uma data válida no formato dd/MM/yyyy: ");
    }
}



