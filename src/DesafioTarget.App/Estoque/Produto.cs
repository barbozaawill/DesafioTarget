namespace DesafioTarget.App.Estoque;

public class Produto
{
    public int CodigoProduto { get; set; }
    public string DescricaoProduto { get; set; } = string.Empty;
    public int Estoque { get; set; }
}

public class ArquivoDeEstoque
{
    public List<Produto> Estoque { get; set; } = new();
}