# SharpJuice.Essentials #

[![NuGet](https://img.shields.io/nuget/v/SharpJuice.Essentials.svg)](https://www.nuget.org/packages/SharpJuice.Essentials/)

Maybe<T> inspired by [this article](http://blog.ploeh.dk/2011/02/04/TheBCLalreadyhasaMaybemonad/) 

#### Creation ####
Maybe<T> is implemented as readonly struct. Maybe is empty when its holded value is null.

```csharp
//new empty
var maybe = new Maybe<User>();
var maybe = default(Maybe<User>);

//from object
var maybe = userObject.ToMaybe();
var maybe = new Maybe<User>(userObject);

//implicit cast
Maybe<object> may = new object();
Maybe<int> maybe = 1;

void Method(Maybe<int> arg) {...}
Method(10);
```


#### Bind ####
Bind method performs an action on the value when maybe contains one.

```csharp
Maybe<Stream> stream = ...

//Func<T,TResult>
Maybe<int> readByte = stream.Bind(s => s.ReadByte());

//Func<T,Task<TResult>>
Maybe<int> readByte = await stream.Bind(s => s.ReadAsync(...)); 

//Action<T>
stream.Bind(s => s.Flush());  
```

#### OrElse ####
OrElse method is opposite to Bind. It performs the action when maybe is empty.

```csharp
Maybe<User> user = ...

//Func<T>
User instance = user.OrElse(() => new User());

//Func<Task<T>>
User instance = await user.OrElse(() => database.Get(...));; 

//Value
User instance = user.OrElse(anonymousUserInstance);
User instance = user.OrDefault();  
```

#### Other Methods ####
Maybe implements IEnumerable<T>, IEquatable<Maybe<T>>, IEquatable<T>

```csharp
Maybe<User> maybe = ...

bool hasValue = maybe.Any();

//Single returns value or throws InvalidOperationException
User instance = maybe.Single();

//Throw exception when maybe contains value.
maybe.BindThrow(() => new Exception());

//Throw exception when maybe is empty
User instance = maybe.OrThrow(() => new NotFoundException());

//Dictionary extensions
IReadOnlyDictionary<string, string> dictionary = ...
Maybe<string> value = dictionary.GetValue(key);

//Cast
Maybe<Administrator> admin = maybe.As<Administrator>();

//Empty string
Maybe<string> stringValue = " ".ToMaybe(emptyAsNull: true);
```
