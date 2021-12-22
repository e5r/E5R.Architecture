Para liberar uma versão
=======================

1. Crie uma branch à partir da _baseline_ (normalmente `develop`) com o número da versão: `release/x.x.x`.
2. Altere o arquivo de notas de lançamento
3. Faça e complete o _Pull Request_
4. Crie ou atualize a próxima _baseline_ (normalmente `develop`):
```
$ git fetch
$ git merge origin/master
```
5. Na baseline de _próxima versão_ faça:
   - Atualize o número da próxima versão em `packages.props`
   - Atualize o arquivo de notas de lançamento para conter o tópico da próxima versão
