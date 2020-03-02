# EntityFrameworkCore.Seed

**This project is not an official entity framework core library**. It aims to give the ability to seed values inside the database for testing/integration purpose.

## Getting Started

### Prerequisites

You need to have a configured Entity Framework Core 2.+ project.

### Installing

#### Install the packages

To make things work, you need to install **two packages** first.

The first one :

```
Package-Install Authfix.EntityFrameworkCore.Seed
```

This is the base package. It will allow you to run the seed method inside your app. It contains the base classes/main mechanisms.

The second one depends of your database provider. **Actually we have only two providers : InMemory and Postgresql**

You can install

```
Package-Install Authfix.EntityFrameworkCore.InMemory
```
or 

```
Package-Install Authfix.EntityFrameworkCore.Postgresql
```

#### Update your program.cs

After packages installation process, you can update your program.cs to run the seed method.

```
public static void Main(string[] args)
{
    var webHost = BuildWebHost(args);

    // Do not forget to migrate database if needed before seeding.
    webHost.SeedData<ApplicationDbContext>();

    webHost.Run();
}
```

This method will check if you have pending seeds and run it if necessary.

#### Update your Startup.cs

```
services
    .AddEntityFrameworkNpgsql()
    .AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        options.UseNpgsqlSeed(Assembly.GetEntryAssembly().FullName);
    });
```

You must add **function of your provider**, the corresponding Use method and the assembly where your seed classes are stored.

#### Add your first seed

To create a seed, it's actually manually, the CLI like Entity Framework Core is not ready yet.

So you need to create a class and add an inheritance and a custom attribute like EF Migration.

```
[SeedAttribute("20171010165344_InitialScenario")]
public class Seed_20171010165344_InitialScenario : SeederBase
{
    protected override void UpdateEntities()
    {
        var users = GetDbSet<User>();

        for (int i = 1; i < 50; i++)
        {
            var newUser = new User
            {
                Id = i
            };

            users.Add(newUser);
        }
    }
}
```
The SeedAttribute take a name like {year}{month}{day}{hour}{minutes}{seconds}_{name}.

You need to inherit the class with **SeederBase** and you just have to fill the UpdateEntities method.

**The name of the class is not important**

## Running the tests

To run the tests, you just have to launch Visual Studio ;)

## Built With

* [Dotnet Core](https://www.microsoft.com/net/) - The base framework used
* [Entiy Framework](https://github.com/aspnet/EntityFrameworkCore) - The ORM

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/Authfix/EntityFrameworkCore.Seed/tags). 

## Authors

* **Thomas Bailly** - *Initial work* - [Authfix](https://github.com/Authfix)

See also the list of [contributors](https://github.com/Authfix/EntityFrameworkCore.Seed/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* My work is based on the migration mechanism created by the [entity framework team](https://github.com/aspnet/EntityFrameworkCore).