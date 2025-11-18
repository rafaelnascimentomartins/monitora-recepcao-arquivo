==============================================
           📝 PROJECT NOTES
==============================================

Project Name: CaseTecnico.MRA.Infrastructure
Project Type: Class Library

----------------------------------------------
1  PROJECT OVERVIEW
----------------------------------------------
Breve descrição do projeto:
- Objetivo: Camada de implementações concretas de recursos externos que o sistema utiliza, como: 
        -> EF Core, Dapper, Mongo... 
        -> Repositórios
        -> Serviços de E-mail, Fila, Storage, Cache e etc...
        -> Intergrações externas (APIs, FTP, WebServices)
        -> Logging da Infra
        -> Context de banco
        -> Mapeamento de ORM
- Dep. project: Domain 
- Tecnologias principais: .NET 9, EF Core ( To Sql )

----------------------------------------------
2  FOLDER STRUCTURE
----------------------------------------------

/BackEnd
 ├── CaseTecnico.MRA.Infrastructure
 │    ├── Configurations/      --> Mapeamento da ORM
      ├── Context/             --> Contexto de configuração do EF Core, DbSets e inicialização.
      ├── Migrations/          --> Execuções da estrutura para o banco de dados referenciadas em Snapshot e histórico de execuções no banco.
      ├── Repositories/        --> Repositório para conexão com banco de dados de acordo com a necessidade via Linq ( Orientado a Objeto ).
      ├── Services/            --> Serviços concretos em gerais
