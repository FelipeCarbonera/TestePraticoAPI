# TestePraticoAPI


O arquivo "ConnectionString.txt" na raiz do projeto deve conter a Connection String do banco de dados utilizado, no caso, estou utilizando um arquivo .mdf que está na pasta UserApi/Database/

Na pasta do projeto FrontEnd, existe um arquivo chamado "UrlApiUser.txt", que deve conter a URL para a API desenvolvida.
Essa URL pode ser encontrada em "UserApi/Properties/launchSettings.json", na propriedade "applicationUrl";


Uma rota de exemplo que retorna todos os Usuários no banco:

http://localhost:54556/users

Retorno:

[{"Id":1,"Nome":"Felipe","CPF":"09289205938","Email":"felipe_carbo@hotmail.com","Telefone":"49999998822","Sexo":"Masculino","DataNascimento":"1995-08-26T00:00:00"},{"Id":4,"Nome":"JUJUBINHA DE MORANGO","CPF":"12345678966","Email":"binha_juju@teste.com","Telefone":"49999998822","Sexo":"Feminino","DataNascimento":"2020-10-01T00:00:00"}]
