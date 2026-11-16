# 02 — Escopo do MVP

## 1. Objetivo

Definir as funcionalidades que farão parte da primeira versão operacional do PDV Web, estabelecendo os limites funcionais, técnicos e operacionais do desenvolvimento.

O MVP deverá permitir que um estabelecimento realize suas operações comerciais essenciais, incluindo controle de produtos, estoque, vendas, pagamentos, caixa, clientes e devoluções.

A primeira versão também deverá contemplar recursos administrativos, operação offline limitada, preparação fiscal interna, IA consultiva básica, gestão interna de assinaturas SaaS e notificações operacionais.

O sistema deverá ser desenvolvido com arquitetura que permita evolução e inclusão de novas funcionalidades sem exigir alterações estruturais desnecessárias.

## 2. Critérios gerais

A primeira versão deverá ser:

- Funcional e utilizável em um estabelecimento real de testes.
- Segura e com controle de permissões.
- Preparada para múltiplos usuários e caixas.
- Preparada para múltiplas empresas e filiais.
- Capaz de operar com internet e em modo offline limitado.
- Desenvolvida com controle de integridade, consistência e rastreabilidade.
- Preparada para expansão de funcionalidades e integrações futuras.
- Otimizada para evitar consultas, verificações e cálculos desnecessários.
- Capaz de processar tarefas pesadas sem comprometer a operação principal do PDV.

As funcionalidades deverão respeitar as regras de acesso, configurações e abrangência definidas para cada empresa, filial e caixa.

## 3. Funcionalidades incluídas no MVP

### 3.1. Acesso e usuários

- Login com usuário e senha.
- Perfis iniciais de administrador, gerente, operador e estoquista.
- Permissões configuráveis por funcionalidade e operação.
- Controle de sessões e acessos.
- Cadastro e gerenciamento de usuários por empresa.
- Registro de ações importantes em auditoria.
- Autorização para operações restritas.
- Quando o usuário não possuir a permissão necessária para executar uma operação, deverá ser possível solicitar autorização a outro usuário que possua a permissão específica, sem exigir que seja administrador.
- O usuário autorizador deverá ser validado conforme suas permissões efetivas.
- O sistema deverá registrar o usuário solicitante, o usuário autorizador, a operação solicitada, a data e o horário e o resultado da autorização.
- A autorização deverá estar vinculada à operação correspondente, mantendo rastreabilidade e auditoria.
- Operações críticas deverão ter suas permissões validadas no backend, independentemente das restrições apresentadas na interface.

### 3.2. Cadastro de produtos

- Cadastro centralizado de produtos, mantendo informações gerais padronizadas entre as filiais.
- Cadastro e edição das informações gerais dos produtos; descrição, unidade de medida e subcategoria são obrigatórias, e código de barras é opcional.
- Cadastro e gerenciamento de departamentos, categorias e subcategorias na hierarquia Departamento > Categoria > Subcategoria.
- Identificação opcional de segmento/nicho no departamento para filtrar categorias no onboarding e de produtos para uso interno.
- Controle de produtos ativos e inativos.
- Cadastro de custos e preços.
- Configuração de markup.
- Configurações comerciais individualizadas por filial, incluindo preços, custos, estoque mínimo, estoque ideal e disponibilidade dos produtos.
- Possibilidade de manter condições comerciais diferentes entre filiais, respeitando as configurações da empresa.
- Cadastro e manutenção das informações fiscais dos produtos, conforme regras definidas na seção 3.12.
- Produtos inativados não deverão perder seu histórico nem comprometer a consulta de vendas anteriores.
- A inativação de um produto não deverá exigir sua exclusão de operações históricas.

### 3.3. Frente de caixa

- Abertura de caixa.
- Identificação do operador responsável.
- Pesquisa de produtos.
- Leitura de código de barras.
- Inclusão e remoção de itens.
- Consulta de informações comerciais necessárias à venda.
- Aplicação de descontos autorizados.
- Finalização de vendas.
- Registro de pagamentos em dinheiro, PIX, crédito e débito.
- Possibilidade de composição de uma venda com diferentes formas de pagamento.
- Emissão de comprovante não fiscal.
- Cancelamento de vendas conforme permissões e regras aplicáveis.
- Recuperação de vendas interrompidas.
- Identificação e tratamento adequado de operações não finalizadas.
- Validação de preços, disponibilidade, estoque e demais regras comerciais no momento da conclusão da venda.
- Registro consistente dos itens, valores, descontos e formas de pagamento utilizados.
- Preservação dos dados históricos da venda, mesmo que o cadastro do produto seja alterado posteriormente.

Os meios de pagamento contemplados nesta seção representam o registro e o controle operacional das formas de pagamento no PDV. A integração com TEF, adquirentes, instituições financeiras ou provedores externos não faz parte do MVP.

### 3.4. Gestão de caixa

- Abertura e fechamento de caixas.
- Identificação do operador responsável por cada período de operação.
- Registro de suprimentos.
- Registro de sangrias.
- Registro das demais movimentações financeiras de caixa.
- Transferência de valores entre caixas.
- Identificação da origem, destino, responsável e valor em cada transferência.
- Troca de operador durante a operação do caixa, mantendo o histórico de responsabilidade de cada operador.
- Fechamento às cegas, permitindo que o operador informe os valores apurados sem visualizar previamente os valores esperados pelo sistema.
- Conferência dos valores por forma de pagamento.
- Comparação entre valores informados e valores calculados pelo sistema.
- Identificação e registro de divergências.
- Histórico de movimentações, operações e fechamentos.
- Relatórios de fechamento.
- Controle de permissões para operações financeiras e administrativas.

As transferências entre caixas deverão manter vínculo entre a movimentação de origem e a movimentação de destino, evitando registros isolados ou inconsistentes.

### 3.5. Estoque básico

