===============================================
           📝 ARQUITETURA GERAL
==============================================

[API]
 ├─ Referências: Application, IoC
 ├─ Responsável por:
 │    - Receber requisições HTTP
 │    - Orquestrar Application Handlers
 │    - Configurar o DI via IoC
 │
 [Application]
 ├─ Referências: Domain, CrossCutting
 ├─ Responsável por:
 │    - Handlers, Commands, Queries
 │    - Regras de aplicação (orquestração)
 │    - Chamar interfaces de serviços definidos em CrossCutting
 │    - Transformar Stream / DTOs e enviar para Repositórios
 │
 [Domain]
 ├─ Referências: N/A
 ├─ Responsável por:
 │    - Entidades, Value Objects
 │    - Regras puras de negócio
 │    - Definir interfaces de repositório (Domain.Interfaces.Repositories)
 │
 [CrossCutting]
 ├─ Referências: N/A
 ├─ Responsável por:
 │    - Interfaces e utilitários compartilhados (ex.: IFileEncryptionService)
 │    - Funcionalidades transversais como logging, hashing, criptografia
 │
 [Infrastructure]
 ├─ Referências: Domain, CrossCutting
 ├─ Responsável por:
 │    - Implementações concretas de Repositórios
 │    - Implementações de serviços (ex.: FileEncryptionService)
 │    - Acesso a banco de dados, arquivos e serviços externos
 │
 [IoC]
 ├─ Referências: Application, CrossCutting, Infrastructure
 ├─ Responsável por:
 │    - Registrar serviços no DI container
 │    - Conectar Application com Infra e CrossCutting
 │    - Fornecer implementações concretas para interfaces



==============================================
           📝 RESUMO ACESSO
==============================================

[API]  --->  [Application] ---> [Domain]
   |              |
   |              +--> [CrossCutting] (interfaces/utilitários)
   |
   +--> [IoC] ---> conecta [Application] com [Infrastructure] e [CrossCutting]

[Infrastructure] ---> [Domain], [CrossCutting]