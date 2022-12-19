### 使用ConfigurationBuilder

```
var configBuilder = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true);
```

### 使用.net core 的builder

> 在.net core中添加的json文件是与其他json文件合并

方式一：

```
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true);
```

方式二(建议使用)：

```
var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("MyConfig.json",
    optional: true,
    reloadOnChange: true);
});
```