- Entrada e saída de produtos.
- Baixa automática do estoque após a conclusão de vendas.
- Atualização do estoque conforme devoluções.
- Consulta de saldo por produto e filial.
- Configuração de estoque mínimo e ideal por produto e filial.
- Controle de disponibilidade dos produtos por filial.
- Realização de inventários.
- Conferência dos saldos físicos com os saldos registrados no sistema.
- Registro de ajustes de estoque conforme permissões.
- Histórico de movimentações.
- Bloqueio de venda por estoque insuficiente, respeitando as regras de autorização configuradas.
- Registro das operações que alterem o saldo de estoque.
- Garantia de consistência em movimentações simultâneas.

O MVP contempla o controle básico de estoque e inventários. Fluxos avançados de transferência entre filiais, incluindo envio, recebimento e tratamento completo de divergências, ficam fora do MVP.

### 3.6. Promoções e descontos

- Cadastro de promoções.
- Promoções por produto e categoria.
- Desconto por quantidade.
- Desconto por valor mínimo.
- Combos.
- Período de validade.
- Configuração de promoções acumulativas.
- Limites de desconto por usuário.
- Autorização para descontos especiais.
- Configuração da ordem de aplicação de promoções, descontos e benefícios.
- Respeito às condições comerciais e regras definidas por empresa e filial.
- Validação das condições da promoção no momento da venda.
- Controle de promoções que dependam obrigatoriamente de validação online.

Promoções que exijam consulta ou validação externa em tempo real não poderão ser aplicadas durante a operação offline.

### 3.7. Clientes

- Cadastro básico de clientes.
- Nome, CPF/CNPJ, telefone e e-mail.
- Associação de vendas ao cliente.
- Consulta do histórico de compras.
- Identificação de clientes para operações que dependam de vínculo, como créditos e vales-troca.
- Respeito às permissões de acesso às informações cadastrais.
- Proteção dos dados pessoais e utilização apenas para as finalidades operacionais previstas.

### 3.8. Relatórios básicos

- Vendas por período.
- Vendas por operador.
- Vendas por produto.
- Movimentações de caixa.
- Movimentações financeiras.
- Relatórios de fechamento de caixa.
- Valores informados e valores esperados nos fechamentos.
- Divergências identificadas.
- Posição de estoque.
- Situação do estoque por produto e filial.
- Indicadores básicos relacionados às vendas e à operação do estabelecimento.
- Controle de acesso aos relatórios conforme as permissões dos usuários.

Os relatórios deverão respeitar os filtros e critérios de consulta disponíveis, priorizando eficiência no processamento e evitando consultas ou cálculos desnecessários.

Relatórios personalizados, análises comparativas avançadas e painéis gerenciais completos ficam fora do MVP.

### 3.9. Operação offline

O PDV deverá permitir a continuidade de operações essenciais quando houver perda de conexão, respeitando limites e restrições configurados por empresa ou estabelecimento.

Funcionalidades previstas:

- Armazenamento local dos dados necessários à operação.
- Consulta de produtos e preços previamente sincronizados.
- Registro de vendas offline.
- Registro de pagamentos básicos permitidos.
- Controle de operações pendentes.
- Sincronização após o retorno da conexão.
- Identificação de conflitos e divergências.
- Proteção contra duplicidade de vendas e operações.
- Priorização da integridade dos dados durante a sincronização.

Os limites de operação offline deverão ser configuráveis, considerando:

- Tempo máximo sem conexão.
- Quantidade máxima de vendas realizadas offline.
- Valor acumulado máximo das vendas offline.

O sistema deverá apresentar avisos conforme a aproximação dos limites configurados, permitindo autorização de usuário com a permissão específica quando prevista pela regra aplicável e aplicando bloqueios graduais conforme a política definida.

**Proteção e recuperação do armazenamento local**

O MVP deverá possuir mecanismos de proteção do armazenamento local utilizado pelos caixas durante a operação offline.

- Monitorar o espaço disponível, considerando limites configuráveis de atenção e crítico.
- Apresentar aviso informativo ao operador quando o limite de atenção for atingido.
- Impedir o início de novas operações que dependam de gravação local quando o espaço disponível não garantir a persistência segura dos dados.
- Tratar operações já iniciadas de maneira segura, preservando sua integridade.
- Nunca excluir automaticamente dados pendentes de sincronização para liberar espaço.
- Após o restabelecimento da conexão, permitir a limpeza dos dados operacionais locais somente após confirmação do backend de que foram recebidos, processados e gravados corretamente no servidor central.
- Manter os dados sincronizados durante um período temporário de retenção configurável antes da remoção automática.
- Preservar as informações técnicas necessárias ao controle da sincronização, prevenção de duplicidades e rastreabilidade.
- Registrar os eventos relevantes de alerta, bloqueio, sincronização e limpeza.
- Executar esses mecanismos sem interromper as operações normais do PDV e evitando verificações, consultas e processamento desnecessários.

A proteção do armazenamento deverá priorizar a integridade dos dados, a continuidade operacional e a economia de processamento, memória e espaço local.
**Restrições da operação offline:**

- Operações administrativas que dependam de validação online não poderão ser executadas.
- Alterações de configurações que dependam de sincronização deverão aguardar o restabelecimento da conexão.
- Promoções especiais que exijam validação online não poderão ser utilizadas.
- Criação e utilização de vales-troca deverão ocorrer exclusivamente online.
- Devoluções deverão ocorrer exclusivamente online.
- Operações que dependam de consulta ou confirmação externa deverão respeitar a disponibilidade da conexão.
- O sistema não deverá permitir que a indisponibilidade da rede provoque perda ou duplicidade de operações já registradas localmente.

Quando a conexão for restabelecida, as operações pendentes deverão ser sincronizadas com mecanismos de identificação, validação e prevenção de duplicidade.



### 3.10. Administração básica

- Cadastro de empresas e filiais.
- Configurações operacionais.
- Cadastro de usuários por empresa.
- Configuração de caixas.
- Personalização básica da identificação visual.
- Configurações gerais e comerciais dos estabelecimentos.
- Definição e gerenciamento dos parâmetros e regras operacionais disponíveis no MVP.
- Configurações individualizadas por empresa, filial e caixa, conforme a abrangência de cada funcionalidade.
- Separação dos dados e das regras entre os estabelecimentos.
- Controle de acesso às configurações conforme permissões.
- Registro de alterações relevantes em auditoria.

