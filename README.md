# Sobre

Projeto de automação de upload de arquivos externos padronizados para 
processamento interno e consulta via portal Web.

O arquivo é enviado via endpoint Api Rest com os dados das empresas
para processamento em background. Após esses processos o operador poderá
análisar o conteúdo dos arquivos processados via aplicação Web.

Especificação Técnica:

.NET9 com conexão SqlServer
Angular20

Objetivo:
Case Técnico.

## Clonei e quero executar via Docker compose

Execute o download do Docker em sua máquina local e siga os passos abaixo:

1) Abrir o CMD ou Powersheel e apontar para raiz do projeto
2) Executar o comando:  `docker compose up --build -d`
	OBS: Sobe todos os serviços definidos no docker-compose e força
	a recrição das imagens usando seus Dockerfiles com `--build`.
3) Para parar o Docker e seus containers: `docker compose down`
4) Para parar e remover redes/volumes base: `docker compose down --volumes`
5) Para observar os logs: `docker compose logs -f`
6) Caso tenha executado o item 3 (down) e queira realizar a subida novamente
executar: `docker compose up -d` 
OBS: Se executar o down item 3, para executar novamente


Observe a documentação do README.md em: https://github.com/rafaelnascimentomartins/monitora-recepcao-arquivo.

## Anotações sobre processos CLOUD

Minhas observações do que poderia ficar na nuvem.

Azure Key Vault

-> Para armazenamento das chaves connections strings e criptografias.

Azure Blob

-> Para armazenamento dos arquivos em backup, de acordo com a plataforma seria
interessante a camada de armazenamento: Cool/Archive, acredito que não necessite acessar 
o arquivo sempre, até porque o conteúdo esta nas tabelas no campo: EstruturaImportacao