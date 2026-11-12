namespace PdvWeb.Application;

/// <summary>
/// Contexto do usuário da requisição. Sem empresa definida, os filtros de isolamento
/// não retornam nenhum dado (falha segura).
/// </summary>
public interface IUsuarioAtual
{
    Guid? UsuarioId { get; }
    Guid? EmpresaId { get; }
    Guid? FilialId { get; }
}
