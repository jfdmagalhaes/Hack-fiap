# Hack-fiap

## API REST para Upload de Vídeo 
Esta API REST permite que você faça upload de vídeos, armazene-os no Azure Blob Storage e no SQL Server (path), e divida os vídeos em frames para imagens. Além disso, utiliza RabbitMQ e MassTransit para comunicação assíncrona.

## Funcionalidades
POST /api/Midia/Upload: Endpoint para upload de vídeo. Aceita um arquivo de vídeo como entrada, armazena-o no Azure Blob Storage e no SQL Server (path), e inicia o processo de divisão do vídeo em frames.

GET /api/Midia/GetAll: Endpoint para obter a lista de todos os uploads que foram feitos o upload anteriormente. (Apenas paths)

## Tecnologias Utilizadas
ASP.NET Core: Framework para o desenvolvimento da API REST.

Azure Blob Storage: Serviço de armazenamento de objetos na nuvem da Microsoft, utilizado para armazenar os vídeos.

SQL Server: Sistema de gerenciamento de banco de dados relacional, utilizado para armazenar os caminhos dos vídeos.

RabbitMQ: Sistema de mensageria de código aberto, utilizado para comunicação assíncrona entre os componentes da aplicação.

MassTransit: Biblioteca para integração do RabbitMQ com aplicativos .NET.

## Usage
Executar o `` docker-compose up `` que está na raiz do diretório.
Esse docker irá executar os scripts de iniciação do banco de dados disponível em /resources/scripts/schemas.sql

## Desenho da arquitetura escolhida do projeto
![image](https://github.com/jfdmagalhaes/hackaton-fiap/assets/145411274/4d49f499-baf1-4a0c-8c58-36d4cc276332)

## Azure
Azure Conta de Armazenamento: hackatonfiapgrupo17
Azure Grupo de Recurso: GR_Hackaton
Link blob storage: https://hackatonfiapgrupo17.blob.core.windows.net/apiimages

