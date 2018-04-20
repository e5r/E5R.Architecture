0.1.0-alpha

* classe E5R.Architecture.Core.**SemVer** básica
* classe E5R.Architecture.Data.**Data** temporária
* Formatador de string CPF
```csharp
using E5R.Architecture.Core.Formatters;

// code block
{
   string cpfFormatado = (FormattedCPF)"12345678901"; 
}

// output $cpfFormatado => "123.456.789-01"
```

* Desformatador de string de CPF
```csharp
using E5R.Architecture.Core.Formatters;

// code block
{
   string cpfDesformatado = (UnformattedCPF)"123.456.789-01"; 
}

// output $cpfDesformatado => "12345678901"
```

# TODO

* Implementar conceito criptografia de dado sensível

> A idéia é um mecanismo que permita a criptografia e descriptografia de dados sensíveis
> como Códigos passados por URL. Teríamos um tipo [EncryptedValue], e todos objetos que
> fossem utilizar o conceito, escreveriam um um conversor implícito/explícito.
