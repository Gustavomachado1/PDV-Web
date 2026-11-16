namespace PDV.Domain;

public enum TipoMovimentoEstoque { EntradaManual = 1, SaidaManual = 2, AjusteInventario = 3 }
public static class TipoMovimentoEstoqueExtensions
{
    public static decimal Sinal(this TipoMovimentoEstoque tipo) => tipo switch
    {
        TipoMovimentoEstoque.EntradaManual => 1m,
        TipoMovimentoEstoque.SaidaManual => -1m,
        _ => throw new InvalidOperationException(
            $"O tipo de movimento {tipo} não tem sinal definido para aplicação direta no saldo.")
    };
}
public enum SituacaoMovimentoEstoque { AguardandoAutorizacao = 1, Concluido = 2, Negado = 3 }
public enum TipoInventarioEstoque { Completo = 1, Parcial = 2 }

public class SaldoEstoque : EntidadeBase, IEmpresaScoped, IFilialScoped, IAuditavel
{
    public Guid EmpresaId { get; set; }
    public Guid FilialId { get; set; }
    public Guid ProdutoId { get; set; }
    public decimal Quantidade { get; set; }
    public decimal? EstoqueMinimo { get; set; }
    public decimal? EstoqueIdeal { get; set; }
    public bool Disponivel { get; set; }
    public Filial? Filial { get; set; }
    public Produto? Produto { get; set; }
}

public class MovimentoEstoque : EntidadeBase, IEmpresaScoped, IFilialScoped, IAuditavel
{
    public Guid EmpresaId { get; set; }
    public Guid FilialId { get; set; }
    public Guid ProdutoId { get; set; }
    public Guid? InventarioEstoqueId { get; set; }
    public Guid? AutorizacaoOperacaoId { get; set; }
    public Guid SolicitanteId { get; set; }
    public Guid? AutorizadorId { get; set; }
    public TipoMovimentoEstoque Tipo { get; set; }
    public SituacaoMovimentoEstoque Situacao { get; set; }
    public decimal Quantidade { get; set; }
    public decimal? SaldoAnterior { get; set; }
    public decimal? SaldoResultante { get; set; }
    public string? Justificativa { get; set; }
    public DateTime SolicitadoEmUtc { get; set; } = DateTime.UtcNow;
    public DateTime? ConcluidoEmUtc { get; set; }
    public Filial? Filial { get; set; }
    public Produto? Produto { get; set; }
    public InventarioEstoque? InventarioEstoque { get; set; }
    public AutorizacaoOperacao? AutorizacaoOperacao { get; set; }
}

public class InventarioEstoque : EntidadeBase, IEmpresaScoped, IFilialScoped, IAuditavel
{
    public Guid EmpresaId { get; set; }
    public Guid FilialId { get; set; }
    public Guid UsuarioId { get; set; }
    public TipoInventarioEstoque Tipo { get; set; }
    public string? Justificativa { get; set; }
    public DateTime ConcluidoEmUtc { get; set; } = DateTime.UtcNow;
    public Filial? Filial { get; set; }
    public ICollection<ItemInventarioEstoque> Itens { get; set; } = [];
}

public class ItemInventarioEstoque : EntidadeBase, IEmpresaScoped, IFilialScoped, IAuditavel
{
    public Guid EmpresaId { get; set; }
    public Guid FilialId { get; set; }
    public Guid InventarioEstoqueId { get; set; }
    public Guid ProdutoId { get; set; }
    public decimal SaldoAnterior { get; set; }
    public decimal QuantidadeContada { get; set; }
    public decimal Diferenca { get; set; }
    public InventarioEstoque? InventarioEstoque { get; set; }
    public Produto? Produto { get; set; }
}