### 3.11. Devoluções e créditos

O MVP deverá contemplar devoluções totais e parciais, respeitando a venda original, as regras comerciais, financeiras e de estoque e as permissões configuradas.

**Regras gerais de devolução:**

- Realização de devoluções totais e parciais de vendas.
- Consulta e validação da venda original para garantir a rastreabilidade.
- A quantidade devolvida de cada item não poderá ultrapassar a quantidade originalmente vendida, descontadas as devoluções anteriores.
- Identificação dos produtos e valores correspondentes à operação original.
- Preservação do histórico da venda original, mesmo quando o produto tiver sido posteriormente inativado ou alterado.
- Produtos inativados ou removidos do catálogo não deverão impedir a devolução de uma venda válida. A operação deverá utilizar os dados históricos da venda, sem exigir reativação ou recriação do produto.
- Tratamento das vendas antigas ou importadas conforme política configurada pela empresa, preservando a rastreabilidade e validando a existência dos dados necessários.

**Prazo e autorização:**

- Prazo padrão de devolução de 30 dias.
- Possibilidade de configuração do prazo por empresa.
- O prazo configurável terá limite máximo de 365 dias.
- Devoluções fora do prazo deverão seguir a política da empresa, que poderá bloquear definitivamente a operação ou permitir sua realização mediante autorização específica.
- Quando autorizada uma devolução fora do prazo, a justificativa será obrigatória.
- Autorizações e justificativas deverão ser registradas em auditoria.

**Caixa e efeitos financeiros:**

- Possibilidade de realizar devoluções após o fechamento do caixa original, conforme configuração da empresa.
- A configuração deverá definir em quais caixas a devolução poderá ocorrer e como serão registrados seus efeitos financeiros.
- O tratamento dos valores deverá considerar as formas de pagamento utilizadas na venda original.
- Vendas parcialmente pagas ou realizadas a prazo deverão respeitar regras configuráveis, considerando valores pagos, valores pendentes e valores devolvidos.
- A operação deverá preservar a rastreabilidade entre a venda, os pagamentos, a devolução e os respectivos efeitos financeiros.

**Destino do estoque:**

- O destino do estoque deverá ser definido individualmente para cada produto devolvido.
- A devolução poderá gerar entrada no estoque ou seguir outro destino permitido pelas regras operacionais.
- A entrada decorrente de uma devolução deverá ser registrada mesmo quando o saldo anterior do produto estiver zerado.
- A existência de saldo zero não deverá, por si só, exigir autorização.
- Divergências efetivamente identificadas deverão ser tratadas conforme as permissões, com justificativa e auditoria quando aplicável.

**Motivos e reversões:**

- Disponibilização de catálogo padrão de motivos de devolução.
- Possibilidade de a empresa adicionar, editar ou inativar motivos próprios.
- Motivos já utilizados não poderão ser excluídos definitivamente, preservando o histórico.
- O motivo da devolução será obrigatório.
- A reversão de devoluções deverá seguir política configurável pela empresa.
- A empresa poderá bloquear a reversão até a regularização ou permitir a operação mediante autorização específica, justificativa obrigatória e auditoria.
- O histórico da devolução original e de sua reversão deverá ser preservado.

**Pagamentos externos e créditos:**

- Quando houver pagamento processado por provedor externo, deverá ser solicitada a operação de cancelamento ou estorno quando houver suporte.
- Quando o provedor não oferecer essa possibilidade, deverá ser prevista a regularização manual conforme os procedimentos da empresa.
- A reversão financeira somente deverá ser considerada concluída após a confirmação necessária.
- Créditos e vales-troca já utilizados não poderão ser revertidos sem a regularização dos valores utilizados.
- Quando um vale-troca tiver sido utilizado, a reversão deverá considerar o valor consumido e impedir a restauração indevida de saldo.

**Créditos e vales-troca:**

Quando uma devolução gerar crédito ao cliente, a empresa poderá configurar uma das seguintes formas:

1. Crédito diretamente no cadastro do cliente.
2. Vale-troca individual vinculado à devolução.

O sistema deverá controlar saldo, validade, utilização, cancelamento e histórico dos créditos e vales-troca.

**Validade dos vales-troca:**

- Validade padrão de 180 dias.
- Possibilidade de configuração da validade pela empresa.
- O prazo de validade será contado a partir da data de emissão.
- Alterações futuras na configuração não deverão modificar retroativamente a validade dos vales já emitidos.
- Vales vencidos não poderão ser utilizados.

**Utilização parcial e saldo restante:**

- Quando um vale-troca for utilizado parcialmente, o vale original deverá ser encerrado e um novo vale deverá ser gerado automaticamente com o saldo restante.
- O novo vale deverá preservar a data de vencimento original.
- A relação entre o vale original e o novo vale deverá ser mantida para garantir rastreabilidade.
- O histórico deverá permitir identificar as utilizações e os saldos resultantes.

**Identificação e consulta:**

- Identificação do vale por código numérico, código de barras ou QR Code, conforme configuração da empresa.
- Consulta por operador e por cliente no PDV.
- Possibilidade de identificação do cliente por CPF/CNPJ e confirmação adicional, conforme configuração.
- Aplicação de controles de privacidade e limites de tentativas de identificação.
- Consulta deverá apresentar código, saldo, data de emissão, validade e situação do vale.
- O histórico detalhado deverá ficar disponível para consulta interna e auditoria.
- Na seleção para utilização, somente vales disponíveis deverão ser apresentados. Vales vencidos, totalmente utilizados ou cancelados não deverão aparecer como opções utilizáveis.

**Regras de utilização:**

