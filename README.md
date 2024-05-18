# REBOOST
API for creative experience application

# Como Executar um Projeto .NET com SQL Server

Este guia fornece instruções passo a passo sobre como configurar e executar um projeto .NET com SQL Server. Certifique-se de ter o .NET SDK e o SQL Server instalados em sua máquina antes de prosseguir.

## Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Configuração do Banco de Dados

1. Abra o SQL Server Management Studio (SSMS).
2. Conecte-se ao seu servidor SQL local ou remoto para checar se está tudo OK.

## Configuração do Projeto
git clone https://github.com/marcelowf/REBOOST
cd REBOOST

## Rodar o Projeto
dotnet restore
dotnet ef database update
dotnet run
