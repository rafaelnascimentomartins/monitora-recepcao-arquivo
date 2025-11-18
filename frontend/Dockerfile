# =========================
# 1) Stage de Build (compilação do Angular)
# =========================

# Usa a imagem oficial do Node.js versão 20
# Necessário para instalar dependências e rodar o 'ng build'
FROM node:20-alpine AS build

# Define o diretório de trabalho dentro do container
WORKDIR /app

# Copia somente package.json e package-lock.json (se existir)
# Isso permite aproveitar o cache de dependências do Docker
COPY package*.json ./

# Instala todas as dependências do Angular
RUN npm install

# Copia todo o restante do código fonte do frontend
COPY . .

# Executa o build de produção do Angular
# Após isso, a pasta /app/dist conterá o site estático pronto
RUN npm run build


# =========================
# 2) Stage de Runtime (NGINX)
# =========================

# Usa o NGINX como servidor web para entregar os arquivos estáticos
FROM nginx:alpine AS final

# Remove o conteúdo padrão do NGINX
# (Ele vem com uma página index.html padrão, que não queremos)
RUN rm -rf /usr/share/nginx/html/*

# Copia o conteúdo buildado no Stage 1 para a pasta que o NGINX serve
# O Angular gera dentro de /dist/NOME_DO_PROJETO, mas ao usar
# COPY /app/dist /usr/share/nginx/html ele copia tudo direto
COPY --from=build /app/dist/frontend/browser /usr/share/nginx/html

# Copia a configuração do NGINX para dentro do container
COPY default.conf /etc/nginx/conf.d/default.conf

# Expõe a porta padrão do nginx para acesso externo
EXPOSE 80

# Comando para manter o NGINX rodando no foreground
# Sem isso, o container pararia imediatamente após subir
ENTRYPOINT ["nginx", "-g", "daemon off;"]
