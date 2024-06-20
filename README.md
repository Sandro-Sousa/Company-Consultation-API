# Projeto Consulta Pessoa Jurídica
## Criando uma API com Asp.net Core Web Api, Jwt, padrão de design Orientada a Dominio DDD, Repository Pattern e Adapter Pattern.

## Tecnologias Usadas

- [.NET 6] - Framework da Microsoft Para Desenvolver Diversos Tipos de Projetos.
- [Entity Framework] - Framework Para Mapeamento de Dados Relacionais.
- [AspNetCore Authentication JwtBearer] - autenticação por JWT.
- [SQLServer] -Gerenciador de Banco de Dados Relacionais.
- [Swagger] - framework Para Interface de Uma APi Para, Documentar, Visualizar, Consumir APIRest.


### Criar Migração
```sh
dotnet ef migrations add NomeDaMigracao --project ProjetoDeInfraestrutura --startup-project ProjetoDeAplicacao --context NomeDoContexto

```

### Criar tabelas do Bando de Dados:

```sh
dotnet ef database update --project ProjetoDeInfraestrutura --startup-project ProjetoDeAplicacao --context NomeDoContexto
```


