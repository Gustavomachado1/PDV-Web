
# 03 — Requisitos Funcionais e Não Funcionais

## 1. Objetivo

Este documento estabelece os requisitos funcionais e não funcionais do PDV Web.

Os requisitos funcionais descrevem as operações e funcionalidades que o sistema deverá oferecer.

Os requisitos não funcionais estabelecem critérios de segurança, desempenho, disponibilidade, usabilidade, integridade e qualidade.

Este documento deverá respeitar os limites definidos no documento **02 — Escopo do MVP**. Requisitos mantidos para evolução futura não serão considerados entregáveis obrigatórios do MVP, mesmo quando detalhados neste documento. A prioridade deverá refletir essa classificação.

## 2. Identificação e classificação dos requisitos

Os requisitos serão identificados pelos seguintes padrões:

- RF: Requisito Funcional.
- RNF: Requisito Não Funcional.
- RN: Regra de Negócio.

Cada requisito deverá possuir identificação única e permanente, descrição objetiva, classificação de escopo, prioridade e critérios de validação.

Quando aplicável, deverá indicar também o módulo relacionado e a referência ao documento de origem, especialmente aos documentos **01 — Visão do Produto** e **02 — Escopo do MVP**.

### 2.1. Classificação de escopo

Todo requisito deverá possuir uma das seguintes classificações:

- **MVP:** requisito aprovado e previsto para implementação na primeira versão, respeitando o documento 02 — Escopo do MVP.
- **Futuro:** requisito preservado para avaliação e implementação em versões posteriores, sem ser considerado obrigatório para a homologação do MVP.
- **A validar:** requisito cuja inclusão no MVP, regra de negócio ou comportamento ainda dependa de decisão ou esclarecimento.

A classificação deverá ser explícita e não poderá ser determinada exclusivamente pela prioridade.

Requisitos classificados como futuros deverão permanecer documentados, sem serem tratados como entregáveis da primeira versão.

### 2.2. Prioridades

Os requisitos deverão possuir uma prioridade compatível com sua importância e classificação:

- **Alta:** essencial para a funcionalidade ou etapa de desenvolvimento correspondente.
- **Média:** importante, mas pode ser implementado após os requisitos de maior prioridade.
- **Baixa:** desejável ou de menor urgência, podendo ser avaliado para etapas posteriores.

A prioridade não altera a classificação de escopo. Um requisito de prioridade alta classificado como futuro não será automaticamente incorporado ao MVP.

### 2.3. Critérios de validação

Cada requisito deverá possuir critérios de validação objetivos, permitindo verificar se o comportamento previsto foi implementado corretamente.

Os critérios deverão descrever resultados observáveis, condições de execução, validações necessárias e comportamentos esperados.

Quando existirem exceções, restrições ou situações de falha relevantes, elas deverão ser contempladas nos critérios de validação.

Requisitos que ainda não possuam critérios suficientes deverão permanecer identificados como pendentes de detalhamento antes do início de sua implementação.

### 2.4. Regras de identificação e rastreabilidade

- Cada código de requisito deverá ser único dentro de seu respectivo tipo.
- Códigos já utilizados não deverão ser reutilizados, mesmo quando um requisito for removido, substituído ou transferido para uma versão futura.
- Requisitos novos deverão receber identificadores sequenciais, respeitando os códigos existentes.
- Requisitos relacionados deverão possuir referências cruzadas quando necessário.
- Regras de negócio compartilhadas por diferentes módulos deverão ser centralizadas, evitando duplicidade e contradições.
- Alterações de requisitos deverão preservar o histórico e identificar o motivo da mudança.
- Toda alteração deverá ser avaliada em relação à Visão do Produto e ao Escopo do MVP.
- Divergências entre documentos deverão ser resolvidas antes da implementação das funcionalidades afetadas.

### 2.5. Estrutura mínima de documentação

Sempre que aplicável, os requisitos deverão ser organizados com os seguintes campos:

- **Código:** identificação única.
- **Descrição:** comportamento que o sistema deverá oferecer.
- **Classificação:** MVP, Futuro ou A validar.
- **Prioridade:** Alta, Média ou Baixa.
- **Critérios de validação:** condições objetivas para verificar o atendimento do requisito.
- **Referências:** módulos, regras de negócio e documentos relacionados.

A ausência de informações necessárias deverá ser registrada como pendência de detalhamento, sem presumir decisões que ainda não tenham sido aprovadas.

### Prioridades

- Alta: obrigatório para o funcionamento do MVP.
- Média: importante, mas pode ser implementado em uma fase posterior do MVP.
- Baixa: desejável ou previsto para evolução futura.

### Regra transversal de autorização

**RN001 — Mecanismo global de autorização por permissão específica**

Toda operação que exigir autorização deverá utilizar um mecanismo único e padronizado, independente do módulo em que a operação ocorrer.

Regras:

- A autorização poderá ser concedida por qualquer usuário que possua a permissão específica exigida pela operação, independentemente de seu cargo ou perfil geral.
- O usuário que solicitar a autorização não poderá autorizar a própria operação.
- O sistema deverá permitir solicitar a autorização de outro usuário que possua a permissão específica necessária. O usuário autorizador deverá realizar sua própria autenticação em uma interface protegida, sem compartilhar sua senha com o solicitante. As credenciais não poderão ser visualizadas, armazenadas ou utilizadas pelo usuário solicitante. A autorização deverá ficar vinculada exclusivamente à operação solicitada.
- A autorização deverá identificar a operação solicitada, o usuário solicitante, o usuário autorizador, a data e o horário, a justificativa quando aplicável e o resultado da validação.
- O mecanismo deverá ser reutilizado pelos módulos do sistema, evitando fluxos de autorização independentes e duplicados.
- A validação deverá ocorrer no backend; a interface não poderá ser a única barreira de segurança.
- As permissões deverão ser específicas para cada ação, sem conceder poderes adicionais não relacionados à operação solicitada.

# 3. Requisitos funcionais

## 3.1. Acesso e autenticação

| Código | Requisito | Prioridade |
|---|---|---|
| RF001 | O sistema deverá permitir autenticação por usuário e senha. | Alta |
| RF002 | O sistema deverá permitir recuperação de senha. | Alta |
| RF003 | O sistema deverá permitir cadastro, edição, bloqueio e inativação de usuários. | Alta |
| RF004 | O sistema deverá disponibilizar os perfis Administrador, Gerente, Operador e Estoquista. | Alta |
| RF005 | O sistema deverá permitir configurar permissões por perfil e por usuário. | Alta |
| RF006 | O sistema deverá controlar sessões e permitir encerramento de sessões. | Alta |
| RF007 | O sistema deverá limitar tentativas de autenticação e bloquear acessos conforme configuração. | Alta |
| RF008 | O sistema deverá registrar data, hora e usuário das autenticações. | Alta |
| RF009 | O sistema deverá permitir autenticação rápida por PIN nas operações de caixa. | Alta |
| RF010 | O sistema deverá exigir autenticação completa para operações administrativas e autorizações sensíveis. | Alta |

## 3.2. Empresas e filiais

| Código | Requisito | Prioridade |
|---|---|---|
| RF011 | O sistema deverá permitir cadastrar empresas. | Alta |
| RF012 | O sistema deverá permitir cadastrar múltiplas filiais por empresa. | Alta |
| RF013 | O sistema deverá permitir vincular usuários a empresas e filiais. | Alta |
| RF014 | O sistema deverá permitir configurar permissões de acesso por filial. | Alta |
| RF015 | O sistema deverá manter isolamento dos dados entre empresas. | Alta |
| RF016 | O sistema deverá permitir compartilhar cadastros de produtos entre filiais da mesma empresa. | Alta |
| RF017 | O sistema deverá permitir preços e estoques independentes por filial. | Alta |

## 3.3. Auditoria

| Código | Requisito | Prioridade |
|---|---|---|
| RF018 | O sistema deverá registrar operações críticas realizadas pelos usuários. | Alta |
| RF019 | O sistema deverá registrar valores anteriores e posteriores às alterações. | Alta |
| RF020 | O sistema deverá identificar o usuário responsável pela operação. | Alta |
| RF021 | O sistema deverá identificar o usuário que autorizou operações restritas. | Alta |
| RF022 | O sistema deverá exigir justificativa para operações que necessitem de autorização. | Alta |
| RF023 | O sistema deverá permitir consulta ao histórico de auditoria conforme permissões. | Alta |
| RF024 | O sistema deverá impedir alterações ou exclusões não autorizadas dos registros de auditoria. | Alta |

# 4. Requisitos não funcionais

## 4.1. Segurança

| Código | Requisito | Prioridade |
|---|---|---|
| RNF001 | O sistema deverá utilizar comunicação criptografada entre cliente, API e servidor. | Alta |
| RNF002 | Senhas deverão ser armazenadas utilizando algoritmos seguros de hash, nunca em texto puro. | Alta |
| RNF003 | O backend deverá validar permissões e autorizações independentemente das validações da interface. | Alta |
| RNF004 | Os dados de uma empresa não poderão ser acessados por usuários de outra empresa sem autorização explícita. | Alta |
| RNF005 | Informações sensíveis deverão possuir controles de acesso adequados. | Alta |
| RNF006 | O sistema deverá registrar tentativas de acesso indevido e operações suspeitas. | Alta |

## 4.2. Desempenho

| Código | Requisito | Prioridade |
|---|---|---|
| RNF007 | As operações comuns do PDV deverão apresentar resposta rápida, mesmo com múltiplos caixas ativos. | Alta |
| RNF008 | Relatórios pesados deverão ser processados em segundo plano. | Alta |
| RNF009 | Processamentos pesados deverão possuir limites configuráveis de consumo de recursos. | Alta |
| RNF010 | O processamento de relatórios não poderá comprometer as operações de venda. | Alta |
| RNF011 | O sistema deverá permitir acompanhar o progresso de tarefas demoradas, quando aplicável. A instalação de atualizações seguirá a comunicação simplificada definida no RF352, sem barra de progresso ou percentual. | Média |
| RNF012 | O sistema deverá permitir cancelar processamentos que ainda não tenham sido concluídos. | Média |

## 4.3. Disponibilidade e continuidade

| Código | Requisito | Prioridade |
|---|---|---|
| RNF013 | O sistema deverá permitir operação offline limitada às vendas e aos pagamentos básicos, sem disponibilizar nesse modo operações administrativas ou funcionalidades complexas de promoções. Os limites de tempo, quantidade de vendas e valores deverão ser configuráveis, conforme o escopo do MVP; seus parâmetros serão definidos no detalhamento técnico. | Alta |
| RNF014 | Os dados necessários às operações offline deverão permanecer disponíveis localmente. | Alta |
| RNF015 | O sistema deverá sincronizar operações pendentes após o restabelecimento da conexão. | Alta |
| RNF016 | A sincronização deverá possuir mecanismos para impedir duplicidade de operações. | Alta |
| RNF017 | O sistema deverá identificar conflitos e divergências durante a sincronização. | Alta |
| RNF018 | O sistema deverá possuir mecanismos de backup e recuperação de dados. | Alta |

## 4.4. Usabilidade e acessibilidade

| Código | Requisito | Prioridade |
|---|---|---|
| RNF019 | A interface deverá ser intuitiva e de fácil aprendizado. | Alta |
| RNF020 | As operações principais deverão permitir utilização prioritária pelo teclado. | Alta |
| RNF021 | Botões e comandos deverão possuir identificação clara. | Alta |
| RNF022 | Mensagens de erro deverão explicar o problema e orientar o usuário. | Alta |
| RNF023 | O sistema deverá permitir utilização por diferentes tamanhos de tela. | Média |
| RNF024 | A interface deverá oferecer recursos básicos de acessibilidade. | Média |

## 4.5. Manutenibilidade e escalabilidade

| Código | Requisito | Prioridade |
|---|---|---|
| RNF025 | O sistema deverá utilizar arquitetura organizada e separação de responsabilidades. | Alta |
| RNF026 | As regras de negócio deverão ser centralizadas na camada apropriada do backend. | Alta |
| RNF027 | O sistema deverá permitir expansão de módulos sem exigir reescrita completa da aplicação. | Alta |
| RNF028 | O sistema deverá possuir testes automatizados para operações críticas. | Alta |
| RNF029 | O sistema deverá registrar erros técnicos com informações suficientes para investigação. | Alta |
| RNF030 | O sistema deverá possuir documentação técnica atualizada. | Alta |
| RNF031 | O sistema deverá priorizar processamento orientado a eventos ou alterações relevantes, evitando consultas, verificações, cálculos e atualizações repetitivas quando não houver mudança que os justifique, sem comprometer a consistência e a segurança dos dados. | Alta |

# 5. Diretrizes de validação

Cada requisito deverá ser validado por meio de pelo menos um dos seguintes métodos:

- Testes funcionais.
- Testes automatizados.
- Testes de integração.
- Testes de segurança.
- Testes de desempenho.
- Homologação com usuários.

Os requisitos poderão ser detalhados e complementados durante o levantamento, desde que as alterações sejam documentadas e não provoquem contradições com as decisões já aprovadas.

# 6. Controle de versão

| Versão | Descrição |
|---|---|
| 1.0 | Criação da estrutura inicial de requisitos do PDV Web. |
| 1.1 | Revisão de consistência: correção de duplicidades e identificadores, consolidação de regras de autorização, alinhamento de desempenho e operação offline e correções de formatação. |
| 1.2 | Alinhamento com o Escopo do MVP: reclassificação de lotes/validade e transferências avançadas como evolução futura, identificação das seções de inventário avançado fora do MVP e explicitação dos limites configuráveis da operação offline. |

# 7. Requisitos funcionais — Produtos e formação de preços

## 7.1. Cadastro de produtos

| Código | Requisito | Prioridade |
|---|---|---|
| RF031 | O sistema deverá permitir cadastrar produtos. | Alta |
| RF032 | O sistema deverá permitir editar os dados dos produtos conforme permissões. | Alta |
| RF033 | O sistema deverá permitir ativar, inativar e bloquear produtos. | Alta |
| RF034 | O sistema deverá permitir cadastrar descrição, categoria, unidade de medida e demais informações do produto. | Alta |
| RF035 | O sistema deverá permitir cadastrar um ou mais códigos de barras vinculados ao mesmo produto. | Alta |
| RF036 | Cada código de barras deverá ser único entre os produtos ativos da mesma empresa, não podendo identificar simultaneamente produtos diferentes. | Alta |
| RF037 | O sistema deverá permitir cadastrar produtos sem código de barras. | Alta |
| RF038 | O sistema deverá permitir pesquisar produtos por descrição ou código de barras. | Alta |
| RF039 | A pesquisa por descrição deverá aceitar correspondência parcial. | Alta |
| RF040 | O sistema deverá bloquear por padrão a venda de produtos inativos, permitindo exceções conforme configuração da empresa ou filial e as regras de autorização definidas no RF106. Produtos com situação bloqueada não poderão ser vendidos. | Alta |
| RF041 | O sistema deverá permitir associar imagens aos produtos. | Média |
| RF042 | O sistema poderá buscar imagens utilizando código EAN ou descrição, permitindo validação antes da associação. | Média |

## 7.2. Custos e formação de preços

| Código | Requisito | Prioridade |
|---|---|---|
| RF043 | O sistema deverá permitir informar e editar o custo dos produtos conforme permissões. | Alta |
| RF044 | O sistema deverá armazenar o histórico de alterações de custo. | Alta |
| RF045 | O sistema deverá permitir trabalhar com custo médio e último custo de aquisição. | Alta |
| RF046 | O sistema deverá permitir configurar markup por produto. | Alta |
| RF047 | O sistema deverá calcular o preço sugerido com base no custo e markup. | Alta |
| RF048 | O sistema deverá permitir editar o markup e visualizar a simulação do preço resultante. | Alta |
| RF049 | O sistema deverá permitir informar preço de venda manualmente, respeitando as permissões. | Alta |
| RF050 | O sistema deverá permitir preços diferentes por filial. | Alta |
| RF051 | O sistema deverá manter histórico de alterações de preços. | Alta |
| RF052 | Alterações de custo não deverão modificar automaticamente o preço de venda. | Alta |

## 7.3. Regras de formação de preços

O cálculo de preço sugerido deverá utilizar a seguinte fórmula:

Preço sugerido = Custo × (1 + Markup / 100)

Exemplo:

- Custo: R$ 10,00
- Markup: 50%
- Preço sugerido: R$ 15,00

O markup representa um percentual aplicado sobre o custo, não devendo ser confundido com margem de lucro.

O sistema deverá permitir que o usuário visualize o resultado antes de confirmar qualquer alteração de preço.

## 7.4. Regras de integridade

- Um produto não poderá possuir mais de um código de barras.
- Não poderão existir códigos de barras duplicados dentro da mesma empresa.
- Produtos inativos ou bloqueados não poderão ser incluídos em novas vendas.
- Alterações de custo e preço deverão ser auditáveis.
- A alteração de preço deverá respeitar as permissões do usuário.
- Produtos compartilhados entre filiais poderão possuir preços e estoques independentes.

## 7.5. Unidades de venda e embalagens

### Regras do MVP

- Cada embalagem será cadastrada como um produto independente.
- Cada embalagem possuirá código de barras próprio, quando disponível.
- Cada embalagem possuirá preço e estoque independentes.
- Não haverá conversão automática entre embalagens no MVP.

### Evolução futura

A arquitetura deverá permitir a implementação de múltiplas unidades de venda vinculadas a um mesmo produto.

Essa evolução deverá contemplar:

- Cadastro de unidades alternativas.
- Fator de conversão entre unidades.
- Conversão automática de estoque.
- Preços específicos por unidade de venda.
- Rastreabilidade das movimentações.

**Exemplo:** a venda de um fardo com 6 unidades poderá representar a baixa de 6 unidades individuais do estoque.

# 8. Requisitos funcionais — Gestão de estoque

## 8.1. Controle de estoque

| Código | Requisito | Prioridade |
|---|---|---|
| RF053 | O sistema deverá controlar o estoque individualmente por filial. | Alta |
| RF054 | O sistema deverá realizar baixa automática do estoque após a finalização de uma venda. | Alta |
| RF055 | O sistema deverá permitir entradas manuais de estoque mediante autorização. | Alta |
| RF056 | O sistema deverá permitir saídas manuais justificadas e autorizadas. | Alta |
| RF057 | O sistema deverá manter histórico de todas as movimentações. | Alta |
| RF058 | O sistema deverá permitir consultar o saldo atual de cada produto. | Alta |
| RF059 | O sistema deverá permitir configurar estoque mínimo e estoque ideal. | Alta |
| RF060 | O sistema deverá gerar alertas de estoque baixo. | Alta |
| RF061 | O sistema deverá bloquear vendas que ultrapassem o estoque disponível. Quando o estoque negativo estiver habilitado, a operação poderá ser liberada por usuário com a permissão específica ou mediante autorização conforme a RN001. Quando o estoque negativo estiver desabilitado, a venda deverá permanecer bloqueada, mesmo mediante autorização. | Alta |
| RF062 | O sistema deverá identificar o usuário responsável e o autorizador das movimentações. | Alta |

## 8.2. Inventário

| Código | Requisito | Prioridade |
|---|---|---|
| RF063 | O sistema deverá permitir realizar inventários de estoque. | Alta |
| RF064 | O sistema deverá permitir inventários completos ou parciais. | Alta |
| RF065 | O sistema deverá comparar quantidade contada com saldo registrado. | Alta |
| RF066 | O sistema deverá identificar divergências de inventário. | Alta |
| RF067 | O sistema deverá exigir justificativa para ajustes decorrentes de divergências. | Alta |
| RF068 | O sistema deverá registrar responsável, data, horário e valores do ajuste. | Alta |

## 8.3. Lotes e validade

| Código | Requisito | Prioridade |
|---|---|---|
| RF069 | O sistema deverá permitir controlar lotes de produtos. | Baixa |
| RF070 | O sistema deverá permitir registrar datas de fabricação e validade, quando aplicáveis. | Baixa |
| RF071 | O sistema deverá alertar sobre produtos próximos do vencimento. | Baixa |
| RF072 | O sistema deverá impedir a venda de produtos vencidos. | Baixa |
| RF073 | O sistema deverá priorizar a saída de produtos com vencimento mais próximo (FEFO). | Baixa |
| RF074 | O sistema deverá permitir registrar baixas justificadas de produtos vencidos ou danificados. | Baixa |

## 8.4. Transferências entre filiais

| Código | Requisito | Prioridade |
|---|---|---|
| RF075 | O sistema deverá permitir transferências de produtos entre filiais da mesma empresa. | Baixa |
| RF076 | O sistema deverá registrar a filial de origem e a filial de destino. | Baixa |
| RF077 | O sistema deverá permitir aprovação de transferências conforme permissões. | Baixa |
| RF078 | O sistema deverá controlar produtos em trânsito. | Baixa |
| RF079 | O sistema deverá registrar o recebimento da transferência na filial de destino. | Baixa |
| RF080 | O sistema deverá manter histórico das transferências realizadas. | Baixa |

## 8.5. Curva ABC e reposição

| Código | Requisito | Prioridade |
|---|---|---|
| RF081 | O sistema deverá calcular a curva ABC dos produtos. | Baixa |
| RF082 | O cálculo deverá considerar valor de estoque e consumo dos produtos. | Baixa |
| RF083 | O sistema deverá permitir configurar os percentuais das faixas ABC. | Baixa |
| RF084 | O sistema deverá sugerir reposição com base no estoque mínimo, ideal e consumo. | Baixa |
| RF085 | As sugestões de reposição não deverão gerar compras ou movimentações automaticamente. | Baixa |

## 8.6. Regras de integridade do estoque

- Toda movimentação deverá possuir identificação única.
- Movimentações concluídas não poderão ser excluídas ou alteradas sem rastreabilidade.
- Correções deverão gerar novos movimentos de ajuste.
- A baixa de estoque deverá ocorrer de forma consistente com a finalização da venda.
- O sistema deverá impedir que duas operações concorrentes comprometam indevidamente o saldo.
- Quando o controle de lotes e validade for disponibilizado em versão futura, produtos vencidos deverão permanecer bloqueados para venda.
- Quando as transferências entre filiais forem disponibilizadas em versão futura, deverão manter rastreabilidade desde a saída até o recebimento.
- O estoque deverá ser isolado por empresa e filial.

## 8.7. Comportamento durante inventários — evolução futura, fora do MVP

O sistema deverá permitir configurar o comportamento operacional durante a realização de inventários, individualmente por empresa ou filial.

### Modalidade 1 — Inventário com bloqueio

- Produtos em inventário ficarão temporariamente bloqueados para vendas e movimentações.
- O bloqueio será encerrado após a conclusão ou cancelamento do inventário.
- O sistema deverá informar quais produtos estão bloqueados.

### Modalidade 2 — Inventário sem bloqueio

- As vendas e movimentações continuarão normalmente.
- O sistema deverá registrar todas as movimentações realizadas durante o inventário.
- O cálculo da divergência deverá considerar as movimentações ocorridas após o início da contagem.
- O sistema deverá manter o saldo de referência utilizado no inventário.

### Regras gerais

- A modalidade deverá ser configurável por empresa ou filial.
- Alterações de configuração deverão ser auditadas.
- O sistema deverá impedir ajustes duplicados decorrentes do mesmo inventário.
- O histórico deverá permitir identificar o saldo original, a quantidade contada e as movimentações consideradas.

## 8.8. Modalidades de contagem de inventário — evolução futura, fora do MVP

O sistema deverá permitir configurar a modalidade de contagem por empresa ou filial.

### Modalidade 1 — Contagem aberta

- O funcionário poderá visualizar o saldo registrado no sistema.
- O funcionário informará a quantidade física encontrada.
- O sistema apresentará a divergência após a contagem.

### Modalidade 2 — Contagem cega

- O funcionário não poderá visualizar o saldo registrado no sistema.
- O funcionário informará somente a quantidade física encontrada.
- O sistema realizará a comparação internamente.
- A divergência será apresentada conforme as permissões do usuário.

### Regras gerais

- A modalidade deverá ser configurável por empresa ou filial.
- A configuração deverá ser registrada em auditoria.
- O histórico deverá identificar a modalidade utilizada.
- O resultado deverá preservar a quantidade contada e o saldo utilizado na comparação.

## 8.9. Aprovação de divergências de inventário — evolução futura, fora do MVP

- Toda divergência identificada em inventário deverá exigir aprovação de usuário que possua a permissão específica correspondente.
- O sistema não deverá ajustar automaticamente o estoque após a contagem.
- O funcionário responsável deverá informar uma justificativa para a divergência.
- O sistema deverá apresentar saldo registrado, quantidade contada e diferença.
- O responsável pela aprovação deverá ser identificado.
- O ajuste somente poderá ser efetivado após a aprovação.
- Divergências rejeitadas deverão permanecer registradas, sem alteração do estoque.
- Toda a operação deverá ser armazenada no histórico de auditoria.

## 8.10. Gestão de produtos próximos do vencimento — evolução futura, fora do MVP

### Alertas de validade

- O sistema deverá identificar produtos próximos do vencimento.
- Os alertas deverão ser direcionados aos usuários autorizados, principalmente gerentes e estoquistas.
- Os períodos de alerta deverão ser configuráveis por empresa ou filial.
- O sistema deverá permitir consultar produtos vencidos e próximos do vencimento.

### Bloqueio automático

- O sistema deverá permitir configurar o bloqueio automático de produtos conforme a quantidade de dias restantes para o vencimento.
- A configuração poderá variar por empresa, filial ou produto.
- Produtos vencidos deverão permanecer bloqueados para venda.
- O sistema deverá registrar os bloqueios realizados.

### Sugestões de promoções

- O sistema deverá identificar produtos próximos do vencimento que possam se beneficiar de uma promoção.
- Deverá sugerir possíveis descontos ou campanhas.
- As sugestões poderão considerar quantidade em estoque, validade e histórico de vendas.
- A ativação de qualquer promoção dependerá de aprovação de usuário autorizado.
- O sistema não poderá alterar preços ou ativar promoções automaticamente.

### Rastreabilidade

- O sistema deverá identificar o lote e a validade dos produtos envolvidos.
- As sugestões deverão possuir histórico.
- As aprovações e rejeições deverão ser registradas em auditoria.

## 8.11. Processo completo de transferência entre filiais — evolução futura, fora do MVP

As transferências deverão seguir um fluxo controlado, com rastreabilidade desde a solicitação até o recebimento.

### Etapas

1. Solicitação da transferência pela filial de destino.
2. Aprovação por usuário autorizado.
3. Separação dos produtos pela filial de origem.
4. Conferência dos produtos separados.
5. Confirmação do envio.
6. Registro dos produtos em trânsito.
7. Recebimento e conferência pela filial de destino.
8. Finalização da transferência.

### Regras de estoque

- A solicitação não deverá alterar o estoque.
- A aprovação não deverá alterar o estoque físico.
- No envio, as quantidades deverão sair do estoque disponível da origem.
- Durante o transporte, as quantidades deverão permanecer registradas como estoque em trânsito.
- No recebimento, as quantidades confirmadas deverão entrar no estoque da filial de destino.
- Divergências deverão ser registradas e tratadas antes da conclusão definitiva.

### Rastreabilidade

- Identificação da filial de origem e destino.
- Usuário solicitante.
- Usuário aprovador.
- Responsável pela separação.
- Responsável pelo envio.
- Responsável pelo recebimento.
- Data e horário de cada etapa.
- Quantidades solicitadas, enviadas e recebidas.
- Justificativas para divergências.
- Histórico completo da transferência.

### Restrições

- Transferências somente poderão ocorrer entre filiais da mesma empresa.
- Não será permitido concluir transferências sem confirmação de recebimento.
- Transferências canceladas deverão preservar seu histórico.
- Todas as movimentações deverão ser auditáveis.

### 8.12. Tratamento de divergências no recebimento de transferências — evolução futura, fora do MVP

**RF086 — Bloqueio por divergência no recebimento**

Quando a quantidade recebida for diferente da quantidade enviada, o sistema deverá impedir a conclusão definitiva da transferência até que a divergência seja analisada e autorizada por um usuário que possua a permissão específica correspondente.

**RF087 — Registro de ocorrência**

O sistema deverá registrar automaticamente uma ocorrência contendo:
- Quantidade enviada.
- Quantidade recebida.
- Quantidade divergente.
- Filial de origem e destino.
- Data e horário do recebimento.
- Usuário responsável pelo recebimento.

**RF088 — Análise e autorização**

O usuário que possua a permissão específica correspondente deverá analisar a divergência, registrar uma justificativa e definir o tratamento adequado. O sistema deverá armazenar o responsável pela decisão, a data, o horário e o histórico da autorização.

Tratamentos possíveis, conforme a análise e a autorização:
- Baixa da quantidade faltante como perda ou avaria, mediante justificativa.
- Reenvio das unidades faltantes pela filial de origem, mediante nova movimentação.
- Outra resolução adequada à investigação, desde que justificada e autorizada.

Regras complementares:
- Toda resolução deverá registrar responsável, autorizador, data, horário, quantidades e justificativa.
- O histórico original da transferência e da divergência deverá ser preservado.
- Nenhuma divergência poderá gerar perda ou ajuste automático de estoque sem análise e autorização.

### 8.13. Controle de quantidade de produtos no carrinho

**RF391 — Validação de estoque no carrinho e na finalização da venda**

O sistema deverá validar a disponibilidade de estoque ao adicionar produtos ou aumentar suas quantidades no carrinho e novamente antes da conclusão da venda.

**Regras:**

1. Ao adicionar um produto ou aumentar sua quantidade, o sistema deverá verificar a disponibilidade de estoque na filial.
2. Caso a quantidade solicitada ultrapasse o estoque disponível, deverão ser aplicadas as regras dos RF061 e RF108.
3. Quando o estoque negativo estiver desabilitado, a operação deverá ser bloqueada, mesmo mediante autorização.
4. Quando o estoque negativo estiver habilitado, a operação poderá prosseguir por usuário que possua a permissão específica correspondente ou mediante autorização concedida conforme a RN001.
5. A autorização deverá estar vinculada ao produto, à quantidade solicitada e à operação correspondente.
6. Antes de finalizar a venda, o backend deverá validar novamente a disponibilidade de estoque, as configurações e as permissões aplicáveis.
7. Caso o estoque tenha sido alterado por outra operação enquanto a venda estava em andamento, o sistema deverá reavaliar a disponibilidade e aplicar as regras de estoque insuficiente.
8. A validação definitiva e a baixa de estoque deverão ocorrer de forma consistente, protegendo contra operações simultâneas que comprometam indevidamente os saldos.
9. A venda suspensa não deverá reservar estoque, conforme definido no RF109. A disponibilidade deverá ser validada novamente quando a venda for retomada e finalizada.
10. Toda operação que resultar em estoque negativo deverá manter os registros necessários para auditoria, incluindo produto, quantidade, saldo anterior, saldo resultante, operador e autorizador, quando aplicável.

### 8.14 Congelamento de preços durante a venda

**RF392 — Preservação do preço no carrinho**

- Ao adicionar um produto ao carrinho, o sistema deverá registrar o preço vigente naquele momento.
- Alterações posteriores no cadastro ou na tabela de preços não deverão modificar automaticamente o valor dos produtos já adicionados.
- O preço registrado deverá ser preservado até a conclusão ou cancelamento da venda.
- O backend deverá garantir que o valor utilizado na finalização corresponda ao preço registrado no item da venda.
- O histórico da venda deverá preservar o preço unitário aplicado.

### 8.15 Permissão para remoção de produtos do carrinho

**RF393 — Controle de permissão para remoção de itens**

- A remoção de produtos do carrinho deverá ser controlada por permissões individuais de usuário.
- A empresa poderá configurar quais perfis ou usuários podem remover itens sem autorização adicional.
- Usuários sem permissão deverão solicitar autorização de usuário que possua a permissão específica correspondente.
- A autorização deverá registrar o responsável, operador solicitante, produto, quantidade, data, horário e justificativa, quando aplicável.
- Toda remoção deverá ser registrada no histórico da venda para auditoria.
- A permissão para remover itens deverá ser independente das permissões para cancelar vendas em andamento ou finalizadas.

### 8.17 Venda de produtos pesáveis

**RF090 — Controle de quantidades fracionadas**

- O cadastro do produto deverá possuir uma configuração que indique se ele é pesável.
- Somente produtos configurados como pesáveis poderão ser vendidos com quantidades fracionadas.
- A precisão decimal deverá ser configurável, permitindo definir a quantidade de casas decimais aceita.
- O sistema deverá validar a quantidade informada conforme a configuração do produto.
- O cálculo do valor total deverá considerar a quantidade fracionada e o preço unitário.
- As validações deverão ocorrer também no backend.

### 8.18 Configuração do valor recebido por forma de pagamento

**RF091 — Controle do valor recebido**

- A exigência de informar o valor recebido deverá ser configurável por empresa/filial e por forma de pagamento.
- Quando obrigatório, o operador deverá informar o valor recebido antes de concluir o pagamento.
- Para pagamentos em dinheiro, o sistema deverá calcular automaticamente o troco quando aplicável.
- Quando não obrigatório, o sistema poderá considerar o pagamento pelo valor exato da operação.
- A configuração deverá ser respeitada tanto na interface quanto no backend.
- O valor recebido, o valor efetivamente pago e o troco deverão ser registrados na operação.

### 8.19 Pagamento dividido

**RF092 — Configuração de múltiplas formas de pagamento**

- O sistema deverá permitir que uma venda seja paga utilizando mais de uma forma de pagamento.
- A funcionalidade deverá ser configurável por empresa/filial.
- Quando desabilitada, o sistema deverá aceitar somente uma forma de pagamento por venda.
- Quando habilitada, o operador poderá distribuir o valor entre as formas de pagamento disponíveis.
- O sistema deverá validar se a soma dos pagamentos corresponde ao valor total da venda, considerando as regras de troco.
- Cada parcela deverá registrar forma de pagamento, valor, usuário responsável e demais informações necessárias para auditoria.
- O backend deverá validar os valores antes da conclusão da venda.

### 8.20 Cancelamento de tentativa de pagamento

**RF093 — Troca de forma de pagamento após falha**

- Quando uma tentativa de pagamento não for concluída ou confirmada, o operador poderá cancelar a tentativa.
- O cancelamento da tentativa não deverá cancelar a venda nem remover os produtos do carrinho.
- Após o cancelamento, o sistema deverá permitir iniciar outra tentativa utilizando uma forma de pagamento disponível.
- O histórico das tentativas deverá ser preservado, incluindo forma de pagamento, valor, data, horário e resultado.
- O sistema deverá diferenciar o cancelamento de uma tentativa de pagamento do cancelamento efetivo da operação no provedor financeiro.
- O backend deverá tratar confirmações tardias para evitar pagamentos duplicados ou inconsistências financeiras.

### 8.21 Emissão e disponibilização de comprovantes

**RF094 — Configuração de comprovantes de venda**

- A empresa/filial poderá configurar o comportamento de emissão dos comprovantes.
- O sistema deverá permitir impressão automática ou manual.
- A impressão automática deverá ocorrer após a conclusão da venda, quando houver impressora configurada e disponível.
- Na impressão manual, o operador poderá escolher se deseja emitir o comprovante.
- O comprovante deverá possuir versão digital disponível para consulta e compartilhamento.
- O documento deverá apresentar os dados essenciais da venda, produtos, quantidades, valores, descontos e formas de pagamento.
- O sistema deverá manter o histórico de emissão e reimpressão dos comprovantes.
- A indisponibilidade da impressora não deverá desfazer uma venda já concluída.

### 8.22 Identificação do cliente na venda

**RF095 — Configuração da obrigatoriedade de identificação**

- A identificação do cliente deverá ser opcional por padrão.
- A empresa/filial poderá configurar quando a identificação será obrigatória.
- A obrigatoriedade poderá variar conforme a forma de pagamento ou o tipo de operação.
- O sistema deverá permitir identificar o cliente por CPF ou CNPJ, conforme os dados cadastrados.
- Quando a identificação for obrigatória, o sistema deverá impedir a finalização até que os dados necessários sejam informados.
- As regras deverão ser validadas tanto na interface quanto no backend.
- O sistema deverá preservar o vínculo entre cliente e venda para consultas e relatórios posteriores.

### 8.23 Identificação de clientes não cadastrados

**RF096 — Venda para cliente não cadastrado**

- Quando o operador informar um CPF ou CNPJ não cadastrado, o sistema deverá permitir continuar a venda sem exigir a criação de um cadastro permanente.
- O operador não deverá ser obrigado a preencher um cadastro completo para concluir a operação.
- Quando a identificação for obrigatória, o documento informado deverá ser preservado nos dados da venda.
- O sistema não deverá criar automaticamente um cadastro permanente de cliente.
- A venda deverá manter o registro da identificação informada para consultas e auditoria.

### 8.24 Exibição de dados do cliente na venda

**RF097 — Resumo cadastral do cliente**

- Ao identificar um cliente cadastrado, o sistema deverá apresentar seu nome ou razão social.
- Deverá apresentar CPF ou CNPJ e telefone, quando disponíveis.
- Deverá permitir a conferência dos dados básicos do cadastro sem sair da tela de venda.
- A ausência de telefone ou de outras informações opcionais não deverá impedir a continuidade da venda.
- O sistema deverá respeitar as permissões de acesso aos dados cadastrais.

### 8.25 Alteração do cliente durante a venda

**RF098 — Autorização para troca ou remoção de cliente**

- A troca ou remoção do cliente vinculado a uma venda em andamento deverá exigir autorização de usuário que possua a permissão específica correspondente.
- A autorização deverá exigir autenticação do responsável.
- O sistema deverá registrar o cliente anterior e o novo cliente, quando houver.
- Deverão ser registrados o operador solicitante, o responsável pela autorização, a data, o horário e a justificativa.
- O histórico da alteração deverá ser preservado para auditoria.
- O backend deverá validar a autorização antes de efetivar a alteração.

### 8.26 Observações da venda

**RF099 — Registro de observações**

- O sistema deverá permitir adicionar observações às vendas em andamento.
- A disponibilidade do recurso deverá ser configurável por empresa/filial.
- O limite de caracteres deverá ser configurável.
- O sistema deverá registrar o usuário responsável pela inclusão ou alteração, a data e o horário.
- Alterações deverão preservar o histórico anterior das observações.
- As observações deverão permanecer vinculadas à venda e disponíveis para consulta conforme as permissões de acesso.

### 8.27 Suspensão e retomada de vendas

**RF100 — Controle de suspensão e retomada**

- O sistema deverá permitir suspender uma venda em andamento para que o operador possa atender outro cliente.
- As permissões para suspender e retomar vendas deverão ser configuráveis por usuário.
- O operador poderá retomar suas próprias vendas, desde que possua a permissão necessária.
- Outro operador poderá retomar uma venda mediante autorização, conforme as permissões configuradas.
- A venda suspensa deverá preservar os produtos, quantidades, preços, cliente, observações e demais informações já registradas.
- O sistema deverá registrar quem suspendeu e quem retomou a venda, com data e horário.
- O histórico de suspensões e retomadas deverá ser preservado para auditoria.

### 8.28 Prazo de validade de vendas suspensas

**RF101 — Controle de prazo de vendas suspensas**

- A empresa/filial poderá configurar um prazo máximo para permanência de vendas suspensas.
- Ao atingir o prazo configurado, o sistema deverá gerar um alerta para gerente e administrador.
- O sistema não deverá cancelar automaticamente uma venda suspensa por decurso do prazo.
- O cancelamento deverá depender de análise e autorização de usuário que possua a permissão específica correspondente.
- A autorização deverá registrar responsável, data, horário e justificativa.
- A venda e todo o seu histórico deverão permanecer disponíveis para consulta e auditoria.

### 8.29 Limite de vendas suspensas

**RF102 — Controle de quantidade de vendas suspensas**

- O sistema deverá permitir configurar a quantidade máxima de vendas suspensas simultaneamente.
- O limite poderá ser definido por empresa/filial, operador e caixa.
- O sistema deverá impedir novas suspensões quando o limite aplicável for atingido.
- O sistema deverá informar ao operador quando o limite estiver próximo ou tiver sido atingido.
- As permissões especiais para ultrapassar o limite deverão ser configuráveis por usuário ou grupo, utilizando o mecanismo global de permissões, sem depender exclusivamente do cargo ocupado.
- O controle deverá ser validado pelo backend.

### 8.30 Recuperação de falhas durante a finalização da venda

**RF103 — Controle e recuperação de falhas**

- O sistema deverá registrar as etapas executadas durante a finalização de uma venda.
- Em caso de falha, deverá identificar e registrar a etapa em que ocorreu o problema.
- Os dados da operação deverão ser preservados para permitir análise e recuperação.
- O sistema deverá permitir recuperar operações interrompidas sem gerar vendas ou pagamentos duplicados.
- Cada operação deverá possuir identificador único para controle de idempotência.
- As tentativas de recuperação deverão ser registradas, incluindo data, horário, resultado e falhas encontradas.
- Falhas que não puderem ser resolvidas automaticamente deverão ser encaminhadas para análise de usuário que possua a permissão específica correspondente.
- O sistema não deverá considerar uma venda concluída enquanto não houver confirmação consistente de todas as etapas obrigatórias.

### 8.31 Reabertura de vendas finalizadas

**RF104 — Controle de reabertura de vendas**

- O sistema deverá permitir reabrir vendas finalizadas conforme permissões configuráveis por usuário.
- Usuários sem permissão deverão solicitar autorização de usuário que possua a permissão específica correspondente.
- Toda solicitação de reabertura deverá apresentar uma justificativa obrigatória.
- O sistema deverá preservar integralmente o histórico original da venda.
- Todas as alterações realizadas após a reabertura deverão ser registradas para auditoria.
- O sistema deverá controlar os impactos das alterações sobre estoque e pagamentos, evitando inconsistências e duplicidades.
- A venda deverá passar novamente pelas validações obrigatórias antes de ser finalizada.
- O sistema deverá registrar quem solicitou, quem autorizou, quem realizou a reabertura e as respectivas datas e horários.

### 8.32 Alteração manual do preço durante a venda

**RF105 — Controle de alteração do preço unitário**

- O sistema deverá permitir alteração manual do preço unitário conforme permissões configuráveis por usuário.
- A empresa/filial poderá definir limites de alteração de preço por usuário ou perfil.
- Usuários autorizados poderão alterar preços dentro dos limites estabelecidos.
- Alterações que ultrapassarem os limites deverão exigir autorização de usuário que possua a permissão específica correspondente.
- Usuários sem permissão deverão solicitar autorização para realizar a alteração.
- Toda alteração deverá exigir justificativa e registrar o preço original, o novo preço, o operador solicitante, o autorizador, a data e o horário.
- A alteração deverá afetar somente o preço aplicado ao item da venda, sem modificar o preço cadastrado do produto.
- O backend deverá validar as permissões e os limites antes de efetivar a alteração.
- O histórico de preços aplicados deverá ser preservado para auditoria.


### 8.33 Venda de produtos inativos

**RF106 — Controle de venda de produtos inativos**

- Por padrão, o sistema deverá bloquear a inclusão de produtos inativos em novas vendas.
- A empresa/filial poderá configurar exceções para permitir a venda de determinados produtos inativos.
- Quando exigida pela configuração, a venda deverá depender de autorização de usuário que possua a permissão específica correspondente.
- Toda autorização deverá registrar o produto, operador solicitante, autorizador, motivo, data e horário.
- A inativação de um produto não deverá excluir seu histórico de vendas ou movimentações.
- O backend deverá validar a situação do produto e as permissões aplicáveis antes da inclusão e finalização da venda.

### 8.34 Bloqueio de produtos com preço zerado e controle de cortesia

**RF107 — Validação de preço e identificação de cortesia**

- O cadastro do produto deverá possuir uma opção denominada "Cortesia".
- Produtos com preço unitário igual a R$ 0,00 deverão ter sua inclusão no carrinho bloqueada por padrão.
- Produtos marcados como cortesia poderão ser vendidos gratuitamente, sem necessidade de autorização adicional.
- Produtos com preço zerado e sem a marcação de cortesia deverão continuar bloqueados.
- O sistema deverá identificar claramente os itens concedidos como cortesia na venda.
- A movimentação de estoque deverá ocorrer normalmente, conforme as regras de baixa de estoque.
- O histórico da venda deverá registrar a condição de cortesia do produto.
- As validações deverão ocorrer na interface e no backend.

### 8.35. Controle de estoque negativo

**RF108 — Configuração e autorização de vendas com estoque negativo**

O sistema deverá permitir configurar, por empresa e filial, se serão permitidas vendas que resultem em estoque negativo.

**Regras:**

1. A configuração deverá permitir habilitar ou desabilitar a realização de vendas que resultem em estoque negativo.
2. Quando o estoque negativo estiver desabilitado, o sistema deverá bloquear qualquer operação que resulte em saldo negativo, mesmo mediante autorização de outro usuário.
3. Quando o estoque negativo estiver habilitado, a operação poderá ser realizada por usuários que possuam a permissão específica correspondente.
4. Usuários sem a permissão necessária poderão solicitar autorização por meio do mecanismo centralizado definido na RN001.
5. A configuração deverá ser independente por filial, respeitando as regras gerais da empresa.
6. Toda operação que resultar em estoque negativo deverá registrar o produto, a quantidade movimentada, o saldo anterior, o saldo resultante, o operador responsável, o autorizador quando aplicável, a data e o horário.
7. O sistema deverá disponibilizar consulta dos produtos com estoque negativo e suas respectivas movimentações.
8. Alterações na configuração deverão exigir permissão específica e gerar registro de auditoria, identificando responsável, empresa, filial, configuração anterior, nova configuração, data e horário.
9. A configuração e as permissões deverão ser validadas no backend, garantindo consistência mesmo em operações simultâneas.
### 8.36 Estoque em vendas suspensas

**RF109 — Não reservar estoque para vendas suspensas**

- O sistema não deverá reservar ou comprometer estoque para produtos incluídos em vendas suspensas.
- Os produtos deverão permanecer disponíveis para outras operações de venda.
- A baixa do estoque deverá ocorrer somente após a finalização efetiva da venda.
- Ao retomar uma venda suspensa, o sistema deverá consultar novamente o estoque disponível.
- Caso não exista quantidade suficiente, deverão ser aplicadas as regras de autorização para estoque insuficiente.
- A suspensão e retomada da venda não deverão gerar movimentações de estoque.

### 8.37. Retomada de vendas suspensas expiradas

**RF110 – Autorização para retomada de venda suspensa expirada**

- O sistema deverá bloquear a retomada de vendas suspensas cujo prazo de validade tenha expirado.
- A retomada deverá exigir autorização de usuário que possua a permissão específica correspondente.
- O sistema deverá registrar o operador solicitante, o autorizador, a data e hora e a identificação da venda.
- A autorização deverá ser validada pelo backend.
- A retomada deverá preservar integralmente os dados originais da venda.
- Após a autorização, o sistema deverá revalidar o estoque e aplicar as regras de disponibilidade e autorização já definidas.

### 8.38. Visibilidade das vendas suspensas

**RF111 – Controle de visualização de vendas suspensas**

- O sistema deverá permitir configurar, por empresa/filial, quais perfis ou usuários podem visualizar vendas suspensas.
- A configuração poderá restringir a visualização às próprias vendas do operador ou permitir a consulta de vendas suspensas de outros operadores.
- Gerentes e administradores poderão ter permissões específicas de consulta, conforme configuração.
- A permissão de visualização deverá ser independente da permissão de retomada ou cancelamento.
- O backend deverá validar as permissões antes de disponibilizar os dados.
- Todas as operações de retomada e cancelamento deverão manter seus registros de auditoria.

### 8.39. Identificação do operador nas vendas

**RF112 – Identificação individual do operador**

- O sistema deverá permitir configurar, por empresa/filial, o modelo de identificação do operador responsável pelas vendas.
- O primeiro modelo permitirá vincular as vendas ao operador que abriu o caixa.
- O segundo modelo exigirá identificação individual de cada operador antes de iniciar uma venda, mesmo quando houver compartilhamento do caixa.
- Cada venda deverá registrar o operador responsável pela operação.
- A troca de operador deverá preservar a identificação de quem realizou cada operação.
- O backend deverá validar a identificação e as permissões do operador.
- Os registros deverão estar disponíveis para auditoria e relatórios gerenciais.

### 8.40. Identificação e vínculo automático de clientes

**RF113 – Configuração do vínculo de clientes durante a venda**

- O sistema deverá permitir configurar, por empresa/filial, como será realizado o vínculo de clientes durante uma venda.
- No modo automático, ao informar um CPF/CNPJ já cadastrado, o sistema deverá localizar e vincular o cliente à venda.
- No modo com confirmação, o sistema deverá exibir os dados do cliente encontrado e solicitar confirmação antes de vinculá-lo.
- Caso não exista cadastro correspondente, deverão ser respeitadas as regras já definidas para identificação de clientes não cadastrados.
- O sistema deverá registrar o cliente vinculado à venda e preservar o histórico de alterações.
- O backend deverá validar os dados e aplicar as configurações da empresa/filial.

### 8.41. Histórico de compras do cliente no PDV

**RF114 – Restrição de consulta ao histórico do cliente**

- O PDV não deverá disponibilizar consulta ao histórico de compras do cliente durante a operação de venda.
- Os dados básicos de identificação do cliente continuarão disponíveis conforme os requisitos já definidos.
- A consulta ao histórico poderá ser considerada futuramente em módulos administrativos ou gerenciais, fora do fluxo de venda do PDV.

### 8.42. Edição de quantidade no carrinho

**RF115 – Alteração da quantidade de produtos**

- O sistema deverá permitir que o operador altere diretamente a quantidade de um produto já adicionado ao carrinho.
- A alteração deverá atualizar automaticamente o subtotal do item e o total da venda.
- O sistema deverá validar a disponibilidade de estoque conforme as regras configuradas.
- Caso a nova quantidade ultrapasse o estoque disponível, deverão ser aplicadas as regras de autorização já definidas.
- Produtos pesáveis deverão respeitar as configurações de quantidade fracionada e precisão decimal.
- A alteração deverá ser registrada no histórico da venda.
- A validação deverá ocorrer também no backend.

### 8.43. Descontos em itens individuais

**RF116 – Configuração de descontos por produto**

- O sistema deverá permitir configurar, por empresa/filial, se os operadores poderão aplicar descontos diretamente em itens individuais do carrinho.
- Quando habilitado, deverá permitir desconto percentual ou em valor fixo por item.
- Os descontos deverão respeitar os limites individuais dos usuários e as regras de autorização já definidas.
- O sistema deverá recalcular automaticamente o subtotal do item e o total da venda.
- O desconto aplicado deverá ser identificado e registrado individualmente, incluindo operador, autorizador, quando houver, valor e justificativa.
- A validação dos limites e das permissões deverá ocorrer também no backend.

### 8.44. Acúmulo de descontos

**RF117 – Configuração de acúmulo de descontos**

- O sistema deverá permitir configurar, por empresa/filial, se descontos automáticos e manuais poderão ser acumulados.
- Cada promoção poderá possuir uma configuração específica de acúmulo, respeitando as políticas gerais da empresa/filial.
- Quando o acúmulo estiver permitido, o sistema deverá calcular os descontos conforme a ordem e as regras definidas.
- Quando o acúmulo não estiver permitido, deverá aplicar a política de prioridade configurada.
- O sistema deverá impedir descontos que ultrapassem os limites autorizados.
- Todos os descontos aplicados deverão ser identificados individualmente e registrados para auditoria.
- As regras deverão ser validadas pelo backend, inclusive durante operações offline, conforme as políticas de sincronização.

### 8.45. Cancelamento de itens individuais

**RF118 – Remoção de produtos durante a venda**

- O sistema deverá permitir remover individualmente produtos do carrinho durante uma venda em andamento.
- A operação deverá respeitar as permissões individuais de remoção configuradas para cada usuário.
- Quando o operador não possuir permissão, deverá ser solicitada autorização conforme as regras já definidas.
- O sistema deverá recalcular automaticamente os valores da venda após a remoção.
- O item removido deverá permanecer identificado no histórico da operação para fins de auditoria.
- A remoção de um item não deverá cancelar os demais produtos nem a venda inteira.
- Como o estoque somente é baixado na finalização da venda, a remoção de itens antes da finalização não deverá gerar movimentação de estoque.

### 8.46. Confirmação de remoção de itens

**RF119 – Confirmação antes da remoção de produtos**

- O sistema deverá solicitar confirmação antes de remover um produto do carrinho.
- A confirmação deverá identificar claramente o produto e a quantidade que será removida.
- Caso o operador desista, o item deverá permanecer no carrinho sem alterações.
- Após a confirmação, deverão ser aplicadas as permissões de remoção definidas no RF393.
- A operação confirmada deverá ser registrada no histórico da venda para fins de auditoria.
- A remoção não deverá gerar movimentação de estoque, pois a baixa ocorre somente na finalização da venda.

### 8.47. Limpeza do carrinho

**RF120 – Remoção individual dos produtos**

- O sistema não deverá disponibilizar uma função para remover todos os produtos do carrinho simultaneamente.
- Os produtos deverão ser removidos individualmente.
- Cada remoção deverá respeitar as permissões definidas no RF393.
- O sistema deverá solicitar confirmação conforme o RF119.
- A remoção deverá manter o registro individual dos itens retirados para fins de auditoria.
- A venda deverá permanecer em andamento enquanto houver itens ou até que seja cancelada conforme as regras existentes.

### 8.48. Exibição de valores no carrinho

**RF121 – Configuração das informações financeiras do carrinho**

- O sistema deverá permitir configurar, por empresa/filial, quais informações financeiras serão exibidas durante a venda.
- Poderão ser configuradas individualmente as seguintes informações:
  - Preço unitário.
  - Quantidade.
  - Desconto aplicado.
  - Subtotal por item.
  - Total geral da venda.
- As informações ocultadas visualmente deverão continuar sendo calculadas e armazenadas corretamente.
- A configuração de exibição não poderá alterar as regras de cálculo, descontos ou pagamentos.
- O backend deverá preservar os valores completos da operação, independentemente das informações exibidas na interface.

### 8.49. Validação de carrinho vazio

**RF122 – Bloqueio de finalização sem produtos**

- O sistema deverá impedir a finalização de vendas sem produtos no carrinho.
- Deverá existir pelo menos um item válido para iniciar o processo de pagamento.
- Ao tentar finalizar uma venda vazia, o PDV deverá apresentar uma mensagem informando que é necessário adicionar pelo menos um produto.
- A validação deverá ocorrer na interface e também no backend.
- Nenhuma venda ou movimentação financeira deverá ser gerada em decorrência de uma tentativa inválida.

### 8.50. Alteração do carrinho durante o pagamento

**RF123 – Autorização para alteração de venda em pagamento**

- O sistema deverá permitir solicitar alterações no carrinho após o início do processo de pagamento.
- Qualquer alteração deverá exigir autorização de usuário que possua a permissão específica correspondente.
- A autorização deverá ser validada pelo backend.
- Após a autorização, o sistema deverá permitir adicionar, remover ou alterar quantidades dos produtos.
- O total da venda deverá ser recalculado automaticamente.
- Tentativas de pagamento ainda não confirmadas deverão ser canceladas de forma controlada antes de aplicar as alterações.
- Confirmações tardias de pagamentos deverão ser tratadas para evitar duplicidade ou inconsistência financeira.
- O sistema deverá registrar a alteração, o operador solicitante, o autorizador, a data, a hora e os valores anteriores e posteriores.

### 8.51. Controle de inatividade durante a venda

**RF124 – Ações automáticas por inatividade**

- O sistema deverá permitir configurar, por empresa/filial, o tempo máximo de inatividade durante uma venda.
- Deverá permitir escolher entre:
  - Bloquear a tela e exigir nova autenticação para continuar.
  - Cancelar automaticamente a venda após o período configurado.
- No bloqueio de tela, o carrinho e os dados da venda deverão ser preservados.
- No cancelamento automático, o sistema deverá registrar a operação e seu motivo.
- Vendas com pagamentos já iniciados ou tentativas pendentes deverão passar pelo tratamento adequado para evitar inconsistências financeiras.
- O sistema deverá registrar data, hora e motivo da ação automática.
- As regras deverão ser aplicadas e validadas pelo backend.

### 8.52. Controle de saída do PDV com venda em andamento

**RF125 – Bloqueio de saída com carrinho preenchido**

- O sistema deverá impedir o encerramento da tela do PDV, saída do módulo ou navegação para outra área enquanto houver uma venda em andamento com produtos no carrinho.
- Antes de sair, o operador deverá finalizar ou suspender a venda, conforme as permissões existentes.
- O sistema deverá impedir que uma atualização ou navegação interna descarte acidentalmente os dados da venda.
- Em caso de tentativa de fechamento ou atualização da página pelo navegador, deverá ser apresentado um aviso de proteção, respeitando as limitações técnicas do navegador.
- Os dados da venda deverão ser preservados até que uma operação válida de finalização, suspensão ou cancelamento seja realizada.

### 8.53. Produto sem preço cadastrado

**RF126 – Definição de preço manual para produto sem preço**

- O sistema deverá identificar produtos ativos que não possuam preço de venda cadastrado para a filial.
- A inclusão desses produtos no carrinho deverá permitir a definição de um preço manual mediante autorização de usuário que possua a permissão específica correspondente.
- O preço informado deverá ser validado pelo backend.
- A operação deverá exigir justificativa.
- O preço manual deverá ser aplicado somente à venda atual, sem alterar o cadastro ou a tabela de preços do produto.
- O sistema deverá registrar o preço original (ausente), o preço informado, o operador, o autorizador, a data e a hora.
- A operação deverá respeitar as regras de auditoria e histórico de alterações de preço.

### 8.54. Venda de produtos sem código de barras

**RF127 – Configuração de inclusão de produtos sem código de barras**

- O sistema deverá permitir configurar, por empresa/filial, se produtos sem código de barras poderão ser adicionados ao carrinho.
- Quando permitido, o operador poderá localizar o produto por descrição ou código interno.
- Quando não permitido, o sistema deverá bloquear a inclusão e informar o motivo.
- A configuração deverá respeitar as permissões de acesso e as regras de cadastro de produtos.
- A inclusão deverá registrar o produto, operador, data e hora da operação.
- A validação deverá ocorrer também no backend.

### 8.55. Código de barras não encontrado

**RF128 – Tratamento de códigos de barras desconhecidos**

- O sistema deverá identificar quando um código de barras lido não corresponder a nenhum produto cadastrado.
- O comportamento deverá ser configurável por empresa/filial.
- Poderão ser disponibilizadas as seguintes ações:
  - Informar que o produto não foi encontrado e permitir nova leitura.
  - Permitir pesquisa manual no cadastro de produtos.
  - Permitir acesso ao cadastro de produtos, respeitando as permissões do usuário.
  - Permitir cadastro rápido mediante autorização, caso essa funcionalidade esteja habilitada.
- O sistema não deverá adicionar produtos automaticamente sem identificação e validação.
- As operações de cadastro deverão respeitar as regras de acesso, auditoria e unicidade dos códigos de barras.

### 8.56. Leitura repetida de código de barras

**RF129 – Confirmação de incremento de quantidade**

- Quando um código de barras corresponder a um produto que já esteja no carrinho, o sistema deverá perguntar ao operador se deseja aumentar sua quantidade.
- Caso confirme, a quantidade deverá ser incrementada conforme a unidade de venda do produto.
- Caso não confirme, a quantidade deverá permanecer inalterada.
- A alteração deverá respeitar as regras de estoque, permissões e produtos pesáveis.
- O sistema deverá atualizar automaticamente os valores do item e o total da venda.
- A operação deverá ser registrada no histórico da venda.

### 8.57. Informações adicionais de produtos

**RF130 – Configuração de informações complementares na venda**

- O sistema deverá permitir configurar, por empresa/filial, se produtos poderão exigir informações complementares durante a venda.
- Quando habilitado, o cadastro do produto poderá definir quais informações deverão ser solicitadas.
- As informações poderão incluir número de série, identificação de equipamento ou outros dados específicos.
- Deverá ser possível definir quais informações são obrigatórias e quais são opcionais.
- O momento da coleta deverá ser configurável, podendo ocorrer na inclusão do produto ou durante a finalização da venda.
- O sistema deverá vincular as informações ao respectivo item da venda.
- Os dados deverão ser preservados no histórico e disponibilizados para consulta posterior.
- O backend deverá validar o preenchimento dos campos obrigatórios.

### 8.58. Produtos Combo

**RF131 – Cadastro e venda de Produtos Combo**

- O sistema deverá permitir cadastrar um tipo específico de produto denominado Produto Combo.
- Cada combo deverá possuir cadastro próprio, com descrição, preço de venda e código de identificação próprio, quando aplicável.
- O cadastro deverá permitir associar um ou mais produtos normais que compõem o combo.
- O Produto Combo deverá aparecer no carrinho e no histórico da venda como um único item, identificado como combo.
- O Produto Combo não possuirá estoque próprio.
- A baixa de estoque ocorrerá exclusivamente nos produtos normais associados à composição do combo.
- A quantidade baixada de cada componente deverá considerar a quantidade vendida do combo e sua composição cadastrada.
- Caso algum componente não possua estoque suficiente, a venda deverá respeitar as regras de autorização de estoque insuficiente, exigindo autorização de usuário que possua a permissão específica correspondente.
- A autorização deverá ser validada pelo backend e registrada para auditoria.
- A venda do combo deverá preservar a identificação dos componentes e suas respectivas movimentações de estoque.
- As regras de promoções permanecerão independentes do cadastro e funcionamento dos Produtos Combo.

### 8.59. Versionamento da composição de Produtos Combo

**RF132 – Histórico de alterações da composição**

- O sistema deverá manter o histórico de versões da composição de cada Produto Combo.
- Sempre que houver alteração nos produtos associados ou em suas quantidades, deverá ser criada uma nova versão da composição.
- As vendas realizadas anteriormente deverão permanecer vinculadas à versão da composição utilizada no momento da venda.
- Alterações na composição não poderão modificar o histórico de vendas já finalizadas.
- Cada versão deverá registrar os produtos componentes, respectivas quantidades, responsável pela alteração, data e hora.
- Somente usuários com permissão poderão alterar a composição.
- O backend deverá garantir a integridade histórica das versões.

### 8.60. Revisão de preço dos Produtos Combo

**RF133 – Alerta de alteração de preço dos componentes**

- O preço de venda do Produto Combo deverá ser independente dos preços individuais de seus componentes.
- Alterações nos preços dos produtos normais associados não deverão modificar automaticamente o preço do combo.
- Quando houver alteração no preço de um componente, o sistema deverá identificar os Produtos Combo relacionados.
- O sistema deverá gerar um alerta para o administrador revisar o preço dos combos afetados.
- A revisão e eventual alteração do preço deverão depender de ação autorizada do usuário.
- O sistema deverá preservar o histórico dos preços anteriores e posteriores.

### 8.61. Desativação de componentes de Produtos Combo

**RF134 – Desativação automática de combos relacionados**

- Quando um produto normal associado a um ou mais Produtos Combo for desativado, o sistema deverá desativar automaticamente todos os combos que utilizam esse componente.
- A desativação deverá impedir novas vendas dos combos afetados.
- O sistema deverá identificar quais combos foram afetados e registrar a relação entre o produto desativado e os combos desativados.
- O administrador deverá ser notificado sobre a desativação.
- A reativação do produto componente não deverá reativar automaticamente os combos.
- A reativação dos combos deverá exigir ação autorizada do administrador, após verificar se todos os componentes estão ativos.
- O histórico de vendas e movimentações anteriores deverá ser preservado.

### 8.62. Quantidades dos componentes de Produtos Combo

**RF135 – Configuração de quantidades na composição**

- O sistema deverá permitir definir a quantidade de cada produto normal associado a um Produto Combo.
- Deverão ser aceitas quantidades inteiras e fracionadas, conforme a configuração do produto componente.
- Produtos que não permitem quantidades fracionadas deverão aceitar somente valores inteiros.
- Produtos pesáveis deverão respeitar a precisão decimal configurada em seu cadastro.
- Na venda do combo, o sistema deverá calcular automaticamente a quantidade de cada componente a ser baixada do estoque.
- O cálculo deverá considerar a quantidade de combos vendidos e a quantidade de cada componente definida na composição.
- As movimentações deverão ser registradas individualmente para cada produto normal.

### 8.63. Disponibilidade de Produtos Combo

**RF136 – Consulta de disponibilidade dos componentes**

- O sistema deverá verificar a disponibilidade de estoque de todos os produtos normais associados ao Produto Combo.
- Quando algum componente apresentar estoque insuficiente, o PDV deverá exibir um alerta identificando os produtos afetados.
- O alerta deverá apresentar a quantidade necessária e a quantidade disponível de cada componente.
- A venda poderá prosseguir mediante autorização de usuário que possua a permissão específica correspondente, respeitando as regras de estoque insuficiente já definidas.
- A autorização deverá considerar todos os componentes com estoque insuficiente.
- As baixas deverão ocorrer individualmente nos estoques dos produtos normais associados.
- A validação definitiva deverá ocorrer no backend durante a finalização da venda.

### 8.64. Compartilhamento de componentes entre Produtos Combo

**RF137 – Utilização de produtos em múltiplos combos**

- O sistema deverá permitir que um mesmo produto normal seja associado a diferentes Produtos Combo.
- Cada combo deverá manter sua própria composição e quantidade de componentes.
- O estoque dos produtos normais será compartilhado entre todos os combos.
- A venda de qualquer combo deverá movimentar o estoque dos respectivos componentes.
- O sistema deverá considerar todas as movimentações realizadas para validar a disponibilidade de estoque.
- Deverão ser respeitadas as regras de estoque insuficiente e autorização já definidas.

### 8.65. Exclusão definitiva de Produtos Combo

**RF138 – Exclusão física com geração de relatório histórico**

- O sistema deverá permitir a exclusão definitiva de Produtos Combo, inclusive quando já possuírem vendas registradas.
- Antes de executar a exclusão, o sistema deverá gerar automaticamente um relatório com o histórico de utilização do combo.
- O relatório deverá conter:
  - Identificação e descrição do Produto Combo.
  - Preço de venda praticado.
  - Quantidade vendida.
  - Produtos componentes.
  - Quantidades de cada componente.
  - Composição utilizada em cada venda.
  - Data e hora completas de cada venda, incluindo dia, mês e ano.
  - Identificação da empresa e filial.
- O relatório deverá ser salvo em um local de armazenamento de documentos, independente do banco de dados.
- O sistema deverá confirmar que o relatório foi gerado e armazenado antes de permitir a exclusão.
- O administrador deverá receber um alerta informando que o combo possui histórico de vendas e que um relatório será gerado para preservação dessas informações.
- A exclusão deverá remover fisicamente o cadastro do combo e seus registros de composição do banco de dados.
- A exclusão não poderá remover os produtos normais associados nem suas movimentações de estoque.
- O sistema deverá impedir a exclusão enquanto houver vendas em andamento ou suspensas vinculadas ao combo.
- A operação deverá exigir permissão de administrador e confirmação explícita.
- Deverá ser registrado para auditoria o usuário responsável, a data e hora da exclusão e a localização do relatório gerado.
- Caso a geração ou o armazenamento do relatório falhe, a exclusão deverá ser bloqueada.

### 8.66. Formato e armazenamento do relatório de exclusão de Produto Combo

**RF139 — Formatos do relatório**

O sistema deverá gerar o relatório histórico de exclusão de Produto Combo nos formatos CSV e Excel (XLSX), disponibilizando ambos os arquivos para armazenamento externo ao banco de dados.

**RF140 — Local de armazenamento**

O sistema deverá permitir que o administrador configure a pasta de armazenamento dos relatórios de exclusão de Produtos Combo para cada empresa.

- O caminho de armazenamento deverá ser configurável pelo administrador.
- Os relatórios deverão ser organizados de forma a permitir sua identificação por empresa, filial, produto e data de geração.
- O sistema deverá validar se a pasta está acessível e permite gravação.
- A exclusão do Produto Combo somente poderá ocorrer após a geração e o armazenamento bem-sucedidos dos dois arquivos.
- Em caso de falha na geração ou gravação de qualquer arquivo, a exclusão deverá ser bloqueada.
- O sistema deverá registrar na auditoria a localização dos arquivos gerados.

### 8.67. Controle de acesso aos relatórios de exclusão

**RF141 — Permissões de consulta dos relatórios**

O sistema deverá controlar o acesso aos relatórios históricos de exclusão de Produtos Combo por meio de permissões específicas.

- Administradores poderão consultar os relatórios.
- Gerentes poderão consultar os relatórios conforme as permissões concedidas.
- Outros usuários poderão receber uma permissão específica de consulta.
- A permissão de consulta deverá ser independente da permissão para excluir Produtos Combo.
- O sistema deverá validar a autorização no backend, não apenas ocultar opções na interface.
- Todas as consultas e tentativas de acesso não autorizado deverão ser registradas na auditoria.

### 8.68. Retenção dos relatórios de exclusão

**RF142 — Retenção permanente, sem exclusão automática**

O sistema deverá manter os relatórios históricos de exclusão de Produtos Combo armazenados permanentemente, sem realizar exclusões automáticas por prazo de retenção.

**Regras:**

1. Os relatórios originais e suas versões deverão permanecer armazenados por tempo indeterminado.
2. O sistema não deverá excluir automaticamente esses arquivos por decurso de prazo.
3. A retenção deverá ser independente da existência do Produto Combo no cadastro do banco de dados.
4. Os relatórios deverão permanecer disponíveis para consulta conforme as permissões definidas no RF141.
5. A exclusão manual excepcional deverá ser permitida exclusivamente conforme os procedimentos de autorização e confirmação definidos nos RF151, RF152 e RF153.
6. A exclusão manual deverá abranger o relatório original e suas versões vinculadas, conforme as regras estabelecidas.
7. Os registros de auditoria da geração, alteração, consulta e eventual exclusão dos arquivos deverão ser preservados, mesmo após a remoção definitiva dos documentos.
8. A exclusão dos arquivos não poderá modificar o histórico das vendas nem os registros históricos dos Produtos Combo.
9. O sistema deverá preservar a localização dos arquivos e seus registros de auditoria enquanto os documentos estiverem armazenados.
### 8.69. Controle de alterações dos relatórios de exclusão

**RF143 — Alteração e versionamento dos relatórios**

O sistema deverá permitir alterações nos relatórios de exclusão de Produtos Combo somente mediante autorização de usuário com permissão adequada.

- Os arquivos originais deverão ser preservados.
- Toda alteração deverá gerar uma nova versão do relatório.
- Cada versão deverá possuir identificação própria e data/hora de geração.
- O sistema deverá registrar o usuário responsável pela alteração, o autorizador, a justificativa e a data/hora.
- As versões anteriores deverão permanecer disponíveis para consulta.
- O acesso às versões deverá respeitar as permissões definidas no RF141.
- A alteração de um relatório não poderá modificar os registros históricos das vendas ou do banco de dados.
- O sistema deverá manter o vínculo entre o relatório original e suas versões posteriores.

### 8.70. Autorização para alteração dos relatórios

**RF144 — Autorização exclusiva de administradores**

A criação de novas versões dos relatórios de exclusão de Produtos Combo deverá exigir autorização exclusiva de um administrador.

- Somente usuários com perfil de administrador poderão autorizar alterações.
- Gerentes e demais usuários não poderão conceder essa autorização.
- O sistema deverá exigir autenticação e confirmação do administrador responsável.
- A autorização deverá estar vinculada à solicitação de alteração e à justificativa apresentada.
- O sistema deverá registrar o administrador autorizador, o usuário solicitante, a data/hora e a versão gerada.
- A validação deverá ocorrer no backend.

### 8.71. Processo de alteração dos relatórios

**RF145 — Edição externa e envio de nova versão**

O sistema deverá permitir que os relatórios de exclusão de Produtos Combo sejam alterados externamente, mediante download e posterior envio de uma nova versão.

- O usuário deverá possuir permissão para solicitar a alteração.
- A autorização deverá ser concedida exclusivamente por um administrador, conforme RF144.
- O usuário poderá baixar o relatório nos formatos CSV ou XLSX.
- Após a edição externa, deverá enviar o arquivo atualizado ao sistema.
- O sistema deverá validar o formato e a integridade básica do arquivo recebido.
- A nova versão deverá ser armazenada sem sobrescrever o arquivo original ou versões anteriores.
- O sistema deverá registrar usuário responsável, administrador autorizador, justificativa, data/hora e arquivo enviado.
- A nova versão deverá permanecer vinculada ao relatório original.
- A alteração do arquivo não poderá modificar os registros históricos das vendas ou do banco de dados.

### 8.72. Identificação de relatórios alterados

**RF146 — Identificação visual de documentos alterados**

Toda nova versão de um relatório de exclusão de Produto Combo deverá receber automaticamente a identificação **“DOCUMENTO ALTERADO”**.

- A identificação deverá ser inserida no próprio arquivo.
- O documento deverá apresentar a data e hora da alteração.
- Deverá identificar o usuário responsável pelo envio da nova versão.
- Deverá identificar o administrador que autorizou a alteração.
- A identificação deverá permanecer visível nos arquivos CSV e XLSX.
- O sistema deverá preservar o arquivo original sem essa identificação, mantendo sua integridade.
- A identificação não poderá ser removida pelo usuário durante a edição externa.

### 8.73. Consulta de versões dos relatórios

**RF147 — Histórico de versões sem comparação automática**

O sistema deverá manter todas as versões dos relatórios de exclusão de Produtos Combo disponíveis para consulta, sem realizar comparação automática entre os arquivos.

- O relatório original deverá permanecer disponível.
- Todas as versões posteriores deverão ser armazenadas individualmente.
- O sistema deverá permitir consultar e baixar cada versão conforme as permissões de acesso.
- Cada versão deverá apresentar sua identificação, data/hora de envio e responsável.
- O sistema não deverá realizar comparação automática de conteúdo entre as versões.
- A consulta deverá respeitar as permissões definidas no RF141.

### 8.74. Validação de arquivos enviados

**RF148 — Bloqueio de arquivos inválidos**

O sistema deverá bloquear o envio de relatórios que apresentem formato incorreto, corrupção ou falha na validação.

- Validar se o arquivo corresponde ao formato esperado (CSV ou XLSX).
- Verificar se o arquivo pode ser aberto e processado corretamente.
- Identificar arquivos corrompidos, vazios ou incompatíveis com a estrutura esperada.
- Informar ao usuário o motivo da rejeição.
- Não substituir nem modificar o relatório original ou versões anteriores.
- Registrar a tentativa de envio e o motivo da rejeição na auditoria.
- Permitir novo envio após a correção do arquivo.
- A validação deverá ocorrer no backend.

### 8.75. Limite de tamanho dos arquivos enviados

**RF149 — Tamanho máximo por arquivo**

O sistema deverá limitar o tamanho dos arquivos enviados para criação de novas versões dos relatórios de exclusão de Produtos Combo.

- O tamanho máximo permitido será de 50 MB por arquivo.
- A validação deverá ocorrer antes do processamento do arquivo.
- Arquivos acima do limite deverão ser rejeitados.
- O sistema deverá informar o tamanho máximo permitido ao usuário.
- A rejeição não poderá modificar o relatório original ou versões anteriores.
- A validação deverá ocorrer no backend.

### 8.76. Manutenção do formato dos relatórios

**RF150 — Preservação do formato original**

O sistema deverá exigir que toda nova versão enviada mantenha o mesmo formato do relatório original.

- Relatórios originalmente gerados em CSV deverão receber novas versões somente em CSV.
- Relatórios originalmente gerados em Excel (XLSX) deverão receber novas versões somente em XLSX.
- O sistema deverá validar a extensão e o conteúdo real do arquivo.
- Arquivos enviados em formato diferente do original deverão ser rejeitados.
- A rejeição deverá apresentar o motivo ao usuário e preservar todas as versões existentes.
- A validação deverá ocorrer no backend.

### 8.77. Exclusão manual de relatórios

**RF151 — Exclusão restrita a administradores**

O sistema deverá permitir a exclusão manual de relatórios de exclusão de Produtos Combo exclusivamente por administradores.

- Somente usuários com perfil de administrador poderão solicitar a exclusão.
- Será obrigatória uma justificativa para a exclusão.
- O sistema deverá solicitar confirmação explícita antes de executar a operação.
- A exclusão deverá ser registrada na auditoria, contendo usuário responsável, data/hora, justificativa e localização do arquivo.
- A exclusão deverá considerar o relatório original e suas versões vinculadas, evitando arquivos órfãos.
- O sistema deverá informar quais versões serão afetadas antes da confirmação.
- A exclusão manual não deverá ocorrer automaticamente por prazo de retenção.
- O sistema deverá preservar o registro de auditoria mesmo após a exclusão dos arquivos.

### 8.78. Exclusão definitiva dos relatórios

**RF152 — Exclusão definitiva sem recuperação**

Quando um administrador confirmar a exclusão manual de um relatório de Produto Combo, o sistema deverá realizar a exclusão definitiva dos arquivos.

- O relatório original e todas as suas versões vinculadas deverão ser excluídos.
- Não deverá existir lixeira ou mecanismo interno de recuperação.
- O sistema deverá solicitar confirmação explícita antes da exclusão definitiva.
- A operação deverá ser executada somente após validação das permissões do administrador.
- O sistema deverá preservar os registros de auditoria da operação, mesmo após a exclusão dos arquivos.
- A auditoria deverá registrar usuário responsável, data/hora, justificativa e identificação dos arquivos excluídos.
- Após a confirmação, os arquivos não poderão ser recuperados pelo sistema.

### 8.79. Autenticação para exclusão definitiva

**RF153 — Confirmação com usuário e senha do administrador**

A exclusão definitiva de relatórios deverá exigir uma nova autenticação do administrador responsável.

- O sistema deverá apresentar uma janela com a identificação do relatório original e de todas as versões que serão excluídas.
- Deverá solicitar novamente o usuário e a senha do administrador.
- A autenticação deverá validar se o usuário possui perfil de administrador e permissão para executar a operação.
- Antes da exclusão, o sistema deverá informar que os dados da operação estão sendo registrados e salvos na auditoria.
- O registro deverá conter usuário responsável, data/hora, justificativa, identificação dos arquivos e resultado da operação.
- A exclusão somente poderá ocorrer após a autenticação bem-sucedida e a confirmação do registro de auditoria.
- Em caso de falha na autenticação ou no registro de auditoria, a exclusão deverá ser bloqueada.
- A senha não deverá ser armazenada nos registros de auditoria.

### 8.80. Histórico de preços do Produto Combo

**RF154 — Versionamento dos preços de Produtos Combo**

O sistema deverá manter o histórico de todas as alterações de preço dos Produtos Combo.

- Cada alteração deverá gerar um novo registro de preço, sem sobrescrever os valores anteriores.
- Cada registro deverá conter o preço, a data e hora de início da validade e o administrador responsável pela alteração.
- As vendas deverão preservar o preço efetivamente praticado no momento da operação.
- Alterações de preço não poderão modificar vendas anteriores, finalizadas ou em andamento.
- O sistema deverá permitir consultar o histórico de preços.
- Todas as alterações deverão ser registradas na auditoria.

### 8.81. Vigência dos preços de Produtos Combo

**RF155 — Aplicação imediata ou agendada de preços**

O sistema deverá permitir que o administrador defina quando um novo preço de Produto Combo entrará em vigor.

- **Aplicação imediata:** o novo preço será aplicado após a confirmação e gravação da alteração.
- **Aplicação agendada:** o administrador poderá definir uma data e horário futuros para início da vigência.
- O sistema deverá preservar o histórico de preços anteriores.
- Cada alteração deverá registrar o administrador responsável, o preço anterior, o novo preço, a data e hora de cadastro e o início da vigência.
- O sistema deverá impedir sobreposição ou inconsistência entre períodos de vigência.
- Vendas deverão utilizar o preço vigente no momento da operação, respeitando as regras de preço congelado no carrinho.
- Falhas na aplicação de preços agendados deverão ser registradas e notificadas ao administrador.

### 8.82. Alteração de preços agendados

**RF156 — Autorização para alteração ou cancelamento de agendamentos**

O sistema deverá permitir alterar ou cancelar agendamentos de preços de Produtos Combo somente mediante autorização de outro administrador.

- O administrador solicitante não poderá autorizar a própria solicitação.
- A autorização deverá ser concedida por um segundo usuário com perfil de administrador.
- O sistema deverá permitir alterar o preço programado, a data e o horário de vigência ou cancelar o agendamento.
- Toda solicitação deverá apresentar uma justificativa obrigatória.
- O sistema deverá registrar o administrador solicitante, o administrador autorizador, os dados anteriores, os novos dados e a data/hora da operação.
- O histórico original do agendamento deverá ser preservado na auditoria.
- A alteração ou o cancelamento somente poderá ocorrer após a autorização.
- A validação deverá ser realizada no backend.

### 8.83. Permissões para cadastro de preços

**RF157 — Permissão específica para alteração de preços**

O sistema deverá permitir que usuários com permissão específica solicitem alterações nos preços de Produtos Combo, independentemente do cargo ocupado.

- A permissão deverá ser configurável por usuário.
- O usuário poderá solicitar alteração imediata ou agendada.
- Toda solicitação deverá informar o novo preço e, quando aplicável, a data e horário de vigência.
- A solicitação deverá exigir justificativa.
- A aplicação do novo preço dependerá da aprovação de um administrador.
- O administrador responsável pela aprovação deverá ser identificado e não poderá ser o próprio solicitante.
- O sistema deverá registrar solicitante, autorizador, preço anterior, novo preço, justificativa e data/hora.
- Enquanto não houver aprovação, o preço vigente deverá permanecer inalterado.
- A validação das permissões e da aprovação deverá ocorrer no backend.

### 8.84. Rejeição de solicitações de alteração de preço

**RF158 — Rejeição com justificativa obrigatória**

O sistema deverá permitir que o administrador rejeite solicitações de alteração de preço de Produtos Combo.

- A rejeição deverá exigir justificativa obrigatória.
- O solicitante deverá ser notificado sobre a rejeição e seu respectivo motivo.
- O preço atualmente vigente deverá ser mantido, sem qualquer alteração.
- Solicitações rejeitadas não poderão ser aplicadas automaticamente.
- O sistema deverá registrar o solicitante, o administrador responsável pela rejeição, a justificativa e a data/hora.
- O histórico da solicitação deverá permanecer disponível para consulta.
- A rejeição deverá ser validada e registrada no backend.

### 8.86. Gestão centralizada de grupos e permissões

**RF160 — Grupos de permissões e personalização individual**

O sistema deverá disponibilizar um módulo centralizado para gerenciamento das permissões dos funcionários.

- Deverá disponibilizar grupos convencionais de permissões, como administrador, gerente, operador e estoquista.
- O administrador poderá personalizar as permissões dos grupos existentes.
- Deverá permitir a criação de novos grupos personalizados.
- Cada grupo deverá possuir permissões configuráveis por funcionalidade e operação.
- Os funcionários poderão ser vinculados a um grupo de permissões.
- O administrador poderá conceder ou retirar permissões individualmente, sem precisar modificar todo o grupo.
- As permissões individuais deverão prevalecer sobre as permissões herdadas do grupo, respeitando as regras de segurança.
- O sistema deverá permitir consultar as permissões efetivas de cada funcionário.
- Alterações nos grupos e permissões deverão ser registradas na auditoria.
- A validação das permissões deverá ocorrer no backend.

### 8.87. Aplicação de alterações nos grupos de permissões

**RF161 — Controle de aplicação das alterações dos grupos**

O sistema deverá permitir que o administrador escolha como as alterações realizadas em um grupo de permissões serão aplicadas aos funcionários vinculados.

- Deverá permitir aplicar as novas permissões automaticamente a todos os funcionários vinculados ao grupo.
- Deverá permitir aplicar as novas permissões somente aos novos funcionários vinculados ao grupo.
- A escolha deverá ser realizada no momento da alteração do grupo.
- O sistema deverá informar quantos funcionários serão afetados antes da confirmação.
- Permissões individuais personalizadas deverão ser preservadas, salvo decisão administrativa específica.
- O sistema deverá registrar as alterações, a opção escolhida, os funcionários afetados, o administrador responsável e a data/hora.
- A aplicação das permissões deverá ocorrer de forma consistente no backend.

### 8.88. Vigência das alterações de permissões

**RF162 — Aplicação das alterações de permissões e acessos**

As alterações realizadas nas permissões de grupos ou funcionários deverão entrar em vigor no próximo login do funcionário, respeitando a exceção específica para retirada de acesso a filiais definida no RF166.

**Regras:**

1. Sessões já iniciadas deverão manter as permissões carregadas no momento da autenticação.
2. Novas permissões concedidas deverão ser carregadas no próximo login do funcionário.
3. Alterações nas permissões funcionais não deverão interromper automaticamente operações em andamento.
4. A retirada de acesso a uma filial deverá seguir o comportamento específico do RF166: o funcionário poderá continuar utilizando o sistema durante a sessão ativa naquela filial, mas não poderá realizar novo acesso após encerrar a sessão ou sair da filial.
5. A exceção de acesso à filial não deverá conceder permissões funcionais adicionais ao funcionário.
6. O sistema deverá registrar a data e o horário da alteração, o responsável e os dados anteriores e posteriores.
7. O histórico das permissões anteriores deverá ser preservado.
8. O backend deverá controlar a vigência das permissões e validar o acesso conforme as regras aplicáveis.

### 8.89. Estrutura de cargos e permissões individuais

**RF163 — Grupos de permissões e personalização individual**

O sistema deverá organizar as permissões dos funcionários por meio de grupos que representam seus cargos.

- No cadastro do funcionário deverá existir o campo **Tipo de permissão**, permitindo selecionar um grupo previamente cadastrado.
- Cada funcionário deverá possuir um grupo principal de permissões.
- A tela de gerenciamento deverá apresentar os grupos cadastrados, suas descrições, permissões e funcionários vinculados.
- O administrador poderá criar, editar e desativar grupos.
- As permissões individuais deverão ser inicialmente herdadas do grupo vinculado.
- O administrador poderá conceder permissões adicionais ou retirar permissões herdadas do grupo para um funcionário específico.
- O sistema deverá identificar visualmente quais permissões são herdadas e quais foram personalizadas.
- Somente administradores poderão criar grupos, alterar suas permissões e realizar personalizações individuais.
- O sistema deverá apresentar as permissões efetivas de cada funcionário.
- Todas as alterações deverão ser registradas na auditoria.
- A validação das permissões deverá ocorrer no backend.

### 8.90. Exclusão de grupos de permissões

**RF164 — Exclusão de grupos com funcionários vinculados**

O sistema deverá permitir a exclusão de grupos de permissões, desde que sejam respeitadas as regras de tratamento dos funcionários vinculados.

- Ao solicitar a exclusão, o sistema deverá verificar se existem funcionários ativos vinculados ao grupo.
- Caso existam funcionários ativos, deverá abrir uma tela de confirmação apresentando:
  - Nome e identificação dos funcionários.
  - Grupo de permissões utilizado.
  - Filial ou filiais às quais estão vinculados.
  - Situação atual de cada cadastro.
- O sistema deverá informar que a exclusão do grupo exigirá a inativação dos funcionários ativos vinculados.
- O administrador poderá cancelar a operação sem realizar alterações.
- Caso confirme, todos os funcionários ativos vinculados deverão ser inativados antes da exclusão do grupo.
- Não deverá ocorrer transferência automática dos funcionários para outro grupo.
- A exclusão deverá ser concluída somente após a confirmação da inativação dos funcionários envolvidos.
- Caso alguma etapa falhe, o sistema deverá impedir a exclusão e evitar que a operação seja concluída parcialmente.
- Funcionários já inativos não deverão impedir a exclusão do grupo.
- O sistema deverá registrar na auditoria o administrador responsável, os funcionários inativados, as filiais envolvidas, o grupo excluído e a data/hora da operação.
- A operação deverá ser validada no backend.

### 8.91. Cadastro de acesso dos funcionários às filiais

**RF165 — Controle de acesso dos funcionários às filiais**

O sistema deverá permitir vincular cada funcionário a uma ou mais filiais da empresa, determinando em quais unidades ele poderá acessar e utilizar o PDV Web.

**Regras:**

1. O cadastro do funcionário deverá permitir selecionar individualmente uma ou mais filiais às quais ele terá acesso.
2. O funcionário somente poderá acessar informações e realizar operações nas filiais às quais estiver vinculado.
3. A seleção de filiais deverá ser independente da atribuição do grupo de permissões.
4. As permissões funcionais serão determinadas pelo grupo de permissões atribuído ao funcionário, respeitando também as personalizações individuais e por filial previstas nos requisitos correspondentes.
5. A vinculação de um funcionário a uma filial não deverá conceder permissões funcionais além daquelas que ele efetivamente possuir.
6. O administrador poderá adicionar ou remover filiais do cadastro do funcionário, respeitando as regras definidas nos RF166, RF167 e RF168.
7. A alteração das filiais autorizadas não deverá modificar automaticamente o grupo de permissões do funcionário.
8. O backend deverá validar o acesso às filiais, impedindo acessos não autorizados mesmo quando realizados por requisições diretas.
9. Todas as alterações deverão ser registradas em auditoria, identificando funcionário, filiais adicionadas ou removidas, administrador responsável, data e horário.
10. A exclusão de grupos de permissões e a inativação global dos funcionários vinculados continuarão seguindo o procedimento específico definido no RF164.

### 8.92. Alteração das filiais de acesso de funcionários

**RF166 — Aplicação de alterações de acesso às filiais**

Quando o administrador retirar uma filial da lista de filiais autorizadas de um funcionário, o sistema deverá aplicar as seguintes regras:

- Se o funcionário estiver conectado à filial no momento da alteração, poderá continuar utilizando o sistema naquela sessão.
- Após encerrar a sessão ou sair da filial, não poderá realizar um novo acesso à filial removida.
- Novas tentativas de acesso deverão ser bloqueadas pelo backend.
- A alteração não deverá interromper vendas ou operações já iniciadas durante a sessão ativa.
- O sistema deverá registrar a filial removida, o funcionário afetado, o administrador responsável e a data/hora da alteração.
- A inclusão de uma nova filial deverá respeitar as mesmas regras de validação e auditoria.

### 8.93. Obrigatoriedade de filial no cadastro de funcionários

**RF167 — Exigência de vínculo com pelo menos uma filial**

O sistema deverá exigir que todo funcionário possua pelo menos uma filial selecionada em seu cadastro.

Regras:

- Não será permitido salvar um novo funcionário sem selecionar pelo menos uma filial.
- Não será permitido manter um funcionário ativo sem nenhuma filial vinculada.
- Caso o administrador tente remover todas as filiais de um funcionário ativo, o sistema deverá bloquear a operação.
- A mensagem deverá informar que é necessário manter pelo menos uma filial vinculada.
- A validação deverá ocorrer tanto na interface quanto no backend.
- A regra deverá ser aplicada independentemente do grupo/cargo atribuído ao funcionário.

### 8.94. Alteração de filiais vinculadas ao funcionário

**RF168 — Gerenciamento manual de filiais do funcionário**

O administrador deverá conseguir alterar as filiais às quais um funcionário possui acesso diretamente pelo cadastro dele.

Regras:

- Permitir adicionar novas filiais ao funcionário.
- Permitir remover filiais anteriormente vinculadas.
- Não exigir uma operação específica de transferência.
- Permitir remover a filial antiga e adicionar a nova na mesma alteração.
- Exigir pelo menos uma filial vinculada para manter o funcionário ativo.
- Aplicar as regras de sessão ativa definidas no RF166.
- Registrar na auditoria as filiais adicionadas e removidas, o funcionário, o administrador responsável e a data/hora.
- Validar todas as alterações no backend.

### 8.95. Permissões personalizadas por filial

**RF169 — Personalização de permissões conforme a filial**

O funcionário deverá possuir um grupo/cargo principal global, cujas permissões serão herdadas em todas as filiais às quais possui acesso. O sistema também deverá permitir personalizações individuais de permissões por filial.

Regras:

- O grupo/cargo principal será único para o funcionário e terá aplicação global.
- As permissões herdadas do grupo serão aplicadas em todas as filiais vinculadas.
- O administrador poderá conceder permissões adicionais específicas para determinada filial.
- O administrador poderá restringir permissões herdadas em determinada filial.
- Uma personalização realizada em uma filial não deverá alterar automaticamente as permissões do funcionário nas demais.
- O sistema deverá apresentar claramente quais permissões são herdadas do grupo e quais são personalizadas para cada filial.
- A tela de gerenciamento deverá permitir consultar as permissões efetivas do funcionário, selecionando a filial desejada.
- As personalizações deverão respeitar as regras de segurança e autorização do sistema.
- Alterações nas permissões deverão seguir a regra de aplicação no próximo login definida no RF162.
- Todas as alterações deverão ser auditadas, identificando funcionário, filial, permissão, alteração realizada, administrador responsável e data/hora.

### 8.96. Hierarquia administrativa e histórico de alterações de permissões

**RF170 — Controle de autoridade administrativa e relatório de alterações de permissões**

O sistema deverá possuir uma hierarquia administrativa na qual o Real ADM, definido pela empresa responsável pelo desenvolvimento e controle da plataforma, determinará quais filiais cada administrador poderá administrar.

Regras:

- O Real ADM será responsável por conceder e restringir os acessos administrativos dos administradores.
- Cada administrador poderá gerenciar funcionários, grupos e permissões somente nas filiais às quais recebeu autorização.
- Administradores não poderão conceder a si próprios acessos administrativos ou permissões superiores às suas próprias atribuições.
- O sistema deverá manter um histórico de todas as alterações de permissões, incluindo concessões, restrições, remoções e alterações de grupos.
- Deverá existir um relatório específico de alterações de permissões, disponível para consulta por usuários devidamente autorizados.
- O relatório deverá apresentar:
  - Funcionário afetado.
  - Filial relacionada.
  - Grupo/cargo anterior e novo, quando aplicável.
  - Permissão anterior e nova.
  - Tipo de alteração realizada.
  - Administrador responsável.
  - Data e horário completos.
  - Justificativa, quando exigida.
- O histórico deverá preservar os valores anteriores e posteriores à alteração.
- Os registros de auditoria não poderão ser alterados ou apagados por administradores comuns.
- O relatório deverá servir como evidência para análise, investigação e resolução de divergências nas permissões.
- Todas as operações deverão ser validadas pelo backend.

### 8.97. Controle de acesso ao relatório de alterações de permissões

**RF171 — Concessão de acesso e ciência das alterações de permissões**

O relatório de alterações de permissões poderá ser consultado por usuários que possuam autorização específica, porém somente o Real ADM poderá conceder ou retirar essa autorização.

Regras:

- Somente o Real ADM poderá conceder ou retirar a permissão de consulta e exportação desse relatório.
- Administradores comuns não poderão conceder essa permissão a si mesmos ou a outros funcionários.
- O acesso deverá respeitar as filiais autorizadas para cada usuário.
- O sistema deverá registrar quem concedeu ou retirou a permissão, o usuário afetado, a data e o horário.
- Sempre que houver alteração nas permissões de um funcionário, ele deverá ser informado sobre as mudanças realizadas.
- O funcionário deverá registrar ciência da alteração, identificando a data e o horário em que tomou conhecimento.
- A ciência não representará aprovação ou autorização da mudança, apenas confirmação de que foi informado.
- A ausência de ciência deverá permanecer registrada e visível para consulta administrativa.
- O histórico de ciência deverá ser preservado e não poderá ser alterado pelo funcionário.
- Todas as operações deverão ser registradas na auditoria e validadas pelo backend.

### 8.98. Comunicação obrigatória de alterações de permissões

**RF172 — Aviso e confirmação de ciência no próximo acesso**

Sempre que houver alteração nas permissões de um funcionário, o sistema deverá apresentar uma janela obrigatória no próximo acesso desse funcionário.

Regras:

- A janela deverá apresentar as permissões concedidas, removidas ou modificadas.
- Deverá identificar a filial relacionada à alteração.
- Deverá informar quem realizou a alteração, a data e o horário.
- O funcionário deverá confirmar que tomou conhecimento das alterações.
- A confirmação de ciência será obrigatória antes de liberar o acesso às funcionalidades do sistema.
- Enquanto não houver confirmação, o funcionário não poderá prosseguir para a utilização normal do sistema.
- A confirmação não significará concordância com a alteração, apenas ciência de seu conteúdo.
- O sistema deverá registrar a confirmação, o funcionário, a data e o horário.
- Caso existam várias alterações pendentes de ciência, todas deverão ser apresentadas antes da liberação.
- O histórico de alterações e confirmações deverá permanecer disponível para consulta e auditoria.
- A validação deverá ocorrer no backend.

### 8.99. Encerramento do aviso de alteração de permissões

**RF173 — Encerramento do aviso após confirmação de ciência**

O aviso apresentado ao funcionário sobre alterações em suas permissões deverá ser exibido somente até que ele confirme que tomou ciência.

Regras:

- O aviso será apresentado no primeiro acesso após a alteração das permissões.
- Após a confirmação, o aviso deverá deixar de aparecer como pendência.
- A confirmação de ciência deverá ser vinculada ao respectivo registro no relatório de alterações de permissões.
- Após a vinculação e confirmação do registro, o aviso poderá ser excluído da lista de pendências do funcionário.
- O funcionário não terá acesso a uma tela de consulta do histórico de alterações de permissões.
- O relatório administrativo deverá preservar permanentemente o registro da alteração e a confirmação de ciência.
- A exclusão do aviso não poderá excluir, modificar ou comprometer o histórico de auditoria.

### 8.100. Limites de atuação do Real ADM

**RF174 — Separação entre administração da plataforma e suporte técnico**

O Real ADM será responsável pela administração global da plataforma PDV Web, sem possuir acesso operacional automático aos dados das empresas e de suas filiais.

Regras:

- O Real ADM poderá administrar os cadastros de administradores e suas respectivas abrangências de acesso.
- Poderá conceder e retirar permissões administrativas conforme as regras da plataforma.
- Poderá administrar configurações globais da plataforma.
- Não terá acesso operacional automático às vendas, clientes, estoque, caixa, financeiro e demais informações internas das empresas.
- As atividades de suporte técnico serão realizadas por usuários ou mecanismos específicos de suporte, conforme suas autorizações.
- Quando uma intervenção técnica exigir acesso a dados de uma empresa, deverá existir um procedimento específico de autorização, identificação do responsável, registro da finalidade, data/hora e operações realizadas.
- O acesso técnico não deverá conceder automaticamente permissões administrativas ou operacionais permanentes.
- Todas as intervenções deverão ser auditadas e rastreáveis.
- A separação entre Real ADM e suporte deverá ser respeitada pelo backend.

### 8.101. Acesso técnico do suporte às empresas

**RF175 — Usuário de suporte técnico e autorização local**

O sistema deverá possuir usuários específicos para a equipe de suporte técnico, separados dos usuários administrativos e operacionais das empresas.

Regras:

- Cada técnico deverá possuir credenciais individuais.
- As permissões técnicas serão definidas e controladas pelo Real ADM.
- O suporte deverá receber notificações quando houver necessidade de intervenção técnica.
- O acesso deverá respeitar as permissões técnicas atribuídas ao usuário.
- A autorização do administrador da empresa não será obrigatória para todas as intervenções.
- Em situações específicas, definidas pelas regras da plataforma, será exigida autorização local de um administrador da empresa antes da intervenção.
- Toda intervenção deverá registrar técnico responsável, empresa, filial, motivo, data/hora, ações realizadas e resultado.
- O acesso técnico não concederá permissões operacionais ou administrativas automaticamente.

### 8.102. Gestão interna dos usuários de suporte técnico

**RF176 — Controle restrito da equipe de suporte**

O gerenciamento dos usuários de suporte técnico deverá ser exclusivo dos desenvolvedores responsáveis pela plataforma, podendo essa responsabilidade ser delegada futuramente a usuários internos expressamente autorizados pelos desenvolvedores.

Regras:

- Somente desenvolvedores ou usuários internos formalmente autorizados poderão criar, editar, bloquear ou excluir usuários de suporte.
- Administradores das empresas clientes não poderão gerenciar usuários de suporte.
- Administradores das empresas clientes não terão acesso à visualização ou configuração das permissões técnicas dos usuários de suporte.
- As permissões técnicas serão definidas e administradas internamente pela equipe responsável pela plataforma.
- A delegação da gestão de usuários de suporte deverá ser controlada e auditada.
- Todas as alterações deverão identificar o responsável, a operação realizada e a data/hora.
- O backend deverá impedir que administradores das empresas clientes acessem ou alterem essas configurações.

### 8.103. Histórico interno de acessos do suporte técnico

**RF177 — Restrição de consulta ao histórico de intervenções técnicas**

O histórico de acessos e intervenções realizadas pelos usuários de suporte técnico deverá ser restrito à equipe interna responsável pelo desenvolvimento e manutenção da plataforma.

Regras:

- Somente desenvolvedores e usuários internos expressamente autorizados poderão consultar esse histórico.
- Administradores das empresas clientes não poderão consultar os registros de acesso ou as intervenções realizadas pelo suporte.
- O histórico deverá registrar técnico responsável, empresa, filial, motivo, data e horário, ações realizadas e resultado.
- Os registros deverão ser preservados para investigação, segurança e auditoria interna.
- Os registros não poderão ser alterados ou excluídos por usuários comuns.
- O backend deverá garantir o isolamento dessas informações.

**RF178 — Acesso técnico controlado e abrangência do suporte**

O sistema deverá permitir que os usuários de suporte técnico tenham acesso às ferramentas e funcionalidades necessárias para realizar atividades de diagnóstico, suporte e manutenção, respeitando as permissões técnicas concedidas pela equipe interna responsável pela plataforma.

**Classificação:** Futuro — relacionado à Central de Ajustes e Manutenções.

**Regras:**

1. Os técnicos de suporte poderão receber permissões técnicas para atuar em diferentes empresas e filiais, conforme suas responsabilidades.
2. O acesso técnico não deverá conceder automaticamente acesso irrestrito aos dados operacionais dos clientes.
3. O acesso a informações ou operações de uma empresa deverá respeitar os procedimentos de autorização e identificação definidos nos RF174 e RF175.
4. As permissões técnicas deverão ser específicas e administradas exclusivamente pelos desenvolvedores ou usuários internos expressamente autorizados, conforme RF176.
5. O acesso a uma empresa deverá respeitar a finalidade da intervenção e as permissões efetivamente concedidas.
6. A autorização para uma intervenção não deverá conceder acesso permanente ou permissões operacionais adicionais.
7. As atividades realizadas deverão ser registradas conforme as regras de auditoria e histórico interno definidas nos RF177 e demais requisitos relacionados.
8. Administradores das empresas clientes não poderão visualizar, alterar ou restringir as permissões técnicas internas dos usuários de suporte.
9. A validação das permissões e autorizações deverá ocorrer no backend.
10. O controle deverá preservar o isolamento dos dados entre empresas e impedir acessos não autorizados.

### 8.105. Registro de acesso dos técnicos de suporte

**RF179 — Registro automático de login e logout do suporte técnico**

O sistema deverá registrar automaticamente os acessos realizados pelos usuários de suporte técnico, sem exigir abertura de atendimento ou preenchimento de justificativa para cada acesso.

Regras:

- Registrar o usuário técnico responsável pelo acesso.
- Registrar a data e o horário exatos do login.
- Registrar a data e o horário exatos do logout.
- Registrar a empresa e a filial acessadas.
- Permitir consulta exclusivamente aos desenvolvedores e usuários internos expressamente autorizados.
- Administradores das empresas clientes não poderão consultar esses registros.
- Não armazenar senhas nos registros de acesso.
- Preservar os registros para auditoria e segurança da plataforma.
- Caso a sessão seja encerrada inesperadamente, registrar essa condição quando o sistema conseguir identificá-la.
- Todas as informações deverão ser registradas e protegidas pelo backend.

### 8.106. Autenticação dos técnicos de suporte

**RF180 — Autenticação individual da equipe de suporte**

Os usuários de suporte técnico deverão acessar a plataforma por meio de credenciais individuais.

Regras:

- Cada técnico deverá possuir um usuário exclusivo.
- Cada usuário deverá possuir uma senha individual.
- Não será permitido compartilhar credenciais entre técnicos.
- O sistema deverá validar as credenciais antes de liberar o acesso.
- As senhas deverão ser armazenadas de forma segura, utilizando hash com algoritmo apropriado e nunca em texto puro.
- Os registros de login e logout deverão identificar individualmente o técnico.
- O gerenciamento das credenciais ficará restrito aos desenvolvedores e usuários internos autorizados.

### 8.107. Bloqueio de usuários de suporte técnico

**RF181 — Bloqueio de acesso de técnicos de suporte**

O sistema deverá permitir que desenvolvedores bloqueiem usuários da equipe de suporte técnico.

Regras:

- O bloqueio impedirá novas autenticações do usuário.
- Sessões já abertas poderão continuar funcionando até serem encerradas normalmente.
- Após o logout, o usuário bloqueado não poderá realizar novo login.
- O sistema deverá registrar quem realizou o bloqueio, o técnico afetado, a data e o horário.
- O histórico de bloqueios deverá permanecer disponível para consulta interna.
- Somente desenvolvedores e usuários internos expressamente autorizados poderão bloquear ou desbloquear técnicos.
- O backend deverá validar o status do usuário em toda nova autenticação.

### 8.108. Desbloqueio de usuários de suporte técnico

**RF182 — Reativação de usuários bloqueados**

O sistema deverá permitir que desenvolvedores desbloqueiem usuários de suporte técnico anteriormente bloqueados.

Regras:

- Um desenvolvedor poderá desbloquear o mesmo usuário sem necessidade de criar um novo cadastro.
- O desbloqueio deverá permitir novas autenticações com as credenciais existentes.
- Todo o histórico anterior de acessos, bloqueios e atividades deverá ser preservado.
- O sistema deverá registrar o responsável pelo desbloqueio, o usuário afetado, a data e o horário.
- O desbloqueio não deverá apagar ou modificar registros anteriores.
- Somente desenvolvedores e usuários internos expressamente autorizados poderão realizar essa operação.
- O backend deverá validar a situação do usuário antes de permitir novos logins.

### 8.109. Exclusão definitiva de usuários de suporte técnico

**RF183 — Exclusão de usuários e preservação da identificação histórica**

O sistema deverá permitir a exclusão definitiva do cadastro de usuários de suporte técnico, preservando a integridade dos registros históricos e de auditoria.

Regras:

- A exclusão deverá remover definitivamente o cadastro e as credenciais do usuário.
- O usuário excluído não poderá realizar novas autenticações.
- Os registros históricos de acesso e auditoria deverão ser preservados.
- Nos relatórios de consulta, quando houver referência ao usuário excluído, seu nome deverá ser substituído pela identificação: "Usuário não faz mais parte da empresa".
- Essa substituição deverá ocorrer exclusivamente na apresentação dos relatórios, sem modificar os registros originais armazenados.
- Os registros originais deverão preservar sua integridade e rastreabilidade.
- A exclusão deverá ser realizada somente por desenvolvedores ou usuários internos expressamente autorizados.
- O sistema deverá registrar a operação de exclusão, o responsável e a data/hora, preservando a auditoria.

### 8.110. Recuperação de senha dos usuários de suporte

**RF184 — Recuperação de senha por e-mail corporativo**

O sistema deverá permitir que os usuários de suporte técnico solicitem a recuperação de suas senhas por meio do e-mail corporativo cadastrado.

Regras:

- O técnico poderá iniciar a recuperação diretamente pela tela de login.
- O sistema deverá enviar um link ou código de recuperação ao e-mail corporativo cadastrado.
- O link ou código deverá possuir validade limitada e utilização única.
- A redefinição deverá exigir a validação do mecanismo de recuperação.
- A nova senha deverá respeitar os critérios de segurança definidos pela plataforma.
- A senha anterior não poderá ser recuperada ou exibida.
- A recuperação deverá ser permitida somente para usuários ativos e não bloqueados.
- O sistema deverá registrar a solicitação e a conclusão da recuperação, com data e horário.
- A senha deverá ser armazenada utilizando hash seguro, nunca em texto puro.

### 8.111. Bloqueio temporário por tentativas incorretas

**RF185 — Proteção contra tentativas repetidas de autenticação**

O sistema deverá bloquear temporariamente o acesso de usuários de suporte técnico após 5 tentativas consecutivas de autenticação com credenciais incorretas.

Regras:

- O contador deverá considerar tentativas consecutivas de login incorretas.
- Ao atingir 5 tentativas, o usuário será bloqueado temporariamente.
- O bloqueio deverá ser aplicado automaticamente, sem necessidade de intervenção de um desenvolvedor.
- Após o período de bloqueio, o usuário poderá tentar autenticar novamente.
- Uma autenticação bem-sucedida deverá reiniciar o contador de tentativas incorretas.
- Todas as tentativas deverão ser registradas para auditoria.
- O registro deverá conter usuário informado, data, horário e resultado da tentativa.
- O bloqueio temporário não deverá alterar o status administrativo do usuário.
- O período de bloqueio será definido durante o detalhamento das regras de segurança.

### 8.112. Expiração automática da sessão de suporte

**RF186 — Encerramento automático por inatividade**

O sistema deverá encerrar automaticamente as sessões dos usuários de suporte técnico após um período de inatividade configurável.

Regras:

- O período de inatividade será definido pela equipe interna responsável pela plataforma.
- O sistema deverá identificar quando o técnico estiver sem realizar atividades.
- Caso exista um processo iniciado pelo técnico que ainda esteja em execução ou aguardando conclusão, a sessão não deverá ser encerrada automaticamente enquanto esse processo estiver ativo.
- A proteção deverá considerar processos efetivamente em execução, não apenas uma tela aberta ou uma solicitação sem resposta.
- Após a conclusão do processo, o controle de inatividade deverá voltar a ser aplicado.
- O encerramento automático deverá registrar usuário, data, horário e motivo do encerramento.
- O encerramento da sessão não deverá cancelar ou comprometer processos que já tenham sido iniciados e que possam continuar independentemente da sessão.
- O sistema deverá registrar o logout automático no histórico interno de acessos.

### 8.113. Sessões simultâneas de suporte técnico

**RF187 — Permissão para múltiplos acessos simultâneos**

O sistema deverá permitir que um mesmo usuário de suporte técnico mantenha várias sessões autenticadas simultaneamente em diferentes computadores ou dispositivos.

Regras:

- Não deverá existir limite de sessões simultâneas por usuário.
- Um novo login não deverá encerrar sessões anteriores.
- Cada sessão deverá possuir identificação própria.
- O sistema deverá registrar individualmente o login e logout de cada sessão, com data e horário.
- Deverá ser possível identificar o dispositivo e a sessão relacionados a cada acesso, conforme os dados técnicos disponíveis.
- O bloqueio do usuário deverá impedir novos logins, respeitando a regra definida no RF181 para sessões já abertas.
- A auditoria deverá manter o histórico individual de todas as sessões.

### 8.114. Gerenciamento manual de sessões de suporte

**RF188 — Ausência de gerenciamento manual de sessões**

O sistema não deverá disponibilizar aos desenvolvedores uma funcionalidade para consultar ou encerrar manualmente as sessões ativas dos técnicos de suporte.

Regras:

- Não haverá tela de gerenciamento manual de sessões ativas.
- As sessões seguirão as regras de autenticação, logout e expiração automática estabelecidas nos requisitos anteriores.
- O bloqueio de um técnico seguirá o comportamento definido no RF181.
- Os registros históricos de login e logout continuarão sendo mantidos conforme o RF179.

### 8.115. Encerramento de sessões após alteração de senha

**RF189 — Revogação de sessões após alteração ou recuperação de senha**

Sempre que um técnico de suporte alterar ou recuperar sua senha, o sistema deverá encerrar imediatamente todas as sessões autenticadas desse usuário em outros dispositivos.

Regras:

- Todas as sessões ativas do técnico deverão ser invalidadas.
- O encerramento deverá ocorrer independentemente do dispositivo utilizado.
- O técnico deverá realizar novo login utilizando a senha atualizada.
- Novas autenticações deverão utilizar exclusivamente a nova senha.
- O sistema deverá registrar o evento de alteração ou recuperação de senha e o encerramento das sessões na auditoria.
- A senha nunca deverá ser armazenada em texto puro nos registros de auditoria.

### 8.116. Notificações de acesso do suporte técnico

**RF190 — Ausência de notificações automáticas de login**

O sistema não deverá enviar notificações automáticas aos desenvolvedores quando um técnico de suporte realizar login.

Regras:

- Cada login e logout deverá continuar sendo registrado conforme o RF179.
- Os registros ficarão disponíveis para consulta exclusiva dos desenvolvedores e usuários internos autorizados.
- Não haverá notificações em tempo real ou por horário de acesso.
- A ausência de notificações não poderá comprometer a auditoria dos acessos.

### 8.117. Acesso do suporte durante manutenção

**RF191 — Bloqueio de acesso durante manutenção**

Durante períodos em que a plataforma estiver em manutenção, o sistema deverá bloquear também o acesso dos técnicos de suporte.

Regras:

- Nenhum técnico poderá iniciar uma nova sessão durante a manutenção.
- O bloqueio deverá ser aplicado independentemente das permissões técnicas do usuário.
- Após o encerramento da manutenção, os técnicos poderão realizar login normalmente, desde que suas contas estejam ativas e desbloqueadas.
- As tentativas de acesso durante a manutenção deverão ser registradas para auditoria.
- A manutenção não deverá alterar ou excluir os registros históricos de acesso.

### 8.118. Encerramento de sessões no início da manutenção

**RF192 — Encerramento imediato das sessões de suporte**

Quando a plataforma entrar em modo de manutenção, o sistema deverá encerrar imediatamente todas as sessões ativas dos técnicos de suporte.

Regras:

- Todas as sessões simultâneas do suporte deverão ser invalidadas.
- Nenhum técnico poderá continuar utilizando a plataforma durante a manutenção.
- Novas autenticações permanecerão bloqueadas até o encerramento da manutenção.
- O encerramento deverá ser registrado com identificação do técnico, data e horário.
- Após a manutenção, os técnicos deverão realizar novo login.
- Processos que possam continuar independentemente da sessão deverão seguir as regras de execução e segurança definidas no RF186.

### 8.119. Comunicação de manutenção

**RF193 — Ausência de avisos automáticos de manutenção**

O sistema não deverá disponibilizar avisos ou notificações automáticas aos técnicos de suporte sobre manutenções programadas ou em andamento.

Regras:

- A comunicação sobre manutenções será realizada presencialmente.
- Não haverá avisos antecipados dentro da plataforma.
- Não haverá notificações automáticas quando a manutenção começar.
- O bloqueio de acesso seguirá os requisitos RF191 e RF192.

### 8.120. Registro de manutenção da plataforma

**RF194 — Auditoria de início e término da manutenção**

O sistema deverá registrar automaticamente os eventos de entrada e saída do modo de manutenção.

Regras:

- Registrar o usuário responsável por iniciar a manutenção.
- Registrar a data e o horário exatos do início.
- Registrar o usuário responsável por encerrar a manutenção.
- Registrar a data e o horário exatos do término.
- Manter histórico de todas as manutenções realizadas.
- Os registros deverão ser protegidos contra alterações por administradores das empresas.
- A consulta ao histórico deverá ser restrita aos desenvolvedores e usuários internos autorizados.

### 8.121. Permissão para controlar o modo de manutenção

**RF195 — Controle de acesso ao modo de manutenção**

O sistema deverá possuir uma permissão específica para ativar e desativar o modo de manutenção da plataforma.

Regras:

- Inicialmente, somente desenvolvedores terão essa permissão.
- A permissão deverá ser independente das permissões administrativas das empresas.
- Os desenvolvedores poderão conceder futuramente essa permissão a outros usuários internos.
- Somente usuários que possuírem a permissão específica poderão iniciar ou encerrar uma manutenção.
- Toda concessão, alteração ou remoção dessa permissão deverá ser registrada em auditoria.
- As validações deverão ocorrer no backend.

### 8.122. Acesso das empresas durante a manutenção

**RF196 — Controle de acesso conforme o tipo de manutenção**

O sistema deverá permitir que o acesso dos usuários das empresas seja mantido ou bloqueado conforme o tipo de manutenção realizada.

Regras:

- A ativação do modo de manutenção não deverá bloquear automaticamente todos os usuários das empresas.
- O comportamento do acesso deverá depender do tipo de manutenção.
- Manutenções que não comprometam a operação das empresas poderão ocorrer com os usuários utilizando normalmente a plataforma.
- Quando uma manutenção exigir indisponibilidade, o acesso poderá ser bloqueado conforme sua necessidade.
- A definição dos tipos de manutenção e seus respectivos comportamentos será detalhada posteriormente na documentação técnica.
- O controle deverá respeitar as regras de segurança e as permissões administrativas da plataforma.

### 8.123. Classificação de manutenções

**RF197 — Não obrigatoriedade de classificação**

O sistema não deverá exigir classificações predefinidas para os tipos de manutenção.

Regras:

- Não haverá categorias obrigatórias de manutenção leve, parcial ou completa.
- A equipe de desenvolvedores poderá conduzir cada manutenção conforme sua necessidade.
- Quando houver impacto nas operações das empresas, os usuários afetados serão comunicados presencialmente.
- O comportamento de acesso seguirá a necessidade de cada manutenção, conforme RF196.

### 8.124. Identificação do motivo da manutenção

**RF198 — Motivo obrigatório para manutenção**

Ao iniciar uma manutenção, o desenvolvedor deverá informar obrigatoriamente o motivo da intervenção.

Regras:

- O motivo deverá ser preenchido antes de ativar o modo de manutenção.
- O sistema não permitirá iniciar a manutenção sem essa informação.
- O motivo deverá ser armazenado junto ao registro de manutenção do RF194.
- O motivo deverá permanecer disponível para consulta no histórico.
- O registro deverá identificar o responsável, a data e o horário de início e término da manutenção.

### 8.125. Alteração do motivo da manutenção

**RF199 — Histórico de alterações do motivo**

O sistema deverá permitir que o desenvolvedor altere o motivo de uma manutenção já iniciada.

Regras:

- A alteração poderá ser realizada sem autorização de outro desenvolvedor.
- O motivo original deverá permanecer preservado no histórico.
- Cada alteração deverá registrar o novo motivo.
- Registrar o usuário responsável, a data e o horário da alteração.
- Manter o histórico completo de todas as alterações.
- Não permitir alterações que eliminem ou sobrescrevam os registros anteriores.

### 8.126. Central de Ajustes e Manutenções

**Nota de planejamento:** os requisitos desta Central permanecem documentados e preservados, mas sua implementação deverá ocorrer somente na última etapa do projeto, após a conclusão do PDV principal.

**RF200 — Centralização do histórico de intervenções em clientes de produção**

O sistema deverá disponibilizar uma Central de Ajustes e Manutenções, acessível exclusivamente aos desenvolvedores e aos usuários internos expressamente autorizados.

A central deverá permitir:

- Consultar o histórico de manutenções realizadas nos clientes.
- Consultar ajustes e intervenções realizados nos ambientes de produção.
- Identificar a empresa e a filial envolvidas.
- Identificar o desenvolvedor responsável.
- Consultar motivos, descrições, datas e horários das intervenções.
- Consultar alterações realizadas durante uma manutenção.
- Manter o histórico centralizado, sem necessidade de acessar individualmente o ambiente do cliente.
- Restringir a consulta conforme as permissões internas.
- Preservar os registros históricos para auditoria.

A central será o ponto de referência para acompanhamento e consulta das intervenções técnicas realizadas nos clientes em produção.

### 8.127. Painel administrativo central dos desenvolvedores

**RF201 — Interface centralizada de administração dos clientes**

O sistema deverá disponibilizar uma interface administrativa exclusiva dos desenvolvedores e usuários internos autorizados, denominada Central de Ajustes.

A central deverá possuir janelas e módulos independentes para administrar e consultar os clientes em produção.

Funcionalidades previstas:

- **Gestão de clientes:** consultar empresas, filiais e informações dos ambientes.
- **Permissões:** administrar permissões de acesso e funcionalidades dos clientes.
- **Regras de negócio:** consultar, configurar e controlar regras aplicáveis a cada cliente.
- **Integrações:** administrar integrações disponíveis, seus parâmetros e configurações.
- **Ajustes em produção:** acompanhar e administrar alterações realizadas nos clientes.
- **Manutenções:** consultar e controlar manutenções da plataforma e dos ambientes.
- **Auditoria:** consultar o histórico de alterações, responsáveis, clientes envolvidos, datas e horários.

Regras:

- O acesso será restrito aos desenvolvedores e usuários internos expressamente autorizados.
- A central deverá permitir administrar os clientes sem exigir acesso individual às suas interfaces operacionais.
- As configurações deverão respeitar a separação entre empresas e filiais.
- As alterações deverão possuir rastreabilidade e registro de auditoria.
- Os detalhes de funcionamento de cada módulo serão definidos nos requisitos específicos.

### 8.128. Seleção e acesso aos clientes na Central de Ajustes

**RF202 — Listagem e acesso administrativo aos clientes**

A Central de Ajustes deverá apresentar uma lista de clientes cadastrados na plataforma.

Regras:

- Exibir os clientes disponíveis para administração.
- Permitir selecionar diretamente a empresa desejada.
- Após selecionar a empresa, apresentar suas filiais, quando houver.
- Permitir selecionar a filial que será administrada.
- Ao selecionar uma filial, abrir uma área administrativa com acesso aos cadastros, permissões, regras de negócio, integrações e demais configurações disponíveis.
- Empresas sem filiais adicionais deverão permitir acesso direto à sua área administrativa.
- Os módulos e funcionalidades dessa área poderão ser ampliados futuramente.
- Todas as consultas e alterações deverão respeitar as permissões do usuário interno e manter auditoria.

### 8.129. Pesquisa e filtros de clientes

**RF203 — Localização avançada de clientes**

A Central de Ajustes deverá disponibilizar pesquisa e filtros para localizar clientes cadastrados na plataforma.

Regras:

- Permitir pesquisar pelo nome da empresa.
- Permitir pesquisar pelo CNPJ.
- Permitir pesquisar pelo código de identificação do cliente.
- Permitir filtrar clientes pela situação: ativo ou inativo.
- Permitir combinar diferentes critérios de pesquisa.
- Exibir os resultados de acordo com as permissões de acesso do usuário interno.
- A pesquisa deverá permitir localizar clientes sem necessidade de percorrer toda a listagem.

### 8.130. Cadastro de empresas clientes

**RF204 — Cadastro e provisionamento de empresas clientes**

O sistema deverá permitir que desenvolvedores autorizados realizem o cadastro e o provisionamento de novas empresas clientes na plataforma, respeitando o modelo de hospedagem definido para cada contratação.

**Classificação:** Futuro — relacionado à Central de Ajustes e Manutenções.

**Regras:**

1. Somente desenvolvedores ou usuários internos expressamente autorizados poderão cadastrar novas empresas clientes na plataforma.
2. Administradores das empresas clientes não poderão cadastrar outras empresas na plataforma.
3. O provisionamento deverá respeitar o modelo de hospedagem definido para o cliente.
4. No modelo compartilhado, os dados deverão permanecer isolados logicamente, impedindo acesso entre empresas sem autorização.
5. Quando contratado ou tecnicamente necessário, o sistema poderá provisionar um banco de dados ou ambiente isolado para a empresa.
6. O sistema deverá vincular cada empresa à estrutura de armazenamento correspondente.
7. O cadastro somente deverá ser considerado concluído após a confirmação de que a estrutura necessária foi criada e configurada corretamente.
8. Falhas no provisionamento deverão ser registradas e impedir a conclusão do cadastro.
9. O processo deverá permitir futuras alterações do modelo de hospedagem, mediante planejamento técnico e preservação da integridade dos dados.
10. Todas as operações deverão possuir auditoria, identificando o responsável, a empresa, o modelo de hospedagem utilizado, a data e o horário da operação.
### 8.131. Ativação e situação operacional das empresas

**RF205 — Ativação manual e controle de status dos clientes**

A ativação de uma empresa cliente deverá ser realizada manualmente por um desenvolvedor, somente após a conclusão da preparação de sua base.

Regras:

- A criação do cadastro e do banco de dados não deverá ativar automaticamente a empresa.
- A empresa permanecerá inativa enquanto sua base estiver em preparação.
- Somente desenvolvedores poderão realizar a ativação.
- O sistema deverá disponibilizar status para identificar a situação de cada empresa.
- Deverá ser possível identificar quando a empresa estiver pronta para operar e quando efetivamente iniciar suas operações com o PDV Web.
- A alteração de status deverá registrar responsável, data e horário.
- A ativação deverá validar se o banco de dados e as configurações necessárias estão preparados.
- Todas as alterações deverão possuir auditoria.

### 8.132. Status das empresas clientes

**RF206 — Controle do ciclo de vida dos clientes**

A Central de Ajustes deverá apresentar os seguintes status para cada empresa:

1. **Cadastro em preparação:** empresa cadastrada, com banco de dados em processo de criação ou configuração.
2. **Aguardando ativação:** base preparada, aguardando liberação de um desenvolvedor.
3. **Ativa:** empresa habilitada para utilizar a plataforma.
4. **Em implantação:** empresa em processo de configuração, testes e preparação operacional.
5. **Em operação:** empresa iniciou efetivamente suas atividades utilizando o PDV Web.
6. **Inativa:** empresa temporariamente ou definitivamente sem operação na plataforma.

Regras:

- A Central de Ajustes deverá exibir o status atual de cada empresa.
- As alterações de status deverão ser realizadas por usuários autorizados, respeitando as permissões de cada operação.
- Toda alteração deverá registrar responsável, data, horário e status anterior e novo.
- O histórico de status deverá permanecer disponível para consulta.
- As transições permitidas entre os status serão detalhadas posteriormente.

### 8.133. Permissão para alteração do status dos clientes

**RF207 — Controle de acesso à alteração de status**

O sistema deverá permitir que desenvolvedores e usuários internos expressamente autorizados alterem manualmente o status das empresas na Central de Ajustes.

Regras:

- A alteração deverá exigir uma permissão específica.
- Inicialmente, os desenvolvedores terão essa permissão.
- Desenvolvedores poderão concedê-la futuramente a outros usuários internos.
- Administradores das empresas não poderão alterar o status da própria empresa.
- O sistema deverá validar a permissão no backend.
- Toda alteração deverá registrar responsável, empresa, status anterior, novo status, data e horário.
- O histórico de alterações deverá permanecer disponível para auditoria.

### 8.134. Exclusão de empresas clientes

**RF208 — Exclusão definitiva mediante autorização e backup**

O sistema deverá permitir a exclusão definitiva de uma empresa cliente somente mediante autorização de outro desenvolvedor e geração prévia de backup.

Regras:

- A exclusão deverá exigir solicitação de um desenvolvedor.
- A autorização deverá ser realizada por outro desenvolvedor, diferente do solicitante.
- Antes da exclusão, o sistema deverá gerar um backup completo do banco de dados individual da empresa.
- A exclusão somente poderá prosseguir após a confirmação de que o backup foi gerado e armazenado corretamente.
- Caso a geração ou o armazenamento do backup falhe, a exclusão deverá ser bloqueada.
- O sistema deverá solicitar confirmação explícita antes da exclusão definitiva.
- A exclusão deverá remover o cadastro da empresa e seu banco de dados individual.
- O histórico de auditoria da solicitação, autorização, backup e exclusão deverá ser preservado.
- Registrar os responsáveis, a empresa, a data e o horário de cada etapa.
- Os detalhes sobre armazenamento, retenção e recuperação dos backups serão definidos na arquitetura.

### 8.135. Condição para exclusão definitiva de empresas

**RF209 — Obrigatoriedade de inativação prévia**

O sistema deverá permitir a solicitação de exclusão definitiva somente para empresas que estejam com o status **Inativa**.

Regras:

- Empresas com qualquer outro status não poderão ter sua exclusão solicitada.
- O sistema deverá validar o status atual antes de permitir a solicitação.
- Caso a empresa não esteja inativa, deverá informar que a inativação é obrigatória.
- A inativação não deverá excluir ou alterar os dados armazenados no banco de dados da empresa.
- Após a inativação, a solicitação deverá seguir todas as etapas de autorização e backup definidas no RF208.
- Todas as tentativas de solicitação deverão ser registradas em auditoria.

### 8.136. Prazo de retenção antes da exclusão definitiva

**RF210 — Prazo configurável para exclusão de empresas**

O sistema deverá permitir configurar um prazo mínimo de retenção após a inativação de uma empresa, antes que sua exclusão definitiva possa ser solicitada.

Regras:

- Somente desenvolvedores poderão configurar esse prazo.
- O prazo poderá ser definido individualmente para cada empresa.
- O sistema deverá registrar a data e o horário da inativação.
- Enquanto o prazo não for cumprido, a solicitação de exclusão deverá permanecer bloqueada.
- Após o cumprimento do prazo, a exclusão poderá ser solicitada, respeitando as regras de autorização e backup do RF208.
- Alterações no prazo deverão ser registradas em auditoria, identificando responsável, valor anterior, novo valor, data e horário.

### 8.137. Acesso após inativação da empresa

**RF211 — Consulta de relatórios históricos**

O sistema deverá permitir manter o acesso da empresa inativa exclusivamente para consulta de relatórios referentes ao período em que esteve ativa.

Regras:

- O acesso somente para consulta será opcional.
- Essa possibilidade somente estará disponível para empresas que possuam o sistema instalado localmente.
- Quando habilitado, os usuários autorizados poderão consultar relatórios históricos do período em que a empresa estava ativa.
- Não será permitido realizar vendas, movimentações, cadastros ou alterações nos dados.
- Os dados históricos deverão permanecer preservados.
- Quando a opção estiver desabilitada, o acesso dos usuários da empresa permanecerá bloqueado.
- A configuração será controlada pela Central de Ajustes e registrada em auditoria.

### 8.138. Prazo de acesso aos relatórios históricos

**RF212 — Disponibilidade permanente dos relatórios após inativação**

O sistema deverá permitir a consulta dos relatórios históricos da empresa inativa por tempo indeterminado, desde que o acesso local esteja habilitado conforme RF211.

Regras:

- O acesso permanecerá disponível até a exclusão definitiva da empresa.
- Os relatórios deverão permanecer armazenados no ambiente local do cliente.
- A conservação dos equipamentos, armazenamento e infraestrutura local será responsabilidade da empresa cliente.
- A empresa responsável pelo PDV Web não será responsável pela perda de acesso decorrente de falhas, danos ou substituição do hardware do cliente.
- A consulta continuará restrita aos relatórios históricos, sem permitir operações ou alterações nos dados.
- A exclusão definitiva seguirá os procedimentos de autorização e backup definidos nos RF208, RF209 e RF210.

### 8.139. Reativação de empresas inativas

**RF213 — Reativação exclusiva por desenvolvedores**

O sistema deverá permitir a reativação de empresas inativas exclusivamente por desenvolvedores.

Regras:

- Somente desenvolvedores poderão executar a reativação.
- Antes da reativação, deverá ser verificada a integridade do banco de dados individual da empresa.
- Deverão ser verificadas as configurações necessárias para o funcionamento da plataforma.
- A reativação deverá preservar o banco de dados e o histórico anterior da empresa.
- Não deverá ser criado um novo banco de dados para a reativação.
- O sistema deverá registrar o responsável, a empresa, a data e o horário da reativação.
- Após a reativação, o status da empresa deverá ser atualizado na Central de Ajustes.
- A reativação deverá restabelecer os acessos conforme as permissões e configurações vigentes.

### 8.140. Acesso dos usuários após inativação

**RF214 — Encerramento de sessões operacionais e acesso local para consultas**

Quando uma empresa for inativada, o sistema deverá encerrar imediatamente todas as sessões operacionais do PDV e desativar seus serviços online, mantendo apenas o acesso local permitido para consultas.

Regras:

- Todas as sessões abertas no PDV deverão ser encerradas imediatamente.
- Novas sessões operacionais não poderão ser iniciadas.
- Os serviços online da empresa deverão ser desativados.
- O sistema deverá continuar disponível exclusivamente no ambiente instalado localmente.
- Usuários administrativos autorizados poderão acessar a área administrativa local exclusivamente para consultas.
- Não será permitido cadastrar, editar, excluir ou alterar informações.
- A consulta aos relatórios históricos seguirá as condições definidas nos RF211 e RF212.
- O acesso deverá respeitar as permissões de consulta de cada usuário.
- A inativação deverá ser aplicada no backend, impedindo operações mesmo que existam telas abertas.

### 8.141. Reativação individual dos serviços online

**RF215 — Seleção dos serviços online durante a reativação**

Quando uma empresa for reativada, o sistema deverá permitir que o desenvolvedor escolha individualmente quais serviços online serão reativados.

Regras:

- A reativação da empresa não deverá ativar automaticamente todos os serviços online.
- O desenvolvedor deverá visualizar os serviços disponíveis para a empresa.
- Cada serviço poderá ser selecionado individualmente para reativação.
- Os serviços não selecionados deverão permanecer desativados.
- A ativação deverá respeitar as configurações, dependências e condições técnicas de cada serviço.
- O sistema deverá registrar quais serviços foram reativados, o responsável, a empresa, a data e a hora da operação.
- A operação deverá ser validada no backend.
- Somente desenvolvedores com permissão específica poderão realizar essa operação.

### 8.142. Monitoramento e histórico dos serviços online

**RF216 — Status detalhado e histórico individual dos serviços online**

O sistema deverá disponibilizar, na Central de Ajustes, a situação individual de cada serviço online de uma empresa, permitindo consultar seu status atual e o histórico de ativações e desativações.

Regras:

- Cada serviço deverá apresentar seu status individual:
  - Ativo.
  - Inativo.
  - Aguardando configuração.
  - Com falha na ativação.
- O sistema deverá manter um histórico individual de cada serviço.
- O histórico deverá registrar ativações e desativações, empresa, serviço, responsável, data e hora.
- Quando houver falha na ativação, o status deverá indicar essa condição.
- O histórico deverá preservar os registros anteriores, sem sobrescrita.
- A consulta deverá respeitar as permissões de acesso à Central de Ajustes.
- Os registros deverão ser armazenados e validados no backend.

### 8.143. Tratamento de falhas na ativação dos serviços online

**RF217 — Registro de falhas e nova tentativa manual**

Quando ocorrer uma falha durante a ativação de um serviço online, o sistema deverá registrar a ocorrência e permitir que o desenvolvedor realize uma nova tentativa manualmente.

Regras:

- O sistema deverá registrar a falha de ativação.
- O serviço deverá permanecer com o status **Com falha na ativação**, conforme RF216.
- O sistema não deverá realizar novas tentativas automaticamente.
- Desenvolvedores com permissão específica poderão solicitar uma nova tentativa de ativação.
- Cada nova tentativa deverá gerar um registro no histórico do serviço.
- O registro deverá conter empresa, serviço, responsável, data, hora e resultado da tentativa.
- A operação deverá ser validada no backend.

### 8.144. Ativação independente de múltiplos serviços

**RF218 — Continuidade da reativação em caso de falha individual**

Quando o desenvolvedor selecionar vários serviços online para reativação e um ou mais apresentarem falhas, o sistema deverá continuar o processo de ativação dos demais serviços selecionados.

Regras:

- Cada serviço deverá ser processado individualmente.
- A falha de um serviço não deverá interromper a ativação dos demais.
- Os serviços ativados com sucesso deverão permanecer ativos.
- Os serviços que apresentarem falha deverão permanecer inativos, com status **Com falha na ativação**.
- O sistema deverá registrar individualmente o resultado de cada serviço.
- O desenvolvedor deverá conseguir identificar quais serviços foram ativados e quais apresentaram falha.
- Novas tentativas para serviços com falha deverão ser exclusivamente manuais, conforme RF217.
- A operação deverá ser validada no backend.

### 8.145. Gerenciamento automático de dependências entre serviços

**RF219 — Ativação automática de serviços dependentes**

Quando um serviço online selecionado para reativação depender de outro serviço que esteja inativo, o sistema deverá identificar essa dependência e ativar automaticamente o serviço necessário.

Regras:

- O sistema deverá identificar previamente as dependências entre os serviços.
- Os serviços necessários deverão ser ativados automaticamente, mesmo que não tenham sido selecionados individualmente pelo desenvolvedor.
- A ativação deverá respeitar a ordem necessária de dependência entre os serviços.
- Cada serviço ativado automaticamente deverá ter seu resultado registrado individualmente.
- Os serviços ativados automaticamente deverão aparecer na relação de serviços reativados.
- Caso um serviço necessário apresente falha, o serviço que depende dele não deverá ser ativado.
-

### 8.146. Desativação de serviços com dependências

**RF220 — Desativação conjunta de serviços dependentes**

Quando um desenvolvedor solicitar a desativação de um serviço online que possua outros serviços ativos dependentes dele, o sistema deverá impedir a desativação isolada.

Regras:

- O sistema deverá identificar todos os serviços ativos que dependam direta ou indiretamente do serviço selecionado.
- Deverá apresentar ao desenvolvedor a relação completa dos serviços afetados.
- O desenvolvedor poderá confirmar a desativação conjunta do serviço selecionado e de todos os seus dependentes.
- Caso não confirme a desativação conjunta, a operação deverá ser cancelada.
- Nenhum serviço deverá ser desativado parcialmente em decorrência dessa operação.
- A desativação deverá respeitar a ordem das dependências.
- Cada serviço desativado deverá gerar um registro individual no histórico, identificando responsável, empresa, serviço, data, hora e motivo da operação.
- A regra poderá ser revista futuramente pelos responsáveis pelo desenvolvimento da plataforma.

### 8.147. Reversão de dependências em caso de falha

**RF221 — Desativação de dependências quando a ativação principal falhar**

Quando um serviço online for ativado e depender de outros serviços que tenham sido ativados automaticamente durante a mesma operação, caso o serviço principal apresente falha, o sistema deverá desativar as dependências ativadas exclusivamente para aquela operação.

Regras:

- O sistema deverá identificar quais dependências foram ativadas automaticamente durante a operação.
- Caso o serviço principal apresente falha, as dependências ativadas exclusivamente para aquela operação deverão ser desativadas.
- Serviços que já estavam ativos antes da operação deverão permanecer ativos.
- A desativação das dependências deverá respeitar a ordem necessária para evitar inconsistências.
- Cada ativação e desativação deverá ser registrada individualmente no histórico.
- O histórico deverá identificar o motivo da reversão e o serviço principal que apresentou falha.
- A operação deverá ser validada no backend.

### 8.148. Controle de dependências compartilhadas

**RF222 — Gerenciamento de dependências utilizadas por múltiplos serviços**

O sistema deverá controlar as dependências compartilhadas entre diferentes serviços online, garantindo que a falha ou desativação de um serviço não prejudique outros serviços que ainda necessitem da mesma dependência.

Regras:

- O sistema deverá identificar todos os serviços que utilizam cada dependência.
- Quando um serviço principal apresentar falha, o sistema deverá verificar se outros serviços ativos ainda dependem dos mesmos recursos.
- Caso exista outro serviço ativo que necessite da dependência, ela deverá permanecer ativa.
- Caso nenhum serviço ativo necessite da dependência, ela poderá ser desativada.
- A desativação deverá considerar dependências diretas e indiretas.
- O sistema deverá evitar desativações que prejudiquem serviços ativos.
- O histórico deverá registrar a utilização, ativação, manutenção e desativação das dependências.
- O sistema deverá identificar quais serviços justificam a permanência de cada dependência.
- Todas as operações deverão ser validadas no backend.

### 8.149. Tratamento de dependências circulares

**RF223 — Identificação, teste e resolução de dependências circulares**

O sistema deverá identificar dependências circulares entre serviços online e tentar encontrar automaticamente uma solução segura antes de solicitar intervenção do desenvolvedor.

Regras:

- O sistema deverá identificar ciclos de dependência entre dois ou mais serviços.
- Antes de solicitar intervenção, deverá realizar uma análise automática para verificar se existe uma ordem segura de ativação.
- A solução automática deverá ser testada antes de ser aplicada.
- Caso a solução automática funcione e seja validada, o sistema poderá prosseguir com a ativação.
- Caso não seja possível resolver o ciclo automaticamente, deverá apresentar ao desenvolvedor os serviços envolvidos e suas dependências.
- O desenvolvedor poderá definir manualmente a solução.
- A solução manual deverá passar por validação antes de ser aplicada.
- Nenhuma alteração deverá ser aplicada enquanto houver risco de inconsistência nas dependências.
- Todas as análises, tentativas, decisões e resultados deverão ser registrados no histórico.
- A operação deverá ser validada no backend.

### 8.150. Versionamento e validação de dependências

**RF224 — Alteração controlada das dependências dos serviços online**

O sistema deverá permitir alterações nas dependências dos serviços online por meio de novas versões de configuração, realizando validações e testes antes da aplicação em produção.

Regras:

- O desenvolvedor deverá poder criar uma nova versão da configuração sem alterar a versão atualmente utilizada em produção.
- O sistema deverá analisar as dependências e identificar ciclos, dependências ausentes e incompatibilidades.
- A nova configuração deverá ser testada em ambiente controlado, sem afetar os serviços em produção.
- Não será necessário criar um ambiente completo e exclusivo para cada alteração.
- Caso os testes apresentem falhas, a nova configuração não poderá ser aplicada em produção.
- Após a aprovação dos testes, o desenvolvedor deverá confirmar a aplicação da nova configuração.
- O sistema deverá preservar as versões anteriores das configurações.
- Deverá ser possível retornar à versão anterior em caso de problemas após a aplicação.
- Todas as alterações, testes, aprovações e aplicações deverão ser registrados no histórico, com responsável, data e hora.
- A operação deverá ser validada no backend.

### 8.151. Tratamento de falhas após alterações em produção

**RF225 — Identificação de falhas, alerta à Central e reversão de configurações**

Quando uma nova configuração de dependências for aplicada em produção e apresentar problemas posteriormente, o sistema deverá identificar a falha, registrar a ocorrência e encaminhá-la à Central de Ajustes e Manutenções.

Regras:

- O sistema deverá monitorar a integridade e o funcionamento das dependências após a aplicação da nova configuração.
- Quando identificar uma falha, deverá gerar um registro da ocorrência.
- A ocorrência deverá ser encaminhada à Central de Ajustes e Manutenções para análise dos desenvolvedores.
- O registro deverá identificar a empresa, os serviços afetados, a configuração aplicada, a versão anterior, a descrição da falha, a data e a hora.
- O desenvolvedor deverá receber um alerta na Central para analisar a ocorrência.
- O desenvolvedor poderá decidir entre manter a configuração atual ou retornar à versão anterior.
- A reversão deverá restaurar a configuração anterior preservada pelo sistema.
- A decisão e o resultado da operação deverão ser registrados no histórico.
- O sistema não deverá realizar reversões automaticamente.
- Todas as operações deverão ser validadas no backend.

### 8.152. Classificação de gravidade das falhas

**RF226 — Classificação automática e ajuste manual da gravidade das ocorrências**

A Central de Ajustes e Manutenções deverá classificar automaticamente as falhas identificadas nos serviços online, considerando seu impacto e abrangência.

Regras:

- A classificação deverá considerar:
  - Quantidade de serviços afetados.
  - Impacto sobre o funcionamento dos serviços.
  - Quantidade de empresas atingidas.
  - Comprometimento das operações dos clientes.
- As ocorrências deverão receber uma das seguintes classificações:
  - Baixa.
  - Média.
  - Alta.
  - Crítica.
- O sistema deverá permitir que desenvolvedores com permissão específica alterem manualmente a classificação.
- Toda alteração manual deverá exigir uma justificativa.
- A classificação original deverá ser preservada no histórico.
- Caso não existam informações suficientes para determinar a gravidade, a ocorrência deverá ser encaminhada para avaliação manual.
- A classificação deverá servir como referência para organização e priorização das ocorrências na Central.
- Todas as classificações e alterações deverão ser registradas com responsável, data e hora.
- A operação deverá ser validada no backend.

### 8.153. Priorização das ocorrências na Central

**RF227 — Priorização automática e ordenação manual das ocorrências**

A Central de Ajustes e Manutenções deverá organizar automaticamente as ocorrências registradas, considerando sua gravidade e impacto, permitindo que os desenvolvedores alterem manualmente a prioridade.

Regras:

- O sistema deverá priorizar automaticamente as ocorrências conforme a classificação de gravidade definida no RF226.
- A quantidade de serviços e empresas afetadas deverá ser considerada na priorização.
- Ocorrências críticas deverão receber maior prioridade.
- Desenvolvedores com permissão específica poderão alterar manualmente a prioridade.
- Alterações manuais deverão exigir justificativa.
- A prioridade automática original deverá ser preservada no histórico.
- A Central deverá permitir visualizar a prioridade atual e a classificação de gravidade de cada ocorrência.
- Todas as alterações deverão registrar responsável, justificativa, data e hora.
- A operação deverá ser validada no backend.

### 8.154. Acompanhamento e gestão das ocorrências

**RF228 — Fluxo detalhado de atendimento e acompanhamento de falhas**

A Central de Ajustes e Manutenções deverá disponibilizar um fluxo detalhado para acompanhamento das ocorrências, permitindo registrar todas as etapas do atendimento até sua resolução.

Regras:

- Cada ocorrência deverá possuir etapas de acompanhamento:
  - Nova.
  - Em análise.
  - Em correção.
  - Aguardando validação.
  - Resolvida.
- O sistema deverá permitir registrar comentários e observações durante o atendimento.
- Deverá permitir registrar procedimentos realizados e soluções aplicadas.
- Cada etapa deverá identificar o responsável pela execução.
- Deverá ser possível registrar resultados de testes e validações.
- O histórico deverá preservar todas as etapas anteriores, sem sobrescrever registros.
- Cada movimentação deverá registrar responsável, data e hora.
- Deverá ser possível consultar todo o histórico da ocorrência.
- A conclusão deverá registrar o resultado final e a solução aplicada.
- O acesso deverá respeitar as permissões da Central de Ajustes e Manutenções.
- Todas as operações deverão ser validadas no backend.

### 8.155. Reabertura de ocorrências resolvidas

**RF229 — Reabertura manual e automática de ocorrências**

O sistema deverá permitir a reabertura de ocorrências anteriormente resolvidas, tanto por ação manual dos desenvolvedores quanto pela identificação automática da recorrência de uma falha.

Regras:

- Desenvolvedores com permissão específica poderão reabrir manualmente uma ocorrência.
- O sistema poderá reabrir automaticamente uma ocorrência quando identificar novamente a mesma falha, conforme critérios técnicos definidos.
- A reabertura deverá preservar integralmente o histórico anterior.
- A ocorrência deverá retornar para o fluxo de atendimento, com status atualizado.
- O sistema deverá registrar se a reabertura foi manual ou automática.
- Deverá registrar o motivo, responsável quando aplicável, data e hora.
- O novo atendimento deverá manter o vínculo com a ocorrência original.
- O histórico deverá permitir distinguir os diferentes ciclos de atendimento e resolução.
- A operação deverá ser validada no backend.

### 8.156. Identificação e agrupamento de ocorrências recorrentes

**RF230 — Agrupamento inteligente de falhas e identificação de causas comuns**

A Central de Ajustes e Manutenções deverá identificar falhas semelhantes que ocorram repetidamente em uma ou várias empresas, permitindo agrupá-las e analisar possíveis causas comuns.

Regras:

- O sistema deverá analisar as características das ocorrências para identificar possíveis semelhanças.
- Falhas identificadas como semelhantes poderão ser agrupadas em uma ocorrência principal.
- Cada ocorrência individual deverá manter seu próprio histórico, empresa, filial, serviços afetados e registros de atendimento.
- A ocorrência principal deverá apresentar uma visão consolidada das empresas, filiais e serviços afetados.
- O sistema deverá identificar possíveis causas comuns, apresentando-as como hipóteses para análise dos desenvolvedores.
- O agrupamento deverá considerar gravidade, quantidade de empresas afetadas e impacto na operação.
- Os desenvolvedores deverão poder revisar os agrupamentos e corrigir associações indevidas.
- A separação de ocorrências deverá preservar todos os registros individuais.
- O sistema deverá registrar agrupamentos, alterações e separações, identificando responsável, data e hora.
- A análise automática não deverá substituir a avaliação técnica dos desenvolvedores.
- Todas as operações deverão ser validadas no backend.

### 8.157. Análise técnica de causa raiz

**RF231 — Análise automática de logs, alterações e relações entre falhas**

A Central de Ajustes e Manutenções deverá realizar análises técnicas automáticas das ocorrências agrupadas, utilizando registros e informações da própria plataforma para auxiliar os desenvolvedores na identificação de possíveis causas raiz.

Regras:

- O sistema deverá analisar os logs relacionados às ocorrências.
- Deverá consultar o histórico de alterações realizadas nos serviços e configurações envolvidos.
- Deverá identificar relações entre serviços, dependências e falhas registradas.
- Deverá considerar a sequência temporal dos acontecimentos.
- Deverá apresentar possíveis causas técnicas identificadas pela análise.
- Quando possível, deverá apresentar as evidências e os registros que fundamentaram cada hipótese.
- As hipóteses deverão ser apresentadas como possibilidades, não como causas confirmadas.
- Os desenvolvedores deverão realizar a avaliação técnica e determinar a causa raiz.
- A análise não deverá realizar correções automaticamente.
- Os resultados da análise deverão ficar vinculados à ocorrência principal e preservar o histórico.
- Todas as análises deverão ser registradas com data e hora.
- O processamento deverá ocorrer no backend.

### 8.158. Consulta de evidências técnicas no ambiente do cliente

**RF232 — Consulta direta de logs e evidências técnicas**

A Central de Ajustes e Manutenções não deverá manter cópias permanentes de logs ou evidências técnicas dos ambientes dos clientes. Quando necessário, os desenvolvedores poderão consultar essas informações diretamente no ambiente de origem.

Regras:

- A Central não deverá armazenar cópias permanentes dos logs e evidências técnicas dos clientes.
- Quando necessário, os desenvolvedores poderão acessar o ambiente do cliente para consultar as informações.
- O acesso deverá respeitar as permissões e os procedimentos de segurança definidos para suporte técnico.
- As informações deverão ser consultadas diretamente no ambiente de origem.
- O desenvolvedor poderá registrar na ocorrência as conclusões da análise, sem necessidade de anexar os logs originais.
- O acesso técnico deverá respeitar as regras de auditoria e identificação previstas nos requisitos de suporte técnico.

### 8.159. Controle de acesso às ocorrências da Central

**RF233 — Acesso e níveis de permissão para consulta e acompanhamento de ocorrências**

A Central de Ajustes e Manutenções deverá permitir o acesso de todos os funcionários da empresa responsável pelo PDV às funcionalidades de consulta e acompanhamento de ocorrências, respeitando os níveis de permissão definidos para cada usuário.

Regras:

- Todos os funcionários da empresa responsável pelo PDV poderão possuir acesso à Central de ocorrências.
- O acesso deverá ser controlado por permissões específicas.
- Os níveis de acesso poderão diferenciar consulta, acompanhamento, comentários, análise técnica, alteração de prioridade, gerenciamento e resolução de ocorrências.
- Cada funcionário deverá visualizar e executar somente as ações permitidas para seu nível de acesso.
- A concessão e alteração de permissões deverão respeitar o mecanismo centralizado de gestão de permissões.
- Operações realizadas nas ocorrências deverão identificar o usuário responsável, data e hora.
- Informações técnicas e acessos aos ambientes dos clientes deverão continuar protegidos pelas permissões específicas de suporte técnico.
- A validação das permissões deverá ocorrer no backend.

### 8.160. Visibilidade e proteção das informações das ocorrências

**RF234 — Consulta geral com restrição de informações por permissão**

A Central de Ajustes e Manutenções deverá permitir que funcionários autorizados consultem as ocorrências registradas, mantendo informações técnicas, dados dos clientes e detalhes de investigação protegidos conforme as permissões individuais.

Regras:

- Funcionários com acesso à Central poderão consultar as ocorrências.
- A visualização das informações deverá respeitar as permissões específicas de cada funcionário.
- Informações gerais sobre as ocorrências poderão ser disponibilizadas conforme o nível de acesso.
- Logs, detalhes técnicos, informações de infraestrutura e procedimentos de investigação deverão possuir permissões específicas.
- Dados dos clientes deverão ser protegidos contra acessos não autorizados.
- Ações de análise técnica e intervenção nos ambientes dos clientes deverão permanecer restritas aos usuários com permissões correspondentes.
- A restrição deverá ser aplicada tanto na interface quanto no backend.
- O sistema deverá registrar os acessos e as operações realizadas conforme as regras de auditoria da plataforma.

### 8.161. Notificações de novas ocorrências

**RF235 — Notificações por gravidade e área de responsabilidade**

A Central de Ajustes e Manutenções deverá notificar os funcionários sobre novas ocorrências, utilizando notificações internas, e-mail e critérios de distribuição conforme a gravidade e a área de responsabilidade.

Regras:

- O sistema deverá disponibilizar notificações dentro da própria plataforma.
- Deverá permitir o envio de notificações por e-mail.
- A distribuição deverá considerar a gravidade da ocorrência.
- A distribuição deverá considerar a área de responsabilidade e as permissões dos funcionários.
- Ocorrências críticas deverão receber tratamento prioritário nas notificações.
- Funcionários sem relação com a ocorrência não deverão receber alertas desnecessários.
- O sistema deverá permitir consultar as notificações recebidas.
- As notificações deverão identificar a ocorrência relacionada.
- O envio e o resultado das notificações deverão ser registrados.
- O mecanismo deverá permitir ajustes futuros nas regras de distribuição.

### 8.162. Personalização das notificações

**RF236 — Preferências individuais de notificações**

A plataforma deverá permitir que cada funcionário personalize suas preferências de notificações, respeitando as comunicações obrigatórias definidas pelo sistema.

Regras:

- Cada funcionário poderá configurar quais tipos de notificações deseja receber.
- A personalização poderá considerar notificações internas e por e-mail.
- O sistema deverá identificar quais notificações são opcionais e quais são obrigatórias.
- Notificações críticas e obrigatórias não poderão ser desativadas pelo funcionário.
- As preferências deverão ser individuais, sem afetar outros usuários.
- Alterações nas preferências deverão ser registradas com usuário, data e hora.
- A plataforma deverá respeitar as preferências configuradas sem comprometer o envio das notificações obrigatórias.
- A validação deverá ocorrer no backend.

### 8.163. Histórico e rastreabilidade das notificações

**RF237 — Histórico completo de notificações e acompanhamento de entrega**

A plataforma deverá manter um histórico completo das notificações enviadas aos funcionários, permitindo consultar seu conteúdo e acompanhar o resultado do envio e da entrega.

Regras:

- O sistema deverá manter o histórico das notificações recebidas por cada funcionário.
- Cada notificação deverá apresentar seu conteúdo, origem, ocorrência relacionada, data e hora.
- Deverá registrar os seguintes status, conforme o canal utilizado:
  - Enviada.
  - Entregue.
  - Visualizada.
  - Falha no envio ou na entrega.
- O sistema deverá permitir consultar notificações antigas.
- As notificações deverão manter seu histórico mesmo após serem visualizadas.
- Falhas de envio deverão ser identificadas e registradas.
- O histórico deverá permitir filtrar notificações por período, tipo, status e ocorrência relacionada.
- O acesso ao histórico deverá respeitar as permissões de cada funcionário.
- Os registros deverão preservar a identificação do destinatário, data e hora das operações.
- O processamento e a atualização dos status deverão ocorrer no backend.

### 8.164. Retenção do histórico de notificações

**RF238 — Retenção de notificações por 90 dias**

A plataforma deverá manter o histórico de notificações dos funcionários pelo período de 90 dias, realizando a exclusão automática dos registros após o término desse prazo.

Regras:

- O histórico de notificações deverá ser mantido por 90 dias, contados a partir da data de registro de cada notificação.
- Após esse período, os registros deverão ser excluídos automaticamente.
- A exclusão deverá ocorrer sem necessidade de intervenção dos desenvolvedores.
- A rotina deverá considerar individualmente a data de cada notificação.
- A exclusão não deverá afetar o histórico das ocorrências relacionadas às notificações.
- O processamento deverá ocorrer no backend.

### 8.165. Tratamento de falhas no envio de notificações

**RF239 — Novas tentativas automáticas e registro de falhas**

Quando uma notificação não puder ser enviada, a plataforma deverá realizar novas tentativas automáticas, respeitando um limite definido.

Regras:

- O sistema deverá identificar falhas no envio das notificações.
- Deverá realizar novas tentativas automáticas de envio.
- A quantidade máxima de tentativas deverá ser definida na configuração da plataforma.
- Cada tentativa deverá ser registrada com data, hora e resultado.
- Caso todas as tentativas falhem, a ocorrência deverá ser registrada como falha definitiva no histórico de notificações.
- O registro deverá identificar o destinatário, canal utilizado, motivo da falha, quantidade de tentativas e resultado final.
- O sistema não deverá realizar tentativas ilimitadas.
- O processamento deverá ocorrer no backend.

### 8.166. Intervalo entre tentativas de envio

**RF240 — Intervalo fixo configurável**

O sistema deverá utilizar um intervalo fixo entre as novas tentativas automáticas de envio de notificações.

Regras:

- O intervalo entre as tentativas deverá ser configurável pela administração da plataforma.
- O mesmo intervalo será aplicado às novas tentativas, respeitando o limite máximo definido no RF239.
- Cada tentativa deverá respeitar o intervalo configurado.
- Todas as tentativas e seus respectivos resultados deverão ser registrados.
- Caso o limite de tentativas seja atingido sem sucesso, deverá ser registrada uma falha definitiva, conforme RF239.
- O processamento deverá ocorrer no backend.

### 8.167. Reenvio manual de notificações

**RF241 — Reenvio manual de notificações com falha**

O sistema deverá permitir que usuários com permissão específica solicitem manualmente uma nova tentativa de envio de notificações que atingiram o limite de tentativas automáticas e permaneceram com falha.

Regras:

- O reenvio deverá depender de permissão específica.
- O sistema deverá permitir consultar a notificação original e o motivo da falha.
- Cada solicitação de reenvio deverá gerar um novo registro de tentativa.
- O histórico original deverá ser preservado.
- Deverão ser registrados o usuário responsável, a data, a hora e o resultado da nova tentativa.
- O reenvio manual não deverá apagar ou substituir os registros anteriores.
- O processamento deverá ocorrer no backend.

### 8.168. Limite de reenvios manuais

**RF242 — Apenas um reenvio manual por notificação**

O sistema deverá permitir apenas um reenvio manual para cada notificação que tenha atingido o limite de tentativas automáticas.

Regras:

- Cada notificação poderá receber somente uma solicitação de reenvio manual.
- Após a solicitação, uma nova tentativa manual não poderá ser realizada para a mesma notificação.
- O controle deverá ser feito pelo backend, impedindo solicitações duplicadas ou simultâneas.
- O resultado do reenvio deverá permanecer registrado no histórico.
- Caso o reenvio também falhe, a falha deverá permanecer registrada, sem permitir outro reenvio manual.
- A regra deverá ser aplicada independentemente do usuário que solicitou o reenvio.

### 8.169. Prevenção de notificações duplicadas

**RF243 — Controle de notificações duplicadas**

O sistema deverá impedir o envio de notificações duplicadas quando já existir uma notificação equivalente relacionada à mesma ocorrência.

Regras:

- A identificação de duplicidade deverá considerar a ocorrência relacionada e o tipo de notificação.
- Enquanto existir uma notificação equivalente em processamento ou já registrada para aquele evento, o sistema não deverá gerar outro envio duplicado.
- Eventos diferentes da mesma ocorrência poderão gerar novas notificações, desde que não sejam equivalentes.
- O controle deverá ocorrer no backend, inclusive para evitar duplicidades causadas por processamento simultâneo.
- A prevenção de duplicidade não deverá apagar nem alterar o histórico das notificações já registradas.

### 8.170. Notificações de atualização de ocorrências

**RF244 — Notificações que exigem ação do funcionário**

O sistema deverá enviar notificações aos funcionários interessados quando uma mudança de etapa ou status de uma ocorrência exigir alguma ação de sua responsabilidade.

Regras:

- Mudanças que não exigirem ação do funcionário não deverão gerar notificações.
- Quando houver uma ação necessária, a notificação deverá identificar a ocorrência e informar claramente o que precisa ser realizado.
- A distribuição deverá respeitar as permissões e responsabilidades dos funcionários.
- As notificações deverão seguir as regras de canais, preferências e obrigatoriedade definidas nos RF235 e RF236.
- O envio e o resultado deverão ser registrados no histórico de notificações.
- O processamento deverá ocorrer no backend.

### 8.171. Notificações de novas ocorrências

**RF245 — Distribuição de notificações conforme área, gravidade e responsabilidade**

Quando uma nova ocorrência for criada na Central de Ajustes e Manutenções, o sistema deverá identificar automaticamente os funcionários que deverão receber a notificação.

Regras:

- A distribuição deverá considerar a área e os serviços afetados.
- Deverá considerar a gravidade da ocorrência.
- Deverá considerar as responsabilidades e permissões dos funcionários.
- Ocorrências de maior gravidade deverão receber prioridade na distribuição.
- Funcionários sem relação com a ocorrência não deverão receber notificações desnecessárias.
- A distribuição deverá respeitar as preferências individuais, sem desativar notificações obrigatórias.
- O envio e os resultados deverão ser registrados no histórico.
- As regras de distribuição deverão ser processadas pelo backend e permitir ajustes administrativos autorizados.

### 8.172. Notificações de ocorrências atribuídas

**RF246 — Notificação obrigatória ao atribuir ocorrência**

Sempre que uma ocorrência for atribuída a um funcionário específico, o sistema deverá enviar uma notificação imediata e obrigatória ao responsável.

Regras:

- Toda nova atribuição deverá gerar uma notificação.
- A notificação será obrigatória, independentemente das preferências individuais do funcionário.
- O envio deverá ocorrer mesmo que o funcionário já esteja acompanhando a ocorrência.
- A notificação deverá identificar a ocorrência e informar que ela foi atribuída ao funcionário.
- Deverão ser registrados o funcionário responsável, quem realizou a atribuição, a ocorrência relacionada, a data, a hora e o resultado do envio.
- A notificação deverá respeitar os canais obrigatórios definidos pela plataforma.
- O processamento deverá ocorrer no backend.

### 8.173. Alteração do responsável pela ocorrência

**RF247 — Substituição de responsável**

Quando o responsável por uma ocorrência for substituído, o sistema deverá atualizar a atribuição e notificar os funcionários envolvidos.

Regras:

- O novo responsável deverá receber uma notificação imediata e obrigatória.
- O responsável anterior também deverá receber uma notificação informando a substituição.
- A responsabilidade pelo acompanhamento deverá ser retirada automaticamente do responsável anterior.
- O novo responsável assumirá o acompanhamento da ocorrência.
- O histórico deverá preservar todas as atribuições anteriores.
- Deverão ser registrados o responsável anterior, o novo responsável, quem realizou a alteração, a ocorrência relacionada, a data, a hora e o resultado das notificações.
- A operação deverá respeitar as permissões específicas e ser validada pelo backend.

### 8.174. Cadastro e gerenciamento de categorias

**RF248 — Categorias de produtos personalizáveis**

O sistema deverá disponibilizar categorias iniciais de produtos e permitir que cada empresa personalize e gerencie suas próprias categorias.

Regras:

- O sistema deverá disponibilizar categorias iniciais para facilitar o primeiro cadastro.
- Cada empresa poderá criar novas categorias.
- Cada empresa poderá editar suas próprias categorias.
- Cada empresa poderá excluir categorias, respeitando as regras de integridade dos produtos vinculados.
- As categorias deverão ser independentes entre empresas.
- Uma alteração realizada por uma empresa não poderá afetar as categorias de outras empresas.
- O sistema deverá identificar a empresa responsável por cada categoria.
- As operações deverão respeitar as permissões dos usuários e ser validadas pelo backend.

### 8.175. Exclusão de categorias com produtos vinculados

**RF249 — Transferência de produtos antes da exclusão da categoria**

O sistema deverá exigir a transferência dos produtos diretamente vinculados a uma categoria antes de permitir sua exclusão, exceto nos casos específicos tratados nos RF254, RF265 e RF266.

**Regras:**

- Antes da exclusão, o sistema deverá verificar se existem produtos diretamente vinculados à categoria.
- Quando existirem produtos diretamente vinculados e a categoria não possuir subcategorias, deverá solicitar a seleção de uma categoria de destino.
- Todos os produtos diretamente vinculados deverão ser transferidos para a categoria selecionada.
- A categoria de destino deverá pertencer à mesma empresa.
- A transferência deverá preservar os dados, históricos e demais vínculos dos produtos.
- A exclusão somente será concluída após a confirmação da transferência de todos os produtos diretamente vinculados.
- Categorias principais que possuam subcategorias deverão seguir os procedimentos específicos dos RF254 e RF266.
- A exclusão de subcategorias deverá seguir o RF265, que permite deixar os produtos sem categoria.
- Caso ocorra uma falha durante a transferência, a exclusão deverá ser cancelada, sem deixar produtos parcialmente transferidos.
- A operação deverá ser registrada no histórico, identificando usuário, categorias envolvidas, quantidade de produtos, data e hora.
- O processamento deverá ser validado pelo backend.

### 8.176. Estrutura hierárquica de categorias por segmento

**RF250 — Categorias e subcategorias conforme segmento comercial**

O sistema deverá disponibilizar estruturas hierárquicas de categorias e subcategorias de acordo com o segmento comercial definido no cadastro inicial de cada empresa.

Regras:

- No cadastro inicial da empresa, deverá ser selecionado seu segmento comercial.
- O sistema deverá disponibilizar as categorias e subcategorias correspondentes ao segmento selecionado.
- A estrutura deverá permitir categorias principais e subcategorias.
- A empresa poderá personalizar as categorias disponibilizadas.
- A empresa poderá criar novas categorias e subcategorias.
- As alterações deverão afetar somente a própria empresa.
- Os segmentos e suas estruturas iniciais serão disponibilizados pelo sistema.
- A estrutura deverá permitir a inclusão de novos segmentos comerciais futuramente.
- O backend deverá validar a hierarquia e o vínculo das categorias com a empresa.

### 8.177. Alteração e personalização do segmento comercial

**RF251 — Alteração de segmento comercial e gerenciamento pela empresa responsável pelo PDV**

A alteração do segmento comercial de uma empresa cliente deverá exigir autorização de um administrador. A empresa responsável pelo PDV terá controle administrativo para personalizar os segmentos comerciais e suas estruturas de categorias e subcategorias.

Regras:

- A alteração do segmento comercial deverá exigir autorização de um administrador da empresa cliente.
- Os produtos já cadastrados deverão ser preservados.
- As categorias e classificações existentes deverão ser preservadas durante a alteração, sem transferências ou exclusões automáticas.
- As novas categorias correspondentes ao segmento selecionado deverão ser disponibilizadas à empresa.
- A empresa cliente poderá organizar seus produtos nas novas categorias conforme sua necessidade.
- A empresa responsável pelo PDV poderá criar, editar, personalizar e gerenciar os segmentos comerciais disponibilizados pelo sistema.
- A empresa responsável pelo PDV poderá gerenciar as estruturas iniciais de categorias e subcategorias de cada segmento.
- Alterações nas estruturas disponibilizadas pelo sistema não deverão modificar automaticamente as categorias personalizadas das empresas clientes.
- Todas as alterações deverão possuir histórico de auditoria, identificando responsável, data, hora e informações alteradas.
- As operações deverão ser validadas pelo backend e respeitar as permissões específicas.

### 8.178. Movimentação de produtos entre categorias

**RF252 — Alteração individual e em lote de categorias de produtos**

O sistema deverá permitir alterar a categoria ou subcategoria de produtos individualmente ou em lote.

Regras:

- A alteração individual poderá ser realizada diretamente pelo cadastro do produto, respeitando as permissões do usuário.
- O sistema deverá permitir selecionar vários produtos para movimentação em lote.
- Movimentações em lote deverão exigir autorização de usuário que possua a permissão específica correspondente.
- A autorização deverá utilizar o mecanismo centralizado de permissões do sistema.
- O usuário deverá selecionar a categoria ou subcategoria de destino.
- Todos os produtos selecionados deverão pertencer à mesma empresa.
- Caso ocorra uma falha durante a movimentação em lote, nenhuma alteração deverá ser aplicada parcialmente.
- O sistema deverá apresentar a quantidade de produtos que serão movimentados antes da confirmação.
- Todas as alterações deverão ser registradas no histórico, identificando produtos, categorias de origem e destino, solicitante, autorizador quando aplicável, data e hora.
- O backend deverá validar as permissões e a integridade das movimentações.

### 8.179. Pesquisa e filtros de produtos por categoria

**RF253 — Pesquisa avançada e filtros combinados de produtos**

O sistema deverá disponibilizar filtros combinados para pesquisa e localização de produtos.

Regras:

- Permitir filtrar por segmento comercial.
- Permitir filtrar por categoria principal.
- Permitir filtrar por subcategoria.
- Permitir pesquisar pelo nome ou descrição do produto.
- Permitir pesquisar pelo código de barras.
- Permitir filtrar pela situação do produto, como ativo ou inativo.
- Permitir combinar vários filtros simultaneamente.
- Permitir limpar os filtros aplicados e iniciar uma nova pesquisa.
- Os resultados deverão respeitar o isolamento de dados de cada empresa.
- A pesquisa deverá considerar a hierarquia de categorias e subcategorias.
- O processamento deverá ocorrer no backend, respeitando as permissões de acesso.

### 8.180. Exclusão de categorias principais com subcategorias

**RF254 — Análise de vínculos e confirmação da exclusão**

Antes de permitir a exclusão de uma categoria principal, o sistema deverá identificar seus produtos diretamente vinculados, subcategorias e produtos vinculados a essas subcategorias, apresentando os impactos da operação ao usuário.

**Regras:**

1. O sistema deverá verificar a existência de produtos diretamente vinculados à categoria principal.
2. Deverá identificar todas as subcategorias vinculadas à categoria principal.
3. Deverá identificar a quantidade de produtos vinculados diretamente à categoria principal e indiretamente por meio das subcategorias.
4. Antes da exclusão, deverá apresentar um resumo dos cadastros que serão afetados.
5. Produtos diretamente vinculados à categoria principal deverão seguir as regras de transferência definidas no RF249.
6. As opções de transferência ou exclusão das subcategorias deverão seguir os procedimentos definidos no RF266.
7. Quando produtos ficarem sem categoria, deverão permanecer cadastrados e bloqueados para venda até receberem uma nova classificação, conforme RF255.
8. A exclusão deverá exigir confirmação explícita do usuário após a apresentação dos impactos.
9. Caso alguma transferência ou operação necessária apresente falha, a exclusão deverá ser cancelada integralmente, sem alterações parciais.
10. O sistema deverá preservar os produtos, históricos de vendas e movimentações, sem exclusão automática de seus cadastros.
11. A operação deverá respeitar as permissões do usuário e manter registro de auditoria, incluindo categorias envolvidas, produtos afetados, responsável, data e horário.
12. As validações deverão ocorrer no backend, garantindo a integridade dos vínculos.
### 8.181. Gerenciamento de produtos sem categoria

**RF255 — Identificação e regularização de produtos sem categoria**

O sistema deverá identificar e disponibilizar uma lista específica de produtos que estejam sem categoria ou subcategoria válida.

Regras:

- Produtos sem categoria deverão permanecer cadastrados, sem exclusão automática.
- A venda desses produtos deverá ser bloqueada até que recebam uma nova classificação.
- O sistema deverá disponibilizar uma lista específica de produtos sem categoria.
- A lista deverá permitir identificar nome, código, código de barras e situação do produto.
- Deverá permitir selecionar um ou vários produtos para classificá-los novamente.
- A movimentação individual deverá respeitar as permissões do usuário.
- A movimentação em lote deverá seguir as regras de autorização definidas no RF252.
- Após a classificação válida, o produto poderá voltar a ser vendido, respeitando as demais regras comerciais e operacionais.
- Todas as alterações deverão ser registradas no histórico.
- O backend deverá validar a classificação antes de liberar a venda.

### 8.182. Importação de categorias e subcategorias

**RF256 — Importação por Excel e CSV**

O sistema deverá permitir importar categorias e subcategorias utilizando arquivos Excel ou CSV.

Regras:

- Permitir importar arquivos nos formatos Excel (.xlsx) e CSV (.csv).
- A importação deverá permitir cadastrar categorias principais e suas respectivas subcategorias.
- Os dados importados deverão respeitar a estrutura hierárquica de categorias.
- As categorias importadas deverão pertencer exclusivamente à empresa responsável pela importação.
- O sistema deverá validar os dados e os vínculos hierárquicos antes de gravá-los.
- Categorias já existentes deverão ser identificadas para evitar duplicidades.
- A importação deverá respeitar as permissões do usuário.
- O backend deverá realizar o processamento e validar a integridade dos dados.

### 8.183. Tratamento de erros na importação

**RF257 — Validação e tratamento de erros em arquivos importados**

O sistema deverá identificar inconsistências nos arquivos Excel e CSV durante a importação de categorias e subcategorias, permitindo que o usuário escolha como proceder.

Regras:

- O sistema deverá identificar categorias duplicadas, dados inválidos, campos obrigatórios ausentes e problemas na hierarquia.
- Deverá apresentar os erros encontrados de forma clara, indicando os registros afetados.
- O usuário poderá cancelar a importação e corrigir o arquivo original.
- O usuário também poderá optar por importar somente os registros válidos.
- Registros inválidos não deverão ser importados.
- O sistema deverá informar a quantidade de registros válidos, inválidos e duplicados.
- A importação deverá preservar a integridade das categorias e subcategorias.
- A decisão do usuário e o resultado da importação deverão ser registrados no histórico.
- O processamento e as validações deverão ocorrer no backend.

### 8.184. Compatibilidade de arquivos para importação

**RF258 — Importação de planilhas personalizadas**

O sistema deverá aceitar arquivos Excel e CSV personalizados para importação de categorias e subcategorias, sem exigir modelos oficiais.

Regras:

- O usuário poderá utilizar planilhas próprias.
- Os arquivos deverão conter os campos necessários para identificar categorias e subcategorias.
- O sistema deverá identificar e validar os campos disponíveis.
- A ausência de informações obrigatórias deverá ser apresentada como erro, conforme RF257.
- A estrutura hierárquica deverá ser preservada durante a importação.
- O sistema não deverá exigir que o arquivo siga um modelo oficial específico.
- As validações deverão ocorrer no backend.

### 8.185. Exportação de categorias e subcategorias

**RF259 — Exportação completa com controle de permissões**

O sistema deverá permitir a exportação de todas as categorias e subcategorias cadastradas pela empresa, mediante permissão específica.

Regras:

- Permitir exportar todas as categorias e subcategorias em Excel (.xlsx) ou CSV (.csv).
- A exportação deverá preservar a estrutura hierárquica das categorias.
- Somente usuários com permissão específica poderão realizar a exportação.
- O controle de permissão deverá ser validado no backend.
- Os dados exportados deverão pertencer exclusivamente à empresa do usuário.
- A exportação deverá registrar usuário responsável, empresa, data, hora e formato do arquivo.
- A operação não deverá alterar os cadastros existentes.

### 8.186. Histórico de alterações nas categorias

**RF260 — Auditoria de categorias e subcategorias**

O sistema deverá manter um histórico das operações realizadas nas categorias e subcategorias de cada empresa.

Regras:

- Registrar criação de categorias e subcategorias.
- Registrar alterações de nomes e demais informações.
- Registrar transferências entre categorias.
- Registrar exclusões.
- Identificar o usuário responsável por cada operação.
- Registrar data e hora de cada alteração.
- Preservar as informações anteriores e posteriores à alteração, quando aplicável.
- Permitir consultar o histórico conforme as permissões do usuário.
- O histórico deverá ser independente para cada empresa.
- O sistema não deverá oferecer restauração automática de versões anteriores nesta funcionalidade.
- Os registros deverão ser preservados para auditoria e não poderão ser alterados pelos usuários.
- O backend deverá garantir a integridade dos registros.

### 8.187. Inativação de categorias

**RF261 — Bloqueio de inativação com produtos vinculados**

O sistema deverá impedir a inativação de categorias que possuam produtos vinculados.

Regras:

- Antes de inativar uma categoria, o sistema deverá verificar se existem produtos vinculados.
- Caso existam produtos, a inativação deverá ser bloqueada.
- O sistema deverá informar a quantidade de produtos vinculados.
- O usuário deverá transferir os produtos para outra categoria válida antes de realizar a inativação.
- A transferência deverá respeitar as regras de movimentação individual ou em lote definidas no RF252.
- Após a transferência de todos os produtos, a categoria poderá ser inativada.
- A operação deverá respeitar as permissões do usuário.
- Todas as tentativas e alterações deverão ser registradas no histórico de auditoria.
- O backend deverá garantir a integridade dos vínculos entre produtos e categorias.

### 8.188. Inativação de categorias principais e subcategorias

**RF262 — Inativação hierárquica de categorias**

O sistema deverá permitir a inativação de uma categoria principal sem produtos diretamente vinculados, mantendo sua estrutura de subcategorias cadastrada.

Regras:

- A categoria principal somente poderá ser inativada se não possuir produtos diretamente vinculados.
- Ao inativar a categoria principal, todas as suas subcategorias deverão ser inativadas automaticamente.
- As subcategorias deverão permanecer cadastradas, sem exclusão de seus registros.
- Os produtos vinculados às subcategorias deverão ser preservados, mas não poderão ser vendidos enquanto suas classificações estiverem inativas.
- A reativação da categoria principal não deverá reativar automaticamente suas subcategorias.
- As subcategorias deverão poder ser reativadas individualmente, respeitando a situação da categoria principal.
- A operação deverá respeitar as permissões e ser registrada no histórico de auditoria.
- O backend deverá validar a hierarquia e impedir inconsistências.

### 8.189. Reativação de categorias e subcategorias

**RF263 — Reativação seletiva de subcategorias**

O sistema deverá permitir que o usuário escolha quais subcategorias deseja reativar quando uma categoria principal for reativada.

Regras:

- A categoria principal deverá ser reativada antes de suas subcategorias.
- Após a reativação, o sistema deverá apresentar as subcategorias disponíveis para recuperação.
- O usuário poderá selecionar individualmente quais subcategorias deseja reativar.
- As subcategorias não selecionadas permanecerão inativas.
- A reativação deverá respeitar as permissões do usuário.
- Produtos vinculados às subcategorias somente poderão voltar a ser vendidos quando suas classificações estiverem ativas e os demais requisitos de venda forem atendidos.
- A operação deverá ser registrada no histórico, identificando categorias reativadas, usuário responsável, data e hora.
- O backend deverá validar a hierarquia e impedir a reativação de subcategorias cuja categoria principal esteja inativa.

### 8.190. Ordenação de categorias e subcategorias

**RF264 — Ordenação alfabética automática**

O sistema deverá organizar automaticamente as categorias e subcategorias em ordem alfabética nas telas de cadastro, consulta e seleção de produtos.

Regras:

- Categorias principais deverão ser exibidas em ordem alfabética.
- Subcategorias deverão ser exibidas em ordem alfabética dentro de suas respectivas categorias.
- A ordenação deverá ser aplicada automaticamente após criações, edições, importações e alterações de situação.
- Não haverá recurso de ordenação manual pelo usuário.
- A ordenação não deverá modificar os vínculos ou a hierarquia das categorias.

### 8.191. Exclusão de subcategorias com produtos vinculados

**RF265 — Exclusão sem transferência obrigatória de produtos**

O sistema deverá permitir a exclusão de uma subcategoria mesmo quando houver produtos vinculados a ela.

Regras:

- Antes da exclusão, o sistema deverá informar a quantidade de produtos vinculados.
- O usuário deverá receber um aviso de que esses produtos ficarão sem categoria.
- A exclusão deverá exigir confirmação explícita.
- Os produtos permanecerão cadastrados, sem perda de seus dados ou histórico.
- Os produtos ficarão bloqueados para venda até serem classificados novamente, conforme RF255.
- A classificação posterior poderá ser individual ou em lote, respeitando as permissões e regras existentes.
- A exclusão deverá respeitar as permissões do usuário e gerar registro de auditoria.
- O backend deverá garantir que nenhum produto seja excluído junto com a subcategoria.

### 8.192. Exclusão de categorias principais com subcategorias vinculadas

**RF266 — Exclusão com transferência ou remoção de subcategorias**

O sistema deverá permitir excluir uma categoria principal que possua subcategorias, oferecendo ao usuário duas possibilidades:

1. **Transferir as subcategorias:** selecionar outra categoria principal existente para receber as subcategorias, preservando seus vínculos com os produtos.
2. **Excluir as subcategorias junto com a categoria principal:** remover as subcategorias e manter os produtos cadastrados, porém sem categoria.

Regras:

- Antes da operação, o sistema deverá informar a quantidade de subcategorias e produtos afetados.
- O usuário deverá receber um aviso explícito de que produtos que ficarem sem categoria serão bloqueados para venda até serem classificados novamente.
- A operação deverá exigir confirmação explícita.
- Os produtos nunca deverão ser excluídos junto com as categorias ou subcategorias.
- A transferência deverá preservar os vínculos dos produtos e respeitar a hierarquia.
- Em caso de falha, nenhuma exclusão ou transferência parcial deverá ser realizada.
- A operação deverá respeitar as permissões do usuário e gerar histórico de auditoria.
- O bloqueio de venda deverá ser aplicado pelo backend, conforme RF255.

### 8.193. Exclusão individual de categorias e subcategorias

**RF267 — Bloqueio de exclusão em lote**

O sistema deverá permitir exclusivamente a exclusão individual de categorias e subcategorias.

Regras:

- Não deverá existir funcionalidade para selecionar e excluir várias categorias ou subcategorias simultaneamente.
- Cada exclusão deverá ser processada individualmente.
- As regras de transferência, preservação dos produtos, avisos e confirmações dos RF249, RF254, RF265 e RF266 deverão ser respeitadas.
- Cada operação deverá gerar seu próprio registro de auditoria.
- O backend deverá impedir operações de exclusão em lote, inclusive por solicitações externas à interface.

### 8.194. Unicidade de nomes de categorias e subcategorias

**RF268 — Bloqueio de nomes duplicados**

O sistema deverá impedir a criação ou alteração de categorias e subcategorias com nomes duplicados dentro da mesma empresa.

Regras:

- A validação deverá considerar todas as categorias e subcategorias da empresa.
- Não será permitido repetir um nome, mesmo que esteja vinculado a categorias principais diferentes.
- A validação deverá ocorrer tanto na criação quanto na edição e importação.
- Registros de outras empresas não deverão interferir nessa validação.
- Categorias e subcategorias inativas também deverão ser consideradas na verificação de duplicidade.
- O sistema deverá informar claramente quando o nome já estiver sendo utilizado.
- A validação deverá ocorrer no backend, evitando duplicidades por operações simultâneas.

### 8.195. Alteração de nomes de categorias e subcategorias

**RF269 — Alteração com controle de permissões e atualização dos produtos**

O sistema deverá permitir a alteração do nome de categorias e subcategorias, respeitando as permissões dos usuários.

Regras:

- A alteração deverá exigir permissão específica correspondente à operação, conforme configuração de permissões da empresa.
- Usuários que já possuírem a permissão necessária poderão realizar a alteração diretamente, sem solicitar autorização adicional.
- Usuários sem a permissão deverão utilizar o mecanismo centralizado de autorização.
- Quando houver produtos vinculados, o sistema deverá atualizar automaticamente a identificação da categoria ou subcategoria nos respectivos produtos.
- Todos os vínculos, dados e históricos dos produtos deverão ser preservados.
- A validação de nomes duplicados deverá seguir o RF268.
- A alteração deverá ser registrada no histórico, incluindo nome anterior, novo nome, usuário responsável, autorizador quando aplicável, data e hora.
- O backend deverá validar as permissões e garantir a consistência dos vínculos.

### 8.196. Consulta do histórico de categorias e subcategorias

**RF270 — Acesso ao histórico por permissão específica**

O sistema deverá permitir que qualquer usuário consulte o histórico de alterações de categorias e subcategorias, desde que possua a permissão específica.

Regras:

- A consulta não ficará restrita a administradores ou gerentes.
- A permissão deverá ser configurável individualmente ou por grupo de usuários, conforme o módulo de permissões.
- O histórico deverá contemplar criações, edições, exclusões, transferências, inativações e reativações.
- Deverá apresentar os dados anteriores e posteriores, quando aplicável, além do usuário responsável, data e hora.
- A consulta deverá respeitar o isolamento dos dados de cada empresa.
- A consulta não permitirá modificar ou excluir registros históricos.
- O backend deverá validar a permissão antes de disponibilizar as informações.

### 8.197. Pesquisa no histórico de categorias e subcategorias

**RF271 — Filtros de pesquisa do histórico**

O sistema deverá disponibilizar filtros para facilitar a localização de registros históricos.

Filtros disponíveis:

- Categoria ou subcategoria.
- Tipo de operação: criação, edição, exclusão, transferência, inativação ou reativação.
- Usuário responsável pela operação.
- Período, com data inicial e final.

Regras:

- Será permitido combinar vários filtros na mesma pesquisa.
- O usuário poderá limpar os filtros e realizar uma nova consulta.
- Os resultados deverão respeitar as permissões de acesso e o isolamento dos dados de cada empresa.
- A pesquisa não poderá alterar os registros históricos.
- A consulta deverá ser processada pelo backend.

### 8.198. Exportação do histórico de categorias e subcategorias

**RF272 — Exportação em Excel e CSV**

O sistema deverá permitir a exportação dos registros históricos de categorias e subcategorias nos formatos Excel (.xlsx) e CSV, respeitando as permissões de consulta e geração do relatório.

**Regras:**

- A exportação deverá estar disponível aos usuários que possuam permissão para consultar e gerar o respectivo relatório, conforme RF270 e RF288.
- Não deverá ser exigida uma permissão adicional exclusivamente para exportação.
- Deverá respeitar os filtros aplicados na pesquisa, conforme RF271.
- O arquivo deverá preservar as informações relevantes do histórico, incluindo categoria ou subcategoria, tipo de operação, dados anteriores e posteriores quando aplicável, usuário responsável, data e horário.
- A exportação deverá respeitar o isolamento dos dados de cada empresa.
- O sistema deverá registrar usuário responsável, empresa, data, horário e formato exportado.
- A exportação não poderá alterar os registros históricos.
- A validação das permissões deverá ocorrer no backend.

### 8.199. Destaque automático de categorias mais utilizadas

**RF273 — Identificação automática de categorias mais utilizadas**

O sistema deverá identificar automaticamente as categorias mais utilizadas e apresentá-las em destaque nas telas de venda e consulta de produtos.

Regras:

- A identificação deverá considerar a frequência de utilização das categorias.
- O destaque será atualizado automaticamente conforme a utilização do sistema.
- A organização alfabética geral das categorias deverá ser preservada.
- O destaque não poderá alterar cadastros, hierarquia ou vínculos das categorias.
- Somente categorias ativas poderão aparecer no destaque.
- A funcionalidade deverá respeitar o isolamento de dados de cada empresa.
- Os critérios de cálculo e atualização serão definidos na etapa de detalhamento técnico.

### 8.200. Critério de utilização das categorias

**RF274 — Frequência de categorias em vendas finalizadas**

O sistema deverá calcular a utilização das categorias com base na quantidade de vendas finalizadas que contenham pelo menos um produto vinculado a cada categoria.

Regras:

- Somente vendas finalizadas serão consideradas.
- Vendas canceladas ou não concluídas não deverão entrar no cálculo.
- Cada venda deverá contar uma única vez para cada categoria, independentemente da quantidade de produtos daquela categoria no carrinho.
- O cálculo deverá considerar os dados da própria empresa.
- O resultado será utilizado para destacar automaticamente as categorias mais utilizadas, conforme RF273.
- Os critérios de atualização e o período de análise serão definidos no detalhamento técnico.

### 8.201. Período de análise das categorias mais utilizadas

**RF275 — Análise de todo o histórico de vendas**

O sistema deverá considerar todo o histórico de vendas finalizadas da empresa para identificar as categorias mais utilizadas.

Regras:

- Não haverá limitação de período para o cálculo.
- Todas as vendas finalizadas registradas desde o início da operação da empresa serão consideradas.
- Vendas canceladas ou não concluídas serão desconsideradas.
- Cada venda contará uma única vez para cada categoria presente nela, conforme RF274.
- O cálculo será independente para cada empresa.
- Os resultados serão utilizados no destaque automático das categorias, conforme RF273.
- Alterações técnicas na forma de cálculo não poderão modificar o histórico original das vendas.

### 8.202. Atualização do destaque de categorias mais utilizadas

**RF276 — Atualização automática programada**

O sistema deverá atualizar automaticamente o destaque das categorias mais utilizadas em intervalos programados.

Regras:

- A atualização não deverá ocorrer individualmente a cada venda finalizada.
- O sistema deverá executar o cálculo periodicamente, considerando todo o histórico de vendas finalizadas, conforme RF274 e RF275.
- O intervalo de atualização será definido na etapa de detalhamento técnico.
- A atualização deverá ocorrer sem necessidade de intervenção do usuário.
- Falhas no processamento deverão ser registradas para análise técnica.
- A rotina deverá respeitar o isolamento dos dados de cada empresa.
- O processamento não poderá comprometer o funcionamento das vendas no PDV.

### 8.203. Exibição das categorias mais utilizadas

**RF277 — Acesso discreto à lista completa de categorias mais utilizadas**

O sistema deverá disponibilizar todas as categorias em uma lista ordenada da mais utilizada para a menos utilizada, sem sobrecarregar visualmente as telas de venda e consulta de produtos.

Regras:

- A tela principal deverá apresentar um acesso discreto e clicável para consultar as categorias mais utilizadas.
- Todas as categorias ativas deverão estar disponíveis nessa consulta.
- Ao clicar no acesso, o sistema deverá apresentar a lista completa, ordenada pela quantidade de vendas finalizadas que contiveram cada categoria.
- A apresentação inicial não deverá ocupar espaço excessivo nem prejudicar as operações do PDV.
- A lista deverá respeitar a atualização programada definida no RF276.
- Categorias sem vendas registradas deverão aparecer após as categorias utilizadas.
- A funcionalidade deverá respeitar o isolamento de dados de cada empresa.
- O acesso deverá respeitar as permissões de visualização das categorias.

### 8.204. Apresentação da lista de categorias mais utilizadas

**RF278 — Painel lateral de consulta**

O sistema deverá apresentar a lista completa de categorias mais utilizadas em um painel lateral, aberto a partir do acesso discreto definido no RF277.

Regras:

- O painel deverá abrir sem substituir ou fechar a tela atual.
- A tela de venda ou consulta deverá permanecer visível ao fundo.
- A lista deverá apresentar todas as categorias ativas, ordenadas da mais utilizada para a menos utilizada.
- O painel deverá permitir fechamento e retorno imediato à tela anterior.
- A abertura e o fechamento do painel não poderão interromper ou perder uma venda em andamento.
- A apresentação deverá ser responsiva e adequada ao espaço disponível.
- A consulta deverá respeitar as permissões e o isolamento de dados de cada empresa.

### 8.205. Seleção de categorias no painel lateral

**RF279 — Destaque sem alteração da tela atual**

O sistema deverá permitir selecionar uma categoria no painel lateral apenas para destacá-la visualmente.

Regras:

- A seleção não deverá filtrar produtos automaticamente.
- A seleção não deverá adicionar produtos ao carrinho.
- A tela atual deverá permanecer inalterada.
- O destaque deverá indicar visualmente qual categoria foi selecionada.
- A seleção não deverá modificar cadastros, vínculos ou histórico.
- O usuário poderá selecionar outra categoria ou fechar o painel normalmente.

### 8.206. Desempate na ordenação das categorias mais utilizadas

**RF280 — Desempate por ordem alfabética**

O sistema deverá utilizar a ordem alfabética como critério de desempate entre categorias com a mesma quantidade de vendas finalizadas.

Regras:

- A quantidade de vendas será o critério principal de ordenação.
- Quando duas ou mais categorias apresentarem a mesma quantidade, será aplicada a ordem alfabética.
- A regra deverá ser aplicada tanto no destaque quanto na lista completa do painel lateral.
- A ordenação não deverá alterar os cadastros ou a hierarquia das categorias.
- O critério deverá ser aplicado automaticamente durante as atualizações programadas.

### 8.207. Exibição de categorias sem vendas registradas

**RF281 — Categorias sem vendas no final da lista**

O sistema deverá incluir na lista de categorias mais utilizadas todas as categorias ativas, inclusive aquelas que ainda não possuem vendas finalizadas.

Regras:

- Categorias com vendas deverão aparecer primeiro, ordenadas pela quantidade de vendas.
- Categorias sem vendas registradas deverão aparecer no final da lista.
- Entre as categorias sem vendas, deverá ser aplicada a ordem alfabética.
- Categorias inativas não deverão aparecer.
- A regra deverá ser aplicada tanto no painel lateral quanto em qualquer outra apresentação dessa lista.

### 8.208. Exibição de informações das categorias mais utilizadas

**RF282 — Consulta de quantidade e valores de vendas**

O sistema deverá permitir consultar informações adicionais de cada categoria no painel lateral, mediante interação do usuário.

Regras:

- A quantidade de vendas finalizadas deverá ser exibida somente quando o usuário passar o mouse ou clicar na categoria.
- O sistema poderá apresentar também o valor total das vendas vinculadas à categoria.
- A exibição dos valores financeiros deverá ser configurável pela empresa cliente.
- O cliente poderá optar por ocultar os valores financeiros, mantendo a consulta da quantidade de vendas.
- A configuração deverá ser independente para cada empresa.
- A quantidade de vendas continuará sendo o critério de ordenação principal, conforme RF274.
- A consulta deverá respeitar as permissões de acesso às informações financeiras.
- Os valores deverão considerar somente vendas finalizadas e respeitar os critérios de cálculo definidos para o relatório.

### 8.209. Permissão para configurar valores financeiros

**RF283 — Configuração exclusiva para administradores**

O sistema deverá permitir que somente usuários com perfil de administrador configurem a exibição dos valores financeiros das categorias mais utilizadas.

Regras:

- Somente administradores poderão ativar ou desativar essa configuração.
- Gerentes e demais usuários poderão consultar as informações financeiras somente quando a exibição estiver habilitada e possuírem as permissões necessárias para essa consulta.
- A configuração será independente para cada empresa.
- Alterações deverão ser registradas no histórico, identificando administrador responsável, configuração anterior, nova configuração, data e hora.
- O backend deverá validar a permissão antes de permitir qualquer alteração.

### 8.210. Configuração do acesso às categorias mais utilizadas

**RF284 — Configuração individual por filial**

O sistema deverá permitir que administradores configurem individualmente, por filial, a disponibilidade do acesso às categorias mais utilizadas.

Regras:

- O administrador poderá ativar ou desativar o recurso em cada filial.
- A configuração de uma filial não deverá alterar as demais.
- Quando desativado, o acesso discreto ao painel lateral não deverá aparecer naquela filial.
- Quando ativado, o recurso deverá seguir as regras dos RF273 a RF282.
- A configuração deverá respeitar as permissões administrativas.
- Cada alteração deverá registrar administrador responsável, filial, configuração anterior, nova configuração, data e hora.
- O backend deverá validar a configuração da filial antes de disponibilizar o recurso.

### 8.211. Controle de acesso às categorias mais utilizadas por usuário

**RF285 — Configuração individual por funcionário**

O sistema deverá permitir que administradores definam individualmente quais funcionários poderão visualizar o acesso às categorias mais utilizadas em cada filial.

Regras:

- O administrador poderá conceder ou retirar o acesso de cada funcionário individualmente.
- A configuração deverá considerar a filial em que o funcionário está autorizado a trabalhar.
- Um funcionário poderá ter acesso ao recurso em uma filial e não ter em outra.
- A alteração não deverá modificar as permissões de outros funcionários.
- O acesso deverá respeitar também a disponibilidade do recurso na filial, conforme RF284.
- As alterações deverão ser registradas no histórico, identificando funcionário, filial, administrador responsável, situação anterior, nova situação, data e hora.
- O backend deverá validar o acesso antes de disponibilizar o recurso.

### 8.212. Ocultação do acesso sem autorização

**RF286 — Ocultação do recurso para usuários sem permissão**

O sistema deverá ocultar completamente o acesso às categorias mais utilizadas quando o funcionário não possuir autorização para utilizá-lo.

Regras:

- O acesso não deverá aparecer nas telas de venda ou consulta.
- Não deverá ser exibida mensagem informando a ausência de permissão.
- A restrição deverá considerar as configurações da filial definidas no RF284 e as permissões individuais definidas no RF285.
- A tentativa de acessar o recurso por outros meios deverá ser bloqueada pelo backend.
- A restrição não deverá afetar as demais funcionalidades de venda e consulta de produtos.

### 8.213. Histórico de utilização das categorias por filial

**RF287 — Relatório de utilização das categorias**

O sistema deverá disponibilizar aos administradores um relatório de utilização das categorias, permitindo analisar o desempenho por filial.

Regras:

- O relatório deverá apresentar a quantidade de vendas finalizadas por categoria.
- Deverá apresentar também o valor total das vendas vinculadas a cada categoria.
- Deverá permitir filtros por período, data inicial, data final e filial.
- Deverá permitir consultar uma filial individualmente ou comparar os resultados entre filiais, respeitando as permissões administrativas.
- Cada venda deverá contar uma única vez por categoria, conforme RF274.
- Vendas canceladas ou não concluídas deverão ser desconsideradas.
- A consulta deverá respeitar o isolamento de dados da empresa.
- O relatório deverá respeitar as configurações de acesso às informações financeiras.
- O backend deverá validar as permissões de consulta.

### 8.214. Exportação de relatórios

**RF288 — Exportação de relatórios em Excel e CSV**

O sistema deverá permitir a exportação dos relatórios gerados nos formatos Excel (.xlsx) e CSV.

**Regra geral para todos os relatórios do PDV Web:**

- Todos os relatórios disponibilizados pelo sistema deverão permitir exportação.
- O usuário que possuir permissão para gerar determinado relatório também terá permissão para exportá-lo.
- Não será necessária uma permissão adicional específica para exportação.
- A exportação deverá respeitar os filtros e parâmetros utilizados na geração do relatório.
- Os arquivos deverão preservar as informações apresentadas no relatório, respeitando as características de cada formato.
- As permissões de acesso aos dados deverão ser respeitadas durante a geração e exportação.
- O isolamento de dados entre empresas e filiais deverá ser mantido.
- A exportação não poderá alterar os dados originais ou os registros do sistema.

Essa regra será transversal e deverá ser aplicada a todos os módulos e relatórios do PDV Web, inclusive os que forem desenvolvidos futuramente.

### 8.215. Formatos de exportação dos relatórios

**RF289 — Exportação em PDF conforme o tipo de relatório**

O sistema deverá permitir a exportação dos relatórios em PDF quando esse formato for adequado à finalidade e à apresentação das informações.

Regras:

- Excel (.xlsx) e CSV deverão permanecer como formatos gerais de exportação.
- PDF deverá ser disponibilizado nos relatórios em que sua utilização fizer sentido.
- A definição dos formatos disponíveis deverá considerar a finalidade e as características de cada relatório.
- O usuário não deverá precisar de uma permissão adicional para exportar em PDF, desde que possua permissão para gerar o respectivo relatório.
- A exportação deverá respeitar filtros, parâmetros, permissões e isolamento de dados.
- A disponibilidade dos formatos deverá ser definida durante o desenvolvimento de cada relatório.

### 8.216. Preservação histórica de nomes em relatórios

**RF290 — Identificação histórica de categorias, subcategorias e produtos excluídos**

O sistema deverá preservar e apresentar nos relatórios históricos os nomes que categorias, subcategorias e produtos possuíam no momento da operação registrada.

Regras:

- Vendas e relatórios históricos deverão manter os nomes originais dos cadastros utilizados na operação.
- Alterações posteriores de nomes não deverão modificar a identificação apresentada nos registros antigos.
- Quando uma categoria for excluída, deverá ser apresentada uma observação: **"Categoria excluída"**.
- Quando uma subcategoria for excluída, deverá ser apresentada uma observação: **"Subcategoria excluída"**.
- Quando um produto for excluído, deverá ser apresentada uma observação: **"Produto excluído"**.
- A identificação histórica deverá ser preservada mesmo após alterações, transferências, inativações ou exclusões dos cadastros originais.
- As observações deverão identificar corretamente o tipo de cadastro e sua situação.
- A exclusão de cadastros não deverá apagar ou modificar o histórico das vendas.
- A regra deverá ser aplicada aos relatórios, consultas e exportações correspondentes.

### 8.217. Exclusão de produtos com histórico de vendas

**RF291 — Exclusão definitiva com preservação histórica**

O sistema deverá permitir a exclusão definitiva de produtos que possuam vendas registradas, sem comprometer os dados históricos.

Regras:

- A exclusão poderá ser realizada conforme as permissões do usuário.
- O sistema deverá solicitar confirmação antes da exclusão.
- O produto deverá ser removido dos cadastros ativos e das consultas de produtos disponíveis para venda.
- O histórico de vendas deverá permanecer integralmente preservado.
- Os relatórios antigos deverão continuar apresentando o nome, código, descrição e demais informações históricas do produto no momento da venda.
- Deverá ser apresentada a observação **"Produto excluído"** quando aplicável, conforme RF290.
- A exclusão não poderá apagar movimentações de estoque, vendas, cancelamentos ou outros registros históricos vinculados ao produto.
- A operação deverá ser registrada na auditoria, identificando usuário responsável, produto, data e hora.
- O backend deverá garantir a integridade dos registros históricos.

### 8.218. Exclusão de produtos com estoque disponível

**RF292 — Bloqueio de exclusão enquanto houver estoque**

O sistema deverá impedir a exclusão de produtos que possuam estoque disponível em qualquer filial da empresa.

Regras:

- Antes de excluir, o sistema deverá verificar o estoque do produto em todas as filiais.
- Caso exista estoque disponível, a exclusão deverá ser bloqueada.
- O sistema deverá informar em quais filiais existe estoque e suas respectivas quantidades.
- O usuário deverá realizar uma operação específica de saída ou transferência para regularizar o estoque.
- Não deverá ocorrer baixa automática de estoque durante a exclusão.
- Após a regularização, o usuário poderá solicitar novamente a exclusão, respeitando as permissões aplicáveis.
- A operação de saída ou transferência deverá seguir as regras próprias de movimentação de estoque.
- A exclusão deverá preservar integralmente o histórico do produto, conforme RF291.
- O backend deverá validar o estoque antes de concluir a exclusão.

### 8.219. Exclusão de produtos vinculados a combos e promoções

**RF293 — Exclusão com inativação automática de vínculos**

O sistema deverá permitir a exclusão de produtos vinculados a combos e promoções, desde que o produto não possua estoque disponível em nenhuma filial da empresa, conforme RF292.

Regras:

- A existência de vínculos com combos ou promoções não deverá impedir a exclusão do produto.
- Após a confirmação da exclusão, todos os combos e promoções ativos que utilizem o produto deverão ser inativados automaticamente.
- Os combos e promoções afetados deverão apresentar uma observação informando: **"Inativado automaticamente devido à exclusão do produto [nome do produto] da composição."**
- A identificação histórica do produto excluído deverá ser preservada.
- Os demais produtos que compõem os combos não deverão ser excluídos ou alterados.
- A exclusão do produto não deverá apagar o histórico de vendas ou movimentações.
- A operação deverá registrar o produto excluído e todos os combos e promoções inativados, incluindo responsável, data e hora.
- O backend deverá garantir que nenhuma venda utilize combos ou promoções inativados.
- A exclusão deverá respeitar as permissões aplicáveis.

**RF294 — Bloqueio de exclusão conforme situação do estoque**

O sistema deverá impedir a exclusão definitiva de um produto enquanto existir qualquer quantidade física desse produto em alguma filial ou enquanto houver operações pendentes relacionadas ao seu estoque.

A validação deverá considerar:

1. **Estoque disponível:** impede a exclusão enquanto existir quantidade física.
2. **Compra solicitada:** impede a exclusão enquanto houver solicitação de compra pendente.
3. **Compra aprovada ou pedido realizado:** impede a exclusão enquanto a operação estiver pendente.
4. **Estoque reservado:** impede a exclusão enquanto houver quantidade comprometida.
5. **Estoque em trânsito:** impede a exclusão enquanto houver transferência pendente de recebimento.
6. **Estoque bloqueado:** impede a exclusão enquanto existir quantidade física ou operação pendente relacionada.
7. **Estoque vencido ou danificado:** também impede a exclusão enquanto o produto ainda existir fisicamente.

**Condição para permitir a exclusão:**

O produto poderá ser excluído quando:
- Não existir nenhuma quantidade física em qualquer filial, independentemente do status do estoque.
- Não existirem compras, reservas, transferências ou outras operações pendentes relacionadas ao produto.
- As operações anteriores tiverem sido concluídas, canceladas ou devidamente encerradas.

O histórico de compras, movimentações e demais operações deverá ser preservado, mesmo após a exclusão do cadastro do produto.

A validação deverá ocorrer no backend, considerando todas as filiais da empresa, impedindo exclusões indevidas.

**RF295 — Tratamento dos vínculos de produtos excluídos**

O sistema deverá tratar os vínculos de produtos excluídos conforme as seguintes regras:

1. **Histórico:** preservar integralmente os registros históricos relacionados ao produto, incluindo vendas, compras, inventários, transferências e movimentações.
2. **Operações pendentes:** impedir a exclusão enquanto existirem operações pendentes relacionadas ao produto.
3. **Vínculos permanentes:** preservar os vínculos e aplicar regras específicas conforme o módulo responsável.
4. **Identificação:** quando um produto excluído aparecer em consultas históricas, relatórios ou outros registros, apresentar sua identificação original acompanhada da observação "Produto excluído".
5. **Integridade:** a exclusão do cadastro não poderá comprometer registros históricos ou informações de outros módulos.

O comportamento específico de cada vínculo será definido durante o levantamento dos respectivos módulos.

**RF296 — Auditoria da exclusão de produtos**

Toda exclusão definitiva de produto deverá gerar um registro de auditoria contendo:

1. Usuário responsável pela exclusão.
2. Identificação completa do produto excluído.
3. Data e horário da operação.
4. Filial ou filiais relacionadas à verificação.
5. Motivo informado para a exclusão.
6. Situação do estoque no momento da exclusão.
7. Operações verificadas durante a validação, incluindo compras, reservas, transferências e demais movimentações pertinentes.
8. Vínculos afetados pela exclusão, incluindo combos, promoções e outros cadastros relacionados.
9. Resultado da operação, identificando se a exclusão foi concluída.

O registro deverá ser permanente, imutável e disponível para consulta por usuários com permissão específica de auditoria.

A exclusão deverá respeitar as validações de estoque e operações pendentes definidas nos RF293 e RF294.

**RF297 — Consulta de produtos excluídos**

O sistema deverá disponibilizar uma tela específica para consulta de produtos excluídos definitivamente.

A tela deverá permitir:

1. Pesquisar produtos pelo nome.
2. Pesquisar pelo código interno do produto.
3. Filtrar pelo período de exclusão.
4. Visualizar os dados históricos preservados do produto.
5. Consultar o usuário responsável pela exclusão, data e horário.
6. Consultar o motivo informado para a exclusão.
7. Consultar as informações de auditoria registradas conforme RF296.

A consulta deverá respeitar as permissões de acesso e o isolamento dos dados de cada empresa.

A tela terá finalidade exclusivamente consultiva, sem permitir restauração ou reativação de produtos excluídos definitivamente.

**RF298 — Exportação da consulta de produtos excluídos**

A consulta de produtos excluídos deverá permitir a exportação dos resultados conforme as regras gerais de exportação de relatórios definidas nos RF288 e RF289.

A exportação deverá respeitar:
- As permissões do usuário para geração do relatório.
- Os filtros aplicados na consulta.
- Os formatos disponíveis para o respectivo relatório.
- O isolamento dos dados de cada empresa.

**RF299 — Reutilização de código de barras de produto excluído**

Quando um usuário tentar cadastrar um produto utilizando um código de barras que já pertenceu a um produto excluído definitivamente, o sistema deverá:

1. Identificar a existência do cadastro anterior.
2. Exibir um aviso informando que aquele código de barras já foi utilizado por um produto excluído.
3. Apresentar o nome do produto anterior, quando disponível.
4. Permitir que o usuário prossiga com o cadastro mediante confirmação explícita.
5. Criar um novo cadastro, sem restaurar ou reutilizar o registro anterior.
6. Preservar integralmente o histórico do produto excluído.
7. Registrar a operação para fins de auditoria.

A validação deverá ocorrer no backend, respeitando a unicidade do código de barras entre os produtos ativos da mesma empresa.

**RF300 — Histórico de utilização de códigos de barras**

O sistema deverá manter um histórico de utilização dos códigos de barras, permitindo identificar todos os produtos que já utilizaram determinado código.

Regras:

1. Cada novo produto deverá receber um identificador interno exclusivo.
2. A reutilização de um código de barras não poderá misturar ou alterar o histórico dos produtos anteriores.
3. O sistema deverá permitir consultar os produtos que já utilizaram determinado código de barras.
4. A consulta deverá apresentar nome do produto, identificador interno, situação atual, data de cadastro e data de exclusão, quando aplicável.
5. O histórico deverá preservar os vínculos com vendas, compras, movimentações e demais registros originais.
6. A consulta deverá respeitar as permissões e o isolamento dos dados de cada empresa.
7. Todas as reutilizações deverão ser registradas para fins de auditoria.


**RF301 — Múltiplos códigos de barras por produto**

O sistema deverá permitir cadastrar vários códigos de barras vinculados ao mesmo produto.

Regras:

1. Um produto poderá possuir vários códigos de barras ativos.
2. Todos os códigos vinculados deverão identificar o mesmo produto durante a leitura no PDV.
3. A leitura de qualquer um desses códigos deverá localizar o cadastro original do produto.
4. Cada código de barras deverá ser único entre os produtos ativos da mesma empresa.
5. O sistema deverá impedir que o mesmo código seja associado a produtos diferentes simultaneamente.
6. A inclusão, alteração e remoção de códigos deverão respeitar as permissões do usuário.
7. O histórico de utilização dos códigos deverá ser preservado, conforme RF300.
8. A alteração dos códigos não poderá modificar os históricos de vendas, compras e movimentações anteriores.

**RF302 — Remoção de códigos de barras vinculados**

O sistema deverá permitir a remoção de códigos de barras associados a um produto, respeitando as permissões dos usuários.

Regras:

1. Usuários com permissão específica poderão remover códigos de barras diretamente.
2. Usuários sem essa permissão deverão solicitar autorização por meio do mecanismo centralizado de autorizações.
3. A remoção de um código não poderá excluir o cadastro do produto.
4. O código removido não poderá mais ser utilizado para identificar o produto nas vendas.
5. O histórico de utilização do código deverá ser preservado, conforme RF300.
6. A remoção deverá registrar o código, produto relacionado, usuário solicitante, autorizador quando aplicável, data, horário e resultado da operação.
7. O sistema deverá impedir a remoção do último código de barras caso exista uma regra futura que exija código obrigatório; essa obrigatoriedade deverá ser configurável por empresa e segmento.

**RF303 — Consulta dos códigos de barras de um produto**

O cadastro do produto deverá disponibilizar a consulta dos códigos de barras vinculados, separando códigos ativos e removidos.

Regras:

1. Exibir todos os códigos de barras ativos diretamente no cadastro do produto.
2. Disponibilizar uma área específica para consultar o histórico de códigos removidos.
3. Permitir visualizar a data de inclusão e a data de remoção de cada código.
4. Identificar o usuário responsável pela inclusão ou remoção.
5. Preservar o histórico de utilização dos códigos, conforme RF300.
6. Não permitir que códigos removidos sejam utilizados para identificar o produto em novas vendas.
7. Respeitar as permissões de consulta e o isolamento dos dados de cada empresa.

**RF304 — Tratamento de códigos de barras duplicados ou inconsistentes**

Quando o sistema identificar que um código de barras está vinculado a mais de um produto ativo, deverá:

1. Bloquear a inclusão do produto no carrinho.
2. Exibir uma mensagem informando que existe uma inconsistência no cadastro do código de barras.
3. Não permitir que o operador escolha manualmente entre os produtos encontrados.
4. Não realizar qualquer movimentação de estoque ou venda decorrente dessa leitura.
5. Permitir que usuários com as permissões necessárias investiguem e corrijam a inconsistência.
6. Registrar a ocorrência para fins de auditoria.
7. Realizar a validação no backend, impedindo que a inconsistência seja contornada pela interface.


**RF305 — Localização de produtos sem código de barras**

O PDV deverá permitir localizar produtos sem código de barras por meio de pesquisa manual.

A pesquisa deverá aceitar:

1. Nome do produto.
2. Descrição do produto.
3. Código interno do produto.

Regras:

- A pesquisa deverá localizar somente produtos disponíveis para venda, respeitando sua situação cadastral e classificação.
- Produtos excluídos definitivamente não poderão aparecer como opções de venda.
- Produtos inativos ou bloqueados para venda não poderão ser adicionados ao carrinho.
- A pesquisa deverá respeitar as permissões e o isolamento dos dados da empresa e filial.
- A localização manual deverá funcionar tanto para produtos com código de barras quanto para produtos sem código de barras.

**RF306 — Apresentação dos resultados da pesquisa de produtos**

Durante a venda, ao pesquisar um produto pelo nome ou descrição, o sistema deverá apresentar uma lista simples contendo:

1. Imagem do produto, quando cadastrada.
2. Nome do produto.
3. Preço de venda vigente na filial.

Regras:

- A imagem deverá facilitar a identificação visual do produto.
- Caso não exista imagem cadastrada, o sistema deverá apresentar um espaço reservado padrão.
- A lista deverá manter uma apresentação limpa e objetiva, sem informações adicionais desnecessárias.
- A seleção de um produto deverá permitir sua inclusão no carrinho, respeitando as regras de venda, estoque e permissões.
- Os preços deverão corresponder à filial em que a venda está sendo realizada.

**RF307 — Configuração de imagem padrão para produtos**

O sistema deverá permitir que cada empresa configure uma imagem padrão para representar produtos sem imagem cadastrada.

Regras:

1. Cada empresa poderá cadastrar sua própria imagem padrão.
2. A imagem padrão será exibida nos resultados de pesquisa quando o produto não possuir imagem própria.
3. A empresa poderá optar por não utilizar imagem padrão.
4. Caso não exista imagem cadastrada para o produto e a empresa não tenha configurado uma imagem padrão, o espaço destinado à imagem deverá permanecer sem conteúdo, sem exibir espaço reservado, ícone ou imagem genérica.
5. A configuração deverá ser independente por empresa.
6. Alterações na imagem padrão não deverão modificar as imagens individuais dos produtos.
7. A configuração deverá respeitar as permissões administrativas.

**RF308 — Formatos de imagem dos produtos**

O sistema deverá aceitar os seguintes formatos de imagem para produtos e imagens padrão das empresas:

1. JPG / JPEG.
2. PNG.
3. WEBP.

Regras:

- Validar o formato real do arquivo, não apenas sua extensão.
- Rejeitar arquivos inválidos ou corrompidos.
- As imagens deverão ser armazenadas e vinculadas ao produto ou à configuração da empresa.
- O sistema deverá preservar a qualidade visual adequada, considerando a otimização de armazenamento e carregamento.
- Os arquivos deverão passar por validações de segurança antes de serem disponibilizados.
- As regras serão aplicadas tanto às imagens individuais dos produtos quanto às imagens padrão das empresas.

**RF309 — Tamanho máximo das imagens dos produtos**

O sistema deverá aceitar imagens de produtos e imagens padrão das empresas com tamanho máximo de 5 MB por arquivo.

Regras:

1. O limite máximo será de 5 MB por imagem.
2. Arquivos que ultrapassarem esse limite deverão ser rejeitados.
3. O sistema deverá informar claramente quando o tamanho máximo for excedido.
4. A validação deverá ocorrer no backend.
5. A regra será aplicada aos formatos JPG, JPEG, PNG e WEBP.
6. O limite deverá ser respeitado tanto no cadastro individual quanto em importações de imagens, caso disponibilizadas futuramente.

**RF310 — Redimensionamento e otimização automática de imagens**

O sistema deverá realizar automaticamente o redimensionamento e a otimização das imagens enviadas para cadastro de produtos e imagens padrão das empresas.

Regras:

1. Redimensionar imagens com resolução superior aos limites definidos pelo sistema.
2. Preservar as proporções originais, sem distorcer a imagem.
3. Manter qualidade visual adequada para identificação dos produtos no PDV.
4. Otimizar o tamanho dos arquivos para reduzir consumo de armazenamento e tempo de carregamento.
5. Preservar o enquadramento da imagem, evitando cortes indevidos.
6. Não alterar a imagem original de forma que prejudique a identificação do produto.
7. Aplicar o processamento automaticamente, sem exigir intervenção do usuário.
8. Definir tecnicamente os limites de resolução e os parâmetros de otimização durante a implementação.


**RF311 — Substituição da imagem de um produto**

Quando um usuário enviar uma nova imagem para um produto que já possui imagem cadastrada, o sistema deverá solicitar confirmação antes de substituir a imagem atual.

Regras:

1. Exibir uma prévia da nova imagem antes da confirmação.
2. Informar que a imagem atual será substituída.
3. Somente realizar a substituição após confirmação explícita do usuário.
4. Caso o usuário cancele, manter a imagem atual sem alterações.
5. A substituição deverá respeitar as permissões do usuário.
6. Registrar a alteração na auditoria do cadastro, identificando usuário, produto, data e horário.
7. Aplicar as regras de formato, tamanho e otimização definidas nos RF308, RF309 e RF310.

**RF312 — Remoção da imagem de um produto**

O sistema deverá permitir a remoção da imagem cadastrada em um produto, mediante confirmação explícita do usuário.

Regras:

1. Antes da remoção, o sistema deverá solicitar confirmação.
2. Após a confirmação, a imagem individual do produto será removida.
3. Caso exista uma imagem padrão configurada pela empresa, ela deverá passar a ser exibida no lugar da imagem removida.
4. Caso a empresa não possua imagem padrão configurada, o espaço destinado à imagem deverá permanecer sem conteúdo.
5. A remoção deverá respeitar as permissões do usuário.
6. Registrar a operação na auditoria, identificando produto, usuário responsável, data e horário.
7. A remoção da imagem não poderá excluir ou alterar o cadastro do produto nem seu histórico de vendas e movimentações.

**RF313 — Visualização ampliada da imagem do produto**

O sistema deverá permitir a visualização ampliada das imagens dos produtos durante a pesquisa no PDV.

Regras:

1. Em dispositivos com mouse, permitir ampliar a imagem ao passar o cursor sobre ela ou clicar.
2. Em dispositivos com tela sensível ao toque, permitir ampliar a imagem por meio de toque.
3. A imagem ampliada deverá preservar suas proporções e qualidade visual.
4. A ampliação não deverá interromper ou cancelar a venda em andamento.
5. O usuário deverá conseguir fechar a visualização e retornar à pesquisa normalmente.
6. A funcionalidade deverá funcionar em diferentes tamanhos de tela, respeitando a responsividade do sistema.
7. Caso o produto não possua imagem individual nem imagem padrão da empresa, não deverá existir opção de ampliação.

**RF314 — Exibição de imagens dos produtos no carrinho**

O sistema deverá exibir uma miniatura da imagem de cada produto adicionado ao carrinho de vendas.

Regras:

1. Exibir uma miniatura ao lado da identificação do produto.
2. Utilizar a imagem individual cadastrada para o produto.
3. Caso não exista imagem individual, utilizar a imagem padrão configurada pela empresa, conforme RF307.
4. Caso não exista nenhuma imagem disponível, manter o espaço destinado à imagem sem conteúdo.
5. A miniatura deverá manter proporções adequadas, sem distorções.
6. A exibição deverá preservar a organização e a facilidade de leitura do carrinho.
7. A imagem não deverá interferir nas operações de quantidade, preço, desconto ou remoção de produtos do carrinho.

**RF315 — Atualização da imagem durante uma venda**

Quando a imagem de um produto for alterada ou removida enquanto existir uma venda em andamento, o sistema deverá manter a imagem que estava sendo exibida no carrinho.

Regras:

1. A imagem exibida no carrinho deverá permanecer inalterada durante a venda em andamento.
2. Alterações posteriores no cadastro do produto não deverão modificar automaticamente a imagem já exibida.
3. Caso o produto seja removido do carrinho e adicionado novamente, deverá ser utilizada a imagem atualmente cadastrada.
4. Caso a imagem individual tenha sido removida, deverá ser utilizada a imagem padrão da empresa, se configurada.
5. Caso não exista imagem individual ou padrão, o espaço destinado à imagem deverá permanecer sem conteúdo.
6. A regra deverá funcionar sem interromper ou alterar os dados da venda em andamento.

**RF316 — Exibição de imagens nas consultas de estoque**

O sistema deverá exibir miniaturas das imagens dos produtos nas listas e consultas de estoque.

Regras:

1. Exibir uma miniatura ao lado da identificação de cada produto.
2. Priorizar a imagem individual cadastrada.
3. Caso não exista imagem individual, utilizar a imagem padrão configurada pela empresa, conforme RF307.
4. Caso não exista imagem individual ou padrão, manter o espaço destinado à imagem sem conteúdo.
5. Preservar as proporções e a qualidade visual das imagens.
6. A exibição deverá manter a organização, legibilidade e desempenho das consultas, inclusive em listas com muitos produtos.
7. A apresentação das imagens não deverá interferir nos filtros, pesquisas, ordenações ou operações de estoque.

**RF317 — Imagens dos produtos nos relatórios**

Os relatórios do PDV Web não deverão exibir imagens dos produtos.

Regras:

1. Os relatórios deverão apresentar as informações textuais e numéricas dos produtos.
2. A identificação deverá utilizar nome, código interno e demais informações pertinentes ao relatório.
3. As imagens dos produtos não deverão ser incluídas nas visualizações nem nos arquivos exportados.
4. A regra será aplicada aos relatórios de estoque, inventário, movimentações e demais módulos do sistema.

**RF318 — Informações complementares e fiscais dos produtos**

O cadastro de produtos deverá permitir o registro de informações complementares e fiscais, preparando a estrutura do sistema para futuras integrações com emissão de documentos fiscais.

**1. Informações comerciais complementares**
- Fabricante.
- Marca.
- Modelo.
- Referência do produto.
- Descrição complementar livre.

**2. Informações fiscais**

O cadastro deverá possuir uma área específica para informações fiscais, contemplando campos necessários para futuras operações fiscais, como:

- NCM.
- CEST, quando aplicável.
- Origem da mercadoria.
- CFOP, conforme operação.
- Unidade comercial e tributável.
- GTIN/EAN, quando aplicável.
- Informações de ICMS, PIS e COFINS.
- Demais classificações e informações fiscais necessárias conforme o segmento e a legislação vigente.

**3. Regras gerais**
- As informações fiscais deverão ser armazenadas separadamente das informações comerciais.
- Os campos deverão permitir preenchimento conforme a aplicabilidade de cada produto.
- O sistema deverá permitir futuras ampliações dos campos fiscais.
- As informações deverão ser preservadas no histórico das operações.
- O cadastro fiscal deverá preparar o sistema para futuras integrações com emissão de notas fiscais.
- A inclusão desses campos não significa que a emissão fiscal estará disponível no MVP.
- Validações e regras tributárias específicas serão detalhadas na etapa de integração fiscal.

**RF319 — Compartilhamento de informações fiscais entre filiais**

O sistema deverá permitir o compartilhamento das informações fiscais dos produtos entre as filiais pertencentes à mesma empresa, mantendo flexibilidade para configurações tributárias específicas de cada filial.

**Regras:**

1. Os dados fiscais de identificação e classificação do produto serão cadastrados uma única vez e compartilhados entre as filiais da mesma empresa.
2. As configurações tributárias relacionadas às operações poderão ser individualizadas por filial.
3. O sistema deverá permitir diferenças de regras fiscais conforme a localização e as características tributárias de cada filial.
4. Alterações nos dados compartilhados do produto deverão refletir nas filiais vinculadas à empresa.
5. As configurações específicas de uma filial não poderão alterar as configurações das demais.
6. O sistema deverá manter histórico das alterações fiscais, identificando responsável, data, horário e informações modificadas.
7. As informações fiscais deverão respeitar o isolamento entre empresas.
8. A estrutura deverá permitir futuras integrações com emissores de documentos fiscais.

**RF320 — Validação das informações fiscais dos produtos**

O sistema deverá permitir o cadastro e a utilização comercial dos produtos mesmo quando suas informações fiscais estiverem incompletas, desde que não exista outra regra que impeça sua utilização.

**Regras:**

1. O sistema permitirá salvar produtos com informações fiscais incompletas.
2. Informações fiscais inválidas deverão gerar alertas durante o cadastro ou edição.
3. O sistema deverá identificar os campos fiscais ausentes ou inválidos.
4. Produtos com informações fiscais incompletas ou inválidas poderão ser utilizados nas operações comerciais comuns do PDV, respeitadas as demais regras do sistema.
5. Quando a integração com emissão de documentos fiscais for implementada, produtos com informações fiscais obrigatórias ausentes ou inválidas deverão ser bloqueados nas operações fiscais correspondentes.
6. O bloqueio deverá informar claramente quais informações precisam ser corrigidas.
7. As validações deverão considerar a legislação e as regras tributárias aplicáveis à empresa, filial e operação.
8. As validações fiscais deverão ser executadas no backend, não dependendo apenas da interface.

**RF321 — Importação de informações fiscais dos produtos**

O sistema deverá permitir a importação em lote das informações fiscais dos produtos por meio de arquivos Excel (.xlsx) e CSV.

**Regras:**

1. Permitir importar informações fiscais por arquivos Excel (.xlsx) e CSV.
2. A importação deverá permitir relacionar os dados fiscais aos produtos já cadastrados, utilizando identificadores como código interno ou outros identificadores disponíveis.
3. Deverá contemplar os campos fiscais previstos no RF318, incluindo NCM, CEST, origem da mercadoria, CFOP, unidades comerciais e tributáveis e informações tributárias aplicáveis.
4. O sistema deverá identificar os campos da planilha, permitindo arquivos personalizados, sem exigir um modelo único obrigatório.
5. Os dados deverão ser validados antes da importação.
6. O sistema deverá apresentar registros válidos, inválidos e duplicados, com a identificação dos erros encontrados.
7. O usuário poderá cancelar a operação, corrigir os dados ou importar somente os registros válidos.
8. Registros inválidos não poderão ser importados.
9. A importação deverá respeitar o compartilhamento fiscal entre filiais definido no RF319.
10. Toda importação deverá registrar usuário responsável, empresa, data, horário, arquivo utilizado, quantidade de registros e resultado da operação.
11. A importação deverá respeitar as permissões de acesso e executar as validações no backend.

**RF322 — Atualização das informações fiscais em lote**

O sistema deverá permitir atualizar as informações fiscais de produtos já cadastrados por meio de arquivos Excel (.xlsx) e CSV.

**Regras:**

1. Permitir atualização completa das informações fiscais de um produto.
2. Permitir atualização parcial, alterando somente os campos informados na planilha.
3. Na atualização parcial, os campos não informados deverão manter seus valores atuais.
4. A planilha deverá identificar os produtos por código interno ou outro identificador válido.
5. O sistema deverá permitir visualizar os dados atuais e os novos valores antes de confirmar a atualização.
6. Todos os dados deverão passar por validação antes da aplicação.
7. Registros inválidos deverão ser identificados e não poderão ser atualizados.
8. O usuário poderá cancelar a operação ou prosseguir somente com os registros válidos.
9. A atualização deverá respeitar o compartilhamento fiscal entre filiais definido no RF319.
10. As alterações deverão preservar o histórico dos dados anteriores e novos, identificando produto, usuário responsável, empresa, data, horário e resultado.
11. A operação deverá respeitar as permissões e executar as validações no backend.

**RF323 — Histórico de alterações fiscais dos produtos**

O sistema deverá manter um histórico completo das alterações realizadas nas informações fiscais dos produtos, disponibilizando consulta individual e centralizada.

**1. Histórico individual**
- Disponível dentro do cadastro de cada produto.
- Apresentar os dados fiscais anteriores e os novos valores.
- Identificar o usuário responsável pela alteração.
- Registrar data e horário.
- Identificar quais campos foram modificados.

**2. Consulta centralizada**
- Disponibilizar uma tela específica para consulta das alterações fiscais.
- Permitir pesquisar por produto, código interno, usuário responsável, filial e período.
- Permitir combinar filtros.
- Apresentar os valores anteriores e posteriores às alterações.
- Identificar alterações realizadas manualmente e por importação de planilhas.

**3. Regras gerais**
- O histórico deverá ser permanente e não poderá ser alterado ou excluído pelos usuários.
- As consultas deverão respeitar as permissões e o isolamento entre empresas.
- Alterações compartilhadas entre filiais deverão ser identificadas adequadamente.
- O histórico deverá registrar também importações e atualizações em lote.
- O backend deverá garantir a integridade dos registros.

**RF324 — Exportação das informações fiscais dos produtos**

O sistema deverá permitir a exportação das informações fiscais cadastradas nos produtos.

**Regras:**

1. Permitir exportação em Excel (.xlsx) e CSV.
2. Permitir exportação em PDF quando fizer sentido para o tipo de relatório ou consulta.
3. A exportação deverá respeitar os filtros aplicados na consulta.
4. Deverá permitir selecionar produtos específicos ou exportar todos os produtos disponíveis dentro do escopo de acesso.
5. As informações exportadas deverão contemplar os dados fiscais cadastrados, incluindo NCM, CEST, origem da mercadoria, CFOP, unidades e informações tributárias aplicáveis.
6. A exportação deverá respeitar as permissões de geração de relatórios definidas nos RF288 e RF289.
7. Os dados deverão respeitar o isolamento entre empresas e as permissões de acesso às filiais.
8. Toda exportação deverá registrar usuário responsável, empresa, data, horário e formato utilizado.
9. A exportação não poderá alterar os cadastros ou históricos dos produtos.

**RF325 — Consulta de pendências fiscais dos produtos**

O sistema deverá disponibilizar uma consulta específica para identificar produtos que possuam informações fiscais incompletas ou inválidas.

**Regras:**

1. Disponibilizar uma tela específica para consulta de pendências fiscais.
2. Apresentar os produtos com informações fiscais obrigatórias ausentes ou inválidas, conforme as regras aplicáveis.
3. Identificar o produto por nome, código interno e demais identificadores disponíveis.
4. Apresentar quais campos fiscais estão ausentes ou inválidos.
5. Permitir filtrar por produto, categoria, situação da pendência e filial, quando aplicável.
6. Permitir combinar filtros e limpar a pesquisa.
7. Permitir acessar o cadastro do produto para realizar as correções, respeitando as permissões.
8. A consulta deverá considerar as informações fiscais compartilhadas entre filiais e as configurações específicas de cada filial.
9. A consulta deverá respeitar o isolamento entre empresas e as permissões de acesso.
10. As pendências deverão ser identificadas conforme as regras fiscais aplicáveis, sem bloquear as operações comerciais comuns do PDV.

**RF326 — Listagem simplificada de pendências fiscais**

O sistema deverá apresentar as pendências fiscais dos produtos sem classificação por gravidade ou prioridade.

**Regras:**

1. As pendências deverão ser apresentadas em uma lista simples.
2. O sistema deverá identificar os produtos com informações fiscais ausentes ou inválidas.
3. Deverá apresentar os campos que precisam de correção.
4. Não haverá classificação por gravidade, prioridade ou tipo de pendência.
5. A consulta deverá utilizar as validações já previstas no RF325, evitando processamento adicional desnecessário.
6. A identificação das pendências deverá respeitar as permissões e o isolamento entre empresas.

**RF327 — Atualização das pendências fiscais por alteração**

O sistema deverá atualizar a situação das pendências fiscais dos produtos sempre que houver alteração em suas informações fiscais.

**Regras:**

1. A validação deverá ocorrer após alterações nas informações fiscais do produto.
2. A validação deverá considerar inclusões, edições e atualizações realizadas por importação.
3. Quando uma pendência for corrigida, deverá deixar de aparecer na consulta.
4. Quando uma alteração gerar uma nova pendência, ela deverá ser identificada na consulta.
5. Não deverão ocorrer verificações periódicas desnecessárias quando não houver alterações nos dados fiscais.
6. A validação deverá respeitar as regras fiscais aplicáveis e as configurações da empresa e de suas filiais.
7. O processamento deverá ocorrer no backend, evitando cálculos repetitivos na interface.

**RF328 — Indicador simplificado de situação fiscal dos produtos**

O sistema deverá apresentar um indicador simples para identificar se o produto possui pendências fiscais.

**Regras:**

1. Apresentar apenas duas situações: **Sem pendências fiscais** ou **Com pendências fiscais**.
2. Não apresentar contagem de pendências no indicador.
3. A situação deverá utilizar as validações já previstas nos RF320, RF325 e RF327.
4. Não realizar consultas ou cálculos adicionais apenas para exibir o indicador.
5. A situação deverá ser atualizada quando houver alterações nas informações fiscais do produto.
6. A identificação deverá respeitar as configurações fiscais da empresa e de suas filiais, quando aplicável.
7. A validação e atualização deverão ocorrer no backend, preservando a consistência dos dados.

**RF329 — Correção de pendências fiscais por importação**

O sistema deverá permitir corrigir informações fiscais de vários produtos por meio de importação de arquivos Excel (.xlsx) e CSV.

**Regras:**

1. A correção em lote deverá utilizar os mecanismos de importação e atualização definidos nos RF321 e RF322.
2. O usuário deverá identificar os produtos por código interno ou outro identificador válido.
3. Será permitido atualizar todos os campos fiscais ou somente os campos informados na planilha.
4. Os dados deverão passar pelas validações fiscais antes da atualização.
5. Registros inválidos não poderão ser atualizados.
6. O sistema deverá apresentar o resultado da importação, identificando registros processados, atualizados e rejeitados.
7. Após a atualização, a situação fiscal dos produtos deverá ser recalculada conforme o RF327.
8. A operação deverá respeitar as permissões, o compartilhamento fiscal entre filiais e o isolamento entre empresas.
9. Todas as alterações deverão preservar o histórico definido no RF323.
10. Não deverá ser criada uma ferramenta independente de edição fiscal em lote, evitando duplicidade de funcionalidades e processamento desnecessário.

**RF330 — Controle de acesso às informações fiscais**

O acesso às informações fiscais dos produtos deverá ser controlado por permissões específicas atribuídas aos usuários.

**Regras:**

1. O sistema deverá permitir configurar permissões específicas para visualizar informações fiscais.
2. Deverá permitir configurar permissões específicas para cadastrar e alterar informações fiscais.
3. A importação e atualização fiscal em lote deverão respeitar as permissões correspondentes.
4. A consulta de pendências fiscais deverá respeitar as permissões de acesso.
5. A exportação das informações fiscais deverá respeitar as permissões de geração de relatórios definidas nos RF288 e RF289.
6. As permissões deverão utilizar o mecanismo centralizado de autorizações já definido no projeto.
7. Usuários sem permissão não poderão acessar ou alterar informações fiscais, inclusive por requisições diretas ao backend.
8. As permissões poderão ser configuradas por grupo de usuários e individualmente, respeitando as regras gerais de permissões do PDV Web.
9. Todas as alterações e operações deverão manter os registros de auditoria aplicáveis.

**RF331 — Obrigatoriedade automática dos campos fiscais**

O sistema deverá identificar automaticamente quais informações fiscais são obrigatórias para cada produto, considerando a legislação vigente e as características da operação fiscal.

**Regras:**

1. A obrigatoriedade dos campos deverá ser determinada conforme as regras fiscais aplicáveis.
2. O sistema deverá considerar a classificação fiscal do produto, sua origem, a operação e as características tributárias da empresa e da filial.
3. Campos não aplicáveis à situação fiscal do produto não deverão ser exigidos.
4. As validações deverão ser utilizadas no cadastro, edição, importação e atualização das informações fiscais.
5. Produtos com informações obrigatórias ausentes ou inválidas deverão ser identificados conforme os RF320 e RF325.
6. As regras fiscais deverão ser centralizadas, evitando duplicidade de validações em diferentes módulos.
7. O sistema deverá permitir atualização futura das regras fiscais conforme mudanças na legislação.
8. As validações deverão ocorrer no backend.
9. A aplicação das regras deverá priorizar eficiência, evitando consultas e cálculos repetitivos desnecessários.

**RF332 — Atualização das regras fiscais**

**Classificação: Futuro — fora do MVP.**

Em versões futuras, o sistema deverá permitir atualizar as regras fiscais por integração com fontes oficiais de informações fiscais e também por atualização manual realizada por desenvolvedores autorizados.

**Regras:**

1. Permitir atualização automática por integração com fontes oficiais de informações fiscais.
2. Permitir atualização manual das regras por desenvolvedores autorizados.
3. Organizar as atualizações por versões, preservando as regras anteriores.
4. Validar as novas regras antes de sua publicação.
5. Impedir a publicação de regras inválidas ou incompletas.
6. Caso uma fonte externa esteja indisponível, manter a última versão válida das regras.
7. Registrar versão, origem da atualização, responsável, data, horário e resultado.
8. Garantir que as atualizações não modifiquem automaticamente os dados fiscais cadastrados nos produtos sem validação.
9. Permitir gerenciamento e publicação somente a usuários com permissões específicas.
10. Implementar os mecanismos de integração, atualização e gerenciamento conforme o escopo e o planejamento de versões futuras.

**RF333 — Histórico e restauração de versões das regras fiscais**

**Classificação: Futuro — fora do MVP.**

Em versões futuras, o sistema deverá manter o histórico das versões das regras fiscais, permitindo consulta e restauração de versões anteriores por desenvolvedores autorizados.

**Regras:**

1. Todas as versões publicadas deverão ser preservadas.
2. Cada versão deverá possuir identificação própria, data, origem da atualização e responsável.
3. Deverá ser possível consultar as regras e alterações de cada versão.
4. Somente desenvolvedores com permissão específica poderão restaurar uma versão anterior.
5. A restauração deverá passar por validação antes de ser aplicada.
6. A versão restaurada deverá gerar um novo registro de publicação, sem apagar ou sobrescrever o histórico existente.
7. O sistema deverá identificar a versão atualmente utilizada.
8. A restauração deverá registrar responsável, data, horário, versão de origem, versão restaurada e justificativa.
9. As regras deverão ser aplicadas de maneira consistente às empresas, filiais e operações correspondentes.
10. O histórico deverá ser permanente e protegido contra alterações ou exclusões indevidas.

A implementação deverá ocorrer somente após a definição e aprovação do mecanismo de gerenciamento de regras fiscais para versões futuras.
**RF334 — Agendamento da aplicação das regras fiscais**

**Classificação: Futuro — fora do MVP.**

Em versões futuras, o sistema deverá permitir que desenvolvedores autorizados agendem a data e o horário de aplicação de novas versões das regras fiscais.

**Regras:**

1. Permitir definir data e horário para entrada em vigor de uma nova versão.
2. A versão deverá passar por validação antes de ser agendada.
3. A versão agendada deverá permanecer inativa até a data e o horário definidos.
4. Até a entrada em vigor, o sistema deverá continuar utilizando a última versão válida e vigente.
5. A aplicação deverá ocorrer automaticamente na data e no horário programados.
6. O sistema deverá impedir o agendamento de versões inválidas ou incompatíveis.
7. Deverá permitir consultar versões agendadas, respectivas datas e horários de aplicação.
8. O cancelamento ou a alteração de um agendamento deverá exigir permissão específica e justificativa.
9. Todas as operações deverão ser registradas em histórico, identificando responsável, data, horário e resultado.
10. O processamento deverá ser otimizado, evitando verificações repetitivas e processamento desnecessário.

A implementação dependerá da definição e aprovação do mecanismo de gerenciamento e versionamento das regras fiscais para versões futuras.

**RF335 — Tratamento de falhas na aplicação das regras fiscais**

**Classificação: Futuro — fora do MVP.**

Em versões futuras, o sistema deverá manter a última versão válida e vigente das regras fiscais caso ocorra falha na aplicação de uma nova versão.

**Regras:**

1. Em caso de falha, a versão anterior deverá permanecer ativa.
2. A nova versão não deverá ser aplicada parcialmente.
3. O sistema deverá registrar a falha, identificando versão, data, horário e motivo.
4. Não deverão ocorrer novas tentativas automáticas.
5. Desenvolvedores com permissão específica poderão realizar uma nova tentativa manual.
6. Cada nova tentativa deverá passar pelas validações necessárias.
7. Todas as tentativas deverão ser registradas, incluindo responsável, data, horário e resultado.
8. A falha na atualização não deverá interromper as operações comerciais comuns do PDV.
9. O sistema deverá preservar a integridade das regras fiscais e evitar processamento desnecessário.

A implementação dependerá da existência do mecanismo de gerenciamento e versionamento das regras fiscais previsto para versões futuras.
**RF336 — Identificação das versões do sistema e das regras fiscais**

**Classificação: Futuro — fora do MVP.**

Em versões futuras, o PDV Web deverá apresentar, na área de identificação do usuário conectado, indicadores discretos das versões atualmente utilizadas.

**Regras:**

1. A área de identificação do usuário poderá apresentar um campo pequeno e discreto para identificação das versões.
2. O campo poderá apresentar:
   - Versão atual do PDV Web.
   - Versão atual das regras fiscais.
3. As versões deverão ser atualizadas quando uma nova versão for efetivamente aplicada.
4. A identificação será informativa, sem permitir alterações pelo usuário comum.
5. A apresentação deverá ser simples e não ocupar espaço excessivo na interface.
6. A consulta das versões deverá utilizar informações já disponíveis, evitando processamento e consultas repetitivas desnecessárias.

A implementação dependerá da definição dos mecanismos de atualização e gerenciamento de versões do sistema e das regras fiscais.

**RF337 — Detalhes das versões do sistema e fiscais**

**Classificação: Futuro — fora do MVP.**

Em versões futuras, o PDV Web deverá permitir consultar detalhes das versões do sistema e das regras fiscais por meio de uma janela acessível pelo indicador de versões apresentado na identificação do usuário.

**Regras:**

1. O indicador deverá permitir interação por clique.
2. Ao clicar, o sistema deverá abrir uma pequena janela com os detalhes das versões.
3. A janela deverá apresentar:
   - Número da versão atual do PDV Web.
   - Data da última atualização do sistema.
   - Número da versão atual das regras fiscais.
   - Data da última atualização das regras fiscais.
4. A janela deverá possuir uma opção para fechar e retornar à tela anterior.
5. A consulta deverá ser somente informativa, sem permitir alterações pelo usuário.
6. A interface deverá ser simples e discreta, sem comprometer a operação do PDV.
7. As informações deverão ser obtidas de forma otimizada, evitando consultas repetitivas desnecessárias.

A implementação dependerá da disponibilização dos mecanismos de identificação e gerenciamento de versões previstos no RF336.
**RF338 — Visualização das versões conforme o perfil do funcionário**

**Classificação: Futuro — fora do MVP.**

Em versões futuras, o sistema deverá apresentar as informações de versão conforme o perfil de acesso do funcionário conectado.

**Regras:**

1. Todos os funcionários poderão visualizar o indicador de versão na identificação do usuário.
2. Operadores poderão visualizar somente o número da versão atual do PDV Web.
3. Os demais funcionários poderão visualizar:
   - Número da versão atual do PDV Web.
   - Data da última atualização do sistema.
   - Número da versão atual das regras fiscais.
   - Data da última atualização das regras fiscais.
4. A visualização deverá respeitar o perfil e as permissões do funcionário.
5. As informações serão somente para consulta, sem permitir alterações.
6. A apresentação deverá permanecer discreta e otimizada.

A implementação dependerá da disponibilização dos mecanismos de identificação e consulta de versões previstos nos RF336 e RF337.

**RF339 — Notificações de atualizações prioritárias do sistema**

**Classificação: Futuro — fora do MVP.**

O PDV Web deverá comunicar aos funcionários quando houver atualizações prioritárias do sistema que exijam atenção ou aplicação em prazo determinado.

**Regras:**

1. Atualizações comuns deverão ocorrer de forma silenciosa, sem exibir notificações aos funcionários.
2. Atualizações prioritárias deverão apresentar uma notificação destacada na interface do PDV Web.
3. A notificação deverá informar que existe uma atualização prioritária disponível.
4. Deverá disponibilizar a opção **“Atualizar agora”**.
5. O funcionário poderá optar por concluir as tarefas em andamento antes de iniciar a atualização.
6. A notificação não deverá interromper automaticamente vendas, fechamentos de caixa ou outras operações em andamento.
7. A aplicação da atualização deverá respeitar as regras de segurança e integridade das operações.
8. A comunicação deverá ser objetiva, evitando notificações repetitivas ou desnecessárias.

A implementação dependerá da existência dos mecanismos de distribuição, identificação e gerenciamento de atualizações do sistema.

**RF340 — Bloqueio de atualização durante operações em andamento**

**Classificação: Futuro — fora do MVP.**

O sistema deverá impedir a aplicação de atualizações prioritárias enquanto existirem operações em andamento que possam comprometer a integridade dos dados.

**Regras:**

1. Ao solicitar uma atualização prioritária, o sistema deverá verificar se existem operações incompatíveis com a atualização.
2. Caso existam operações incompatíveis, a atualização deverá ser bloqueada temporariamente.
3. O sistema deverá informar quais operações precisam ser concluídas antes da atualização, sem expor dados desnecessários.
4. A mensagem deverá orientar o funcionário a finalizar ou cancelar as operações pendentes, respeitando as permissões e regras de cada módulo.
5. Após a conclusão das operações incompatíveis, o funcionário poderá solicitar novamente a atualização.
6. Nenhuma venda, pagamento, movimentação de estoque ou outra operação deverá ser perdida, duplicada ou interrompida indevidamente pela atualização.
7. A atualização somente poderá começar após a confirmação de que o ambiente está em condição segura.
8. As verificações deverão ser otimizadas, evitando consultas repetitivas e processamento desnecessário.
9. O backend deverá garantir que a atualização não seja aplicada enquanto houver operações incompatíveis em andamento.
10. A validação deverá considerar também operações cuja gravação ou sincronização ainda não tenha sido concluída.

A implementação dependerá dos mecanismos de atualização e controle de integridade operacional do sistema.

**RF341 — Atualização prioritária individual com controle de compatibilidade e sincronização**

**Classificação: Futuro — fora do MVP.**

O sistema deverá permitir a atualização individual dos caixas, respeitando a compatibilidade entre versões e a integridade da sincronização de dados.

**Regras:**

1. Cada caixa poderá ser atualizado individualmente assim que estiver em condição segura para receber a atualização.
2. O servidor central deverá controlar as versões utilizadas pelos caixas.
3. Caixas em versões diferentes poderão operar simultaneamente somente quando houver compatibilidade entre elas.
4. A comunicação e a sincronização deverão preservar a integridade dos dados entre versões compatíveis.
5. Vendas, pagamentos e movimentações não poderão ser duplicados, perdidos ou processados incorretamente durante a atualização.
6. O caixa deverá concluir ou resolver operações pendentes antes de iniciar sua atualização.
7. Caso não exista compatibilidade entre as versões, a atualização individual deverá ser bloqueada para os caixas afetados.
8. Quando necessário, deverá ser realizada uma atualização coordenada dos caixas envolvidos.
9. Falhas na atualização de um caixa não deverão interromper os demais, desde que a compatibilidade esteja garantida.
10. O sistema deverá registrar versões anteriores e atuais, caixas atualizados, resultados e eventuais falhas.
11. O mecanismo deverá considerar também a operação offline e a sincronização posterior, preservando os dados gerados durante a falta de conexão.
12. Um caixa com dados locais ainda não sincronizados não poderá receber uma atualização que coloque esses dados em risco. A atualização deverá aguardar a sincronização ou utilizar um mecanismo comprovadamente seguro de preservação e migração desses dados.
13. O controle deverá ser centralizado e executado no backend, evitando verificações e processamento desnecessários.
14. A compatibilidade deverá ser validada antes da liberação da atualização, considerando as versões do sistema, os formatos de dados e os mecanismos de sincronização envolvidos.

A implementação dependerá dos mecanismos de distribuição de versões, compatibilidade e atualização centralizada do sistema.
**RF342 — Recuperação automática após falha na atualização de um caixa**

**Classificação: Futuro — fora do MVP.**

O sistema deverá possuir um mecanismo de recuperação automática caso ocorra falha durante a atualização de um caixa.

**Regras:**

1. Antes de iniciar a atualização, o sistema deverá preservar os componentes necessários para permitir a recuperação da versão anterior.
2. Caso ocorra falha durante a atualização, deverá tentar restaurar automaticamente a versão anterior.
3. A restauração deverá preservar os dados e as operações já registradas e confirmadas, sem provocar duplicidade, perda ou reversão indevida de informações.
4. A restauração deverá preservar também os dados locais ainda não sincronizados, quando existentes.
5. Após a restauração, o sistema deverá validar a integridade da instalação e a compatibilidade com o servidor central e os demais caixas.
6. Se a versão anterior for compatível e estiver íntegra, o caixa poderá voltar a operar normalmente.
7. Se a restauração falhar ou a compatibilidade não puder ser garantida, o caixa deverá permanecer bloqueado para operações que possam comprometer os dados.
8. A falha e a tentativa de recuperação deverão ser registradas, incluindo caixa, versões envolvidas, data, horário e resultado.
9. Não deverão ocorrer tentativas automáticas repetitivas de atualização após uma falha.
10. Uma nova tentativa de atualização deverá depender de ação autorizada.
11. O mecanismo deverá priorizar a integridade dos dados, a sincronização e o baixo consumo de recursos.
12. A recuperação deverá ser executada de forma controlada, sem interferir nos demais caixas que estejam operando normalmente.

A implementação dependerá dos mecanismos de atualização, preservação de versões e recuperação do sistema.
**RF343 — Histórico técnico e acompanhamento das atualizações dos caixas**

**Classificação: Futuro — fora do MVP.**

O sistema deverá disponibilizar uma consulta técnica exclusiva para desenvolvedores e usuários internos expressamente autorizados pela empresa responsável pelo PDV Web.

**Regras:**

1. Apresentar a versão atual de cada caixa.
2. Manter histórico centralizado e permanente dos eventos relacionados às atualizações realizadas.
3. Registrar os eventos de atualização, incluindo disponibilização, início, conclusão, falha, cancelamento e restauração, conforme aplicável.
4. Registrar os dados resumidos dos eventos, incluindo empresa, filial, caixa, versões envolvidas, data, horário e resultado.
5. Os registros resumidos do histórico centralizado deverão ser distintos dos logs técnicos detalhados armazenados localmente, conforme RF345.
6. A consulta centralizada não dependerá da transmissão automática dos logs detalhados armazenados nos caixas.
7. Permitir pesquisar e filtrar por caixa, filial, empresa, período e resultado da atualização.
8. Apresentar atualizações concluídas, falhas, cancelamentos e restaurações.
9. O acesso será exclusivo aos desenvolvedores e usuários internos expressamente autorizados pela empresa responsável pelo PDV Web.
10. Administradores, gerentes e demais funcionários das empresas clientes não poderão visualizar esse relatório.
11. As permissões deverão ser controladas pelo mecanismo de acesso técnico da plataforma, com validação obrigatória no backend.
12. O histórico centralizado deverá ser permanente e protegido contra alterações ou exclusões indevidas.
13. A consulta deverá utilizar os registros já existentes, evitando processamento e consultas desnecessárias.

**RF344 — Registro técnico de falhas nas atualizações**

**Classificação: Futuro — fora do MVP.**

O sistema deverá registrar nos logs técnicos detalhados as falhas relacionadas às atualizações dos caixas, permitindo investigação posterior pela equipe técnica autorizada.

**Regras:**

1. Registrar falhas durante o download, instalação, validação e aplicação das atualizações.
2. Registrar tentativas de restauração da versão anterior e seus resultados.
3. Registrar a identificação da empresa, filial e caixa afetado.
4. Registrar versões envolvidas, data, horário, etapa da atualização, descrição técnica da falha e resultado da recuperação, quando aplicável.
5. Os detalhes técnicos deverão ser armazenados localmente no próprio caixa, conforme as regras do RF345.
6. Os eventos relevantes deverão gerar registros resumidos no histórico centralizado, conforme RF343, sem exigir a transmissão automática dos logs detalhados.
7. Não enviar notificações automáticas à equipe técnica em decorrência dos registros realizados por este requisito.
8. As notificações específicas de falhas durante liberações controladas seguirão a exceção prevista no RF368.
9. Os registros deverão permanecer disponíveis para investigação técnica durante o período de retenção definido no RF346.
10. O acesso aos logs deverá respeitar as permissões técnicas da plataforma.
11. O registro deverá ocorrer de maneira eficiente, sem consultas ou verificações repetitivas que aumentem desnecessariamente o consumo de recursos.

O mecanismo deverá preservar os registros originais e não poderá interferir nas operações normais do caixa.
**RF345 — Coleta manual de logs para investigação técnica**

**Classificação: Futuro — fora do MVP.**

O sistema deverá manter os logs técnicos detalhados das atualizações armazenados localmente em cada caixa, permitindo sua coleta manual pela equipe técnica quando necessário.

**Regras:**

1. Os logs técnicos detalhados deverão permanecer armazenados no próprio caixa onde os eventos ocorreram, conforme as regras de retenção definidas no RF346.
2. Não haverá transmissão contínua ou sincronização automática dos logs detalhados com o servidor central.
3. A equipe técnica poderá solicitar a coleta dos logs quando for acionada para investigar uma ocorrência.
4. A coleta deverá ser realizada mediante ação autorizada de um desenvolvedor ou usuário interno com permissão técnica específica.
5. Os registros coletados deverão preservar as informações originais, incluindo datas, horários, versões e detalhes técnicos das ocorrências.
6. A coleta não deverá interferir nas vendas ou nas demais operações do caixa.
7. A operação deverá ser realizada de maneira controlada, evitando consumo desnecessário de processamento, memória e rede.
8. O sistema deverá registrar a solicitação e o resultado da coleta, identificando o responsável, o caixa, a empresa, a filial e a data/hora.
9. A coleta dos logs não deverá alterar ou excluir os registros originais armazenados no caixa.
10. O acesso aos registros coletados será exclusivo da equipe técnica autorizada.
11. A coleta dos logs detalhados não deverá alterar o histórico resumido centralizado definido no RF343.

A implementação deverá respeitar as regras de proteção e retenção dos logs técnicos.
**RF346 — Retenção e armazenamento dos logs técnicos**

**Classificação: Futuro — fora do MVP.**

O sistema deverá manter os logs técnicos detalhados armazenados localmente nos caixas por um período de retenção configurável, definido tecnicamente durante a implementação.

**Regras:**

1. O período de retenção deverá ser configurável pela equipe técnica autorizada, sem prazo fixo definido neste momento.
2. A retenção deverá considerar a capacidade de armazenamento disponível em cada caixa.
3. O prazo de retenção deverá ser contado a partir da geração de cada registro.
4. Após o término do período configurado, os registros poderão ser removidos automaticamente.
5. A remoção deverá ocorrer sem interferir nas operações do PDV.
6. A coleta manual dos logs, conforme RF345, não deverá reiniciar o prazo de retenção nem provocar a exclusão antecipada dos registros originais.
7. A limpeza automática deverá respeitar os critérios de retenção configurados e não poderá remover registros antes do término do prazo.
8. Os logs deverão permanecer disponíveis durante todo o período de retenção para eventual investigação técnica.
9. Quando houver uma investigação técnica em andamento e a equipe autorizada solicitar a preservação dos registros, o sistema deverá impedir sua remoção até que a preservação seja liberada.
10. O mecanismo deverá evitar o crescimento indefinido dos arquivos de logs, utilizando processamento mínimo e sem verificações repetitivas desnecessárias.
11. A remoção dos logs locais não deverá excluir ou alterar os registros resumidos do histórico centralizado definido no RF343.
**RF347 — Proteção e integridade dos logs técnicos**

**Classificação: Futuro — fora do MVP.**

O sistema deverá proteger os logs técnicos armazenados localmente nos caixas contra alterações, adulterações e exclusões indevidas, preservando a confiabilidade dos registros durante todo o período de retenção.

**Regras:**

1. Os logs não poderão ser alterados manualmente por usuários ou pela equipe técnica.
2. Não deverá existir funcionalidade de exclusão manual dos registros originais.
3. Os registros deverão ser armazenados de maneira que impeça alterações indevidas e permita identificar eventuais violações de integridade.
4. A única remoção automática permitida será a limpeza conforme os critérios e o período de retenção definidos no RF346.
5. A limpeza automática deverá respeitar os prazos configurados e as solicitações de preservação de registros durante investigações técnicas.
6. A coleta manual prevista no RF345 não poderá modificar ou excluir os registros originais.
7. Os registros deverão preservar sua integridade durante todo o período de armazenamento.
8. Eventuais falhas na proteção ou validação da integridade deverão ser registradas tecnicamente, sem alterar os registros originais.
9. Os mecanismos de proteção deverão funcionar sem interferir nas operações do PDV e sem consumo desnecessário de recursos.
10. A remoção dos logs locais não poderá alterar ou excluir os registros resumidos do histórico centralizado definido no RF343.

**RF348 — Atualizações prioritárias em caixas offline**

**Classificação: Futuro — fora do MVP.**

O sistema deverá permitir que caixas operando offline mantenham suas atividades normalmente enquanto aguardam a disponibilidade de uma atualização prioritária.

**Regras:**

1. Quando uma atualização prioritária já disponibilizada for identificada pelo caixa e ele estiver sem conexão com o servidor central, sua aplicação deverá permanecer pendente.
2. A operação offline do caixa não deverá ser interrompida apenas pela existência de uma atualização pendente, salvo quando houver uma restrição obrigatória por risco de segurança, integridade ou compatibilidade, conforme RF350.
3. O sistema deverá preservar a identificação da atualização pendente, incluindo versão e informações necessárias para sua validação posterior.
4. Caso o caixa não tenha recebido as informações da atualização antes de ficar offline, deverá identificá-la somente após restabelecer a comunicação com o servidor central.
5. Assim que a conexão for restabelecida, o caixa deverá obter as informações necessárias para disponibilizar a atualização.
6. A aplicação deverá respeitar as condições de segurança definidas nos RF340, RF341 e RF354.
7. A atualização não poderá interromper vendas, pagamentos ou movimentações em andamento.
8. Caso existam incompatibilidades entre versões, deverão ser aplicadas as regras de compatibilidade e atualização coordenada do RF341.
9. A indisponibilidade temporária da internet não deverá provocar perda ou duplicidade de dados durante a operação offline ou a sincronização posterior.
10. Dados locais ainda não sincronizados deverão ser preservados durante todo o processo.
11. O sistema deverá evitar verificações repetitivas e tentativas desnecessárias enquanto o caixa estiver offline.

A implementação dependerá dos mecanismos de distribuição e gerenciamento de atualizações, sem alterar as regras de operação offline previstas para o MVP.

**RF349 — Disponibilização de atualização prioritária após retorno da conexão**

**Classificação: Futuro — fora do MVP.**

Após o restabelecimento da conexão com o servidor central, o sistema deverá disponibilizar as informações de atualizações prioritárias pendentes e permitir que o operador solicite sua aplicação, respeitando as condições de segurança e compatibilidade.

**Regras:**

1. Após recuperar a conexão, o caixa deverá identificar se existe uma atualização prioritária pendente e autorizada para aquele ambiente.
2. O sistema deverá apresentar ao operador uma comunicação informando que a atualização está disponível.
3. Deverá oferecer as seguintes opções:
   - **Atualizar agora:** solicita o início da atualização, respeitando as condições de segurança dos RF340 e RF341 e a finalização das operações pendentes conforme RF354.
   - **Atualizar depois:** mantém a atualização pendente, permitindo a continuidade normal das operações quando não houver restrição obrigatória conforme RF350.
4. A opção de atualizar depois não deverá cancelar, remover ou invalidar a atualização pendente.
5. A atualização deverá permanecer disponível até sua aplicação bem-sucedida ou até que seja cancelada pela equipe técnica autorizada, conforme RF355.
6. A escolha do operador não poderá provocar perda, duplicidade ou inconsistência de vendas, pagamentos, movimentações ou dados pendentes de sincronização.
7. O sistema deverá respeitar a compatibilidade entre versões, as dependências e a sincronização dos dados.
8. Não deverão ser exibidas notificações repetitivas enquanto a mesma atualização permanecer pendente, salvo quando houver mudança relevante em sua situação ou obrigatoriedade.
9. Caso a atualização passe a ser obrigatória, deverão ser aplicadas as regras específicas do RF350, independentemente da escolha anterior do operador.
10. A aplicação deverá ser validada pelo backend, que deverá confirmar a disponibilidade e a autorização da atualização antes de iniciar o processo.
11. A apresentação e o controle da atualização deverão evitar verificações repetitivas e consumo desnecessário de recursos.
**RF350 — Atualizações prioritárias obrigatórias**

**Classificação: Futuro — fora do MVP.**

O sistema deverá permitir que atualizações prioritárias sejam classificadas como obrigatórias pela equipe técnica responsável quando sua aplicação for necessária para corrigir riscos de segurança, integridade ou compatibilidade.

**Regras:**

1. Atualizações prioritárias poderão ser adiadas pelo operador, conforme RF349, enquanto não forem classificadas como obrigatórias.
2. A equipe técnica autorizada poderá classificar uma atualização como obrigatória, mediante justificativa técnica.
3. A obrigatoriedade deverá ser fundamentada em motivos técnicos, como:
   - Correção de vulnerabilidades de segurança.
   - Prevenção de inconsistências ou corrupção de dados.
   - Correção de incompatibilidades entre versões.
   - Proteção da comunicação e sincronização entre caixas e servidor central.
4. Quando uma atualização for classificada como obrigatória, o sistema deverá informar ao operador que ela não poderá mais ser adiada.
5. A obrigatoriedade deverá considerar exclusivamente os caixas, versões e operações efetivamente afetados pelo problema.
6. Quando a versão em uso representar risco à segurança, à integridade dos dados ou à compatibilidade, o sistema deverá bloquear as novas operações afetadas até que a atualização seja aplicada com sucesso ou que a equipe técnica disponibilize uma solução segura.
7. O bloqueio não deverá interromper operações já iniciadas. Essas operações deverão ser finalizadas ou tratadas de forma segura antes da atualização, conforme RF340 e RF354.
8. Caixas e operações que não apresentem o risco identificado não deverão ser bloqueados desnecessariamente.
9. A classificação como obrigatória deverá ser realizada exclusivamente por desenvolvedores ou usuários internos expressamente autorizados.
10. Registrar a justificativa, atualização, versões afetadas, responsável pela classificação, empresas, filiais, caixas envolvidos, restrições aplicadas, data e horário.
11. A aplicação deverá respeitar as regras de compatibilidade, sincronização, instalação e recuperação previstas nos requisitos anteriores.
12. O controle deverá ocorrer no backend, evitando verificações e processamentos desnecessários.

A implementação dependerá dos mecanismos de gerenciamento de atualizações e controle de compatibilidade do sistema.
**RF352 — Comunicação durante a instalação de atualizações**

**Classificação: Futuro — fora do MVP.**

Durante a instalação de atualizações, o sistema deverá apresentar ao operador uma comunicação simples, informando que o processo está em andamento, sem expor detalhes técnicos desnecessários.

**Regras:**

1. Exibir a mensagem: **“Atualização em andamento. Aguarde.”**
2. Não apresentar barra de progresso, percentual ou detalhamento das etapas técnicas.
3. A mensagem deverá permanecer visível durante o processo de atualização.
4. Enquanto a atualização estiver em andamento, o sistema não deverá permitir novas operações incompatíveis com o processo.
5. Após a conclusão, informar se a atualização foi realizada com sucesso.
6. Em caso de falha, apresentar uma mensagem simples informando que não foi possível concluir a atualização.
7. Os detalhes técnicos das etapas, falhas e resultados deverão permanecer restritos aos logs e às consultas técnicas da equipe autorizada, conforme RF343 e RF344.
8. A comunicação deverá ser simples e discreta, sem apresentar informações técnicas desnecessárias ao operador.
9. A interface deverá evitar atualizações visuais repetitivas e consumo desnecessário de processamento.
10. A comunicação não poderá comprometer a integridade dos dados nem interferir indevidamente nas operações que precisem ser concluídas antes da atualização.

A implementação dependerá dos mecanismos de instalação e gerenciamento de atualizações do sistema.

**RF353 — Retorno à operação após atualização**

**Classificação: Futuro — fora do MVP.**

Após a conclusão bem-sucedida de uma atualização, o caixa deverá retornar à tela de identificação do operador, exigindo uma nova autenticação para liberar o acesso ao PDV.

**Regras:**

1. Após a atualização, apresentar a tela de identificação do operador.
2. Solicitar novamente usuário e senha, mesmo que o operador estivesse autenticado antes da atualização.
3. Não restaurar automaticamente a sessão autenticada anterior.
4. Após autenticação bem-sucedida, permitir que o operador retome o trabalho, respeitando suas permissões atuais.
5. Sempre que tecnicamente possível e seguro, restaurar apenas o contexto informativo da tela em que o operador estava trabalhando antes da atualização.
6. A restauração do contexto não poderá reabrir automaticamente operações concluídas, confirmar transações ou executar novamente ações anteriores.
7. Preservar os dados operacionais necessários para evitar perda de informações, inclusive dados locais ainda não sincronizados.
8. Não restaurar operações que possam gerar duplicidade, inconsistência ou comprometimento dos dados.
9. Caso não seja possível garantir a integridade do contexto anterior, o sistema deverá retornar à tela inicial do módulo, sem executar operações automaticamente.
10. Caso a autenticação falhe, manter o caixa bloqueado até a identificação de um usuário autorizado.
11. Registrar a conclusão da atualização e o novo acesso do operador conforme as regras de auditoria.
12. O retorno deverá ocorrer sem processamento ou consultas desnecessárias.

A implementação deverá preservar a separação entre autenticação, contexto operacional e dados pendentes, respeitando as permissões e os mecanismos de segurança do sistema.

**RF354 — Finalização das operações antes da atualização**

**Classificação: Futuro — fora do MVP.**

Antes de iniciar uma atualização do PDV, o sistema deverá verificar se existem operações em andamento que possam comprometer a integridade dos dados durante o processo.

**Regras:**

1. O sistema deverá identificar operações em andamento, gravações pendentes e processos de sincronização que possam ser incompatíveis com a atualização.
2. O operador deverá finalizar ou cancelar as operações que ainda estejam em andamento, conforme as regras, permissões e condições de cada módulo.
3. Enquanto houver operações incompatíveis que impeçam a atualização, o processo deverá permanecer bloqueado.
4. O sistema deverá informar ao operador quais operações precisam ser resolvidas, sem expor detalhes técnicos desnecessários.
5. Não deverá haver salvamento automático do estado de operações incompletas para retomada após a atualização.
6. Operações já concluídas e registradas deverão permanecer preservadas.
7. Dados de operações concluídas que ainda aguardem sincronização deverão ser preservados. A atualização somente poderá prosseguir se houver garantia de que esses dados permanecerão íntegros e compatíveis com a nova versão. Caso contrário, deverá aguardar a sincronização.
8. A atualização somente poderá começar após a confirmação de que todas as operações incompatíveis foram finalizadas ou canceladas e que os dados que precisam ser preservados estão protegidos.
9. A validação deverá ocorrer também no backend, conforme RF340, evitando perda, duplicidade ou inconsistência de dados.
10. A verificação deverá considerar alterações ou novas operações ocorridas entre a solicitação e o início efetivo da atualização.
11. O mecanismo deverá utilizar verificações eficientes, sem processamento desnecessário ou consultas repetitivas.
12. A atualização não deverá exigir o cancelamento de dados offline já registrados e válidos apenas porque ainda não foram sincronizados.

A implementação deverá respeitar as regras de compatibilidade, sincronização e recuperação previstas nos RF340, RF341 e RF342..

**RF355 — Cancelamento de atualizações prioritárias**

**Classificação: Futuro — fora do MVP.**

O sistema deverá permitir que a equipe técnica autorizada cancele atualizações prioritárias que ainda estejam pendentes de instalação, respeitando as condições de segurança, compatibilidade e obrigatoriedade.

**Regras:**

1. Somente desenvolvedores ou usuários internos expressamente autorizados poderão cancelar uma atualização.
2. O cancelamento deverá retirar a atualização da lista de pendências dos caixas afetados que ainda não iniciaram a instalação.
3. Caixas que já iniciaram a instalação deverão concluir o processo normalmente, respeitando as regras de segurança e recuperação previstas nos requisitos anteriores.
4. O sistema não deverá interromper instalações em andamento devido ao cancelamento.
5. Após a conclusão, os caixas que já foram atualizados deverão permanecer na nova versão, salvo decisão técnica específica conforme RF356.
6. O cancelamento deverá impedir que os caixas que ainda não iniciaram a instalação recebam ou apliquem aquela atualização.
7. Atualizações classificadas como obrigatórias conforme RF350 não poderão ser canceladas enquanto permanecer o motivo técnico que justificou sua obrigatoriedade. Sua retirada dessa condição dependerá de avaliação e autorização técnica, com justificativa registrada.
8. Caso a atualização seja posteriormente disponibilizada novamente, deverá ser tratada como uma nova disponibilização, com identificação e histórico próprios.
9. Registrar o motivo do cancelamento, atualização, versões envolvidas, responsável, empresas, filiais, caixas afetados, data e horário.
10. O cancelamento deverá respeitar a integridade dos dados, a compatibilidade entre versões e a preservação dos dados locais ainda não sincronizados.
11. O controle deverá ser centralizado no backend, evitando verificações e processamento desnecessários.

A implementação dependerá dos mecanismos de gerenciamento de atualizações e controle de permissões técnicas da plataforma.
**RF356 — Decisão sobre caixas já atualizados após cancelamento**

**Classificação: Futuro — fora do MVP.**

Quando uma atualização prioritária for cancelada após já ter sido aplicada em alguns caixas, o sistema deverá permitir que a equipe técnica autorizada decida individualmente o tratamento de cada caixa atualizado.

**Regras:**

1. A equipe técnica poderá decidir entre:
   - Manter o caixa na versão atualizada.
   - Restaurar o caixa para a versão anterior, desde que a operação seja tecnicamente segura.
2. A decisão poderá considerar individualmente cada caixa afetado.
3. A restauração deverá seguir os procedimentos de segurança e recuperação definidos no RF342.
4. Antes de restaurar, o sistema deverá verificar a compatibilidade da versão anterior com o servidor central, os demais caixas e as dependências necessárias.
5. Deverão ser preservadas as vendas, os pagamentos, as movimentações e os demais dados já registrados, inclusive os dados locais ainda não sincronizados.
6. A restauração não poderá provocar perda, duplicidade, reversão indevida ou inconsistência de operações realizadas após a atualização.
7. Caso a restauração não seja segura ou não seja possível garantir a integridade dos dados, o caixa deverá permanecer na versão atualizada ou receber restrições operacionais específicas, conforme avaliação técnica.
8. Quando não for possível garantir a segurança da operação, o caixa deverá permanecer bloqueado para as operações que possam comprometer os dados.
9. A decisão deverá ser realizada por desenvolvedores ou usuários internos expressamente autorizados, mediante justificativa.
10. Registrar a decisão, justificativa, caixa, empresa, filial, versões envolvidas, responsável, data, horário e resultado.
11. A operação deverá preservar a integridade dos dados e ser processada individualmente, sem afetar caixas que não necessitem de intervenção.
12. O histórico deverá integrar os registros técnicos definidos no RF343, evitando duplicidade de informações.

A implementação dependerá dos mecanismos de atualização, compatibilidade e recuperação do sistema.

**RF357 — Histórico de cancelamentos e decisões sobre atualizações**

**Classificação: Futuro — fora do MVP.**

O sistema deverá incorporar ao histórico técnico centralizado de atualizações, definido no RF343, os registros de cancelamentos e decisões tomadas pela equipe técnica.

**Regras:**

1. Registrar todos os cancelamentos de atualizações prioritárias.
2. Registrar as decisões tomadas para os caixas que já receberam a atualização.
3. Identificar se cada caixa permaneceu na nova versão ou retornou à versão anterior.
4. Registrar a justificativa da decisão, o responsável, a empresa, a filial, o caixa e a data/hora.
5. Permitir consultar essas informações pelos filtros já definidos no RF343.
6. O histórico será acessível exclusivamente aos desenvolvedores e usuários internos expressamente autorizados pela empresa responsável pelo PDV Web.
7. Administradores, gerentes e demais funcionários das empresas clientes não poderão acessar essas informações.
8. Os registros deverão ser permanentes e protegidos contra alterações ou exclusões indevidas.
9. Os eventos deverão ser incorporados à estrutura de histórico existente no RF343, sem criar tabelas, relatórios ou mecanismos duplicados desnecessariamente.
10. A consulta deverá utilizar os registros existentes, evitando processamento e consultas desnecessárias.
**RF358 — Priorização e ordem de aplicação das atualizações**

**Classificação: Futuro — fora do MVP.**

O sistema deverá permitir que a equipe técnica autorizada defina a prioridade e a ordem de aplicação das atualizações comuns e prioritárias, considerando dependências, compatibilidade, segurança e integridade operacional.

**Regras:**

1. A equipe técnica poderá definir a prioridade e a ordem de aplicação das atualizações disponíveis.
2. A definição deverá considerar dependências entre componentes, compatibilidade entre versões e requisitos técnicos de instalação.
3. O sistema deverá impedir a aplicação de atualizações em uma sequência que possa gerar incompatibilidade, falha ou inconsistência de dados.
4. Atualizações que dependam de outras deverão respeitar obrigatoriamente a sequência técnica necessária para seu funcionamento, conforme RF359.
5. A prioridade definida manualmente não poderá ignorar dependências obrigatórias nem validações de segurança.
6. Atualizações classificadas como obrigatórias por motivos de segurança, integridade ou compatibilidade deverão respeitar as condições estabelecidas no RF350.
7. Atualizações comuns e prioritárias poderão ser aplicadas na mesma operação somente quando a sequência for tecnicamente segura e compatível.
8. Caso não seja possível garantir a compatibilidade, as atualizações deverão ser aplicadas separadamente ou permanecer bloqueadas até a resolução da incompatibilidade.
9. A equipe técnica poderá alterar a prioridade ou a ordem de aplicação mediante justificativa e autorização específica.
10. Registrar a ordem definida, prioridades, atualizações envolvidas, versões, responsável, justificativa, data e horário.
11. As informações deverão integrar o histórico técnico centralizado definido no RF343, evitando duplicação de registros.
12. O controle deverá ser centralizado no backend, evitando verificações repetitivas e processamento desnecessário.

A implementação dependerá dos mecanismos de gerenciamento de atualizações, dependências e compatibilidade do sistema.

**RF359 — Identificação e instalação automática de dependências**

**Classificação: Futuro — fora do MVP.**

O sistema deverá identificar automaticamente as dependências necessárias para a aplicação de uma atualização e instalar primeiro os componentes exigidos, respeitando a ordem técnica de execução, a autenticidade dos pacotes e a compatibilidade entre versões.

**Regras:**

1. Antes de iniciar uma atualização, o sistema deverá verificar as dependências previamente cadastradas e vinculadas ao pacote.
2. Quando uma dependência necessária não estiver instalada ou estiver desatualizada, deverá ser identificada automaticamente.
3. O sistema deverá instalar primeiro as dependências necessárias e, posteriormente, a atualização principal.
4. A sequência deverá respeitar as relações de dependência e compatibilidade entre versões, conforme a ordem definida no RF358.
5. Dependências compartilhadas por diferentes componentes não deverão ser instaladas ou atualizadas repetidamente sem necessidade.
6. O sistema deverá identificar dependências incompatíveis ou conflitantes antes de iniciar a instalação.
7. Caso uma dependência apresente falha ou não atenda aos requisitos de segurança e compatibilidade, a atualização principal não deverá ser iniciada.
8. Somente dependências previamente validadas, autorizadas e disponibilizadas pela equipe técnica poderão ser instaladas automaticamente.
9. A autenticidade e a integridade dos pacotes de dependências deverão ser verificadas conforme os RF376 e RF377.
10. Falhas deverão seguir os mecanismos de recuperação e registro técnico definidos nos requisitos anteriores.
11. O sistema deverá preservar a compatibilidade entre os caixas e o servidor central, considerando também os componentes compartilhados.
12. As dependências instaladas ou atualizadas deverão ser identificadas no histórico técnico centralizado definido no RF343.
13. Registrar as versões anteriores e novas das dependências, os resultados da instalação e eventuais falhas, sem duplicar informações já disponíveis no histórico técnico.
14. O processamento deverá ser centralizado e otimizado, evitando verificações, transferências e instalações redundantes.

A implementação dependerá dos mecanismos de distribuição de pacotes, gerenciamento de dependências, segurança e compatibilidade do sistema.
**RF360 — Tratamento de falhas em dependências de atualização**

**Classificação: Futuro — fora do MVP.**

O sistema deverá avaliar automaticamente as dependências envolvidas em uma operação de atualização e identificar quais componentes poderão permanecer ativos e quais precisarão ser restaurados em caso de falha.

**Regras:**

1. Caso uma dependência apresente falha, a atualização principal deverá ser interrompida.
2. O sistema deverá identificar e avaliar as dependências que foram instaladas ou atualizadas durante a mesma operação.
3. Dependências que possam permanecer sem comprometer a integridade, a compatibilidade e o funcionamento dos demais componentes poderão ser mantidas.
4. Dependências que possam causar incompatibilidade ou inconsistência deverão ser restauradas para suas versões anteriores, desde que a restauração seja tecnicamente segura.
5. A avaliação deverá considerar outros componentes e caixas que utilizem as mesmas dependências, incluindo possíveis impactos decorrentes de versões compartilhadas.
6. Uma dependência compartilhada não poderá ser restaurada quando essa ação comprometer componentes que já dependam de sua versão atual.
7. A restauração deverá respeitar as regras de segurança e recuperação definidas no RF342.
8. Caso não seja possível garantir a integridade do ambiente, os caixas afetados deverão permanecer bloqueados para operações que possam comprometer os dados.
9. A atualização principal somente poderá ser disponibilizada novamente após a resolução da falha e a validação das dependências envolvidas.
10. Registrar as dependências envolvidas, versões anteriores e novas, decisões tomadas, restaurações realizadas e resultados.
11. Os registros deverão integrar o histórico técnico centralizado definido no RF343, sem duplicação desnecessária de informações.
12. O processamento deverá evitar restaurações desnecessárias e verificações repetitivas.

A implementação dependerá dos mecanismos de gerenciamento de dependências, compatibilidade e recuperação do sistema.
**RF361 — Validação e liberação gradual de atualizações**

**Classificação: Futuro — fora do MVP.**

O sistema deverá permitir que novas versões sejam validadas em ambiente interno e posteriormente disponibilizadas para um grupo controlado de caixas antes da distribuição geral.

**Regras:**

1. Toda atualização deverá passar por testes em ambiente interno antes de ser disponibilizada aos clientes.
2. Após a aprovação dos testes internos, a equipe técnica poderá selecionar um grupo controlado de empresas, filiais e caixas para receber inicialmente a atualização.
3. A seleção do grupo será realizada exclusivamente por desenvolvedores ou usuários internos expressamente autorizados.
4. A atualização deverá respeitar as regras de compatibilidade, autenticidade, integridade e dependências definidas nos requisitos anteriores.
5. Durante a liberação controlada, deverão ser avaliados o funcionamento, a estabilidade, a compatibilidade e possíveis falhas da nova versão.
6. A equipe técnica poderá interromper novas liberações caso identifique problemas, conforme o RF363.
7. A distribuição geral somente poderá ocorrer após a avaliação dos resultados da liberação controlada e a aprovação manual prevista no RF362.
8. Caso sejam identificadas falhas, deverão ser aplicados os procedimentos de análise, correção, interrupção, cancelamento ou restauração definidos nos requisitos anteriores.
9. Registrar versões, ambientes de teste, empresas, filiais, caixas selecionados, resultados, responsáveis e datas.
10. O histórico deverá integrar a consulta técnica exclusiva definida no RF343, evitando duplicação de registros.
11. O processo deverá evitar verificações e processamento desnecessários, preservando o desempenho dos caixas e do servidor central.

A implementação dependerá dos mecanismos de validação, distribuição controlada e gerenciamento de versões do sistema.

**RF362 — Aprovação para distribuição geral de atualizações**

**Classificação: Futuro — fora do MVP.**

O sistema deverá permitir que a equipe técnica avalie e aprove manualmente a distribuição geral de uma atualização após sua liberação controlada.

**Regras:**

1. A atualização deverá permanecer em liberação controlada durante um período definido tecnicamente.
2. Durante esse período, deverão ser avaliados os resultados dos testes, a estabilidade, o funcionamento, a compatibilidade e a ocorrência de falhas.
3. A equipe técnica deverá analisar os resultados antes de autorizar a distribuição geral.
4. O término do período de avaliação não deverá liberar automaticamente a atualização.
5. A distribuição geral dependerá de aprovação manual de desenvolvedor ou usuário interno expressamente autorizado.
6. A aprovação deverá considerar os resultados das empresas, filiais e caixas participantes da liberação controlada.
7. Caso sejam identificados problemas, a equipe técnica poderá manter a atualização em avaliação, interromper novas liberações, cancelar a distribuição ou solicitar correções.
8. Após uma correção, a atualização deverá passar novamente pelas validações necessárias, conforme RF361.
9. A aprovação deverá respeitar as regras de compatibilidade, dependências, autenticidade e integridade dos pacotes.
10. Registrar período de avaliação, resultados e indicadores analisados, decisão, justificativa, responsável, versão e data/hora.
11. O histórico deverá integrar a consulta técnica exclusiva definida no RF343, sem duplicação desnecessária de registros.
12. A avaliação deverá utilizar informações já disponíveis, evitando coleta, consultas ou processamento desnecessários.

A implementação dependerá dos mecanismos de validação, liberação controlada e gerenciamento de versões do sistema.
**RF363 — Interrupção da liberação controlada**

**Classificação: Futuro — fora do MVP.**

O sistema deverá permitir interromper a distribuição controlada de uma atualização quando forem identificadas falhas ou problemas durante o período de avaliação.

**Regras:**

1. Ao identificar uma falha, a equipe técnica autorizada poderá interromper imediatamente novas instalações da atualização.
2. Os caixas que ainda não iniciaram a instalação não deverão receber ou aplicar a atualização enquanto a liberação estiver interrompida.
3. Os caixas que já iniciaram a instalação deverão concluir o processo normalmente, respeitando as regras de segurança, integridade e recuperação existentes.
4. Após a conclusão das instalações em andamento, a equipe técnica deverá avaliar individualmente os caixas atualizados e decidir entre:
   - Manter a nova versão instalada, caso esteja funcionando corretamente e seja compatível.
   - Restaurar a versão anterior, caso seja identificada incompatibilidade, falha ou risco à operação, desde que a restauração seja tecnicamente segura.
5. A decisão deverá considerar a integridade dos dados, a compatibilidade com o servidor central, os demais caixas e as dependências.
6. Caso não seja possível garantir a segurança da operação, deverão ser aplicadas as restrições de funcionamento previstas nos requisitos anteriores.
7. A restauração deverá seguir o procedimento definido no RF342.
8. A interrupção não deverá provocar perda, duplicidade ou inconsistência de vendas, pagamentos, movimentações ou dados pendentes de sincronização.
9. A equipe técnica deverá informar uma justificativa para a interrupção e para cada decisão de manter ou restaurar a versão.
10. Registrar a atualização, versão, caixas afetados, empresa, filial, motivo, decisões, responsáveis, datas, horários e resultados.
11. O histórico deverá integrar a consulta técnica definida no RF343, respeitando as permissões de acesso e evitando duplicação de registros.
12. O processamento deverá ser individualizado por caixa, evitando restaurações e verificações desnecessárias.
13. A interrupção da liberação não deverá alterar automaticamente a situação de caixas que não participem do grupo afetado.

A implementação dependerá dos mecanismos de distribuição controlada, compatibilidade e recuperação do sistema.

**RF364 — Retomada da liberação após interrupção**

**Classificação: Futuro — fora do MVP.**

O sistema deverá permitir retomar a distribuição de uma atualização interrompida somente após análise das falhas identificadas, aplicação das correções necessárias e realização de uma nova validação controlada.

**Regras:**

1. A retomada da distribuição dependerá de avaliação e aprovação manual da equipe técnica autorizada.
2. As falhas identificadas durante a liberação anterior deverão ser analisadas e documentadas.
3. As correções necessárias deverão ser aplicadas antes de uma nova liberação.
4. Quando as correções alterarem o conteúdo do pacote, deverá ser gerada uma nova versão ou identificação do pacote, com as respectivas validações de integridade e autenticidade.
5. A atualização deverá passar novamente pelos testes internos definidos no RF361.
6. Após a aprovação interna, deverá ser realizada uma nova liberação para um grupo controlado de caixas.
7. A equipe técnica deverá acompanhar novamente os indicadores de estabilidade, funcionamento, compatibilidade e ocorrência de falhas.
8. A distribuição geral dependerá de nova aprovação manual, conforme RF362.
9. A retomada não deverá ocorrer automaticamente após a correção ou o encerramento da interrupção.
10. Caso as correções exijam uma nova versão, o sistema deverá preservar o vínculo com a versão e a liberação anteriores.
11. Registrar falhas, correções, versões, testes, resultados, grupos de caixas, responsáveis, decisões, justificativas e datas/horários.
12. O histórico deverá integrar a consulta técnica definida no RF343, evitando duplicação desnecessária de registros.
13. O processo deverá preservar a integridade dos dados e evitar verificações ou processamentos desnecessários.

A implementação dependerá dos mecanismos de validação, distribuição controlada e gerenciamento de versões do sistema.
**RF365 — Histórico das liberações controladas**

**Classificação: Futuro — fora do MVP.**

O sistema deverá manter um histórico completo e permanente das liberações controladas de atualizações, incluindo suas etapas, interrupções, retomadas, avaliações e aprovações.

**Regras:**

1. Registrar todas as liberações controladas realizadas.
2. Registrar as etapas e os eventos relevantes de cada processo, incluindo:
   - Disponibilização para o grupo controlado.
   - Início e término do período de avaliação.
   - Resultados dos testes e indicadores analisados.
   - Falhas identificadas.
   - Interrupções da distribuição.
   - Correções realizadas.
   - Retomadas após novas validações.
   - Aprovações ou reprovações para distribuição geral.
   - Cancelamentos e encerramentos da liberação.
3. Identificar a versão da atualização, empresas, filiais e caixas envolvidos.
4. Registrar responsável, decisão, justificativa, data e horário de cada evento, conforme aplicável.
5. Preservar os registros originais, sem permitir alterações ou exclusões manuais.
6. Novas correções ou decisões deverão gerar registros complementares, sem sobrescrever o histórico anterior.
7. A consulta deverá permitir identificar toda a trajetória de uma liberação, desde sua disponibilização inicial até sua aprovação, interrupção ou encerramento.
8. O acesso será exclusivo de desenvolvedores e usuários internos expressamente autorizados pela empresa responsável pelo PDV Web.
9. Administradores, gerentes e demais funcionários das empresas clientes não poderão acessar esse histórico técnico.
10. Os eventos deverão integrar o histórico centralizado definido no RF343, evitando a criação de estruturas ou registros duplicados desnecessariamente.
11. As alterações nas configurações e nos parâmetros das liberações deverão seguir também as regras específicas de auditoria do RF370.
12. O armazenamento e as consultas deverão ser otimizados, evitando processamento desnecessário.

**RF366 — Consulta do histórico de liberações**

**Classificação: Futuro — fora do MVP.**

O sistema deverá disponibilizar uma consulta técnica do histórico de liberações controladas de atualizações, permitindo localizar registros por meio de filtros individuais ou combinados.

**Regras:**

1. A consulta deverá permitir os seguintes filtros:
   - Versão da atualização.
   - Empresa.
   - Filial.
   - Caixa.
   - Período.
   - Responsável pela operação.
   - Etapa ou resultado da liberação.
2. Os filtros deverão funcionar individualmente ou combinados.
3. O resultado deverá apresentar as informações relevantes da liberação, incluindo versão, empresa, filial, caixas envolvidos, etapa, resultado, responsável e data/hora.
4. Deverá ser possível consultar os detalhes de cada liberação, incluindo interrupções, correções, retomadas, avaliações, aprovações e cancelamentos.
5. A consulta deverá utilizar o histórico definido no RF365, sem criar registros ou armazenamentos duplicados.
6. O acesso será exclusivo de desenvolvedores e usuários internos expressamente autorizados pela empresa responsável pelo PDV Web.
7. Administradores, gerentes e demais funcionários das empresas clientes não terão acesso a essa consulta.
8. O backend deverá validar as permissões de acesso, independentemente das restrições apresentadas na interface.
9. A consulta deverá apresentar somente os registros correspondentes aos filtros aplicados, com carregamento controlado dos resultados.
10. As consultas deverão ser otimizadas, evitando carregamento desnecessário de todo o histórico e processamento excessivo.

A implementação dependerá da estrutura de histórico e dos mecanismos de controle de acesso técnico da plataforma.
**RF367 — Exportação do histórico de liberações**

O sistema deverá permitir a exportação do histórico de liberações controladas de atualizações nos formatos Excel (.xlsx) e CSV.

**Regras:**

1. A exportação deverá estar disponível para usuários internos com permissão específica de consulta e exportação desse histórico.
2. Deverá respeitar todos os filtros aplicados na consulta do RF366.
3. O arquivo deverá conter os registros correspondentes aos filtros selecionados.
4. As informações exportadas deverão incluir versão, empresa, filial, caixa, etapa da liberação, resultado, responsável, justificativa e data/hora, conforme os dados disponíveis.
5. A exportação não poderá alterar ou excluir registros do histórico.
6. O acesso deverá ser validado pelo backend.
7. Registrar usuário responsável, data/hora, formato e resultado da exportação.
8. A exportação deverá integrar o histórico do RF365, sem duplicar registros.
9. O processamento deverá ser otimizado, evitando consumo excessivo de memória e processamento em grandes volumes de dados.
**RF367 — Exportação do histórico de liberações**

**Classificação: Futuro — fora do MVP.**

O sistema deverá permitir a exportação do histórico de liberações controladas de atualizações nos formatos Excel (.xlsx) e CSV.

**Regras:**

1. A exportação deverá estar disponível exclusivamente para desenvolvedores e usuários internos expressamente autorizados pela empresa responsável pelo PDV Web.
2. A funcionalidade deverá respeitar as permissões técnicas de consulta e exportação, com validação obrigatória no backend.
3. A exportação deverá respeitar todos os filtros aplicados na consulta do RF366.
4. O arquivo deverá conter somente os registros correspondentes aos filtros selecionados e disponíveis para o usuário autorizado.
5. As informações exportadas poderão incluir versão, empresa, filial, caixa, etapa da liberação, resultado, responsável, justificativa e data/hora, conforme os dados existentes.
6. A exportação não poderá alterar, excluir ou modificar registros do histórico.
7. Registrar usuário responsável, data/hora, formato e resultado da exportação.
8. O registro da exportação deverá integrar o histórico técnico e de auditoria existente, evitando estruturas ou registros duplicados.
9. A exportação deverá respeitar as regras gerais de geração de relatórios e arquivos definidas no RF288, sem criar mecanismos de autorização redundantes.
10. O processamento deverá ser otimizado, evitando consumo excessivo de memória e processamento em grandes volumes de dados.

A implementação dependerá dos mecanismos de consulta, exportação e controle de acesso técnico da plataforma.
**RF368 — Notificação sobre falhas na liberação controlada**

**Classificação: Futuro — fora do MVP.**

O sistema deverá enviar notificações internas e por e-mail aos responsáveis técnicos quando forem identificadas falhas relevantes durante a liberação controlada de atualizações.

**Regras:**

1. As notificações deverão ser direcionadas conforme a gravidade da falha, os serviços afetados e a área de responsabilidade dos funcionários.
2. Falhas críticas que possam comprometer a segurança, a integridade dos dados, a compatibilidade ou a continuidade da liberação deverão receber prioridade de comunicação.
3. Não deverão ser enviados alertas a funcionários sem relação com a atualização ou os serviços afetados.
4. As notificações deverão identificar a atualização, versão, empresa, filial e caixas envolvidos, quando aplicável.
5. Deverão apresentar uma descrição resumida da falha e indicar a necessidade de análise ou intervenção técnica.
6. O envio deverá respeitar as permissões e responsabilidades técnicas dos destinatários.
7. O sistema deverá registrar o conteúdo, destinatários, canais utilizados, data/hora e resultado do envio.
8. Esta funcionalidade constitui uma exceção específica para falhas identificadas durante liberações controladas. O simples registro de falhas nos logs técnicos, conforme RF344, não deverá gerar notificações automáticas.
9. As notificações deverão seguir os mecanismos e regras gerais definidos nos RF235–RF247, quando aplicáveis, evitando estruturas ou canais duplicados.
10. A identificação e o registro das falhas não deverão depender do sucesso do envio das notificações.
11. Não deverão ser enviadas notificações repetidas para a mesma falha enquanto ela permanecer sem alteração relevante. Uma nova notificação poderá ocorrer quando houver agravamento, mudança de situação ou necessidade de nova intervenção.
12. Falhas no envio das notificações deverão ser registradas, sem provocar tentativas repetitivas ou consumo desnecessário de recursos.
13. O processamento deverá evitar envios duplicados e comunicações desnecessárias.

A implementação dependerá dos mecanismos de liberação controlada e do sistema geral de notificações da plataforma.
**RF369 — Acompanhamento simplificado da liberação controlada**

**Classificação: Futuro — fora do MVP.**

O sistema deverá apresentar um status geral da liberação controlada de atualizações, permitindo que a equipe técnica acompanhe a situação do processo sem exigir um painel detalhado por caixa.

**Regras:**

1. Exibir o status geral da liberação, identificando se está:
   - Aguardando início.
   - Em liberação controlada.
   - Em avaliação.
   - Interrompida.
   - Aprovada para distribuição geral.
   - Reprovada.
   - Cancelada.
2. O status deverá refletir as etapas e decisões efetivamente registradas no processo.
3. Quando uma liberação envolver diferentes empresas, o status geral deverá considerar a situação de todos os grupos participantes, sem ocultar falhas, interrupções ou pendências individuais.
4. A consulta deverá permitir identificar a atualização e sua versão correspondente.
5. O acompanhamento deverá utilizar os registros já existentes nos RF365 e RF366, sem duplicar informações.
6. A consulta estará disponível exclusivamente para desenvolvedores e usuários internos expressamente autorizados.
7. O acompanhamento deverá utilizar informações já disponíveis, sem gerar verificações ou processamento desnecessários.
8. As informações detalhadas por caixa, indicadores consolidados e painéis gerenciais ficam fora do escopo deste requisito, podendo ser avaliados em versões futuras.

A implementação dependerá dos mecanismos de gerenciamento e acompanhamento de liberações controladas do sistema.
**RF370 — Histórico de alterações no processo de liberação**

**Classificação: Futuro — fora do MVP.**

O sistema deverá registrar todas as alterações relevantes realizadas pela equipe técnica durante o processo de liberação controlada de atualizações.

**Regras:**

1. Registrar alterações relevantes como:
   - Inclusão ou remoção de empresas, filiais e caixas do grupo controlado.
   - Alteração do grupo selecionado para a liberação.
   - Alteração da prioridade ou ordem de aplicação.
   - Interrupção da distribuição.
   - Retomada da distribuição.
   - Aprovação ou reprovação da atualização.
   - Cancelamento da liberação.
   - Alterações relevantes nas configurações e nos parâmetros do processo.
2. Cada registro deverá identificar a operação realizada, os valores anteriores e posteriores quando aplicável, o responsável, a justificativa, a data e o horário.
3. Os registros deverão ser permanentes e protegidos contra alterações ou exclusões manuais.
4. O histórico deverá estar vinculado à atualização e à respectiva liberação controlada.
5. A consulta será exclusiva de desenvolvedores e usuários internos expressamente autorizados.
6. O histórico deverá integrar os registros técnicos centralizados definidos nos RF343 e RF365, evitando duplicidade de informações.
7. Consultas simples e acessos ao acompanhamento não precisarão gerar registros adicionais neste requisito.
8. O backend deverá garantir a integridade dos registros e a aplicação das permissões.
9. As justificativas deverão ser obrigatórias para alterações que modifiquem a estratégia, a abrangência ou o resultado da liberação.
10. O armazenamento deverá ser otimizado, evitando registros repetidos ou desnecessários.

A implementação dependerá dos mecanismos de auditoria, histórico técnico e gerenciamento de liberações da plataforma.

**RF371 — Limite de caixas na liberação controlada**

**Classificação: Futuro — fora do MVP.**

O sistema deverá permitir que a equipe técnica defina manualmente quais caixas participarão de cada liberação controlada de atualização, sem estabelecer quantidade máxima fixa de participantes.

**Regras:**

1. Desenvolvedores ou usuários internos expressamente autorizados poderão selecionar manualmente os caixas participantes.
2. A quantidade de caixas será definida conforme a necessidade e a estratégia de cada atualização.
3. O sistema deverá validar a compatibilidade dos caixas selecionados com a versão, o ambiente e as dependências necessárias.
4. A seleção não poderá comprometer a integridade dos dados, a comunicação com o servidor central ou a sincronização entre caixas.
5. O sistema deverá impedir a inclusão de caixas que apresentem incompatibilidades técnicas conhecidas ou que não atendam aos critérios de participação.
6. Não haverá percentual máximo fixo ou quantidade padrão obrigatória.
7. Alterações na seleção dos caixas deverão respeitar as regras de interrupção e retomada dos RF363 e RF364.
8. Registrar caixas selecionados, empresa, filial, versão, responsável pela seleção, data e horário.
9. O histórico deverá integrar os registros técnicos definidos nos RF365 e RF370, evitando duplicação de informações.
10. A validação deverá ocorrer no backend, evitando verificações repetitivas e processamento desnecessário.

A implementação dependerá dos mecanismos de seleção de participantes, compatibilidade e gerenciamento de liberações controladas do sistema.
**RF372 — Seleção de caixas de diferentes empresas**

**Classificação: Futuro — fora do MVP.**

O sistema deverá permitir que uma mesma liberação controlada de atualização envolva caixas de diferentes empresas, mantendo grupos e controles individualizados por empresa.

**Regras:**

1. A equipe técnica poderá selecionar caixas pertencentes a diferentes empresas para participar de uma mesma liberação controlada.
2. Os caixas deverão ser organizados em grupos separados por empresa e, quando aplicável, por filial.
3. Cada grupo deverá possuir acompanhamento individual de sua participação na liberação.
4. A quantidade de caixas poderá ser definida separadamente para cada empresa, conforme o RF371.
5. A situação de um grupo não deverá alterar automaticamente a situação dos demais.
6. Uma falha em determinada empresa deverá permitir a interrupção ou análise individual daquele grupo, sem interromper automaticamente os demais, desde que a compatibilidade e a segurança estejam garantidas.
7. A aprovação para distribuição geral deverá considerar os resultados dos grupos participantes e respeitar as regras de aprovação individual ou geral do RF373.
8. A equipe técnica deverá conseguir identificar quais empresas, filiais e caixas participaram de cada liberação.
9. O isolamento dos dados e das configurações de cada empresa deverá ser preservado, independentemente da participação em uma mesma liberação.
10. O acesso e as operações sobre os grupos deverão respeitar as permissões técnicas específicas da plataforma.
11. Registrar grupos, caixas selecionados, versões, resultados, decisões, responsáveis e datas/horários.
12. O histórico deverá integrar os registros técnicos definidos nos RF365 e RF370, evitando duplicação de informações.
13. Todas as validações deverão ocorrer no backend, com processamento otimizado.

A implementação dependerá dos mecanismos de gerenciamento de liberações, isolamento entre empresas e controle de acesso técnico da plataforma.

**RF373 — Aprovação individual ou geral por empresa**

**Classificação: Futuro — fora do MVP.**

O sistema deverá permitir que a equipe técnica escolha entre aprovar a distribuição geral de uma atualização para todas as empresas participantes ou aprovar individualmente cada empresa após a liberação controlada.

**Regras:**

1. A equipe técnica poderá optar pela aprovação geral ou individual por empresa.
2. Na aprovação geral, os resultados de todos os grupos participantes deverão ser considerados antes da autorização da distribuição.
3. Na aprovação individual, cada empresa poderá avançar para a distribuição geral após apresentar resultados satisfatórios e receber aprovação técnica específica.
4. Empresas com falhas poderão permanecer em avaliação, ter a liberação interrompida ou receber tratamento específico, sem bloquear automaticamente as demais, desde que não exista dependência ou incompatibilidade que comprometa a segurança.
5. A aprovação deverá ser sempre manual, realizada por desenvolvedor ou usuário interno expressamente autorizado.
6. A decisão deverá considerar estabilidade, funcionamento, falhas, compatibilidade, dependências e integridade dos dados.
7. Uma aprovação individual não poderá liberar automaticamente outras empresas participantes.
8. A aprovação geral somente poderá ocorrer quando todos os grupos participantes atenderem aos critérios técnicos necessários ou tiverem recebido tratamento formal que permita sua exclusão da distribuição.
9. Registrar o tipo de aprovação, empresas envolvidas, resultados avaliados, justificativa, responsável, versão, data e horário.
10. As decisões deverão integrar o histórico técnico definido nos RF365 e RF370, sem duplicação de registros.
11. O backend deverá garantir que nenhuma empresa receba a distribuição geral sem a aprovação correspondente.
12. O processamento deverá ser individualizado e otimizado, evitando avaliações repetitivas desnecessárias.

A implementação dependerá dos mecanismos de liberação controlada, compatibilidade e aprovação técnica do sistema.

**RF374 — Compatibilidade entre empresas durante a liberação**

**Classificação: Futuro — fora do MVP.**

O sistema deverá avaliar automaticamente a compatibilidade de cada empresa participante antes de disponibilizar uma atualização, permitindo a liberação somente para empresas que atendam aos requisitos técnicos e de segurança.

**Regras:**

1. A compatibilidade deverá ser avaliada individualmente por empresa, considerando versões, configurações, componentes, dependências e ambiente de destino.
2. Empresas compatíveis poderão avançar no processo de liberação sem depender da aprovação das demais, desde que não existam dependências compartilhadas que comprometam sua segurança.
3. Empresas incompatíveis deverão permanecer fora da distribuição até que as condições técnicas necessárias sejam atendidas.
4. O sistema deverá identificar dependências técnicas compartilhadas entre empresas e avaliar se uma atualização pode comprometer o funcionamento de outros clientes ou serviços.
5. A existência de dependências compartilhadas que possam gerar incompatibilidade, indisponibilidade ou inconsistência deverá impedir liberações que apresentem risco às empresas envolvidas.
6. A análise deverá considerar os efeitos da atualização sobre os componentes compartilhados, mesmo quando as empresas possuírem versões ou configurações diferentes.
7. A análise detalhada das dependências compartilhadas, seus relacionamentos, impactos e riscos técnicos será de acesso exclusivo dos desenvolvedores e usuários internos expressamente autorizados.
8. Administradores, gerentes, operadores e demais funcionários das empresas clientes não poderão visualizar essas informações técnicas.
9. Para usuários das empresas clientes, eventuais restrições deverão apresentar somente as informações operacionais necessárias, sem revelar arquitetura, dependências internas ou detalhes técnicos.
10. Desenvolvedores poderão consultar os detalhes da análise e realizar avaliações ou intervenções conforme suas permissões técnicas.
11. Registrar resultados das avaliações, empresas envolvidas, versões, incompatibilidades identificadas, decisões, responsáveis e datas/horários.
12. O histórico deverá integrar os registros técnicos definidos nos RF365 e RF370, evitando duplicação de informações.
13. Todas as validações deverão ocorrer no backend, preservando o isolamento entre empresas, a segurança e o desempenho da plataforma.

A implementação dependerá dos mecanismos de gerenciamento de versões, análise de dependências, compatibilidade e isolamento entre empresas.
**RF375 — Controle de ambientes e compatibilidade por credenciais**

**Classificação: Futuro — fora do MVP.**

O sistema deverá possuir uma camada centralizada de controle de ambientes, versões e compatibilidade, utilizando credenciais técnicas específicas para proteger a distribuição e impedir que versões não autorizadas sejam disponibilizadas aos clientes.

**1. Ambientes**

- **DEBUG:** ambiente exclusivo de desenvolvimento e testes internos.
- **HML:** ambiente de homologação, utilizado para validar funcionalidades, correções, integrações e compatibilidade.
- **PRD:** ambiente de produção, destinado às empresas clientes, contendo somente versões devidamente aprovadas.

**2. Controle de ambientes**

1. Clientes poderão utilizar exclusivamente versões classificadas como PRD.
2. DEBUG e HML serão exclusivos dos desenvolvedores e usuários internos expressamente autorizados.
3. Versões não aprovadas não poderão ser disponibilizadas em PRD.
4. A passagem de versões entre ambientes deverá ser controlada por desenvolvedores autorizados.
5. Toda promoção de versão deverá validar as credenciais e os critérios correspondentes ao ambiente de destino.
6. A passagem para PRD deverá exigir uma segunda aprovação, realizada por outro desenvolvedor autorizado, diferente do responsável pela solicitação.
7. Não será permitida autoaprovação.
8. A aprovação deverá considerar compatibilidade, integridade, dependências, autenticidade e resultados das validações anteriores.
9. Uma versão não poderá ser promovida para um ambiente sem cumprir os critérios obrigatórios definidos para essa etapa.

**3. Credenciais de compatibilidade**

1. Cada cliente deverá possuir credenciais técnicas próprias de compatibilidade.
2. Cada versão deverá possuir credenciais complementares de compatibilidade, que permitam validar sua utilização pelo cliente correspondente.
3. A liberação de uma atualização em PRD deverá validar a combinação entre cliente, versão e ambiente de destino.
4. Credenciais inválidas, incompatíveis, revogadas ou não autorizadas deverão bloquear a atualização.
5. As credenciais deverão ser geradas, armazenadas e validadas por mecanismos criptográficos seguros, sem armazenamento de segredos em texto puro.
6. As credenciais técnicas não poderão ser utilizadas como senhas de autenticação de funcionários ou clientes.
7. A alteração, substituição ou revogação de credenciais deverá exigir autorização específica e gerar histórico.
8. As credenciais não deverão ser compartilhadas entre clientes.
9. As credenciais e os segredos utilizados na validação não poderão ser expostos na interface do PDV, nos navegadores dos funcionários ou em consultas acessíveis às empresas clientes.
10. A validação deverá ocorrer no backend, preservando o isolamento entre empresas.

**4. Aprovação e auditoria**

1. Registrar ambiente de origem e destino, versão, cliente, identificação das credenciais utilizadas, responsável pela solicitação e responsável pela aprovação.
2. Registrar justificativas, resultados das validações, data e horário.
3. Preservar o histórico de promoções, reprovações, bloqueios e alterações de credenciais.
4. O controle detalhado de ambientes, credenciais e compatibilidade será exclusivo dos desenvolvedores e usuários internos expressamente autorizados.
5. O mecanismo deverá impedir que versões DEBUG ou HML sejam disponibilizadas acidentalmente aos clientes.
6. A aprovação em PRD deverá respeitar a separação de responsabilidades, impedindo que o solicitante aprove a própria promoção.
7. O processo deverá ser integrado aos requisitos de atualização e liberação controlada, evitando duplicidade de controles e processamento desnecessário.

A implementação dependerá dos mecanismos de gerenciamento de ambientes, credenciais técnicas, autorização e distribuição de versões da plataforma.

**3. Credenciais de compatibilidade**

1. Cada cliente deverá possuir credenciais próprias de compatibilidade.
2. Cada versão deverá possuir credenciais complementares de compatibilidade.
3. A liberação de uma atualização em PRD deverá validar a combinação entre cliente e versão.
4. Credenciais inválidas, incompatíveis ou não autorizadas deverão bloquear a atualização.
5. As credenciais deverão ser armazenadas e validadas por mecanismos criptográficos seguros, sem armazenamento de senhas em texto puro.
6. A alteração, substituição ou revogação de credenciais deverá exigir autorização e gerar histórico.
7. As credenciais não deverão ser compartilhadas entre clientes.
8. A validação deverá ocorrer no backend, preservando o isolamento entre empresas.

**4. Aprovação e auditoria**

1. Registrar ambiente de origem e destino, versão, cliente, credenciais utilizadas, responsável pela solicitação e responsável pela aprovação.
2. Registrar justificativas, resultados das validações, data e horário.
3. Preservar o histórico de promoções, reprovações, bloqueios e alterações de credenciais.
4. O controle detalhado de ambientes, credenciais e compatibilidade será exclusivo dos desenvolvedores.
5. O mecanismo deverá impedir que versões DEBUG ou HML sejam disponibilizadas acidentalmente aos clientes.
6. O processo deverá ser integrado aos requisitos de atualização e liberação controlada, evitando duplicidade de controles e processamento desnecessário.

**RF376 — Identificação e integridade dos pacotes de atualização**

**Classificação: Futuro — fora do MVP.**

O sistema deverá identificar individualmente os pacotes de atualização e validar sua autenticidade e integridade antes de permitir sua instalação.

**Regras:**

1. Cada pacote deverá possuir:
   - Número da versão.
   - Identificador exclusivo.
   - Assinatura digital.
   - Hash criptográfico SHA-256.
   - Identificação do ambiente de destino (DEBUG, HML ou PRD).
2. O identificador exclusivo deverá permitir distinguir cada pacote, inclusive quando houver correções ou novas compilações de uma mesma versão.
3. O hash deverá ser calculado durante a geração do pacote e armazenado junto aos seus metadados.
4. Durante o recebimento da atualização, o sistema deverá calcular novamente o hash do pacote e comparar com o valor registrado.
5. A assinatura digital deverá ser validada para confirmar a autenticidade e a origem autorizada do pacote.
6. A identificação do ambiente de destino deverá ser validada antes da instalação, impedindo que pacotes destinados a DEBUG ou HML sejam aplicados em PRD.
7. Pacotes com assinatura inválida, hash divergente, identificação inconsistente ou ambiente de destino incompatível deverão ter sua instalação bloqueada.
8. A verificação deverá ocorrer antes da instalação, sem necessidade de processamento contínuo durante a operação normal do PDV.
9. Os pacotes deverão possuir identificação que impeça confusão entre versões, compilações ou ambientes diferentes.
10. A substituição de um pacote deverá exigir a geração de novos identificadores e a realização das validações correspondentes.
11. Registrar versão, identificador, hash, resultado da assinatura, ambiente, responsável pela geração, data/hora e resultado da validação.
12. As informações técnicas detalhadas serão acessíveis exclusivamente aos desenvolvedores e usuários internos expressamente autorizados.
13. A validação deverá ocorrer no backend ou em mecanismo seguro de instalação, sem depender exclusivamente de verificações realizadas pela interface do PDV.
14. O processo deverá ser otimizado, evitando recálculos e verificações desnecessárias.

A implementação dependerá dos mecanismos de geração, assinatura digital, armazenamento e validação segura dos pacotes de atualização.

**RF377 — Proteção e gerenciamento das chaves de assinatura digital**

**Classificação: Futuro — fora do MVP.**

O sistema deverá possuir um mecanismo seguro de gerenciamento das chaves utilizadas na assinatura digital dos pacotes de atualização, garantindo a autenticidade dos pacotes e impedindo a utilização de chaves não autorizadas.

**Regras:**

1. Utilizar conjuntos de chaves de assinatura distintos para os ambientes DEBUG, HML e PRD.
2. As chaves privadas deverão ser armazenadas em ambiente seguro, com acesso restrito a desenvolvedores e usuários internos expressamente autorizados.
3. As chaves privadas deverão permanecer protegidas em ambiente controlado de assinatura e não poderão ser disponibilizadas nos caixas das empresas clientes.
4. Os caixas deverão possuir acesso somente aos mecanismos de validação necessários, como as chaves públicas correspondentes ou certificados confiáveis.
5. Uma chave de determinado ambiente não poderá autorizar pacotes destinados a outro ambiente.
6. O processo de assinatura deverá ocorrer em ambiente controlado, sem exposição das chaves privadas.
7. O sistema deverá identificar qual chave foi utilizada na assinatura de cada pacote, permitindo relacionar a assinatura à respectiva chave e ao ambiente.
8. Deverá existir possibilidade de revogar e substituir chaves comprometidas ou que não devam mais ser utilizadas.
9. A substituição deverá preservar a capacidade de validar pacotes legítimos anteriormente assinados, enquanto suas assinaturas e chaves permanecerem válidas e confiáveis conforme a política de segurança.
10. A revogação de uma chave deverá impedir novas assinaturas e bloquear pacotes cuja assinatura não seja mais considerada confiável, conforme a política de segurança definida.
11. A revogação não deverá invalidar indiscriminadamente pacotes legítimos anteriores, salvo quando a avaliação de segurança determinar que suas assinaturas não podem mais ser consideradas confiáveis.
12. Registrar a utilização das chaves, identificando ambiente, pacote, versão, responsável, data/hora e resultado.
13. Alterações, revogações e substituições deverão exigir autorização específica e gerar histórico permanente, protegido contra alterações indevidas.
14. O gerenciamento detalhado das chaves será exclusivo dos desenvolvedores e usuários internos expressamente autorizados.
15. O mecanismo deverá utilizar recursos criptográficos seguros e evitar processamento ou verificações desnecessárias durante a operação normal do PDV.

A implementação dependerá dos mecanismos de assinatura digital, armazenamento seguro de chaves, validação criptográfica e gerenciamento de confiança da plataforma.
**RF378 — Rotação e substituição das chaves de assinatura**

**Classificação: Futuro — fora do MVP.**

O sistema deverá permitir o gerenciamento da rotação periódica e da substituição emergencial das chaves de assinatura digital utilizadas nos ambientes DEBUG, HML e PRD.

**Regras:**

1. Desenvolvedores autorizados poderão definir individualmente a periodicidade de rotação das chaves de cada ambiente.
2. A periodicidade poderá ser diferente entre DEBUG, HML e PRD.
3. O sistema deverá permitir a substituição emergencial de uma chave, independentemente da periodicidade configurada.
4. A substituição emergencial poderá ocorrer em situações de suspeita de comprometimento, exposição indevida ou necessidade de segurança.
5. A nova chave deverá ser gerada, cadastrada e validada antes de ser utilizada para novas assinaturas.
6. A transição deverá preservar a possibilidade de validar pacotes legítimos assinados anteriormente, enquanto suas assinaturas e chaves permanecerem válidas e confiáveis, respeitando a política de confiança e revogação definida no RF377.
7. A chave anterior deverá deixar de autorizar novas assinaturas após sua substituição, sem impedir automaticamente a validação de pacotes anteriores que continuem confiáveis.
8. A substituição deverá preservar a identificação das chaves anteriores e novas, permitindo rastreabilidade das assinaturas realizadas em cada período.
9. Registrar ambiente, chave substituída, nova chave, motivo, responsável, data/hora e resultado.
10. A rotação não deverá interromper a operação normal dos caixas nem comprometer atualizações legítimas.
11. O gerenciamento será exclusivo dos desenvolvedores e usuários internos expressamente autorizados.
12. A implementação deverá evitar verificações periódicas desnecessárias, utilizando mecanismos eficientes para controlar os prazos configurados.
13. A substituição ou rotação das chaves deverá integrar o histórico técnico e de auditoria da plataforma, sem duplicação desnecessária de registros.

A implementação dependerá dos mecanismos de assinatura digital, armazenamento seguro de chaves, validação criptográfica e gerenciamento de confiança definidos no RF377.
**RF379 — Validade das assinaturas digitais dos pacotes**

**Classificação: Futuro — fora do MVP.**

O sistema deverá controlar a validade das assinaturas digitais dos pacotes de atualização, considerando o prazo definido no momento da assinatura, a situação da chave utilizada e a política de confiança estabelecida.

**Regras:**

1. Cada pacote deverá possuir uma assinatura digital com data de geração e prazo de validade.
2. O prazo de validade deverá ser definido por desenvolvedores autorizados, conforme o ambiente de destino.
3. A assinatura somente poderá autorizar novas instalações enquanto estiver dentro do prazo estabelecido.
4. A chave utilizada deverá estar cadastrada e ser considerada confiável no momento da validação.
5. Assinaturas vinculadas a chaves revogadas ou não confiáveis deverão ser rejeitadas, conforme a política de segurança definida no RF377.
6. Pacotes com assinaturas vencidas deverão ter novas instalações bloqueadas.
7. A expiração da assinatura de um pacote já instalado não deverá invalidar automaticamente a versão em funcionamento no caixa.
8. A renovação da assinatura deverá exigir uma nova validação da autenticidade e integridade do pacote.
9. A renovação não poderá modificar o conteúdo do pacote sem recalcular seu hash e gerar uma nova assinatura digital.
10. Caso o conteúdo seja alterado, deverá ser gerado um novo identificador de pacote, preservando o vínculo histórico com a versão anterior.
11. Registrar pacote, versão, ambiente, chave utilizada, data de geração, prazo de validade, resultado da validação e eventuais rejeições.
12. As informações técnicas detalhadas serão acessíveis exclusivamente aos desenvolvedores e usuários internos expressamente autorizados.
13. A validação deverá ocorrer durante o processo de atualização, sem verificações repetitivas durante a operação normal do PDV.
14. O controle de validade deverá respeitar os mecanismos de rotação e revogação de chaves definidos nos RF377 e RF378.

A implementação dependerá dos mecanismos de assinatura digital, gerenciamento de chaves e validação criptográfica da plataforma.

**RF380 — Bloqueio de pacotes de atualização não autorizados**

**Classificação: Futuro — fora do MVP.**

O sistema deverá bloquear automaticamente a instalação de qualquer pacote de atualização que não esteja autorizado para o cliente, caixa ou ambiente de destino.

**Regras:**

1. Validar se o pacote está autorizado para a empresa cliente e para o ambiente de destino, conforme as regras do RF375.
2. Pacotes sem autorização, com credenciais inválidas, revogadas ou incompatíveis deverão ter a instalação bloqueada.
3. Pacotes com identificação, assinatura digital, hash ou prazo de validade inválidos também deverão ser bloqueados, conforme os RF376, RF377, RF378 e RF379.
4. A validação deverá considerar também a compatibilidade da versão e suas dependências, conforme os requisitos anteriores.
5. O bloqueio deverá ocorrer antes da instalação e não poderá interromper o funcionamento normal do PDV nem afetar pacotes legítimos já instalados.
6. Registrar a tentativa em log técnico, incluindo identificação do pacote, versão, empresa, caixa, filial, ambiente, motivo do bloqueio, data/hora e resultado da validação.
7. Os registros detalhados deverão respeitar as regras de armazenamento, proteção e retenção dos logs técnicos definidas nos RF344, RF345, RF346 e RF347.
8. Os eventos relevantes deverão integrar o histórico técnico centralizado definido no RF343, sem exigir a transmissão automática dos logs detalhados.
9. Os logs técnicos serão acessíveis exclusivamente a desenvolvedores e usuários internos expressamente autorizados.
10. Não serão enviadas notificações automáticas à equipe técnica em decorrência desses bloqueios, salvo quando houver uma condição específica de notificação prevista em outro requisito.
11. A validação deverá ocorrer antes da instalação, sem verificações repetitivas durante a operação normal do PDV.
12. O backend deverá garantir a aplicação das regras, sem depender exclusivamente de verificações realizadas pela interface do caixa.

A implementação dependerá dos mecanismos de autorização, assinatura digital, validação de integridade e gerenciamento seguro de pacotes de atualização.

**RF381 — Controle de reinstalação de pacotes de atualização**

**Classificação: Futuro — fora do MVP.**

O sistema deverá impedir a reinstalação automática ou acidental de pacotes de atualização que já tenham sido aplicados anteriormente, permitindo sua reinstalação somente mediante autorização técnica específica.

**Regras:**

1. O sistema deverá identificar pacotes já instalados por meio de seus identificadores exclusivos e registros de instalação.
2. Ao receber novamente um pacote já aplicado no mesmo caixa, o sistema deverá bloquear sua instalação padrão.
3. A reinstalação somente poderá ocorrer mediante autorização de um desenvolvedor com permissão técnica específica.
4. A autorização deverá ser realizada antes do início da reinstalação.
5. O desenvolvedor deverá informar uma justificativa para a reinstalação.
6. A reinstalação deverá respeitar todas as validações obrigatórias de assinatura digital, hash, credenciais, ambiente, validade, integridade e compatibilidade estabelecidas nos requisitos anteriores.
7. A autorização para reinstalação não poderá ignorar ou substituir as validações de segurança obrigatórias.
8. A reinstalação manual de um pacote não deverá substituir os mecanismos de recuperação automática de versão anterior definidos no RF342.
9. Registrar o pacote, versão, identificador, ambiente, cliente, caixa, filial, motivo, desenvolvedor autorizador, data/hora e resultado da reinstalação.
10. O histórico original da instalação deverá ser preservado, sem sobrescrita ou exclusão.
11. Os registros deverão integrar o histórico técnico centralizado definido no RF343, evitando duplicação de informações.
12. O controle deverá ser realizado pelo backend, evitando instalações duplicadas e processamento desnecessário.

A implementação dependerá dos mecanismos de identificação de pacotes, histórico de instalações, autorização técnica e validação de segurança da plataforma.

**RF382 — Retenção e exclusão de versões antigas dos pacotes**

**Classificação: Futuro — fora do MVP.**

O sistema deverá controlar a quantidade de versões dos pacotes de atualização armazenadas, mantendo preferencialmente as 5 versões mais recentes de cada pacote e ambiente, com o objetivo de otimizar o espaço de armazenamento.

**Regras:**

1. Manter preferencialmente as 5 versões mais recentes de cada pacote de atualização, separadamente por ambiente.
2. Ao disponibilizar uma nova versão, verificar a quantidade de versões armazenadas.
3. Quando o limite for ultrapassado, identificar as versões mais antigas que possam ser removidas com segurança.
4. Antes da exclusão, verificar se a versão ou pacote ainda é necessário para:
   - Caixas que estejam utilizando a versão correspondente.
   - Recuperação ou restauração de versões anteriores.
   - Atualizações ou instalações em andamento.
   - Processos técnicos de validação, compatibilidade ou dependências.
5. Versões necessárias para as situações descritas na regra anterior não poderão ser excluídas enquanto existir dependência ou risco à operação.
6. Caso seja necessário preservar mais de 5 versões por motivos de segurança, compatibilidade ou recuperação, o sistema poderá ultrapassar temporariamente esse limite.
7. Quando uma versão protegida deixar de ser necessária, poderá ser removida automaticamente conforme a política de retenção.
8. A exclusão dos arquivos dos pacotes deverá ser permanente, sem possibilidade de recuperação pelo sistema.
9. A exclusão dos arquivos não poderá remover ou alterar os registros históricos de instalações, atualizações, falhas, autorizações e auditorias.
10. A remoção dos pacotes não poderá comprometer versões atualmente instaladas nos caixas.
11. Registrar a versão excluída, pacote, ambiente, data/hora e resultado da exclusão.
12. O controle deverá ocorrer no backend, evitando verificações repetitivas e consumo desnecessário de recursos.
13. A retenção deverá ser aplicada individualmente por pacote e ambiente, respeitando as dependências e os mecanismos de recuperação do sistema.

**RF383 — Proteção de versões em uso e identificação de caixas desatualizados**

**Classificação: Futuro — fora do MVP.**

O sistema deverá impedir a exclusão de pacotes de atualização que ainda sejam necessários para caixas em operação ou processos de recuperação, identificando também caixas que estejam utilizando versões anteriores.

**Regras:**

1. Manter armazenadas as versões que ainda sejam utilizadas ou necessárias por algum caixa ou processo técnico.
2. Quando nenhum caixa ou processo depender de determinada versão, permitir sua exclusão automática conforme os critérios de retenção definidos no RF382.
3. Identificar caixas que estejam utilizando versões anteriores à versão mais recente aprovada e disponibilizada para sua empresa, ambiente e configuração.
4. Uma versão mais recente não deverá ser considerada obrigatória ou aplicável a determinado caixa somente por existir. Deverão ser consideradas sua aprovação, disponibilidade, compatibilidade e autorização para aquele ambiente e cliente.
5. Quando existir uma atualização compatível, autorizada e disponível para o caixa, apresentar uma notificação informativa indicando que há uma atualização disponível.
6. A notificação não deverá interromper as operações nem bloquear o funcionamento normal do caixa, salvo quando houver classificação obrigatória e restrições específicas conforme RF350.
7. A atualização deverá respeitar todas as regras de compatibilidade, segurança, dependências e liberação estabelecidas nos requisitos anteriores.
8. Não apresentar notificações repetitivas enquanto a mesma atualização permanecer pendente, salvo quando houver uma nova versão ou mudança relevante na situação ou obrigatoriedade.
9. Caixas offline deverão preservar suas operações e dados locais, identificando atualizações disponíveis somente após receberem informações válidas do servidor central.
10. A identificação deverá utilizar os registros de versões já existentes, evitando consultas e verificações desnecessárias.
11. Registrar as identificações relevantes e as atualizações realizadas conforme o histórico técnico centralizado definido no RF343.
12. A consulta e o acompanhamento técnico dessas informações serão exclusivos dos desenvolvedores e usuários internos expressamente autorizados.

A implementação dependerá dos mecanismos de gerenciamento de versões, compatibilidade, distribuição e histórico técnico da plataforma.
**RF384 — Tratamento de caixas com atualizações obrigatórias pendentes**

**Classificação: Futuro — fora do MVP.**

O sistema deverá identificar caixas que estejam utilizando versões anteriores quando existir uma atualização classificada como obrigatória pela equipe técnica, aplicando restrições somente quando houver riscos reais de segurança, integridade ou compatibilidade.

**Regras:**

1. Identificar caixas que estejam utilizando versões anteriores à atualização obrigatória, considerando a empresa, filial, ambiente e compatibilidade.
2. Informar ao operador que existe uma atualização obrigatória pendente, apresentando uma explicação operacional objetiva sobre a necessidade da atualização.
3. A classificação como obrigatória deverá possuir justificativa técnica registrada e estar vinculada aos caixas e versões efetivamente afetados.
4. Permitir a continuidade normal das operações quando a versão atual ainda não representar riscos à segurança, integridade dos dados ou compatibilidade.
5. Quando a versão representar risco, bloquear somente as novas operações efetivamente afetadas pelo problema.
6. Operações já iniciadas deverão receber tratamento seguro, permitindo sua conclusão quando possível ou aplicando o procedimento adequado para preservar a integridade dos dados.
7. O bloqueio deverá atingir somente os caixas e as operações efetivamente afetados, sem interromper os demais caixas compatíveis.
8. A atualização deverá respeitar as regras de compatibilidade, sincronização, instalação e recuperação já definidas.
9. Caso o caixa esteja offline, deverá preservar os dados locais e aplicar as restrições operacionais necessárias conforme o risco identificado. A indisponibilidade de conexão, por si só, não deverá provocar perda de dados ou bloqueio de operações sem relação com o risco.
10. Quando a conexão for restabelecida, o caixa deverá obter as informações da atualização obrigatória e seguir os procedimentos de aplicação definidos nos requisitos anteriores.
11. Registrar a atualização pendente, motivo, versão utilizada, versão obrigatória, caixa, empresa, filial, restrições aplicadas, responsável pela classificação, data/hora e resultado.
12. Os registros deverão integrar o histórico técnico centralizado definido no RF343, evitando duplicação de informações.
13. O controle deverá ocorrer pelo backend, evitando verificações repetitivas e processamento desnecessário.

A implementação dependerá dos mecanismos de gerenciamento de versões, classificação de atualizações obrigatórias, compatibilidade e controle operacional do sistema.
**RF385 — Relatório de conformidade das versões dos caixas**

**Classificação: Futuro — fora do MVP.**

O sistema deverá disponibilizar à equipe interna autorizada um relatório consolidado para acompanhamento das versões utilizadas pelos caixas, identificando situações de atualização, compatibilidade e conformidade.

**Regras:**

1. Disponibilizar consulta do status atual de cada caixa, incluindo:
   - Empresa e filial.
   - Identificação do caixa.
   - Ambiente de execução.
   - Versão atualmente instalada.
   - Versão mais recente aprovada e disponibilizada para aquele cliente e ambiente.
   - Situação da atualização.
   - Situação de compatibilidade.
   - Existência de atualização obrigatória pendente.
2. Identificar caixas atualizados, desatualizados, incompatíveis ou com atualização obrigatória pendente, considerando somente versões autorizadas e aplicáveis a cada caixa.
3. Permitir consultar o histórico de alterações de versões, atualizações, falhas, restaurações e mudanças de compatibilidade, utilizando os registros existentes no RF343.
4. Disponibilizar filtros combinados por empresa, filial, caixa, versão, ambiente, situação de atualização, compatibilidade e período.
5. Permitir exportação em Excel (.xlsx) e CSV, respeitando as regras gerais de exportação de relatórios definidas no RF288.
6. A exportação deverá respeitar os filtros aplicados e apresentar somente os dados disponíveis para o usuário autorizado.
7. Registrar usuário responsável, data/hora, formato e resultado da exportação, utilizando o mecanismo de auditoria existente.
8. O acesso será exclusivo aos desenvolvedores e usuários internos expressamente autorizados pela empresa responsável pelo PDV Web.
9. Administradores, gerentes e funcionários das empresas clientes não terão acesso a esse relatório técnico.
10. O backend deverá validar as permissões de acesso, independentemente das restrições apresentadas na interface.
11. Preservar o histórico técnico conforme as regras de retenção e auditoria já estabelecidas.
12. Reutilizar os registros existentes, evitando duplicação de dados, consultas desnecessárias e processamento excessivo.

A implementação dependerá dos mecanismos de consulta técnica, gerenciamento de versões, histórico e exportação da plataforma.
**RF386 — Detecção de caixas sem comunicação**

**Classificação: Futuro — fora do MVP.**

O sistema deverá identificar caixas que perderem comunicação com o servidor central, registrando a ocorrência e notificando a equipe técnica quando o período de indisponibilidade ultrapassar um limite configurável.

**Regras:**

1. Identificar a ausência de comunicação entre cada caixa e o servidor central por meio dos mecanismos de comunicação já utilizados pela plataforma.
2. Utilizar um intervalo de tolerância configurável pela equipe técnica autorizada antes de considerar a comunicação interrompida.
3. Registrar a última comunicação confirmada, o início estimado da indisponibilidade e a identificação do caixa, empresa e filial.
4. Quando o período sem comunicação ultrapassar o limite configurado, gerar uma notificação para os responsáveis técnicos.
5. Evitar notificações repetidas para a mesma ocorrência enquanto a comunicação permanecer interrompida.
6. Quando a comunicação for restabelecida, registrar a data/hora do retorno e o período estimado de indisponibilidade.
7. A recuperação da comunicação deverá encerrar a ocorrência correspondente, preservando seu histórico.
8. A ausência de comunicação não deverá interromper automaticamente a operação local do PDV, respeitando as regras de funcionamento offline e sincronização.
9. A indisponibilidade de comunicação deverá ser tratada separadamente de falhas internas do PDV, evitando classificações incorretas.
10. A identificação deverá utilizar mecanismos leves de comunicação, evitando consultas frequentes, verificações repetitivas e consumo desnecessário de recursos.
11. O histórico deverá ficar disponível para consulta pelos desenvolvedores e usuários internos expressamente autorizados.
12. Registrar também falhas no processo de identificação e envio das notificações, sem comprometer a operação local do caixa.

A implementação dependerá dos mecanismos de monitoramento técnico, comunicação e notificações internas da plataforma, sem alterar as regras de operação offline previstas para o MVP.
**RF387 — Monitoramento técnico da sincronização após retorno da conexão**

**Classificação: Futuro — fora do MVP.**

O sistema deverá disponibilizar mecanismos de monitoramento técnico detalhado do processo de sincronização dos dados pendentes dos caixas que retornarem à comunicação com o servidor central após operar offline.

**Regras:**

1. Registrar automaticamente o início da sincronização após o restabelecimento da comunicação.
2. Acompanhar o andamento do processo utilizando os registros e controles já existentes de sincronização.
3. Registrar a conclusão da sincronização, incluindo data, horário e resultado.
4. Em caso de falha, registrar a ocorrência e permitir a notificação da equipe técnica responsável, conforme os mecanismos de notificação da plataforma.
5. Não enviar notificações quando a sincronização for concluída normalmente.
6. Registrar a identificação do caixa, empresa, filial, período offline, início da sincronização, resultado e eventuais falhas.
7. Falhas deverão preservar os dados pendentes, sem provocar perda, duplicidade ou inconsistência nas operações.
8. A retomada da sincronização deverá respeitar as regras de integridade e consistência dos dados definidas para a operação offline.
9. Evitar notificações duplicadas para a mesma falha, respeitando as regras gerais de notificações.
10. Disponibilizar os registros para consulta exclusiva dos desenvolvedores e usuários internos expressamente autorizados.
11. O monitoramento deverá reutilizar os mecanismos e registros existentes, evitando verificações repetitivas, consultas desnecessárias e consumo excessivo de recursos.

**Importante:** a execução da sincronização, a preservação dos dados pendentes e a retomada após o restabelecimento da conexão permanecem obrigatórias no MVP, conforme as regras de operação offline. Este requisito trata exclusivamente do monitoramento técnico detalhado.
**RF388 — Monitoramento e proteção do armazenamento local durante operação offline**

**Classificação: MVP — primeira versão.**

O sistema deverá monitorar a disponibilidade do armazenamento local utilizado pelos caixas durante a operação offline, evitando que a falta de espaço comprometa a gravação, a integridade e a preservação dos dados operacionais.

**Regras:**

1. Monitorar o espaço disponível no armazenamento local utilizado pelo PDV, considerando os dados operacionais e as informações necessárias à sincronização.
2. Permitir configurar limites de atenção e limite crítico para o armazenamento local, respeitando a capacidade disponível no dispositivo.
3. Ao atingir o limite de atenção, apresentar um aviso informativo e discreto ao operador, sem interromper as operações.
4. Ao atingir o limite crítico, impedir o início de novas operações que dependam de gravação local quando não houver espaço suficiente para garantir sua persistência segura.
5. Operações já iniciadas deverão receber tratamento seguro, permitindo sua conclusão quando houver condições de gravação ou realizando o encerramento controlado quando não for possível garantir a integridade dos dados.
6. Os dados pendentes de sincronização nunca poderão ser excluídos automaticamente para liberar espaço.
7. O sistema não deverá comprometer registros de vendas, pagamentos, movimentações ou outras operações já confirmadas.
8. O PDV deverá informar ao operador quando houver bloqueio decorrente da falta de espaço disponível, apresentando uma orientação objetiva.
9. Após a liberação de espaço, o sistema deverá verificar se existem condições seguras para retomar as operações, sem exigir reinicializações desnecessárias.
10. A recuperação do espaço deverá priorizar mecanismos seguros de limpeza de dados temporários e já sincronizados, respeitando as regras do RF389.
11. Não enviar notificações automáticas à equipe técnica em decorrência dos limites de armazenamento atingidos.
12. Registrar os eventos de alerta, bloqueio e liberação, com identificação do caixa, empresa, filial, data/hora e situação identificada.
13. O monitoramento deverá ocorrer de maneira eficiente, priorizando verificações em eventos que possam consumir armazenamento e antes de gravações relevantes, evitando verificações excessivamente frequentes e processamento desnecessário.
14. As regras deverão preservar a integridade dos dados tanto durante a operação offline quanto durante a sincronização após o restabelecimento da conexão.

Este mecanismo deverá fazer parte da proteção básica da operação offline do MVP, sem depender dos recursos futuros de monitoramento técnico centralizado.
**RF389 — Recuperação do armazenamento local após sincronização**

**Classificação: MVP — primeira versão.**

O sistema deverá liberar automaticamente o espaço ocupado pelos dados operacionais locais após a confirmação de sua sincronização e gravação correta no servidor central, mantendo uma retenção temporária configurável para segurança.

**Regras:**

1. Os dados operacionais locais somente poderão ser removidos após confirmação do backend de que foram recebidos, processados e gravados corretamente no servidor central.
2. A confirmação deverá identificar quais operações foram sincronizadas com sucesso, permitindo controlar a limpeza individualmente.
3. Após a confirmação, os dados deverão permanecer armazenados localmente por um período curto e configurável, definido tecnicamente durante a implementação.
4. O período de retenção deverá priorizar a economia de armazenamento, sem comprometer a segurança ou a integridade das informações.
5. Após o término do período configurado, os dados operacionais locais já sincronizados poderão ser removidos automaticamente.
6. Dados ainda não sincronizados, com sincronização incompleta ou cuja integridade não tenha sido confirmada nunca poderão ser removidos por essa rotina.
7. Falhas de comunicação, interrupções ou respostas inconclusivas do servidor deverão manter os dados locais preservados até que exista confirmação válida da sincronização.
8. A limpeza local não poderá remover ou alterar os registros oficiais armazenados no servidor central.
9. Informações técnicas mínimas necessárias para controle de sincronização, prevenção de duplicidades e rastreabilidade deverão ser preservadas enquanto forem necessárias, mesmo após a remoção dos dados operacionais locais.
10. A limpeza deverá ocorrer sem interromper as operações normais do PDV.
11. Registrar a confirmação da sincronização e a execução da limpeza, com identificação do caixa, empresa, filial, operações envolvidas, data/hora e resultado.
12. O processo deverá ser otimizado, utilizando eventos e confirmações já existentes, evitando verificações repetitivas, consumo excessivo de memória e processamento desnecessário.
13. A limpeza dos dados operacionais sincronizados deverá respeitar os mecanismos de integridade, idempotência e recuperação da operação offline.

Este mecanismo deverá fazer parte da proteção básica da operação offline do MVP e trabalhar em conjunto com o RF388.

**Gestão básica de assinaturas SaaS**
**RF390 — Cadastro e manutenção de planos SaaS**

O sistema deverá permitir cadastrar e manter os planos SaaS disponibilizados pela plataforma.
**RF391 — Vinculação de empresa a plano SaaS**

O sistema deverá permitir vincular uma empresa contratante a um plano SaaS disponível na plataforma.
**RF392 — Registro dos limites e condições comerciais contratados**

O sistema deverá permitir registrar os limites e as condições comerciais associados ao plano SaaS contratado por cada empresa.

**RF393 — Controle da situação da assinatura SaaS**

O sistema deverá permitir consultar a situação da assinatura de cada empresa e realizar manualmente sua suspensão ou reativação, conforme as permissões de acesso.

**RF394 — Histórico de alterações das assinaturas SaaS**

O sistema deverá manter o histórico das alterações realizadas nas assinaturas das empresas, incluindo mudanças de plano, limites contratados, condições comerciais e situação da assinatura.

**RF395 — Registro e acompanhamento de cobranças e renovações SaaS**

O sistema deverá permitir registrar e acompanhar manualmente as cobranças e renovações das assinaturas das empresas, sem realizar cobrança recorrente ou renovação automática.