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

A fundação multiempresa, a autenticação da API, o fluxo de login/logout da interface e um comando administrativo de provisionamento inicial estão integrados. O comando cria uma única empresa, filial, conta Administrador, os quatro perfis padrão, permissões iniciais e o plano Essencial, com limite de 1 filial, 2 caixas e 3 usuários. Esses dados do plano são padrões permanentes do produto e não são configuráveis pelo ambiente de bootstrap. A API emite sessões revogáveis; a interface mantém o ticket no servidor e entrega ao navegador um cookie protegido. O armazenamento de tickets atual é em memória, adequado a uma única instância web. Para múltiplas instâncias ou sessões que precisem sobreviver a reinícios, configure armazenamento distribuído e um chaveiro compartilhado do ASP.NET Core Data Protection. Consulte [Autenticação Web](04-AUTENTICACAO-WEB.md).

O catálogo compartilhado de produtos e a gestão básica de estoque/inventário por filial estão integrados. Os módulos de vendas, pagamentos, caixa e devoluções ainda estão pendentes. O comando de provisionamento não é um endpoint HTTP e não define credenciais padrão; consulte abaixo os requisitos e as instruções de execução segura.

### Tratamento de erros da API

Exceções não tratadas produzem uma resposta HTTP 500 genérica em todos os ambientes, inclusive Development. A API registra apenas tipo da exceção, stack trace e identificador da requisição; cabeçalhos, credenciais e mensagens de exceção não são incluídos no log. Os clientes web ignoram os corpos de respostas HTTP com falha e exibem mensagens seguras baseadas no status.

### Estoque e inventário por filial

A tela **Estoque e inventário** consulta somente filiais vinculadas ao usuário e isola saldos, configurações, inventários e movimentações por empresa e filial. Um produto sem configuração de estoque na filial começa indisponível e com saldo zero; a disponibilidade precisa ser habilitada explicitamente por filial. Estoque mínimo e ideal são opcionais, e o alerta é exibido quando o saldo fica abaixo do mínimo.

Entradas e saídas manuais criam solicitações pendentes; outro usuário deve informar seu próprio login e senha para autorizá-las ou negá-las, e o sistema não persiste essas credenciais. A permissão de autorização é específica por tipo de movimento, a saída exige justificativa e pode resultar em saldo negativo. Inventários completos incluem todos os produtos da empresa; os parciais incluem apenas os selecionados. Ao concluir, divergências ajustam o saldo à contagem, exigem justificativa e geram movimentos rastreáveis com saldos anterior e resultante, responsável, horário e auditoria. As regras de concorrência são protegidas por transação e controle de versão do saldo.

As permissões `estoque.consultar`, `estoque.configurar`, `estoque.movimentar.solicitar`, `estoque.movimento.entrada.autorizar`, `estoque.movimento.saida.autorizar` e `estoque.inventario.gerenciar` são atribuídas aos perfis padrão pelo bootstrap. A migration também provisiona essas permissões para perfis existentes quando já há perfis cadastrados. As rotas HTTP ficam sob `/api/estoque`; nenhum fluxo de venda/caixa, lote, validade ou transferência entre filiais faz parte desta entrega.

## Pré-requisitos e configuração local

- .NET 10 SDK.
- Uma instância acessível do SQL Server.

A API lê a connection string `ConnectionStrings:PdvWeb` em `src/PDV.API/appsettings.Development.json`. Ajuste-a para a instância disponível no ambiente local. A interface lê `Api:BaseUrl` em `src/PDV.Web/appsettings.Development.json`; o valor do repositório aponta para `https://localhost:7296/`. Mantenha credenciais e configurações pessoais fora dos commits.

Não cole connection strings, senhas ou outros segredos no chat. Antes de qualquer comando administrativo, confira localmente se `ASPNETCORE_ENVIRONMENT` é `Development` e se `ConnectionStrings__PdvWeb` (ou `appsettings.Development.json`) aponta para o SQL Server e o banco de desenvolvimento corretos. O nome do ambiente, sozinho, não confirma o destino da conexão.

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

Nenhuma credencial padrão é fornecida. Informe os dados da empresa, filial e administrador por um gerenciador de segredos ou variáveis de ambiente temporárias e não os inclua em comandos salvos, arquivos versionados ou logs. A senha não é exibida pelo comando. O catálogo inicial contém os exemplos `usuarios.gerenciar` e `venda.cancelar`, concedidos somente ao perfil Administrador; os outros perfis começam sem concessões. O serviço valida e normaliza os dados antes de persistir, usa uma transação e bloqueio no SQL Server, e recusa a execução quando já há dados de provisionamento.

Após configurar `ConnectionStrings__PdvWeb` por um meio seguro, aplique as migrations e execute o bootstrap na raiz do repositório:

```powershell
dotnet ef database update --project src/PDV.Infrastructure/PDV.Infrastructure.csproj --startup-project src/PDV.API/PDV.API.csproj
dotnet run --project src/PDV.API/PDV.API.csproj -- --bootstrap
```

O pré-requisito é .NET 10 SDK, acesso ao SQL Server configurado e migration atualizada. Não há rota pública de bootstrap; executar o comando novamente falha sem alterar os dados existentes.

### Recuperar a assinatura local

Se o bootstrap já criou a empresa, mas não há assinatura, use o comando separado abaixo. Ele não inicia o servidor HTTP, não executa migrations e só funciona em `Development`; também exige confirmação explícita do banco local. Não use em produção nem em qualquer banco cujo destino não tenha sido confirmado.

Na raiz do repositório, confira a connection string localmente sem copiá-la para o chat e execute em PowerShell:

```powershell
$env:ASPNETCORE_ENVIRONMENT = "Development"
dotnet run --project .\src\PDV.API\PDV.API.csproj -- --provisionar-assinatura-local --confirmar-banco-local
```

O comando exige exatamente uma empresa ativa com ao menos uma filial ativa e nenhuma assinatura ou histórico de assinatura. Cria o plano Essencial com 1 filial, 2 caixas e 3 usuários, ou reutiliza o plano ativo com esses mesmos limites sem modificá-lo. Se o plano existente divergir, ou houver empresas, assinaturas, histórico ou estado parcial ambíguos, o comando falha antes de gravar e orienta a revisão manual. A assinatura criada é ativa, replica esses limites e registra mensalidade de R$ 0,00 exclusivamente para desenvolvimento local, sem cobrança. Não existe endpoint HTTP para esta recuperação.

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

Para aplicar migrations ao banco local, com a ferramenta `dotnet-ef` disponível, confira a connection string e o destino do banco, faça backup e revise primeiro o SQL idempotente. Os comandos abaixo são instruções para execução operacional futura; não foram executados pelo agente:

```powershell
dotnet ef migrations script --idempotent --project src\PDV.Infrastructure\PDV.Infrastructure.csproj --startup-project src\PDV.API\PDV.API.csproj --output pdv-migrations.sql
dotnet ef database update --project src\PDV.Infrastructure\PDV.Infrastructure.csproj --startup-project src\PDV.API\PDV.API.csproj
```

## Documentação de referência

- [01 — Visão do Produto](01-VISAO-DO-PRODUTO.md)
- [02 — Escopo do MVP](02-ESCOPO-MVP.md)
- [03 — Requisitos Funcionais e Não Funcionais](03-REQUISITOS-FUNCIONAIS-NAO-FUNCIONAIS.md)
- [04 — Autenticação Web](04-AUTENTICACAO-WEB.md)

Use os documentos 01–03 para decisões de produto e comportamento. Este guia é voltado à estrutura e ao fluxo de desenvolvimento; não substitui nem duplica os requisitos oficiais.
