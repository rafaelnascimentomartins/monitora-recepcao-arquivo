# Sobre

Projeto de automação de upload de arquivos externos padronizados para processamento interno e consulta via portal Web.

Especificação Técnica:

.NET9 com conexão SqlServer

Objetivo:
Case Técnico.

## O que eu preciso para iniciar projeto localmente?

1) Visual Studio 2022
2) Docker Desktop ( Opcional )

## Como baixar/clonar o projeto?

Pode ser realizado o download do projeto de forma manual ou clonando com git `(Recomendado)`.

1) Download manual 
-> Clicar no botão "Code" e "Download Zip".

2) Clonar via commando git

-> Baixar o git se necessário em: https://git-scm.com/install/windows
-> Abrir a pasta necessária no Bash ou CMD 
-> Executar o comando: `git clone https://github.com/rafaelnascimentomartins/monitora-recepcao-arquivo.git`

## Sobre as branches?

O projeto possui um histórico de uso das branches, não foram deletadas, mas foram mergiadas para branch principal `master`.

Utilizar a branch `main` que já possui toda a alteração final.

## Clonei o projeto e quero executar o projeto localmente (debug) sem Docker?

1) Abrir o projeto via plataforma Visual Studio 2022
2) Abrir o arquivo appsettings.json dentro do projeto `Api`
	OBS: O projeto contem a estrutura de appsettings.json padrão como ( Localhost, Development, Staging e Production )
	Foi realizada uma configuração no arquivo launchSettings.json definindo a execução do projeto apontando para a configuração
	que necessita.
3) Atualizar a conexão de banco `MRAConnection` para sua conexão de banco local, não precisa de usuário SA.
	OBS1: Teria de ter o SqlServer localmente instalado ou DbBeaver ( mas para usá-lo localmente precisa realizar algumas configurações internas )
	OBS2: Apenas é necessário o banco ativo, não precisa criar nada de tabelas e etc... o Entity Framework faz esse papel.
4) Clicar no botão `play` do Visual Studio 2022, selecione o arquivo appseetings que deseja executar: (localhost, development ou staging)
	OBS: localhost executa o appsettings.json principal. 
	
##  Clonei o projeto e quero executar o projeto localmente (debug), mas o banco de dados em container

1) Execute o comando `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MRASenhaCT#181125" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest`
	Explicações:
		`docker run` -> Inicia um novo container baseado em uma imagem
		`-e "ACCEPT_EULA=Y"` -> seta variáveis de ambiente dentro do container, o SQL exige licença EULA.
		`-e "SA_PASSWORD=SENHA_DEFINIDA" -> Usuário SA, ADM do padrão SQL Server
		`-p 1433:1433 ->` Portas mapeadas host e container.
		`--name sqlserver` -> Nome do container (definição)
		`-d` -> modo detached , sem isso trava seu terminal.
		`mcr.microsoft.com/mssql/server:2022-latest` -> imagem do sql 2022
	OBS: Caso coloque uma senha diferente, adicione no appsettings.

2) Execute o comando: `docker ps` para verificar se o container com a imagem foi gerada ou abra o Docker Desktop.
3) Para finalizar verificar a explicação: `Clonei o projeto e estou na branch master, como executo o projeto localmentem sem Docker?`

## Clonei e quero executar via Docker compose

Neste caso não precisa do Node instalado na máquina.

Observe a documentação do README.md em: https://github.com/rafaelnascimentomartins/monitora-recepcao-arquivo.


## OBSERVAÇÕES GERAIS

O projeto possui um documento README.md explicando sobre o projeto em si, este README.md
é apenas para mostrar o processo como todo para execução do projeto