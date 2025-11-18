# ============================
# Stage 1: Build (compilação da aplicação)
# ============================
# Usa a imagem oficial do .NET SDK 9.0 (compilador / ferramentas)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Define a pasta de trabalho dentro da imagem (como o "cd" do Linux)
WORKDIR /app

# ============================
# Copia TODOS os projetos da solution
# ============================
# Ao invés de copiar apenas *.csproj da API, copie todos os projetos da solution
COPY CaseTecnico.MRA.Api/*.csproj ./CaseTecnico.MRA.Api/
COPY CaseTecnico.MRA.Application/*.csproj ./CaseTecnico.MRA.Application/
COPY CaseTecnico.MRA.IoC/*.csproj ./CaseTecnico.MRA.IoC/
COPY CaseTecnico.MRA.Domain/*.csproj ./CaseTecnico.MRA.Domain/
COPY CaseTecnico.MRA.Infrastructure/*.csproj ./CaseTecnico.MRA.Infrastructure/
COPY CaseTecnico.MRA.CrossCutting/*.csproj ./CaseTecnico.MRA.CrossCutting/

# Restaura todos os pacotes NuGet
RUN dotnet restore ./CaseTecnico.MRA.Api/CaseTecnico.MRA.Api.csproj

# Copia TODO o restante do código-fonte
# (somente aqui, pois isso invalida o cache sempre que o código muda)
COPY . .
#COPY CaseTecnico.MRA.Api/ ./CaseTecnico.MRA.Api/
#COPY CaseTecnico.MRA.Application/ ./CaseTecnico.MRA.Application/
#COPY CaseTecnico.MRA.IoC/ ./CaseTecnico.MRA.IoC/
#COPY CaseTecnico.MRA.Domain/ ./CaseTecnico.MRA.Domain/
#COPY CaseTecnico.MRA.Infrastructure/ ./CaseTecnico.MRA.Infrastructure/
#COPY CaseTecnico.MRA.CrossCutting/ ./CaseTecnico.MRA.CrossCutting/

# Compila o projeto em modo Release e joga o resultado em /app/publish
#RUN dotnet publish ./CaseTecnico.MRA.Api/CaseTecnico.MRA.Api.csproj -c Release -o /app/publish
#RUN dotnet publish -c Release -o /app/publish || true

RUN dotnet publish ./CaseTecnico.MRA.Api/CaseTecnico.MRA.Api.csproj -c Release -o /app/publish

# ============================
# Stage 2: Runtime (execução)
# ============================
# Usa a imagem oficial do .NET ASP.NET Runtime 9.0 (APENAS para rodar)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

# Define o diretório de trabalho no container final
WORKDIR /app

# Copia os arquivos compilados do stage anterior (build)
COPY --from=build /app/publish .

# Informa ao Docker qual porta a API escuta DENTRO do container
# Isso NÃO abre porta na máquina, apenas documenta e permite mapeamento
EXPOSE 5163

# Configura o ASP.NET Core para rodar na porta 5163, aceitando qualquer host (0.0.0.0)
ENV ASPNETCORE_URLS=http://+:5163
ENV ASPNETCORE_ENVIRONMENT=Development

# Comando que inicia sua aplicação dentro do container
# Aqui deve ser exatamente o nome da DLL gerada no publish
ENTRYPOINT ["dotnet", "CaseTecnico.MRA.Api.dll"]