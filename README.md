# CadPlus | ERP Hospitalar

Esta é uma aplicação .NET e C#; também utilizei Entity Framework, SQL Server.

#### Aqui está o [Front-end](https://github.com/filiperv7/cadPlus_front) desta aplicação.

CadPLus é um gerenciador hospitalar de funcionários e pacientes. Nele você pode:
- criar contas (se for Admin);
- editar contas (se for Admin);
- excluir cotas (se for Admin);
- editar seu próprio perfil; e
- evoluir pacientes (se for Médico(a) ou Enfermeiro(a));

## Regras de negócio
1. Somente o Perfil "Admin" pode dar entrada de um "Paciente".
3. O usuário de Perfil "Admin" pode editar tudo em qualquer usuário, exceto "EstadoSaude".
4. Somente Perfis "Médico(a)" ou "Enfermeiro(a)" podem evoluir um "Paciente".
5. Usuários podem editar tudo em si mesmos (exceto "EstadoSaude").

## Algumas decisões e observações
Escolhi usar o SQL Server com autenticação padrão do Windows para melhor facilitação na hora de um terceiro rodar o projeto.
Além disso, também existe um usuário padrão que é criado ao atualizar o banco de dados com o detnet (passo 4) que pode ser editado depois de acessar a aplicação.
Email: admin@default.com, Senha: Adm!n123

É importante que o projeto de inicialização seja o CadPlus.API e que o appsettings.json dele seja alterado com uma nova Secret Key.

## Como rodar a aplicação (6 passos)
##### 1. Clone o projeto
```bash
git clone https://github.com/filiperv7/cadPlus_back
```

##### 2. Acesse a pasta do projeto
```bash
cd cadPlus_back
```

##### 3. Crie a primeira migração com o .NET
```bash
dotnet ef migrations add InitialCreate --project CadPlus.Infrastructure --startup-project CadPlus.API
```

##### 4. Crie as tabelas no banco com o .NET
```bash
dotnet ef database update --project CadPlus.Infrastructure --startup-project CadPlus.API
```

##### 5. Adicione um _Secret Key_ no arquivo appsettings.json (dentro do projeto CadPlus.API)

##### 6. Agora é só rodar o projeto no Visual Studio ou como você preferir
##### Obs.: para uma experiência completa, não deixe de rodar também o [Front-end](https://github.com/filiperv7/cadPlus_front)