- O cliente ou operador deverá selecionar ou informar o vale desejado.
- Antes da aplicação, o sistema deverá validar saldo, validade, situação, vínculo, abrangência e limite de utilização.
- A aplicação deverá apresentar um resumo ao usuário e exigir confirmação.
- O servidor deverá validar novamente as condições antes de concluir a operação.
- A empresa poderá configurar a abrangência de utilização: qualquer caixa, uma filial específica ou toda a rede.
- A empresa poderá configurar a quantidade máxima de vales utilizados em uma mesma venda.
- As formas de pagamento complementares permitidas deverão ser configuráveis.
- A ordem de aplicação de vales, descontos e promoções deverá ser configurável pela empresa.
- Quando um produto possuir restrição à utilização de vales, a venda não poderá utilizar nenhum vale, conforme a regra definida.
- A empresa poderá configurar o comportamento quando o saldo do vale for superior ao valor da compra: permitir utilização parcial, com geração de novo vale para o saldo restante, ou bloquear a utilização.
- Quando houver mais de um vale disponível e a aplicação automática for necessária, o sistema deverá priorizar o vale com vencimento mais próximo. Em caso de empate, deverá priorizar o vale emitido há mais tempo.

**Cancelamento e estorno de utilização:**

- O cancelamento de uma venda que utilizou vale deverá restaurar o valor consumido ao vale original.
- A restauração não deverá criar crédito indevido em um novo vale.
- A validade original deverá ser preservada.
- Cancelamentos parciais deverão restaurar o valor proporcional correspondente ao vale original.
- O histórico deverá identificar o consumo e a restauração do saldo.

**Segurança e consistência:**

- A utilização de um mesmo vale deverá ser protegida contra consumo duplicado.
- Operações simultâneas deverão ser controladas no servidor, permitindo apenas o consumo válido do saldo disponível.
- Em caso de concorrência, a operação conflitante deverá ser rejeitada e o saldo atualizado deverá ser apresentado.
- O sistema deverá utilizar identificador único de operação para impedir que uma mesma solicitação seja processada mais de uma vez.
- Repetições com o mesmo identificador e os mesmos dados não poderão consumir o saldo novamente.
- Requisições repetidas com dados divergentes deverão ser rejeitadas e registradas em auditoria.
- Em caso de falha após a confirmação do usuário, o sistema deverá consultar a situação da operação antes de permitir uma nova tentativa.
- Em vendas canceladas ou confirmadamente não concluídas, o consumo do vale deverá ser revertido de forma consistente.
- A indisponibilidade do serviço de vales deverá bloquear somente a utilização do vale, informar o usuário e permitir que a venda continue com outras formas de pagamento.
- Não deverão ocorrer tentativas automáticas repetidas de utilização durante a indisponibilidade do serviço.
- A criação e a utilização de vales-troca deverão ocorrer exclusivamente online.

Todas as operações de devolução, crédito e vale-troca deverão respeitar permissões, integridade, rastreabilidade e auditoria.

### 3.12. Preparação fiscal básica

- Cadastro e manutenção das informações fiscais dos produtos.
- Registro da classificação e do enquadramento fiscal aplicável a cada produto.
- Identificação visual simples de produtos com pendências fiscais.
- Atualização da situação das pendências fiscais somente quando houver alteração nos dados fiscais do produto, evitando verificações e recálculos desnecessários.
- Possibilidade de correção de informações fiscais em lote, exclusivamente por meio de importação de arquivos Excel ou CSV.
- Preparação e registro interno das informações fiscais relacionadas às vendas concluídas.
- Preservação dos dados fiscais utilizados no momento da venda, mesmo que o cadastro do produto seja alterado posteriormente.
- Registro da situação da preparação fiscal e de eventuais pendências ou falhas internas relacionadas à organização dos dados.
- Organização e disponibilização das informações necessárias para uma futura integração fiscal.

**Limites do MVP:**

- Não contempla transmissão de documentos fiscais à SEFAZ ou a provedores fiscais.
- Não contempla autorização, consulta ou acompanhamento de documentos fiscais junto à SEFAZ ou a provedores.
- Não contempla emissão efetiva de NF-e, NFC-e ou outros documentos fiscais.
- Situações como documento emitido, autorizado ou rejeitado dependerão de uma futura integração fiscal e não fazem parte do controle operacional desta versão.

### 3.13. IA consultiva básica

- Disponibilização de assistente de IA para consultas e orientações relacionadas ao uso e funcionamento do PDV Web.
- Utilização exclusiva da documentação oficial e aprovada do sistema como base de conhecimento.
- Apresentação de respostas, explicações e orientações de caráter consultivo.
- Possibilidade de sugerir procedimentos e ações, sem executá-los automaticamente.
- Qualquer ação operacional sugerida dependerá da execução e aprovação humana, respeitando as permissões e os registros de auditoria do sistema.
- Não deverá existir armazenamento de histórico individual de conversas associado aos usuários.
- Registro apenas de indicadores agregados de avaliações positivas e negativas, sem vinculação a usuários, perguntas ou conversas individuais.
- Disponibilização dos indicadores exclusivamente para a equipe interna responsável pelo desenvolvimento e manutenção.
- Controle inteligente do consumo do serviço de IA, sem estabelecer limite fixo de perguntas por usuário.
- A gestão de consumo deverá buscar equilíbrio entre disponibilidade, desempenho e custos operacionais.

**Limites do MVP:**

- Não contempla execução autônoma de operações.
- Não contempla alteração direta de dados pela IA.
- Não contempla tomada de decisões operacionais pela IA.
- Não contempla recursos avançados de inteligência artificial.
- Não contempla armazenamento de histórico individual de conversas.

A indisponibilidade do serviço de IA não deverá comprometer o funcionamento do PDV.

Em caso de indisponibilidade, o sistema deverá informar temporariamente a situação e orientar o usuário a tentar novamente mais tarde, sem realizar tentativas automáticas repetidas. As demais funcionalidades do PDV deverão continuar operando normalmente.

### 3.14. Gestão básica de assinaturas SaaS

