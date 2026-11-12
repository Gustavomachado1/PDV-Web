namespace PDV.Domain;

public enum StatusUsuario { Ativo = 1, Bloqueado = 2, Inativo = 3 }
public enum TipoPerfil { Administrador = 1, Gerente = 2, Operador = 3, Estoquista = 4, Personalizado = 5 }
public enum ResultadoAutorizacao { Pendente = 1, Aprovada = 2, Negada = 3, Expirada = 4 }

public class Perfil : EntidadeBase, IEmpresaScoped, IAuditavel
{
    public Guid EmpresaId { get; set; }
    public string Nome { get; set; } = "";
    public TipoPerfil Tipo { get; set; }
    public ICollection<PerfilPermissao> Permissoes { get; set; } = [];
}

/// <summary>Catálogo global de permissões (ex.: "usuarios.gerenciar", "venda.cancelar").</summary>
public class Permissao : EntidadeBase
{
    public string Codigo { get; set; } = "";
    public string Descricao { get; set; } = "";
}

public class PerfilPermissao : EntidadeBase, IEmpresaScoped
{
    public Guid EmpresaId { get; set; }
    public Guid PerfilId { get; set; }
    public Guid PermissaoId { get; set; }
    public Perfil? Perfil { get; set; }
    public Permissao? Permissao { get; set; }
}

public class Usuario : EntidadeBase, IEmpresaScoped, IAuditavel
{
    public Guid EmpresaId { get; set; }
    public string Nome { get; set; } = "";
    public string Login { get; set; } = "";
    public string? Email { get; set; }
    public string SenhaHash { get; set; } = "";
    public string? PinHash { get; set; }               // RF009: autenticação rápida no caixa
    public StatusUsuario Status { get; set; } = StatusUsuario.Ativo;
    public int TentativasFalhas { get; set; }          // RF007
    public DateTime? BloqueadoAteUtc { get; set; }
    public Guid PerfilId { get; set; }
    public Perfil? Perfil { get; set; }
}

/// <summary>Exceção por usuário sobre o perfil (RF005): concede ou revoga uma permissão específica.</summary>
public class UsuarioPermissao : EntidadeBase, IEmpresaScoped, IAuditavel
{
    public Guid EmpresaId { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid PermissaoId { get; set; }
    public bool Concedida { get; set; }
}

/// <summary>Vínculo do usuário com as filiais que ele pode acessar (RF013/RF014).</summary>
public class UsuarioFilial : EntidadeBase, IEmpresaScoped, IAuditavel
{
    public Guid EmpresaId { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid FilialId { get; set; }
}

public class Sessao : EntidadeBase, IEmpresaScoped
{
    public Guid EmpresaId { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid? FilialId { get; set; }
    public string TokenHash { get; set; } = "";
    public string? Dispositivo { get; set; }
    public DateTime ExpiraEmUtc { get; set; }
    public DateTime? EncerradaEmUtc { get; set; }      // RF006
}

/// <summary>RF008: data, hora e usuário de cada tentativa de autenticação.</summary>
public class LogAutenticacao : EntidadeBase, IEmpresaScoped
{
    public Guid EmpresaId { get; set; }
    public Guid? UsuarioId { get; set; }
    public string Login { get; set; } = "";
    public bool Sucesso { get; set; }
    public string? MotivoFalha { get; set; }
    public string? Ip { get; set; }
    public DateTime OcorridoEmUtc { get; set; } = DateTime.UtcNow;
}

/// <summary>Solicitação de autorização a outro usuário com a permissão necessária (RF021/RF022).</summary>
public class AutorizacaoOperacao : EntidadeBase, IEmpresaScoped, IAuditavel
{
    public Guid EmpresaId { get; set; }
    public Guid? FilialId { get; set; }
    public Guid SolicitanteId { get; set; }
    public Guid? AutorizadorId { get; set; }
    public string Operacao { get; set; } = "";         // código da permissão exigida
    public string? Referencia { get; set; }            // id da entidade alvo, quando houver
    public string Justificativa { get; set; } = "";
    public ResultadoAutorizacao Resultado { get; set; } = ResultadoAutorizacao.Pendente;
    public DateTime SolicitadaEmUtc { get; set; } = DateTime.UtcNow;
    public DateTime? DecididaEmUtc { get; set; }
}
