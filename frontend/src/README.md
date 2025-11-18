==============================================
           📝 PROJECT NOTES
==============================================

O projeto segue uma arquitetura modular e escalável baseada em três camadas principais:

----------------------------------------------
1  Core
----------------------------------------------

Contém lógica global e serviços singleton.

Armazena models, enums, interfaces, utilitários e serviços que são utilizados em toda a aplicação.

Não deve conter componentes visuais, layouts ou diretivas de UI, garantindo baixo acoplamento e alta reutilização.

----------------------------------------------
2  Shared
----------------------------------------------

Contém componentes, pipes, diretivas e layouts reutilizáveis em múltiplos módulos.

Modelos de UI que são compartilhados entre componentes globais também ficam aqui.

Suporta a criação de layouts globais, como headers, sidebars e footers, organizados em shared/layout.

----------------------------------------------
3  Modules/Features
----------------------------------------------

Cada módulo representa uma feature específica da aplicação, com suas páginas, serviços, DTOs e utilitários exclusivos.

Evita dependência direta entre módulos, promovendo isolamento e independência de cada feature.

Padrões e Boas Práticas

BaseService no Core: servindo como classe base para serviços das features, garantindo consistência e reaproveitamento de código.

Models e Enums no Core: apenas aqueles que representam domínio global ou contratos usados em várias features.

Models de UI e layouts: colocados no Shared, não no Core, evitando acoplamento com a camada de domínio.

Utils globais: colocados no Core quando são transversais e reutilizáveis, utilitários específicos de feature ficam dentro do módulo correspondente.

ESLint

O projeto utiliza ESLint com regras específicas para manter a arquitetura limpa e evitar más práticas:

Bloqueio de criação de componentes no Core: impede que arquivos com @Component, @Directive ou @Pipe sejam adicionados à pasta core, garantindo que o Core permaneça apenas com lógica global e serviços singleton.

Restrição de importação: impede que o Core importe componentes de outros módulos ou do Shared, evitando dependência de UI na camada de domínio.

Análise estática: todas as violações de regras são detectadas antes da execução, tanto no editor (via extensão ESLint) quanto na linha de comando (npm run lint).

Essas regras ajudam a manter a consistência arquitetural e a qualidade do código, facilitando a escalabilidade do projeto.