- Cadastro e controle interno dos planos contratados pelas empresas.
- Registro dos limites e condições comerciais associados a cada contratação.
- Controle da situação da assinatura, permitindo identificar seu estado atual.
- Registro do histórico de alterações de planos, limites e condições comerciais.
- Consulta interna das informações para acompanhamento administrativo das assinaturas.

**Limites do MVP:**

- A cobrança será realizada manualmente.
- A renovação será realizada manualmente.
- A suspensão ou reativação das assinaturas será realizada manualmente.
- Não contempla cobrança recorrente automatizada.
- Não contempla renovação automática.
- Não contempla suspensão ou reativação automática por inadimplência.
- Não contempla gestão comercial avançada de planos e módulos.

### 3.15. Notificações operacionais

O MVP deverá contemplar um sistema básico de notificações para acompanhamento de operações relevantes, pendências e ocorrências relacionadas aos caixas.

**Visibilidade e acesso:**

- As notificações deverão ser vinculadas ao caixa em que a operação ocorreu.
- Somente gerentes e administradores vinculados ao caixa correspondente poderão visualizar suas notificações.
- Usuários com acesso a vários caixas poderão selecionar qual caixa desejam consultar.
- As notificações de cada caixa deverão ser apresentadas separadamente.
- Ao acessar novamente a área de notificações, o sistema deverá selecionar o caixa em operação, quando houver.
- Quando não houver caixa em operação, nenhuma seleção deverá ser realizada automaticamente.
- O usuário poderá escolher outro caixa ao qual tenha acesso.
- A alteração do caixa selecionado deverá atualizar imediatamente a lista de notificações.

**Leitura e atualização:**

- Abrir ou visualizar uma notificação não deverá marcá-la automaticamente como lida.
- A leitura deverá ocorrer somente quando o usuário selecionar manualmente a opção "Marcar como lida".
- O sistema deverá registrar o usuário e a data e o horário da leitura.
- O usuário deverá poder marcar ou desmarcar uma notificação como importante.
- Quando uma operação pendente for resolvida, a notificação deverá ser atualizada automaticamente.
- O resultado final deverá substituir a informação de pendência, preservando o histórico e a auditoria.
- Se a notificação estiver resolvida, mas ainda não tiver sido lida, deverá permanecer não lida, mesmo com a atualização do conteúdo.

**Organização e retenção:**

- Notificações não lidas e notificações lidas deverão ser apresentadas em categorias separadas.
- Notificações lidas deverão ser removidas após cinco dias da data da leitura.
- Notificações não lidas deverão ser removidas após quinze dias da criação, desde que a operação correspondente esteja resolvida.
- Notificações relacionadas a operações ainda pendentes não poderão ser removidas por esse prazo.
- Não será necessário apresentar aviso prévio de exclusão.
- A classificação como importante não deverá alterar os prazos de retenção.
- A classificação de importância deverá ser compartilhada entre os usuários autorizados a acessar as notificações do mesmo caixa.

**Indicador de notificações:**

- O sistema deverá apresentar somente um indicador visual informando a existência de notificações não lidas no caixa selecionado.
- Não deverá apresentar contagem numérica de notificações.
- O indicador deverá desaparecer quando não houver notificações não lidas no caixa selecionado.
- A existência de notificações em outros caixas não deverá interferir no indicador do caixa atual.

As notificações deverão respeitar as permissões, a separação por caixa e os registros de auditoria.

## 4. Funcionalidades fora do MVP

As funcionalidades abaixo não fazem parte da primeira versão do PDV Web. Poderão ser avaliadas e incorporadas em versões futuras, conforme prioridades do produto, necessidades dos clientes e planejamento de desenvolvimento.

### 4.1. Gestão avançada de produtos e estoque

- Controle avançado por lotes, números de série e datas de validade.
- Gestão avançada de inventário e movimentações.
- Fluxos completos de transferência entre filiais, incluindo envio, recebimento e tratamento de divergências.
- Gestão completa de compras, fornecedores e processos de abastecimento.

### 4.2. Gestão financeira avançada

- Contas a pagar e receber completas.
- Conciliação bancária.
- Gestão financeira avançada.
- DRE e relatórios gerenciais financeiros avançados.

### 4.3. Gestão avançada de clientes

- Portal de autoatendimento do cliente.
- Programas de fidelidade, pontuação e benefícios.
- Campanhas de relacionamento e marketing automatizado.
- Recursos avançados de relacionamento e segmentação de clientes.

### 4.4. Inteligência artificial avançada

- Execução automática de operações pela IA.
- Alterações autônomas em cadastros, vendas, estoque ou configurações.
- Tomada de decisões operacionais sem aprovação humana.
- Análises preditivas e recursos avançados de inteligência artificial.

**Observação:** a IA consultiva básica, limitada às funcionalidades descritas na seção 3.13, faz parte do MVP.

### 4.5. Gestão avançada de assinaturas SaaS

- Cobrança recorrente automatizada.
- Renovação automática de assinaturas.
- Suspensão e reativação automáticas por inadimplência ou outras condições.
- Gestão comercial avançada de planos, módulos e regras automatizadas de contratação.

**Observação:** o controle interno básico de planos, limites, status e histórico das assinaturas faz parte do MVP, conforme seção 3.14.

### 4.6. Recursos fiscais e integrações avançadas

- Emissão efetiva de NF-e, NFC-e ou outros documentos fiscais.
- Transmissão e autorização de documentos junto à SEFAZ ou provedores fiscais.
- Consulta automatizada de documentos e eventos fiscais por integrações externas.
- Integrações fiscais, financeiras ou comerciais que não estejam expressamente previstas no MVP.

**Observação:** o cadastro fiscal dos produtos e a preparação fiscal interna das vendas fazem parte do MVP, conforme seção 3.12.

### 4.7. Outros recursos avançados

- Aplicativo ou portal externo para clientes.
- Recursos administrativos e gerenciais que não sejam essenciais à operação inicial do PDV.
- Funcionalidades adicionais identificadas após a validação e utilização da primeira versão.

