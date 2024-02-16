# RBlazeLabs.RAuth

API de Authenticação de Usuário e Aplicações padrão JWT.

##### Available Authentication types
- Usuários internos (base de dados própria)

### Requerimentos
- `MySql Server` 5.7.22 ou superior para que a aplicação crie e utilize seu próprio banco de dados. Consulte https://dev.mysql.com/.
- `SEQ Server` para a aplicação enviar registros de log. Consulte https://github.com/rodriguesrm/rsoft-logs.

### Dependências de pacotes NuGet
- `Serilog`. Veja em https://serilog.net/.

### Configurações e Variáveis de Ambientes

##### Variáveis de ambiente
- `ASPNETCORE_ENVIRONMENT` deve ser atribuído corretamente para informar o ambiente de execução do serviço. Deve ser preenchido de acordo com os padrões estabelecidos pela Microsoft para esta variável (Development, Staging ou Production).
##### Configurações
&nbsp;
The application uses the standard `appsettings.json` and its variants recommended by Microsoft. Below is a preview of the file for the production environment.

```json
{
	// TODO
}

```

### Licença
MIT
