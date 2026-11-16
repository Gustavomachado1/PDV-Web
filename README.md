# PDV Web — Sistema de Ponto de Venda SaaS

**Sistema SaaS de Gestão Comercial e Ponto de Venda**

Plataforma web de gestão comercial e ponto de venda destinada a pequenos e médios varejistas, com suporte a múltiplas empresas, filiais e caixas.

O projeto está em desenvolvimento. A fundação técnica, o isolamento por empresa, o fluxo de autenticação e o cadastro inicial do catálogo já estão implementados; os demais módulos de negócio do MVP ainda estão por desenvolver.

## 1. Objetivo do projeto

Desenvolver uma plataforma SaaS que centralize as operações comerciais e administrativas de estabelecimentos varejistas, oferecendo recursos de:

- Gestão de empresas e filiais.
- Controle de usuários, perfis e permissões.
- Ponto de venda (PDV).
- Cadastro e gestão de produtos.
- Controle de estoque e inventário.
- Promoções.
- Gestão de clientes.
- Controle de caixa.
- Relatórios gerenciais.
- Devoluções, créditos e vales-troca.
- Operação offline limitada.
- Preparação fiscal interna.
- Inteligência artificial consultiva.
- Gestão de planos e assinaturas SaaS.
- Notificações.

O desenvolvimento seguirá o escopo aprovado para o MVP, mantendo funcionalidades adicionais para etapas futuras.

## 2. Tecnologias utilizadas

| Tecnologia | Utilização |
|---|---|
| C# | Linguagem principal |
| .NET 10 LTS | Plataforma de desenvolvimento |
| ASP.NET Core | API e aplicações web |
| Blazor Web App (Interactive Server) | Interface web inicial |
| Entity Framework Core 10 | ORM e acesso a dados |
| SQL Server Express | Banco de dados de desenvolvimento local |
| SignalR | Comunicação em tempo real |
| Swagger / Swashbuckle | Documentação e testes da API em desenvolvimento |
| Git / GitHub | Controle de versão |
| Visual Studio Code | Editor |
| GitHub Copilot | Assistência ao desenvolvimento |
| ChatGPT | Apoio à arquitetura, requisitos, documentação e revisão |

A tecnologia da interface da frente de caixa ainda será reavaliada considerando os requisitos de operação offline.

## 3. Arquitetura da solução

A solução é organizada em projetos por responsabilidade e inclui projetos de testes automatizados:

```text
PDV-Web/
├── PDV-Web.slnx
├── src/
│   ├── PDV.API/
│   ├── PDV.Application/
│   ├── PDV.Domain/
│   ├── PDV.Infrastructure/
│   └── PDV.Web/
├── tests/
│   ├── PDV.API.Tests/
│   └── PDV.Infrastructure.Tests/
├── documentation/
│   └── documentation/
│       ├── 01-VISAO-DO-PRODUTO.md
│       ├── 02-ESCOPO-MVP.md
│       ├── 03-REQUISITOS-FUNCIONAIS-NAO-FUNCIONAIS.md
│       └── 04-AUTENTICACAO-WEB.md
└── README.md
```

### Responsabilidades dos projetos

**PDV.Domain**

Contém as entidades fundamentais e regras centrais do domínio.

**PDV.Application**

Contém as abstrações da aplicação; os casos de uso serão desenvolvidos conforme os módulos de negócio forem implementados.

**PDV.Infrastructure**

Responsável pela persistência com Entity Framework Core e SQL Server, filtros de isolamento por empresa e auditoria.

**PDV.API**

Responsável pelos endpoints, autenticação e integração dos serviços. Também expõe Swagger em desenvolvimento, o hub inicial de notificações e a API autenticada para departamentos, categorias, subcategorias, produtos e códigos de barras.

**PDV.Web**

Interface Blazor Web App com renderização interativa no servidor, telas de login e sessão autenticada e o fluxo inicial de gestão do catálogo.

## 4. Funcionalidades previstas para o MVP

