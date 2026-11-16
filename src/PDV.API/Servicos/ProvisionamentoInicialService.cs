using System.Data;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PDV.Domain;
using PDV.Infrastructure;

namespace PDV.API.Servicos;

public sealed class ProvisionamentoInicialConfiguracao
{
    private ProvisionamentoInicialConfiguracao(
        string empresaCodigo,
        string empresaRazaoSocial,
        string? empresaNomeFantasia,
        string empresaCnpj,
        string filialCodigo,
        string filialNome,
        string administradorNome,
        string administradorLogin,
        string? administradorEmail,
        string administradorSenha)
    {
        EmpresaCodigo = empresaCodigo;
        EmpresaRazaoSocial = empresaRazaoSocial;
        EmpresaNomeFantasia = empresaNomeFantasia;
        EmpresaCnpj = empresaCnpj;
        FilialCodigo = filialCodigo;
        FilialNome = filialNome;
        AdministradorNome = administradorNome;
        AdministradorLogin = administradorLogin;
        AdministradorEmail = administradorEmail;
        AdministradorSenha = administradorSenha;
    }

    public string EmpresaCodigo { get; }
    public string EmpresaRazaoSocial { get; }
    public string? EmpresaNomeFantasia { get; }
    public string EmpresaCnpj { get; }
    public string FilialCodigo { get; }
    public string FilialNome { get; }
    public string AdministradorNome { get; }
    public string AdministradorLogin { get; }
    public string? AdministradorEmail { get; }
    public string AdministradorSenha { get; }

    public static ProvisionamentoInicialConfiguracao CarregarDoAmbiente()
    {
        var nomes = new[]
        {
            "PDV_BOOTSTRAP_EMPRESA_CODIGO",
            "PDV_BOOTSTRAP_EMPRESA_RAZAO_SOCIAL",
            "PDV_BOOTSTRAP_EMPRESA_NOME_FANTASIA",
            "PDV_BOOTSTRAP_EMPRESA_CNPJ",
            "PDV_BOOTSTRAP_FILIAL_CODIGO",
            "PDV_BOOTSTRAP_FILIAL_NOME",
            "PDV_BOOTSTRAP_ADMIN_NOME",
            "PDV_BOOTSTRAP_ADMIN_LOGIN",
            "PDV_BOOTSTRAP_ADMIN_EMAIL",
            "PDV_BOOTSTRAP_ADMIN_SENHA"
        };
        var ambiente = nomes.ToDictionary(nome => nome, Environment.GetEnvironmentVariable, StringComparer.Ordinal);
        return Parse(ambiente);
    }

