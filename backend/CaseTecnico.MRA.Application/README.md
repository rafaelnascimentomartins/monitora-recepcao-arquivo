==============================================
           📝 PROJECT NOTES
==============================================

Project Name: CaseTecnico.MRA.Application
Project Type: Class Library

----------------------------------------------
1  PROJECT OVERVIEW
----------------------------------------------
Breve descrição do projeto:
- Objetivo: Camada responsável por orquestrar o comportamento do sistema.
- Dep. project: Application
- Tecnologias principais: .NET 9, AutoMapper

----------------------------------------------
2  FOLDER STRUCTURE
----------------------------------------------

/BackEnd
 ├── CaseTecnico.MRA.Application
 │    ├── Common/            --> Uso global para objetos pré-definidos abstratos e extensões.
      ├── Interfaces/        --> Interfaces de Parsers ou usos dentro do próprio projeto.
      ├── Mappings/          --> Mapeamento de conversão dos modelos DTOxEntitidade.
      ├── Parsers/           --> Componentes para interpretas, validar ou extrair dados.
      ├── Settings/          --> Singleton variável AppSettings (uso de orquestração de casos no handler).
      ├── UseCases/          --> Organizador de operações separadas, fluxo de operação.