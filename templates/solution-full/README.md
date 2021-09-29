# E5R.Architecture.Template.FullSolution

## IMPORTANTE!

Renomeie o arquivo `editorconfig.file` para `.editorconfig` e `gitignore.file` para `.gitignore`
caso queira utilizá-los ou simplesmente remova-os de sua solução.


Descreva aqui seu projeto.



## Para compilar e distribuir seu projeto

Para compilar seu projeto:
```powershell
# PowerShell
.\build.ps1 --target=build
```

```sh
# Unix Shell
$ ./build.sh --target=build
```

Para gerar os artefatos de distribuição do seu projeto:
```powershell
# PowerShell
.\build.ps1 --target=dist
```

```sh
# Unix Shell
$ ./build.sh --target=dist

# Você também pode escolher o formato do arquivo de distribuição
# ./build.sh --target=dist --dist-format=bzip2,gzip,zip
```

> Isso irá resultar em um diretório `artifacts` com seus binários
> e outro diretório `dist` com seus pacotes prontos para distribuição.

Em qualquer shell, e em todos os comandos de build, você também pode informar
outros parâmetros adicionais para controlar o processo de compilação:

* `configuration` Nome da configuração. `$dotnet publish --configuration`
* `runtime` [Identificador de Runtime](https://docs.microsoft.com/pt-br/dotnet/core/rid-catalog). `$dotnet publish --runtime`
* `version-suffix` Sufixo da versão. `$dotnet publish --version-suffix` 
