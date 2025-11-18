==============================================
           📝 PROJECT NOTES
==============================================

Project Name: CaseTecnico.MRA.CrossCutting
Project Type: Class Library

----------------------------------------------
1  PROJECT OVERVIEW
----------------------------------------------
Breve descrição do projeto:
- Objetivo: Camada criada para abstrair funcionalidades comuns ao projeto inteiro, de modo 
a não poluir Applicaiton, Domain ou Infra com responsábilidades secondárias. 
Ele possui serviços utilitários e infraestruturais, mas que não pertencem diretamente a infra, porque 
são genéricos, reutilizáveis e independentes de tecnologia.
- Dep. project: Application, Infrastructure 
- Tecnologias principais: .NET 9

----------------------------------------------
2  FOLDER STRUCTURE
----------------------------------------------

/BackEnd
 ├── CaseTecnico.MRA.CrossCutting
 │   ├── Interfaces/          --> Interfaces que representam serviços utilitários genéricos, pois são tranversais e podem ser usadas por qualquer camada (App, Infra, API) sem depender de tecnologia específica
     ├── Utils/               --> Configurações de utilidades pelo projeto com conversão de campos e etc.. itens simples e reutilizáveis.
