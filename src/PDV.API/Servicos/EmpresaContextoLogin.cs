namespace PDV.API.Servicos;

public sealed class EmpresaContextoLogin
{
    public Guid? EmpresaId { get; private set; }
    public Guid? UsuarioId { get; private set; }
    public Guid? FilialId { get; private set; }

    public void Definir(Guid empresaId)
    {
        if (empresaId == Guid.Empty)
            throw new ArgumentException("A empresa deve ser válida.", nameof(empresaId));

        EmpresaId = empresaId;
        UsuarioId = null;
        FilialId = null;
    }

    public void DefinirUsuario(Guid usuarioId) => UsuarioId = usuarioId;

    public void Limpar()
    {
        EmpresaId = null;
        UsuarioId = null;
        FilialId = null;
    }
}
