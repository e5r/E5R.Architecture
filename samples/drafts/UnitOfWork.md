* Uma implementação clássica para IUnitOfWork atendendo a bancos relacionais sobre ADO.NET.

```csharp
class UnitOfWork : IUnitOfWork
{
    ISqlConnection _connection;
    IDbTransaction _transaction;

    UnitOfWork(UnitOfWorkOptions options, MyDbContext_A dbA, MyDbContext_B dbB)
    {
        _connection = new SqlConnection(options.ConnectionString);

        // ADO properties
        Property<SqlTransaction>(new Lazy(() => Begin()));

        // Dapper properties
        Property<IDbConnection >(new Lazy(() => {
            Begin();
            return _connection;
        });

        // EntityFramework properties
        Property<MyDbContext_A>(new Lazy(() => {
            dbA.DataBase.UseTransaction(Begin());
            return dbA;
        }));

        Property<MyDbContext_B>(new Lazy(() => {
            dbB.DataBase.UseTransaction(Begin());
            return dbB;
        }));
    }

    private IDbTransaction Begin()
    {
        if(_connection.State != ConnectionState .Open)
        {
            _connection.Open();
        }

        if(_transaction == null)
        {
            _transaction = _connection.BeginTransaction();
        }

        return _transaction;
    }

    void SaveWork()
    {
        if (_transaction != null)
        {
            _transaction.Commit();
            _transaction = null;
        }

        UnloadProperties();
    }
}
```

> Observe o registro das propriedades por tipos `Property<SqlTransaction>(...);`

* Aqui temos um genérico para alcançar as propriedades registradas no IUnitOfWork
  diretamente se precisar.

```csharp
class UnitOfWorkProperty<T>
{
    UnitOfWorkProperty(IUnitOfWork uow)
    {
        Property = uow.Property<T>();
    }

    T Property { get; private set }
}
```

* Aqui um exemplo de repositório __EntityFramework__ que obtém o contexto como uma
  propriedade de IUnitOfWork de forma explícita no construtor:

```csharp
class Repository_1 : IRepository<MyDtoA>
{	
    MyDbContext_A _context;

    Repository_1(IUnitOfWork uow)
    {
        _context = uow.Property<MyDbContext_A>();
    }

    int Create(MyDtoA dto)
    {
        var p = new Person
        {
            Name = dto.Name
        };

        _context.Persons.Add(p);
        _context.SaveChanges();

        return p.Id;
    }
}
```

* Aqui um outro exemplo de repositório __EntityFramework__, mas agora obtendo o
  contexto de IUnitOfWork via genérico de propriedade direto no construtor:
  
```csharp
// EntityFramework
class Repository_2 : IRepository<MyDtoB>
{
    MyDbContext_B _context;

    Repository_2(UnitOfWorkProperty<MyDbContext_B> context)
    {
        _context = context;
    }

    string Create(MyDtoB dto)
    {
        var blog = new Blog
        {
            Url = dto.Url,
            BlogName = dto.Name
        };

        _context.Blogs.Add(blog);
        _context.SaveChanges();

        return blog.Url;
    }
}
``

* Aqui um exemplo de repositório __ADO.NET__ que trabalha direto na `SqlTransaction`:

```csharp
class Repository_3 : IRepository<MyDtoC>
{
    SqlTransaction _transaction;

    Repository_2(UnitOfWorkProperty<SqlTransaction> transaction)
    {
        _transaction = transaction;
    }

    IEnumerable<MyDtoC> LoadAll()
    {
        var list = new List<MyDtoC>();
        var sql = "SELECT Id, Name FROM dbo.Persons ORDER BY Name";
        var cmd = new SqlCommand(sql, _transaction.Connection);
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new MyDtoC
            {
                Code = reader[0],
                Name = reader[1]
            });
        }

        reader.Close();

        return list;
    }
}
```

* Aqui um exemplo __Dapper__ que usa uma propriedade `IDbConnection`:

```csharp
class Repository_4 : IRepository<MyDtoD>
{
    IDbConnection _conn;

    Repository_2(UnitOfWorkProperty<IDbConnection> conn)
    {
        _conn = conn;
    }

    IEnumerable<MyDtoC> AddTwoUsers(UserDto user1, UserDto user2)
    {
        var count = _conn.Execute(
            @"INSERT dbo.User(Name, Email) VALUES (@a, @b)",
            new[] { user1, user2 }
        );
    }
}
```