As funcionalidades abaixo descrevem o escopo pretendido; exceto pela fundação, autenticação e cadastro inicial do catálogo descritos na seção 6, ainda não representam módulos de negócio concluídos.

### Fundação e segurança
- Autenticação de usuários.
- Gestão de usuários.
- Perfis e permissões.
- Empresas e filiais.
- Isolamento de dados entre empresas.
- Auditoria de operações.
- Gestão de sessões.

### Produtos e operação
- Cadastro centralizado de departamentos, categorias, subcategorias, produtos, unidades de medida e códigos de barras.
- Custos, preços, fiscal, estoque e operação comercial não fazem parte deste fluxo inicial.
- Preparação fiscal interna no cadastro de produtos, sem emissão fiscal.
- Gestão de estoque e inventário.
- Promoções.

### Operação comercial
- Frente de caixa.
- Cadastro e gestão de clientes.
- Operações de caixa.
- Relatórios básicos.
- Devoluções totais e parciais.
- Créditos e vales-troca.

### Recursos complementares
- Operação offline limitada.
- IA consultiva básica.
- Gestão interna de planos e assinaturas SaaS com cobrança manual.
- Notificações.

A implementação seguirá o Escopo do MVP e os Requisitos Funcionais e Não Funcionais oficiais.

## 5. Decisões arquiteturais importantes

### Multiempresa e multifilial

- Os dados de empresas distintas devem permanecer isolados.
- Filiais da mesma empresa podem compartilhar informações conforme as regras e permissões definidas.
- Produtos podem ter cadastros compartilhados entre filiais da mesma empresa.
- Preços e estoques permanecem independentes por filial.

### Auditoria

O sistema registra operações relevantes, identificando empresa, filial, usuário, alterações realizadas e informações necessárias para rastreabilidade. Valores com nomes contendo `Hash` e o token de concorrência não são incluídos nos valores auditados.

### Preparação para operação offline

Mesmo com o desenvolvimento inicial concentrado na versão web, a arquitetura já considera:

- Identificadores GUID gerados na origem por meio de `Guid.CreateVersion7`.
- Controle de concorrência otimista com `RowVersion`.
- Datas registradas em UTC.
- Campos de ocorrência e recebimento na auditoria.
- Preparação para sincronização segura.
- Proteção contra duplicidade de operações, a implementar nos fluxos de negócio.

### Integridade dos dados

As operações devem respeitar as regras de autorização, o isolamento entre empresas e a integridade referencial. As exclusões em cascata das chaves estrangeiras estão desativadas na configuração atual.

## 6. Estado atual do desenvolvimento

**Fundação técnica e autenticação — parcialmente implementadas**

- Solução `PDV-Web.slnx` com cinco projetos de aplicação e dois projetos de testes.
- Entidades fundamentais do domínio, incluindo empresa, filial, usuários, perfis, permissões, sessões, auditoria, planos e assinaturas.
- Interface `IUsuarioAtual`, `PdvDbContext`, configurações do EF Core e migration inicial `Inicial` versionada no repositório.
- Registro de infraestrutura por injeção de dependência e `AuditoriaInterceptor`.
- API com endpoint de saúde (`/saude`), endpoints de login, consulta da sessão e logout, Swagger em desenvolvimento e hub inicial de notificações.
- Autenticação da API por sessão opaca revogável: o token é armazenado como hash, a sessão dura oito horas e cinco tentativas inválidas podem bloquear o usuário por quinze minutos.
- Login e logout integrados à interface Blazor. O navegador recebe um cookie `HttpOnly`, `Secure` e `SameSite=Lax`; o token da API fica no armazenamento de tickets do servidor web, não no armazenamento do navegador.
- Claims de usuário, empresa e filial são obtidas após validação da sessão pela API.
- Filtros globais de consulta para entidades vinculadas a empresas; sem empresa no contexto, essas consultas não retornam registros.
- Novas gravações recebem a empresa do contexto; gravações sem empresa válida ou que tentem alterar dados de outra empresa são rejeitadas.
- Auditoria automática de alterações com valores anteriores e posteriores em JSON. O fluxo normal do EF Core impede alteração ou exclusão de registros de auditoria.

