# PDV Web — Fundação (Fase 1)

Estrutura em camadas e modelo inicial (RF001–RF024 e controle básico de assinaturas).

## Como gerar a solução
1. Copie esta pasta para a raiz do repositório (ficam `fonte/` e `criar-solucao.ps1`).
2. Execute `./criar-solucao.ps1`.
3. Gere a migration inicial (comandos exibidos no final do script).

## Camadas
- **Domain**: entidades e regras puras (sem dependência de framework).
- **Application**: abstrações e, depois, casos de uso. Ainda só `IUsuarioAtual`.
- **Infrastructure**: EF Core + SQL Server, filtros de empresa, interceptor de auditoria.
- **Api**: ASP.NET Core, Swagger, SignalR (hub reservado).
- **Web**: Blazor (Server para o back-office; a frente de caixa será reavaliada na fase offline).

## Decisões já embutidas
- `Guid` gerado na origem em todas as entidades; `RowVersion` para concorrência otimista.
- Isolamento por empresa: filtro global em todas as entidades `IEmpresaScoped`; sem empresa no contexto, nada é retornado. Gravações dessas entidades também exigem uma empresa atual e rejeitam dados de outra empresa.
- Autenticação da API: login por código da empresa + usuário e senha; sessões opacas revogáveis armazenadas como hash, com validade de 8 horas e bloqueio de 15 minutos após 5 falhas.
- Auditoria automática (valores anteriores/posteriores, sem campos "Hash"); registros de auditoria são somente-inserção no código.
- Exclusões em cascata desligadas (restrição de SQL Server e segurança dos dados).

## Pendências e decisões
1. **Administração do SaaS** (planos, assinaturas de todas as empresas): precisa de um contexto fora do tenant (`IgnoreQueryFilters`) e perfil próprio. Decidir quem opera isso no MVP.
2. **Auditoria imutável no banco**: negar UPDATE/DELETE na tabela `Auditoria` ao usuário da aplicação (o interceptor cobre só o código).
3. **Seed**: permissões, perfis iniciais (Administrador, Gerente, Operador, Estoquista) e plano padrão.
4. Completar os demais requisitos de acesso (RF002–RF010), incluindo recuperação de senha, gestão de usuários e permissões, PIN e autenticação completa para operações sensíveis.
5. Autenticação da equipe de suporte técnico (RF175–RF189) permanece futura e requer um modelo separado, inclusive para inatividade e processos ativos.
