# REBOOST
API for creative experience application

## Como Executar um Projeto .NET com SQL Server

Este guia fornece instruções passo a passo sobre como configurar e executar um projeto .NET com SQL Server. Certifique-se de ter o .NET SDK e o SQL Server instalados em sua máquina antes de prosseguir.

### Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Configuração do Banco de Dados

1. Abra o SQL Server Management Studio (SSMS).
2. Conecte-se ao seu servidor SQL local ou remoto para checar se está tudo OK.

### Configuração do Projeto
git clone https://github.com/marcelowf/REBOOST
cd REBOOST

### Rodar o Projeto
dotnet restore
dotnet ef database update
dotnet run

# Dados tecnicos de Funcionalidades

## Users:

### Atributos:
Id: Identificador único do usuário.
IsAdmin: Indica se o usuário é um administrador.
IsActive: Indica se o usuário está ativo ou inativo.
Billing: Detalhes de cobrança do usuário.
Nome: Nome do usuário.
Email: Endereço de email do usuário.
Senha: Senha do usuário.
LastLogin: Último acesso do usuário.
Criado em: Data e hora de criação do usuário.

### Endpoints Disponíveis:
POST /users: Cria um novo usuário.
GET /users/{id}: Retorna informações de um usuário específico com base no ID.
GET /users: Retorna uma lista de todos os usuários cadastrados.
PUT /users/{id}: Atualiza as informações de um usuário existente.
DELETE /users/{id}: Exclui um usuário específico do sistema.
(SOFT-DELETE): Desativa um usuário específico, mantendo seus dados no sistema.

## Battery:

### Atributos:
Id: Identificador único da bateria.
IsActive: Indica se a bateria está ativo ou inativo.
Código Externo: Código externo de identificação da bateria.
Modelo: Modelo da bateria.
Marca: Marca da bateria.
Capacidade: Capacidade da bateria.
Preço por Hora: Preço de aluguel por hora da bateria.
Preço Total: Preço total da bateria.

### Endpoints Disponíveis:
POST /batteries: Cria uma nova bateria.
GET /batteries/{id}: Retorna informações de uma bateria específica com base no ID.
GET /batteries: Retorna uma lista de todas as baterias cadastradas.
PUT /batteries/{id}: Atualiza as informações de uma bateria existente.
DELETE /batteries/{id}: Exclui uma bateria específica do sistema.
(SOFT-DELETE): Desativa uma bateria específica, mantendo seus dados no sistema.

## Cabinet:

### Atributos:
Id: Identificador único do gabinete.
Ativo: Indica se o gabinete está ativo ou inativo.
Código Externo: Código externo de identificação do gabinete.
CEP do Endereço: CEP do endereço do gabinete.
Rua do Endereço: Rua do endereço do gabinete.
Número do Endereço: Número do endereço do gabinete.
Bairro do Endereço: Bairro do endereço do gabinete.
Latitude do Endereço: Latitude do endereço do gabinete.
Longitude do Endereço: Longitude do endereço do gabinete.
Número de Gavetas: Número de gavetas do gabinete.

### Endpoints Disponíveis:
POST /cabinets: Cria um novo gabinete.
GET /cabinets/{id}: Retorna informações de um gabinete específico com base no ID.
GET /cabinets: Retorna uma lista de todos os gabinetes cadastrados.
PUT /cabinets/{id}: Atualiza as informações de um gabinete existente.
DELETE /cabinets/{id}: Exclui um gabinete específico do sistema.
(SOFT-DELETE): Desativa um gabinete específico, mantendo seus dados no sistema.

## Rent:

### Atributos:
Id: Identificador único do aluguel.
Ativo: Indica se o aluguel está ativo ou inativo.
Data de Início: Data e hora de início do aluguel.
Data de Término: Data e hora de término do aluguel.
Id do Gabinete de Origem: Identificador do gabinete de origem do aluguel.
Id do Gabinete de Destino: Identificador do gabinete de destino do aluguel.
Id do Usuário: Identificador do usuário associado ao aluguel.
Id da Bateria: Identificador da bateria associada ao aluguel.
Criado em: Data e hora de criação do aluguel.

### Endpoints Disponíveis:
POST /rents: Cria um novo registro de aluguel.
GET /rents/{id}: Retorna informações de um registro de aluguel específico com base no ID.
GET /rents: Retorna uma lista de todos os registros de aluguel cadastrados.
PUT /rents/{id}: Atualiza as informações de um registro de aluguel existente.
DELETE /rents/{id}: Exclui um registro de aluguel específico do sistema.
(SOFT-DELETE): Desativa um aluguel específico, mantendo seus dados no sistema.

## Rent:

## CabinetBattery (correlated):

### Atributos:
Id: Identificador único da associação entre gabinete e bateria.
Order: Ordem da associação.
FkCabinetId: ID do gabinete associado.
FkBatteryId: ID da bateria associada.

## Tokens

### Geração de Tokens Únicos: 
A funcionalidade permite a geração de tokens únicos para identificação e autenticação de usuários, gabinetes e baterias no sistema Reboost. Esses tokens são gerados aleatoriamente e associados a gabinete, bateria e usuário.

### Alteração de Tokens: 
Os tokens gerados podem ser alterados a qualquer momento, fornecendo uma nova identificação aleatória.

### Remoção Automática de Tokens Expirados: 
O sistema verifica continuamente os tokens armazenados no banco de dados e remove automaticamente aqueles que têm mais de 15 minutos de idade.