### 4.8. Relatórios e análises avançadas

- Relatórios personalizados e configuráveis.
- Painéis gerenciais avançados.
- Análises comparativas entre períodos, filiais e empresas.
- Ferramentas avançadas de análise e apoio à tomada de decisão.
- Processamento avançado de grandes volumes de dados e recursos adicionais de exportação.

### 4.9. Central de Ajustes e Manutenções

- Ambiente interno exclusivo para a equipe responsável pelo desenvolvimento e manutenção do produto.
- Ferramentas administrativas para diagnóstico e acompanhamento.
- Recursos controlados para ajustes e manutenções.
- Registro e auditoria das intervenções realizadas.

**Observação:** os requisitos da Central de Ajustes e Manutenções deverão ser preservados durante o desenvolvimento do PDV, mas sua implementação ficará fora do MVP. Seu desenvolvimento será planejado para uma etapa posterior à conclusão do PDV principal e de seus módulos essenciais.

## 5. INTEGRAÇÕES FORA DO MVP

Inicialmente, não serão implementadas integrações externas que dependam de comunicação com plataformas, instituições financeiras, provedores ou serviços de terceiros, incluindo:

- Emissão fiscal por integração com a SEFAZ ou provedores fiscais.
- Integração com TEF para processamento de pagamentos.
- Integrações bancárias automáticas.
- Integrações com aplicativos e plataformas externas de fidelidade.
- Integrações complexas com plataformas comerciais ou serviços de terceiros.

As formas de pagamento previstas no MVP deverão permitir o registro e controle operacional dos pagamentos, sem exigir integração automática com adquirentes, bancos ou provedores externos.

### 5.1. Preparação para integrações futuras

A arquitetura deverá permitir que essas integrações sejam adicionadas futuramente, sem exigir alterações estruturais desnecessárias no sistema.

**Exceção fiscal prevista no MVP:** o cadastro e a manutenção dos dados fiscais dos produtos, assim como a preparação e o registro interno das informações fiscais das vendas, fazem parte do MVP, conforme definido na seção 3.12. Essas funcionalidades não representam uma integração fiscal externa nem contemplam a emissão efetiva de documentos fiscais.

## 6. REQUISITOS OBRIGATÓRIOS DE QUALIDADE

Mesmo sendo uma primeira versão, o MVP deverá respeitar os seguintes requisitos de qualidade.

### 6.1. Segurança e permissões

- Validar no backend todas as permissões e regras críticas de negócio.
- Garantir isolamento dos dados entre empresas e filiais, conforme as regras de acesso.
- Controlar sessões e acessos dos usuários.
- Registrar operações relevantes e autorizações em auditoria.
- Impedir que operações restritas sejam executadas sem a autorização necessária.
- Validar a autorização de usuários conforme a permissão específica exigida pela operação.
- Proteger dados pessoais e informações comerciais.
- Impedir acesso a dados e notificações de caixas não autorizados.

### 6.2. Integridade e consistência

- Garantir a integridade das vendas, pagamentos, devoluções e movimentações financeiras.
- Controlar a concorrência em operações que alterem estoque, saldos ou créditos.
- Impedir consumo duplicado de vales-troca, pagamentos ou processamento repetido de operações.
- Garantir rastreabilidade entre operações relacionadas.
- Preservar o histórico necessário para auditoria, inclusive em cancelamentos e estornos.
- Garantir consistência entre vendas, pagamentos, caixa e estoque.
- Garantir que operações parcialmente concluídas sejam tratadas de forma segura.
- Utilizar mecanismos transacionais e identificadores de operação quando necessários para impedir duplicidades.

### 6.3. Desempenho e eficiência

- Evitar consultas repetitivas, verificações desnecessárias e cálculos em tempo real quando atualizações por eventos ou alterações forem suficientes.
- Priorizar processamento eficiente e uso controlado de memória e recursos do servidor.
- Evitar que relatórios e processos pesados comprometam a operação do PDV.
- Utilizar processamento em segundo plano quando adequado.
- Considerar escalabilidade e crescimento da quantidade de empresas, filiais, usuários e operações.
- Evitar processamento desnecessário em operações que não sofreram alterações.
- Considerar o impacto de consultas simultâneas e grandes volumes de dados.
- Priorizar soluções que mantenham a operação principal do PDV responsiva.

### 6.4. Confiabilidade e recuperação

- Implementar tratamento adequado de erros e falhas.
- Garantir mecanismos de backup e recuperação de dados.
- Proteger operações contra duplicidade em situações de instabilidade ou repetição de solicitações.
- Garantir consistência em operações que envolvam múltiplas alterações relacionadas.
- Preservar os dados e o histórico em situações de falha.
- Permitir identificar operações concluídas, pendentes, canceladas ou que necessitem de regularização.
- Evitar que falhas em serviços complementares, como IA, comprometam a operação principal do PDV.

### 6.5. Operação offline e sincronização

- Garantir sincronização confiável após o restabelecimento da conexão.
- Identificar e tratar conflitos e divergências.
- Evitar duplicidade no envio ou processamento de operações.
- Respeitar os limites e as restrições configuradas para funcionamento offline.
- Impedir operações que dependam obrigatoriamente de validação online quando não houver conexão.
- Garantir que operações pendentes sejam identificadas e tratadas corretamente.
- Priorizar a integridade dos dados durante a sincronização.

### 6.6. Testes e validação

- Realizar testes funcionais das operações principais.
- Testar permissões, autorizações e auditoria.
- Testar concorrência e integridade dos dados.
- Testar cenários de falha, recuperação e sincronização.
- Realizar testes de desempenho nas operações críticas.
- Validar os fluxos de vendas, pagamentos, devoluções, estoque e fechamento de caixa.
- Validar a utilização de créditos e vales-troca, incluindo consumo parcial, cancelamento, concorrência e restauração de saldo.
- Validar as regras de operação offline e os respectivos limites.
- Validar a separação de dados entre empresas, filiais e caixas.
- Validar o comportamento do sistema quando serviços complementares estiverem indisponíveis.