    public static ProvisionamentoInicialConfiguracao Parse(IReadOnlyDictionary<string, string?> ambiente)
    {
        var empresaCnpj = Obrigatorio(ambiente, "PDV_BOOTSTRAP_EMPRESA_CNPJ", 18);
        var cnpjNormalizado = Regex.Replace(empresaCnpj, "[./-]", "");
        if (!Regex.IsMatch(cnpjNormalizado, @"^\d{14}$") || !CnpjValido(cnpjNormalizado))
            throw new InvalidOperationException("A variável PDV_BOOTSTRAP_EMPRESA_CNPJ deve conter um CNPJ válido.");

        var senha = Valor(ambiente, "PDV_BOOTSTRAP_ADMIN_SENHA");
        if (senha is null || string.IsNullOrWhiteSpace(senha) || senha.Length < 12 ||
            senha.Length > 1024 || senha.Any(char.IsControl))
            throw new InvalidOperationException(
                "A variável PDV_BOOTSTRAP_ADMIN_SENHA deve conter entre 12 e 1024 caracteres não controlados.");

        var email = Opcional(ambiente, "PDV_BOOTSTRAP_ADMIN_EMAIL", 200);
        if (email is not null &&
            (!MailAddress.TryCreate(email, out var endereco) ||
             !string.Equals(endereco.Address, email, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("A variável PDV_BOOTSTRAP_ADMIN_EMAIL deve conter um e-mail válido.");

        return new ProvisionamentoInicialConfiguracao(
            Obrigatorio(ambiente, "PDV_BOOTSTRAP_EMPRESA_CODIGO", 30),
            Obrigatorio(ambiente, "PDV_BOOTSTRAP_EMPRESA_RAZAO_SOCIAL", 200),
            Opcional(ambiente, "PDV_BOOTSTRAP_EMPRESA_NOME_FANTASIA", 200),
            cnpjNormalizado,
            Obrigatorio(ambiente, "PDV_BOOTSTRAP_FILIAL_CODIGO", 30),
            Obrigatorio(ambiente, "PDV_BOOTSTRAP_FILIAL_NOME", 200),
            Obrigatorio(ambiente, "PDV_BOOTSTRAP_ADMIN_NOME", 200),
            Obrigatorio(ambiente, "PDV_BOOTSTRAP_ADMIN_LOGIN", 100),
            email,
            senha);
    }

    private static string Obrigatorio(IReadOnlyDictionary<string, string?> ambiente, string nome, int maximo)
    {
        var valor = Opcional(ambiente, nome, maximo);
        return valor ?? throw new InvalidOperationException($"A variável de ambiente {nome} é obrigatória.");
    }

    private static string? Opcional(IReadOnlyDictionary<string, string?> ambiente, string nome, int maximo)
    {
        var valor = Valor(ambiente, nome);
        if (string.IsNullOrWhiteSpace(valor))
            return null;

        var normalizado = valor.Trim().Normalize(NormalizationForm.FormC);
        if (normalizado.Length > maximo || normalizado.Any(char.IsControl))
            throw new InvalidOperationException($"A variável de ambiente {nome} possui formato ou tamanho inválido.");

        return normalizado;
    }

    private static string? Valor(IReadOnlyDictionary<string, string?> ambiente, string nome)
        => ambiente.TryGetValue(nome, out var valor) ? valor : null;

    private static bool CnpjValido(string cnpj)
    {
        if (cnpj.Distinct().Count() == 1)
            return false;

        var pesosPrimeiroDigito = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        var pesosSegundoDigito = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        return CalcularDigito(cnpj[..12], pesosPrimeiroDigito) == cnpj[12] - '0' &&
               CalcularDigito(cnpj[..13], pesosSegundoDigito) == cnpj[13] - '0';
    }

    private static int CalcularDigito(string valor, int[] pesos)
    {
        var soma = 0;
        for (var i = 0; i < pesos.Length; i++)
            soma += (valor[i] - '0') * pesos[i];

        var resto = soma % 11;
        return resto < 2 ? 0 : 11 - resto;
    }
}

public sealed class ProvisionamentoInicialService(
    PdvDbContext db,
    EmpresaContextoLogin empresaContexto,
    IPasswordHasher<Usuario> passwordHasher)
{
    private const string PlanoPadraoNome = "Essencial";
    private const int PlanoPadraoMaxFiliais = 1;
    private const int PlanoPadraoMaxCaixas = 2;
    private const int PlanoPadraoMaxUsuarios = 3;

    public async Task ExecutarAsync(
        ProvisionamentoInicialConfiguracao configuracao,
        CancellationToken cancellationToken)
    {
        await using var transacao = db.Database.IsRelational()
            ? await db.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken)
            : null;

        if (db.Database.IsSqlServer())
            await AdquirirBloqueioAsync(cancellationToken);

        await GarantirSemProvisionamentoAsync(cancellationToken);
        CriarDados(configuracao);
        await db.SaveChangesAsync(cancellationToken);
        if (transacao is not null)
            await transacao.CommitAsync(cancellationToken);
    }

    private async Task GarantirSemProvisionamentoAsync(CancellationToken cancellationToken)
    {
        if (await db.Empresas.IgnoreQueryFilters().AnyAsync(cancellationToken) ||
            await db.Usuarios.IgnoreQueryFilters().AnyAsync(cancellationToken) ||
            await db.Perfis.IgnoreQueryFilters().AnyAsync(cancellationToken) ||
            await db.Permissoes.AnyAsync(cancellationToken) ||
            await db.Planos.AnyAsync(cancellationToken))
            throw new InvalidOperationException(
                "O provisionamento inicial já foi executado ou existem dados de provisionamento no banco.");
    }

    private void CriarDados(ProvisionamentoInicialConfiguracao configuracao)
    {
        var empresa = new Empresa
        {
            Codigo = configuracao.EmpresaCodigo,
            RazaoSocial = configuracao.EmpresaRazaoSocial,
            NomeFantasia = configuracao.EmpresaNomeFantasia,
            Cnpj = configuracao.EmpresaCnpj
        };
        var filial = new Filial
        {
            EmpresaId = empresa.Id,
            Codigo = configuracao.FilialCodigo,
            Nome = configuracao.FilialNome
        };
        var plano = new Plano
        {
            Nome = PlanoPadraoNome,
            MaxFiliais = PlanoPadraoMaxFiliais,
            MaxCaixas = PlanoPadraoMaxCaixas,
            MaxUsuarios = PlanoPadraoMaxUsuarios
        };
        var permissoes = new[]
        {
            new Permissao { Codigo = "catalogo.gerenciar", Descricao = "Gerenciar catálogo de produtos" },
            new Permissao { Codigo = "usuarios.gerenciar", Descricao = "Gerenciar usuários" },
            new Permissao { Codigo = "venda.cancelar", Descricao = "Cancelar vendas" },
            new Permissao { Codigo = "estoque.consultar", Descricao = "Consultar estoque e movimentações" },
            new Permissao { Codigo = "estoque.configurar", Descricao = "Configurar estoque por filial" },
            new Permissao { Codigo = "estoque.negativo.configurar", Descricao = "Configurar a regra de estoque negativo" },
            new Permissao { Codigo = "estoque.negativo.permitir", Descricao = "Autorizar operações que resultem em estoque negativo" },
            new Permissao { Codigo = "estoque.movimentar.solicitar", Descricao = "Solicitar movimentação manual de estoque" },
            new Permissao { Codigo = "estoque.movimento.entrada.autorizar", Descricao = "Autorizar entradas manuais de estoque" },
            new Permissao { Codigo = "estoque.movimento.saida.autorizar", Descricao = "Autorizar saídas manuais de estoque" },
            new Permissao { Codigo = "estoque.inventario.gerenciar", Descricao = "Realizar e concluir inventários" }
        };
        var perfis = new[]
        {
            new Perfil { EmpresaId = empresa.Id, Nome = "Administrador", Tipo = TipoPerfil.Administrador },
            new Perfil { EmpresaId = empresa.Id, Nome = "Gerente", Tipo = TipoPerfil.Gerente },
            new Perfil { EmpresaId = empresa.Id, Nome = "Operador", Tipo = TipoPerfil.Operador },
            new Perfil { EmpresaId = empresa.Id, Nome = "Estoquista", Tipo = TipoPerfil.Estoquista }
        };
        var administrador = perfis.Single(perfil => perfil.Tipo == TipoPerfil.Administrador);
        var gerente = perfis.Single(perfil => perfil.Tipo == TipoPerfil.Gerente);
        var estoquista = perfis.Single(perfil => perfil.Tipo == TipoPerfil.Estoquista);
        var usuario = new Usuario
        {
            EmpresaId = empresa.Id,
            Nome = configuracao.AdministradorNome,
            Login = configuracao.AdministradorLogin,
            Email = configuracao.AdministradorEmail,
            PerfilId = administrador.Id,
            Status = StatusUsuario.Ativo
        };
        usuario.SenhaHash = passwordHasher.HashPassword(usuario, configuracao.AdministradorSenha);

        empresaContexto.Definir(empresa.Id);
        empresaContexto.DefinirUsuario(usuario.Id);

        db.Empresas.Add(empresa);
        db.Filiais.Add(filial);
        db.Planos.Add(plano);
        db.Permissoes.AddRange(permissoes);
        db.Perfis.AddRange(perfis);
        var permissoesGerente = permissoes.Where(x => x.Codigo is
            "estoque.consultar" or
            "estoque.movimentar.solicitar" or
            "estoque.movimento.entrada.autorizar" or
            "estoque.movimento.saida.autorizar");
        var permissoesEstoquista = permissoes.Where(x => x.Codigo is
            "estoque.consultar" or
            "estoque.configurar" or
            "estoque.movimentar.solicitar" or
            "estoque.inventario.gerenciar");
        db.PerfisPermissoes.AddRange(
            permissoes.Select(permissao => new PerfilPermissao
            {
                EmpresaId = empresa.Id,
                PerfilId = administrador.Id,
                PermissaoId = permissao.Id
            })
            .Concat(permissoesGerente.Select(permissao => new PerfilPermissao
            {
                EmpresaId = empresa.Id,
                PerfilId = gerente.Id,
                PermissaoId = permissao.Id
            }))
            .Concat(permissoesEstoquista.Select(permissao => new PerfilPermissao
            {
                EmpresaId = empresa.Id,
                PerfilId = estoquista.Id,
                PermissaoId = permissao.Id
            })));
        db.Usuarios.Add(usuario);
        db.UsuariosFiliais.Add(new UsuarioFilial
        {
            EmpresaId = empresa.Id,
            UsuarioId = usuario.Id,
            FilialId = filial.Id
        });
    }

    private async Task AdquirirBloqueioAsync(CancellationToken cancellationToken)
    {
        var conexao = db.Database.GetDbConnection();
        await using var comando = conexao.CreateCommand();
        comando.Transaction = db.Database.CurrentTransaction?.GetDbTransaction();
        comando.CommandText = """
            DECLARE @resultado int;
            EXEC @resultado = sys.sp_getapplock
                @Resource = N'PDV-Web:ProvisionamentoInicial',
                @LockMode = N'Exclusive',
                @LockOwner = N'Transaction',
                @LockTimeout = 60000;
            SELECT @resultado;
            """;

        var resultado = Convert.ToInt32(await comando.ExecuteScalarAsync(cancellationToken), CultureInfo.InvariantCulture);
        if (resultado < 0)
            throw new InvalidOperationException(
                "Não foi possível obter o bloqueio para executar o provisionamento inicial.");
    }
}
