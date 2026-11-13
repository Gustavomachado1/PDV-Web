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

A fundação multiempresa, a autenticação da API e o fluxo de login/logout da interface já estão integrados. A API emite sessões revogáveis; a interface mantém o ticket no servidor e entrega ao navegador um cookie protegido. O armazenamento de tickets atual é em memória, adequado a uma única instância web. Para múltiplas instâncias ou sessões que precisem sobreviver a reinícios, configure armazenamento distribuído e um chaveiro compartilhado do ASP.NET Core Data Protection. Consulte [Autenticação Web](04-AUTENTICACAO-WEB.md).

Os módulos de negócio do MVP ainda estão pendentes. Não há provisionamento inicial de usuários, perfis, permissões e plano padrão; portanto, a interface de login integrada não significa que já exista uma conta inicial pronta para entrar.

## Pré-requisitos e configuração local

- .NET 10 SDK.
- Uma instância acessível do SQL Server.

A API lê a connection string `ConnectionStrings:PdvWeb` em `src/PDV.API/appsettings.Development.json`. Ajuste-a para a instância disponível no ambiente local. A interface lê `Api:BaseUrl` em `src/PDV.Web/appsettings.Development.json`; o valor do repositório aponta para `https://localhost:7296/`. Mantenha credenciais e configurações pessoais fora dos commits.

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