**Validação mais recente:** `dotnet build` concluiu com sucesso e `dotnet test` aprovou os 29 testes da solução.

O provisionamento inicial de empresa, filial, conta Administrador, perfis, permissões e plano é feito pelo comando administrativo de execução única `--bootstrap`. Para recuperar uma assinatura ausente em um banco local já provisionado, há o comando explícito `--provisionar-assinatura-local`, restrito a Development e com confirmação do banco; ele cria a assinatura Essencial ativa sem cobrança somente para desenvolvimento local. Consulte o [guia técnico](documentation/documentation/README.md) para conferir o banco e executar os comandos com segurança. Os demais módulos de negócio do MVP e a proteção da auditoria por permissões do banco de dados permanecem pendentes.

### Migrações e catálogo padrão

As migrações `CatalogoInicial`, `HierarquiaCatalogo` e `HistoricoCodigosBarras` criam as tabelas de departamentos, categorias, subcategorias, produtos e códigos de barras, e cadastram a permissão de catálogo para os perfis Administrador existentes. Elas foram geradas, mas não foram aplicadas a nenhum banco pelo agente. Antes de aplicar, faça backup e confirme que a connection string aponta para o ambiente e banco corretos. Gere e revise o SQL idempotente:

```powershell
dotnet ef migrations script --idempotent --project src\PDV.Infrastructure\PDV.Infrastructure.csproj --startup-project src\PDV.API\PDV.API.csproj --output catalogo-migrations.sql
```

Após revisar o script e obter a aprovação operacional, aplique a migração de forma explícita:

```powershell
dotnet ef database update --project src\PDV.Infrastructure\PDV.Infrastructure.csproj --startup-project src\PDV.API\PDV.API.csproj
```

O seed padrão é uma operação separada, repetível e limitada à empresa informada. Depois de confirmar novamente a connection string, defina `PDV_CATALOGO_EMPRESA_ID`; opcionalmente defina `PDV_CATALOGO_SEGMENTO` como `Mercado/Alimentação`, `Mercado/Farmácia`, `Moda`, `Papelaria/Tech`, `Bazar/Construção` ou `Geral - Uso Interno/Operacional`. Sem o filtro, todas as árvores padrão serão adicionadas:

```powershell
$env:PDV_CATALOGO_EMPRESA_ID = "<GUID-da-empresa>"
$env:PDV_CATALOGO_SEGMENTO = "Moda" # opcional
dotnet run --project src\PDV.API\PDV.API.csproj -- --seed-catalogo-padrao --confirmar-catalogo-padrao
```

O seed não substitui nem exclui cadastros existentes. Códigos removidos do cadastro ativo permanecem preservados e auditáveis, e não podem ser reutilizados por outro produto da empresa. Não execute a migração nem o seed sem backup, validação do ambiente e autorização de quem administra o banco.

O armazenamento de tickets de autenticação da interface web é mantido em memória e atende à configuração atual de instância única. Execuções com múltiplas instâncias ou que precisem sobreviver a reinícios exigirão armazenamento distribuído compartilhado e um chaveiro compartilhado de ASP.NET Core Data Protection. Consulte [a documentação da autenticação web](documentation/documentation/04-AUTENTICACAO-WEB.md).

## 7. Roadmap de desenvolvimento

| Fase | Descrição | Situação |
|---|---|---|
| 1 | Fundação, isolamento e autenticação | Parcialmente implementada; autenticação da API e login web integrados |
| 2 | Produtos, cadastros e estoque | Parcialmente implementada; catálogo inicial disponível |
| 3 | Frente de caixa e relatórios | Planejada |
| 4 | Clientes, devoluções e créditos | Planejada |
| 5 | Operação offline | Planejada |
| 6 | Recursos complementares, IA, assinaturas e notificações | Planejada |

A implementação seguirá a sequência estabelecida no documento oficial de Escopo do MVP.

## 8. Próximas atividades