## 7. CRITÉRIOS DE CONCLUSÃO DO MVP

O MVP será considerado funcional quando os principais fluxos operacionais e administrativos previstos para a primeira versão estiverem implementados, testados e homologados.

Para essa validação, deverá ser possível:

### 7.1. Estrutura e administração

1. Cadastrar empresas e filiais.
2. Criar usuários e configurar perfis e permissões.
3. Controlar acessos e registrar operações relevantes em auditoria.
4. Configurar os parâmetros operacionais de cada empresa, filial e caixa, conforme a abrangência de cada configuração.

### 7.2. Produtos e estoque

5. Cadastrar e manter produtos, preços, custos e informações fiscais.
6. Configurar condições comerciais específicas por filial.
7. Realizar entradas, saídas, ajustes, inventários e consultas de estoque.
8. Configurar estoques mínimo e ideal por produto e filial.
9. Controlar a disponibilidade dos produtos e bloquear vendas por estoque insuficiente conforme as regras de autorização configuradas.
10. Atualizar o estoque de forma consistente após vendas e devoluções.

### 7.3. Vendas e caixa

11. Abrir e operar caixas com identificação dos operadores.
12. Realizar vendas com diferentes formas de pagamento.
13. Aplicar descontos e promoções conforme as regras e permissões.
14. Emitir comprovantes não fiscais.
15. Recuperar vendas interrompidas sem gerar duplicidade.
16. Realizar sangrias, suprimentos e transferências entre caixas, identificando origem, destino, responsável e valor.
17. Realizar troca de operador, mantendo o histórico de responsabilidade.
18. Fechar caixas, incluindo fechamento às cegas.
19. Identificar divergências entre valores informados e esperados.
20. Consultar relatórios operacionais básicos.

### 7.4. Devoluções e créditos

21. Realizar devoluções totais e parciais conforme as regras configuradas.
22. Validar a venda original, as quantidades já devolvidas e os limites de prazo.
23. Aplicar as regras de autorização e justificativa quando necessárias.
24. Registrar corretamente os efeitos financeiros e as movimentações de estoque.
25. Permitir a definição individual do destino de estoque de cada produto devolvido.
26. Gerar créditos diretamente no cadastro do cliente ou por meio de vales-troca individuais vinculados à devolução.
27. Controlar saldo, validade, utilização e cancelamento dos créditos e vales-troca.
28. Validar as regras de utilização parcial, geração de novo vale e preservação da validade original.
29. Garantir rastreabilidade e proteção contra consumo duplicado ou concorrente dos vales-troca.

### 7.5. Recursos fiscais

30. Cadastrar e manter informações fiscais dos produtos.
31. Identificar produtos com pendências fiscais.
32. Atualizar a situação das pendências somente quando houver alteração nos dados fiscais correspondentes.
33. Permitir correções fiscais em lote exclusivamente por importação de arquivos Excel ou CSV.
34. Preparar e registrar internamente as informações fiscais das vendas, sem emissão efetiva de documentos fiscais.

### 7.6. IA consultiva

35. Disponibilizar a IA consultiva básica com respostas fundamentadas exclusivamente na documentação oficial aprovada.
36. Garantir que a IA não execute operações automaticamente nem altere diretamente os dados do sistema.
37. Garantir que ações sugeridas dependam de execução e aprovação humana, respeitando permissões e auditoria.
38. Garantir que não exista histórico individual de conversas associado aos usuários.
39. Disponibilizar indicadores agregados de avaliação exclusivamente à equipe interna autorizada.
40. Controlar o consumo do serviço de IA sem estabelecer limite fixo de perguntas por usuário.
41. Garantir que a indisponibilidade da IA não gere tentativas automáticas repetidas nem comprometa as demais funcionalidades do PDV.

### 7.7. Gestão SaaS

42. Consultar os planos, limites contratados e situação das assinaturas.
43. Registrar e consultar o histórico de alterações comerciais e de assinatura.
44. Permitir o controle manual de cobrança, renovação e suspensão ou reativação.
45. Garantir que a gestão de assinaturas respeite os limites do MVP, sem exigir automação de cobrança ou suspensão.

### 7.8. Operação offline e confiabilidade

46. Operar com as funcionalidades essenciais permitidas em modo offline.
47. Respeitar os limites e as restrições configurados para operação desconectada, considerando tempo sem conexão, quantidade de vendas e valor acumulado.
48. Apresentar avisos, autorizações e bloqueios graduais conforme as regras definidas.
49. Impedir a criação e a utilização de vales-troca durante a operação offline.
50. Impedir devoluções e operações que dependam obrigatoriamente de validação online durante a operação desconectada.
51. Sincronizar as operações após o retorno da conexão, sem duplicidades e com tratamento de conflitos.
52. Garantir integridade, segurança, permissões e auditoria nas operações críticas.

### 7.9. Notificações operacionais

53. Disponibilizar notificações vinculadas ao caixa em que as operações ocorreram.
54. Restringir a visualização aos gerentes e administradores autorizados para o respectivo caixa.
55. Permitir selecionar e alternar entre caixas autorizados, mantendo as notificações separadas.
56. Exigir ação manual para marcar uma notificação como lida.
57. Registrar usuário e data e horário da leitura.
58. Permitir marcar e desmarcar notificações como importantes.
59. Atualizar automaticamente notificações quando operações pendentes forem resolvidas, preservando o histórico.
60. Respeitar os prazos de retenção de cinco dias para notificações lidas e quinze dias para notificações não lidas cuja operação já esteja resolvida.
61. Proteger notificações de operações ainda pendentes contra remoção automática.
62. Apresentar somente indicador visual de existência de notificações não lidas no caixa selecionado, sem contagem numérica.

### 7.10. Respeito aos limites do MVP

