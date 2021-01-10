---
title: Tutorial - Lendo objetos armazenados
---

Lendo objetos armazenados
=========================

> TODO...

Notas:
```
Criar documentação conceitual mínima (camada de dados)
  : [x] Find()
  - Obtém um único registro previamente conhecido por seu(s) identificador(es)
  : [x] Get()
  - Pega "todos" os registros disponíveis. Trata-se de um "GetAll()", portanto,
    é uma consulta "perigosa" porque pode simplesmente ser muito custoza.
    Com isso, é obrigatório informar uma limitação (IDataLimiter) explicitamente,
    o que inclui limites de offset, e ordenação. Mesmo que se queira todos os
    registros sem limite de offset, precisa-se ser explícito devido a natureza
    "perigosa" desse tipo de consulta.
  - O resultado inclui informações estatísticas de paginação. Mesmo que só se
    queira o resultado em si, as informações quanto a quantidade total de registros
    disponíveis, bem como o range onde os dados retornados se enquadram, estarão
    disponíveis para tomada de decisão do dev responsável pela pesquisa.
  : [x] Search()
  - Faz uma busca, ou pesquisa, na base. O objetivo é coletar um grupo de registros
    que atendam a critérios de busca (IDataFilter). Não requer limitação (IDataLimiter)
    ou informações de ordenamento, porque entende-se que os critérios da buscam já fazem
    o papel de limitador, e como o objetivo é encontrar correspondências, não há necessidade
    de customizar a ordenação dos dados, visto que essa é uma tarefa secundária,
    além de poder ser feito após a obtenção dos dados com algorítimos de ordenação
    suficientemente eficazes com os dados já em memória, ou até mesmo simpesmente
    confiar na ordenação indexada nos próprios mecanismos de banco de dados.
    Uma pesquisa muito abrangente é tão perigosa quanto a uma obtenção de todos os
    registros sem nenhum limite. Mesmo que seja uma busca amplamente abrangente, ainda
    assim precisa-se ser explícito quanto ao filtro.
  - O resultado é uma lista com os itens correspondentes sem informações estatísticas
    de paginação.
  : [x] LimitedSearch()
  - É uma combinação de Get() + Search()
  - O resultado inclui informações estatísticas de paginação.
  ```
