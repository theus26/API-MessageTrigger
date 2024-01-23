# API-MessageTrigger

Essa API foi desenvolvida para realizar o disparo de mensagem em massa para o Whatsapp, agendamento de mensagem, tudo isso usando a EVOLUTION API.

## Tecnologias Utilizadas

* REST
* C#
* Entity Framework Core
* .Net Core
* MySQL
* EVOLUTION API

## Pré-requisitos

* [.NET 8 SDK](https://dotnet.microsoft.com/pt-br/download/).
* MySQL


## Configuração

1. Clone o repositório:

```
git clone https://github.com/theus26/API-MessageTrigger.git
```

2. Acesse o diretório do projeto:

```
cd seu-repositorio
```

3. Configure as variáveis de ambiente do sistema necessárias.

```
ex: 
Nome Da Variável: MESSAGE_TRIGGER_CONNECTION
Valor da Variavel: Server=localhost;Port=3306; DataBase=Trigger; Uid=root; Pwd=; 
```


4. Execute os seguintes comandos para restaurar as dependências e iniciar a API:

```
dotnet restore
dotnet run
```

5. Acesse a API em http://localhost:porta, onde "porta" é a porta configurada para a sua API.

*Observação: Certifique-se de estar rodando a evolution para realizar os testes e trocar a Url da Evolution API no AppSettings, caso não for usar localmente, trocar por sua urls de teste*

```
 "Urls": {
    "UrlEvolutionApi": "http://localhost:8080"
  }
```

## Funcionalidades

A API tem duas funcionalidades importantes, são elas:

* *CreateInstance* - Cria uma instancia na evolution, conectando seu celular a evolution e ao Whatsapp, com isso é possivel realizar o disparo de mensagem em massa.

* *SendMessageTrigger* - Realiza o disparo de varias mensagem para varios numeros de Whatsapp


Exemplo:

* `POST /MessageTrigger/CreateInstance`: Criar Intancia na evolution.

Body:

```
NameIntance: (Nome da instancia),
Token: (Token de acesso),
QrCode: true,
Number: (numero de telefone responsavél por realizar o disparo de mensagem)
``` 

* `POST /MessageTrigger/SendMessageTrigger`: Realiza o disparo de mensagem em massa.

Body:

```
File: (Arquivo onde contém os números),
MediaMessage(Arquivo de media a ser enviado como mensagem)
Texto: (Texto a ser enviado),
``` 
## Banco de Dados

O Entity Framework Core é uma estrutura de mapeamento de objeto/relacional. Ele mapeia os objetos de domínio em seu código para entidades em um banco de dados relacional. Na maior parte do tempo, você não precisa se preocupar com a camada de banco de dados, pois o Entity Framework cuida dela para você. Seu código manipula os objetos e as alterações são persistentes em um banco de dados.

Exemplo:

A API utiliza o Entity Framework Core para se comunicar com o banco de dados. O banco de dados padrão é o Mysql. Para configurar o banco de dados:

1. Crie em seu sistema a variavel de conexão, como mostrado no exemplo anterior, logo acima.

2. Antes de executar as migrations para gerar o banco de dados, certifique-se de que a porta 3306 esteja instanciada para poder usar o MySQL. E a sua connctionString está correta, após isso execute:

```
dotnet ef database update
```
_Esse comando executará todas as migrations criadas e irá gerar toda parte do Banco de Dados._
