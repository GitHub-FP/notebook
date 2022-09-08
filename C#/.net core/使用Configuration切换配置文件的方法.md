### 使用ConfigurationBuilder

```
var configBuilder = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true);
```

### 使用.net core 的builder

```
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true);
```

