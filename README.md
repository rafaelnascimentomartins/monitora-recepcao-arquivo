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


Observe a documentação do README.md em: https://github.com/rafaelnascimentomartins/monitora-recepcao-arquivo.