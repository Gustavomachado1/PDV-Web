# Guia técnico de desenvolvimento

Este guia reúne a estrutura da solução, o estado técnico implementado e os comandos para desenvolver e executar o PDV Web. A visão do produto e as regras funcionais ficam no [README principal](../../README.md) e nos documentos oficiais listados ao final.

## Estrutura da solução

`PDV-Web.slnx` reúne cinco projetos de aplicação e dois projetos de testes:

| Projeto | Responsabilidade atual |
|---|---|
| `src/PDV.Domain` | Entidades e tipos centrais do domínio. |
| `src/PDV.Application` | Abstrações consumidas pelas outras camadas. |
| `src/PDV.Infrastructure` | Persistência com EF Core e SQL Server, filtros por empresa e interceptação de auditoria. |
| `src/PDV.API` | API ASP.NET Core, autenticação por sessão, Swagger em desenvolvimento e hub de notificações protegido. |
| `src/PDV.Web` | Interface Blazor Web App com renderização interativa no servidor e autenticação integrada à API. |
| `tests/PDV.API.Tests` | Testes da API e dos serviços de autenticação. |
| `tests/PDV.Infrastructure.Tests` | Testes da infraestrutura, incluindo auditoria. |

O contexto `PdvDbContext` e a migration inicial `Inicial` estão em `src/PDV.Infrastructure`. A solução e os projetos usam .NET 10.

## Estado técnico

A fundação multiempresa, a autenticação da API, o fluxo de login/logout da interface e um comando administrativo de provisionamento inicial estão integrados. O comando cria uma única empresa, filial, conta Administrador, os quatro perfis padrão, permissões iniciais e o plano informado pelo operador. A API emite sessões revogáveis; a interface mantém o ticket no servidor e entrega ao navegador um cookie protegido. O armazenamento de tickets atual é em memória, adequado a uma única instância web. Para múltiplas instâncias ou sessões que precisem sobreviver a reinícios, configure armazenamento distribuído e um chaveiro compartilhado do ASP.NET Core Data Protection. Consulte [Autenticação Web](04-AUTENTICACAO-WEB.md).

Os módulos de negócio do MVP ainda estão pendentes. O comando de provisionamento não é um endpoint HTTP e não define credenciais padrão; consulte abaixo os requisitos e as instruções de execução segura.

## Pré-requisitos e configuração local

- .NET 10 SDK.
- Uma instância acessível do SQL Server.

A API lê a connection string `ConnectionStrings:PdvWeb` em `src/PDV.API/appsettings.Development.json`. Ajuste-a para a instância disponível no ambiente local. A interface lê `Api:BaseUrl` em `src/PDV.Web/appsettings.Development.json`; o valor do repositório aponta para `https://localhost:7296/`. Mantenha credenciais e configurações pessoais fora dos commits.

### Provisionamento inicial

O comando `--bootstrap` executa uma única vez o provisionamento no banco configurado para a API e encerra o processo sem iniciar o servidor HTTP. A migration inicial precisa estar aplicada antes da execução. Configure os valores abaixo no ambiente do processo que executará o comando:

| Variável | Finalidade |
|---|---|
| `PDV_BOOTSTRAP_EMPRESA_CODIGO` | Código de login da empresa. |
| `PDV_BOOTSTRAP_EMPRESA_RAZAO_SOCIAL` | Razão social. |
| `PDV_BOOTSTRAP_EMPRESA_NOME_FANTASIA` | Nome fantasia (opcional). |
| `PDV_BOOTSTRAP_EMPRESA_CNPJ` | CNPJ válido, com ou sem pontuação. |
| `PDV_BOOTSTRAP_FILIAL_CODIGO` | Código da filial inicial. |
| `PDV_BOOTSTRAP_FILIAL_NOME` | Nome da filial inicial. |
| `PDV_BOOTSTRAP_ADMIN_NOME` | Nome da conta Administrador. |
| `PDV_BOOTSTRAP_ADMIN_LOGIN` | Login da conta Administrador. |
| `PDV_BOOTSTRAP_ADMIN_EMAIL` | E-mail (opcional). |
| `PDV_BOOTSTRAP_ADMIN_SENHA` | Senha inicial (12–1024 caracteres); armazenada somente como hash do ASP.NET Core Identity. |
| `PDV_BOOTSTRAP_PLANO_NOME` | Nome do plano padrão. |
| `PDV_BOOTSTRAP_PLANO_MAX_FILIAIS` | Limite positivo de filiais. |
| `PDV_BOOTSTRAP_PLANO_MAX_CAIXAS` | Limite positivo de caixas. |
| `PDV_BOOTSTRAP_PLANO_MAX_USUARIOS` | Limite positivo de usuários. |

Nenhum valor ou credencial padrão é fornecido. Informe-os por um gerenciador de segredos ou variáveis de ambiente temporárias e não os inclua em comandos salvos, arquivos versionados ou logs. A senha não é exibida pelo comando. O catálogo inicial contém os exemplos `usuarios.gerenciar` e `venda.cancelar`, concedidos somente ao perfil Administrador; os outros perfis começam sem concessões. O serviço valida e normaliza os dados antes de persistir, usa uma transação e bloqueio no SQL Server, e recusa a execução quando já há dados de provisionamento.

Após configurar `ConnectionStrings__PdvWeb` por um meio seguro, aplique as migrations e execute o bootstrap na raiz do repositório:

```powershell
dotnet ef database update --project src/PDV.Infrastructure/PDV.Infrastructure.csproj --startup-project src/PDV.API/PDV.API.csproj
dotnet run --project src/PDV.API/PDV.API.csproj -- --bootstrap
```

O pré-requisito é .NET 10 SDK, acesso ao SQL Server configurado e migration atualizada. Não há rota pública de bootstrap; executar o comando novamente falha sem alterar os dados existentes.

## Restaurar, compilar e testar

Execute os comandos na raiz do repositório:

```powershell
dotnet restore PDV-Web.slnx
dotnet build PDV-Web.slnx
dotnet test PDV-Web.slnx
```

## Executar localmente

Inicie primeiro a API e mantenha-a em execução:

```powershell
dotnet run --project src/PDV.API/PDV.API.csproj --launch-profile https
```

O perfil HTTPS da API usa `https://localhost:7296` (HTTP: `http://localhost:5051`). Em outro terminal, inicie a interface:

```powershell
dotnet run --project src/PDV.Web/PDV.Web.csproj --launch-profile https
```

O perfil HTTPS da interface usa `https://localhost:7091` (HTTP: `http://localhost:5022`). Em desenvolvimento, a API expõe o Swagger; o endpoint de saúde é `/saude`.

Para aplicar migrations ao banco local, com a ferramenta `dotnet-ef` disponível, use o projeto de infraestrutura como destino e a API como projeto de inicialização:

```powershell
dotnet ef database update --project src/PDV.Infrastructure/PDV.Infrastructure.csproj --startup-project src/PDV.API/PDV.API.csproj
```

## Documentação de referência

- [01 — Visão do Produto](01-VISAO-DO-PRODUTO.md)
- [02 — Escopo do MVP](02-ESCOPO-MVP.md)
- [03 — Requisitos Funcionais e Não Funcionais](03-REQUISITOS-FUNCIONAIS-NAO-FUNCIONAIS.md)
- [04 — Autenticação Web](04-AUTENTICACAO-WEB.md)

Use os documentos 01–03 para decisões de produto e comportamento. Este guia é voltado à estrutura e ao fluxo de desenvolvimento; não substitui nem duplica os requisitos oficiais.