1. Completar os requisitos de acesso ainda pendentes, como recuperação de senha, gestão de usuários e permissões, PIN e autorização de operações sensíveis.
2. Completar produtos e avançar para condições comerciais, estoque e inventário.
5. Restringir UPDATE e DELETE na tabela de auditoria também por meio das permissões do banco de dados.
6. Definir o contexto administrativo global do SaaS sem comprometer o isolamento entre empresas.
7. Definir os limites concretos de operação offline e avaliar a tecnologia da frente de caixa para esse cenário.

## 9. Documentação oficial

O desenvolvimento deve respeitar os documentos oficiais em [`documentation/documentation/`](documentation/documentation/):

1. **01 — Visão do Produto:** proposta, objetivos e características gerais do sistema.
2. **02 — Escopo do MVP:** funcionalidades da primeira versão e roadmap.
3. **03 — Requisitos Funcionais e Não Funcionais:** comportamentos, regras e requisitos técnicos.
4. **04 — Autenticação Web:** integração do login da interface Blazor com a API e detalhes do armazenamento da sessão.

A documentação é a referência para decisões de arquitetura e implementação. Alterações de código devem respeitar os requisitos aprovados.

## 10. Ferramentas de desenvolvimento e apoio

### GitHub Copilot

Utilizado como assistente no desenvolvimento e gerenciamento da implementação, auxiliando na criação, análise e manutenção do código.

### ChatGPT

Utilizado como apoio em arquitetura e decisões técnicas, análise de requisitos, documentação e revisão de implementações.

### Git e GitHub

Utilizados para versionamento, histórico de alterações e controle do desenvolvimento.

## 11. Diretrizes obrigatórias de desenvolvimento

- Priorizar código organizado, reutilizável e de fácil manutenção.
- Preservar as decisões arquiteturais aprovadas e evitar alterações desnecessárias na arquitetura.
- Consultar a documentação oficial antes de implementar funcionalidades e não inventar regras de negócio.
- Evitar consultas desnecessárias ao banco de dados.
- Priorizar eficiência de processamento e utilização de memória.
- Garantir isolamento entre empresas.
- Respeitar permissões e validar controles de autorização no backend.
- Preservar integridade e rastreabilidade dos dados.
- Considerar a operação offline desde as decisões estruturais.
- Evitar funcionalidades fora do escopo aprovado.
- Validar alterações com compilação e testes.
- Utilizar Git para versionamento e rastreabilidade das alterações.

## 12. Ambiente de desenvolvimento

**Diretório local:**

```text
C:\Projetos\PDV-Web\PDV-Web
```

O projeto utiliza .NET 10 e SQL Server Express para desenvolvimento local. O banco configurado chama-se `PdvWeb`; configure a connection string `PdvWeb` em `src/PDV.API/appsettings.Development.json` para a instância SQL Server disponível no ambiente.

**Compilar a solução:**

```powershell
dotnet build
```

**Executar os testes da solução:**

```powershell
dotnet test
```

**Verificar a versão do .NET:**

```powershell
dotnet --version
```

Para executar localmente, inicie primeiro a API e depois a interface web, em terminais separados:

```powershell
dotnet run --project src/PDV.API/PDV.API.csproj --launch-profile https
```

```powershell
dotnet run --project src/PDV.Web/PDV.Web.csproj --launch-profile https
```

O perfil HTTPS da API usa `https://localhost:7296`, endereço configurado como `Api:BaseUrl` no ambiente de desenvolvimento da interface web. O perfil HTTPS da interface usa `https://localhost:7091`.

As credenciais e configurações locais devem permanecer fora do controle de versão.

## 13. Controle de versão

O Git é utilizado para registrar a evolução do projeto, permitindo acompanhar alterações, recuperar versões anteriores e manter o desenvolvimento organizado.

Recomenda-se registrar cada etapa concluída em commits específicos, com mensagens que identifiquem claramente as alterações.

---

**PDV Web — Desenvolvimento em andamento.**

Plataforma SaaS de gestão comercial e ponto de venda, construída de forma incremental, com foco em segurança, desempenho, integridade dos dados e evolução sustentável.
