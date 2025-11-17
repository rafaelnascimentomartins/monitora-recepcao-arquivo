# Sobre

Especificação Técnica:

Angular20

Objetivo:
Case Técnico.

## O que eu preciso para iniciar projeto localmente?

Caso deseja clonar o projeto e visualizar o código:
-> Visual Studio Code
-> Node 20.19.5, pode se utilizar a ferramenta `nvm.exe` e baixar via Web, ela permite gerenciar as 
versões do Node em sua máquina, podendo ter mais de 1 versão e a qualquer momento
definir a versão que deseja via CMD usando os comandos:
`nvm ls` Para análise de versões instaladas
`nvm use [NUMERO DA VERSÃO]` Para uso da versão
`nvm install [NÚMERO DA VERSÃO]` Para instalação
A versão do Node pode ser 18x ate 20x compatível com essa versão de Angular. 

OBS: Não necessita ter o angular cli instalado, no terminal executar: `npm start` internamente na configuração do start 
esta sendo executado `npx ng serve` ou seja ele vai olhar para o versionamento do devDependences do projeto que contem o angular-cli 
da versão necessária.
 
Para execução de Docker
-> Docker Desktop

## Como baixar/clonar o projeto?

Pode ser realizado o download do projeto de forma manual ou clonando com git `(Recomendado)`.

1) Download manual 
-> Clicar no botão "Code" e "Download Zip".

2) Clonar via commando git

-> Baixar o git se necessário em: https://git-scm.com/install/windows
-> Abrir a pasta necessária no Bash ou CMD 
-> Executar o comando: `git clone https://github.com/rafaelnascimentomartins/monitora-recepcao-arquivo-frontend.git`

## Sobre as branches?

O projeto possui um histórico de uso das branches, mas foram mergiadas para 
branch principal `master`, esse histórico pode ser analisado em: 
GitHub -> Code<> -> Commits.

## Clonei o projeto e quero executar localmente sem Docker

Neste caso será necessário ter o Node instalado.

1) Abrir o projeto via plataforma Visual Studio Code
2) Abrir o arquivo environment.ts dentro da pasta `src/app/environment`
	OBS: A url atual já esta apontando certamente para porta da Api.
3) Abrir o terminal do Visual Studio Code e executar o comando: `npm i` e aguardar ate a finalização.
    OBS: Caso ocorra erro `( improvável )` de vulnerabilidade executar `npm i --force`.
4)  No terminal executar o comando: `npm start`.


## Clonei e quero executar via Docker compose

Neste caso não precisa do Node instalado na máquina.

Observe a documentação do README.md em: https://github.com/rafaelnascimentomartins/monitora-recepcao-arquivo.