63. Garantir que os recursos excluídos do MVP, incluindo emissão fiscal efetiva, integrações externas não previstas, gestão financeira avançada e Central de Ajustes e Manutenções, não sejam considerados requisitos obrigatórios para a homologação da primeira versão.
64. Garantir que a ausência de funcionalidades futuras não impeça o funcionamento dos recursos básicos previstos para o MVP.
65. Garantir que os requisitos aprovados e as regras funcionais correspondentes estejam implementados e validados antes da homologação das funcionalidades relacionadas.

A conclusão do MVP dependerá da implementação e validação dos requisitos previstos neste escopo e na documentação funcional correspondente, respeitando os limites estabelecidos para a primeira versão.

## 8. ESTRATÉGIA DE DESENVOLVIMENTO

O desenvolvimento do PDV Web será realizado por entregas incrementais, organizadas em fases para facilitar a implementação, os testes e a validação das funcionalidades.

A divisão abaixo representa uma sequência macro de desenvolvimento. A ordem e as dependências específicas poderão ser refinadas durante o planejamento técnico, sem descartar os requisitos aprovados para o MVP.

### Fase 1 — Fundação

- Estrutura da solução e organização dos projetos.
- Modelagem e implementação inicial do banco de dados.
- Autenticação e controle de sessões.
- Cadastro de empresas e filiais.
- Cadastro de usuários.
- Perfis, permissões e auditoria.
- Estrutura inicial de controle das assinaturas SaaS.
- Definição da separação dos dados entre empresas e filiais.
- Estrutura inicial para registro de operações e rastreabilidade.

### Fase 2 — Produtos e operação comercial

- Cadastro e manutenção de produtos.
- Categorias, departamentos e códigos de barras.
- Custos, preços, markup e configurações comerciais por filial.
- Cadastro e manutenção das informações fiscais dos produtos.
- Estoque básico e movimentações.
- Realização de inventários e conferência dos saldos de estoque.
- Configuração de estoque mínimo e ideal por produto e filial.
- Controle de disponibilidade e bloqueio de vendas por estoque insuficiente, conforme regras de autorização.
- Frente de caixa.
- Pesquisa e leitura de produtos.
- Vendas e formas de pagamento.
- Comprovantes não fiscais.
- Recuperação de vendas interrompidas.

### Fase 3 — Gestão de caixa e relatórios

- Abertura e fechamento de caixa.
- Sangrias e suprimentos.
- Transferências entre caixas.
- Troca de operador.
- Fechamento às cegas.
- Registro e consulta de divergências.
- Histórico de movimentações.
- Relatórios operacionais básicos.
- Processamento eficiente de relatórios e tarefas pesadas.

### Fase 4 — Recursos comerciais e devoluções

- Cadastro e consulta de clientes.
- Histórico de compras.
- Promoções e descontos.
- Regras de autorização para descontos.
- Devoluções totais e parciais.
- Regras de prazo e autorização para devoluções.
- Tratamento financeiro e movimentação de estoque das devoluções.
- Créditos em cadastro de clientes e vales-troca.
- Controle de saldo, validade, utilização e cancelamento dos vales-troca.
- Regras de utilização parcial, reversão e restauração de saldo.
- Proteção contra consumo duplicado e concorrente dos vales.

### Fase 5 — Operação híbrida

- Armazenamento local dos dados necessários.
- Funcionamento offline dentro dos limites configurados.
- Registro de vendas e pagamentos permitidos sem conexão.
- Controle de operações pendentes.
- Sincronização após o retorno da conexão.
- Tratamento de conflitos e divergências.
- Proteção contra duplicidade de operações.
- Priorização da integridade dos dados durante a sincronização.
- Aplicação das restrições para operações exclusivamente online.

### Fase 6 — Recursos complementares e validação

- Preparação e registro interno das informações fiscais das vendas.
- Implementação da IA consultiva básica.
- Controle inteligente do consumo da IA.
- Conclusão dos controles internos de planos, limites e assinaturas SaaS.
- Implementação das notificações operacionais.
- Configuração de visibilidade, leitura, retenção e importância das notificações.
- Testes funcionais dos módulos.
- Testes de segurança, permissões e auditoria.
- Testes de concorrência e integridade.
- Testes de desempenho e consumo de recursos.
- Testes de operação offline e sincronização.
- Testes de devoluções, créditos e vales-troca.
- Correções e homologação do MVP.

### Etapa final — Central de Ajustes e Manutenções

A Central de Ajustes e Manutenções será desenvolvida na última etapa, somente após a conclusão do PDV principal.

Os requisitos já definidos para essa Central deverão ser preservados durante o desenvolvimento das demais fases, sem antecipar sua implementação nem comprometer a entrega das funcionalidades operacionais prioritárias.

A conclusão de cada fase deverá considerar a implementação, os testes e a validação das funcionalidades correspondentes.

## 9. Controle de alterações de escopo

Novas funcionalidades poderão ser propostas durante o desenvolvimento.

Toda alteração deverá ser avaliada considerando:

- Impacto técnico.
- Complexidade.
- Segurança.
- Prazo.
- Dependências.
- Benefício para o produto.
- Impacto sobre funcionalidades já aprovadas.
- Efeitos sobre desempenho, integridade e manutenção do sistema.

Nenhuma nova funcionalidade deverá comprometer a estabilidade ou atrasar injustificadamente a entrega do MVP.

### 9.1. Governança das alterações

- Toda alteração deverá ser registrada e avaliada antes de sua incorporação ao escopo.
- Decisões e requisitos já aprovados não poderão ser removidos ou modificados sem revisão explícita.
- Alterações deverão ser refletidas nos documentos de visão, escopo e requisitos funcionais correspondentes.
- Divergências entre documentos deverão ser identificadas e resolvidas antes de iniciar o desenvolvimento da funcionalidade afetada.
- A inclusão de novas funcionalidades deverá considerar seu impacto sobre o MVP, evitando ampliações não planejadas.
- Requisitos definidos para versões futuras deverão permanecer documentados, mesmo quando sua implementação não fizer parte do MVP.
- A aprovação de uma alteração deverá considerar a consistência entre os documentos e a preservação das decisões anteriores.

