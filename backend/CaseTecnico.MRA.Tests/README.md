==============================================
           📝 PROJECT NOTES
==============================================

Project Name: CaseTecnico.MRA.Tests
Project Type: Test Unitx

----------------------------------------------
1  PROJECT OVERVIEW
----------------------------------------------
Breve descrição do projeto:
- Objetivo: Camada para testes de integrações e unitário com mock.
Unit Test (mock): Usa Moq, simula DbSet, rápido
Integration Test (realista): Usa EF Core InMemory, testa LINQ, paginação, ordenação

OBS: o AppContext é uma classe sealed e não possui métodos virtuais de DbSet,
neste caso o Mock não consegue simular, pois necessita que seja virtual ou interface!
Foi criada uma interface IAppContext para utilizar apenas o Mock (lembrando que mesmo assim se limita
não da pra simular métodos que utilizam AsNotracking, Include)
Então o Mock nesse projeto ficou apenas para ações como: Delete, Insert , Update ....
- Dep. project: Domain 
- Tecnologias principais: xunit , Moq e EF Core InMemory

----------------------------------------------
2  FOLDER STRUCTURE
----------------------------------------------

/BackEnd
 ├── CaseTecnico.MRA.Tests
 │    ├── IntegrationTests/      --> Dentro possui a pasta do projeto referenciado e Usa EF Core InMemory, testa LINQ, paginação, ordenação
      ├── UnitTests/             --> Dentro possui a pasta do projeto referenciado e Usa Moq, simula DbSet, rápido
