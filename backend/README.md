# monitora-recepcao-arquivo-backend
Projeto para realização de upload de arquivos padronizados para processamento. Especificação Técnica: .NET9 com conexão SqlServer. Objetivo: Case Técnico.

## O que eu preciso para iniciar projeto localmente?

1) Visual Studio 2022
2) Docker Desktop

## Como baixar/clonar o projeto?

Pode ser realizado o download do projeto de forma manual ou clonando com git `(Recomendado)`.

1) Download manual 
-> Clicar no botão "Code" e "Download Zip".

2) Clonar via commando git

-> Baixar o git se necessário em: https://git-scm.com/install/windows
-> Abrir a pasta necessária no Bash ou CMD 
-> Executar o comando: `git clone https://github.com/rafaelnascimentomartins/monitora-recepcao-arquivo-backend.git`

## Sobre as branches?

O projeto possui um histórico de uso das branches, não foram deletadas, mas foram mergiadas para branch principal `master`.

Utilizar a branch `master` que já possui toda a alteração final.

## Baixei o projeto e estou na branch master, o que preciso fazer para incia-lo?

Etapa 1 Docker de início
1) Abra o terminal CMD ou Powersheel 
2) Execute o comando: `docker ps`, será listado todos os containers e imagens ativas em sua máquina

OBS: Todas as informações de análise de container, parar, iniciar e etc... pode ser feita pelo Docker Desktop também na aba containers.

Etapa 2 Iniciar/configurar o arquivo DockerFile do projeto para criar a estrutura geral Sql, Api e Angular.

-- A FAZER

Etapa 3 (Opcional caso não Etapa 2)

Criar container com imagem do SqlServer 2022

1) Execute o comando `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MRASenhaCT#181125" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest`
	Explicações:
		`docker run` -> Inicia um novo container baseado em uma imagem
		`-e "ACCEPT_EULA=Y"` -> seta variáveis de ambiente dentro do container, o SQL exige licença EULA.
		`-e "SA_PASSWORD=SENHA_DEFINIDA" -> Usuário SA, ADM do padrão SQL Server
		`-p 1433:1433 ->` Portas mapeadas host e container.
		`--name sqlserver` -> Nome do container (definição)
		`-d` -> modo detached , sem isso trava seu terminal.
		`mcr.microsoft.com/mssql/server:2022-latest` -> imagem do sql 2022


Parar execução do container

1) Executar o comando: `docker pause sqlserver`


