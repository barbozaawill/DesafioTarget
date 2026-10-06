namespace DesafioTarget.App.Comissao;

public class Venda
{
    public string Vendedor { get; set; } = "";
    public decimal Valor { get; set; }
}

public class ArquivoDeVendas
{
    public List<Venda> Vendas { get; set; } = new();
}
