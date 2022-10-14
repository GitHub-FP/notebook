### 依赖注入自定义类

```
接口：
public static IServiceCollection AddTransient<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory)

实现：
context.Services.AddTransient<IDingTalkApi>(options =>
        {
            return new DingTalkApi("dingfdn5vcsywj41iwio", "BsVKgDn8x2bI-s62kyRh_Hqvmam000KFqACnRABQSn0CXXWJBevuKj1YYlGJqJJh");
        });
```

### IOC(IServiceProvider)一个几口多个实现类的获取方式

```
使用IServiceProvider的方式
IEnumerable<IDingTalkApiClient> shows = _services.GetServices<IDingTalkApiClient>();


使用构造函数的方式
 public BookManager( IEnumerable<IDingTalkApiClient> IDingTalkApiClients )
 {
 	_services = services;
 }
```

