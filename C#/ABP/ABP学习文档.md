## ABP 基础

### 配置与选项模式

>  ABP也是基于ASP.NET Core，未做特殊处理

#### 配置模式

```
var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("MyConfig.json",
    optional: true,
    reloadOnChange: true);
});
```

#### .net core 的选项模式

```
public void ConfigureServices(IServiceCollection services)
{
    services.Configure<AppSettingsConfig>(Configuration.GetSection("AppSettings"));
    services.AddScoped(cfg => cfg.GetService<IOptions<AppSettingsConfig>>().Value);
}
```

### ABP模块

### HTTP请求

### 邮件发送

> Volo.Abp.Emailing默认安装在领域层，并且注意删除以下代码(此代码不会在DeBug环境中发送邮件)：context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());

#### 选项模式

1.在appsetting.json文件中配置，password必须要要加密，所以自定义实现ISettingEncryptionService接口为password进行加密解密配置

```c#
[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
public class CustomSettingEncryptionService : ISettingEncryptionService
{
    public string Decrypt(SettingDefinition settingDefinition, string encryptedValue)
    {
        return encryptedValue;
    }

    public string Encrypt(SettingDefinition settingDefinition, string plainValue)
    {
    	return plainValue;
    }
}
```

2.在在appsetting.json中配置邮件参数

```json
{
  ...,
  "Settings": {
    "Abp.Mailing.Smtp.Host": "smtp.qq.com",
    "Abp.Mailing.Smtp.Port": "587",
    "Abp.Mailing.Smtp.UserName": "1446355013@qq.com",
    "Abp.Mailing.Smtp.Password": "ynuvcjnaagqkhbaj",
    "Abp.Mailing.Smtp.EnableSsl": "false",
    "Abp.Mailing.Smtp.UseDefaultCredentials": "false",
    "Abp.Mailing.DefaultFromAddress": "1446355013@qq.com",
    "Abp.Mailing.DefaultFromDisplayName": "ABP application"
  }
}
```

#### 设置模式

1.自定义继承SettingDefinitionProvider，通过sql或者自定义设置参数

```C#
public class EmailSettingProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        var smtpHost = context.GetOrNull("Abp.Mailing.Smtp.Host");
        context.Add(
            new SettingDefinition("Abp.Mailing.DefaultFromAddress", "1446355013@qq.com"),
            new SettingDefinition("Abp.Mailing.Smtp.Host", "smtp.qq.com"),
            new SettingDefinition("Abp.Mailing.Smtp.Port", "587"),
            new SettingDefinition("Abp.Mailing.Smtp.UserName", "1446355013@qq.com"),
            new SettingDefinition("Abp.Mailing.Smtp.Password", "ynuvcjnaagqkhbaj"),
            new SettingDefinition("Abp.Mailing.Smtp.EnableSsl", "false"),
            new SettingDefinition("Abp.Mailing.Smtp.UseDefaultCredentials", "false")    
        );
    }
}
```

####  集成MailKit

1.将Volo.Abp.Emailing替换为Volo.Abp.MailKit

2.替换[DependsOn(typeof(AbpEmailingModule))] 为 [DependsOn(typeof(AbpMailKitModule))]



#### 自定义MailKit

继承MailKitSmtpEmailSender

```
[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
    public class CustomMailKitSmtpEmailSender: MailKitSmtpEmailSender
    {
        protected AbpMailKitOptions AbpMailKitOptions { get; set; }

        protected ISmtpEmailSenderConfiguration SmtpConfiguration { get; set; }

        public CustomMailKitSmtpEmailSender(ISmtpEmailSenderConfiguration smtpConfiguration, IBackgroundJobManager backgroundJobManager, IOptions<AbpMailKitOptions> abpMailKitConfiguration)
          : base(smtpConfiguration, backgroundJobManager, abpMailKitConfiguration)
        {
            AbpMailKitOptions = abpMailKitConfiguration.Value;
            SmtpConfiguration = smtpConfiguration;
        }

		// protected 子类可以访问
        protected override async Task ConfigureClient(SmtpClient client)
        {
            await client.ConnectAsync(
                await SmtpConfiguration.GetHostAsync(),
                await SmtpConfiguration.GetPortAsync(),
                await GetSecureSocketOption()
            );

            if (await SmtpConfiguration.GetUseDefaultCredentialsAsync())
            {
                return;
            }

            await client.AuthenticateAsync(
                await SmtpConfiguration.GetUserNameAsync(),
                await SmtpConfiguration.GetPasswordAsync()
            );
        }
    }
```

### 数据过滤

> 在EFcore的继承上实现的全局过滤，建议使用ABP自带的接口

多个数据过滤

### GUID 和 时钟(IClock)

GUID：数据库的生成方式不一样，初始化配置需要修改，默认mssql

IClock: 使用datetime，使用utc时间，默认是0时区（拒绝DateTimeOffset），如果转换其他时区可以在前后端添加一个util方法

### 模型验证

BP与ASP.NET Core模型验证系统系统兼容 

特点：

- 定义 `IValidationEnabled` 向任意类添加自动验证. 所有的[应用服务](https://docs.abp.io/zh-Hans/abp/latest/Application-Services)都实现了该接口,所以它们会被自动验证.
- 自动将数据注解属性的验证错误信息本地化.
- 提供可扩展的服务来验证方法调用或对象的状态.
- 提供[FluentValidation](https://fluentvalidation.net/)的集成.

### 文件上传（Blob）

多个不同类型的blob的使用方式？

### 日志及异常处理 

### 本地化

了解即可

### 路由配置

### 依赖注入

> 注意：ABP的引用其他模块必须适应DependsOn引用此模块
>

### API层(控制器层)

> 注意：写控制器层必须要添加IRemoteService接口，不然不会变为API控制器

#### 自动API控制器

1. 扫描程序集带有IRemoteService 接口的类和自动添加为控制器

```
Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options
                .ConventionalControllers
                .Create(typeof(BookStoreApplicationModule).Assembly);
        });
```

### 应用层

> 1.继承ApplicationService类，内部帮忙继承了一些类方便使用，特别是继承了IUnitOfWorkEnabled，可以帮助我们实现事务
>
> 2.遵循DTO->Entity /  Entity->DTO , 不要重用DTO

#### 应用层构建组成

- **应用服务(Application Service)**: [应用服务](https://docs.abp.io/zh-Hans/abp/latest/Application-Services)是为实现用例的无状态服务.展现层调用应用服务获取DTO.应用服务调用多个领域服务实现用例.用例通常被视为一个工作单元.
- **数据传输对象(DTO)**: [DTO](https://docs.abp.io/zh-Hans/abp/latest/Data-Transfer-Objects)是一个不含业务逻辑的简单对象,用于应用服务层与展现层间的数据传输.
- **工作单元(UOW)**: [工作单元](https://docs.abp.io/zh-Hans/abp/latest/Unit-Of-Work)是事务的原子操作.UOW内所有操作,当成功时全部提交,失败时全部回滚.

#### 应用程序服务

命名前缀：...Service

#### DTO （对象到对象映射）（AutoMapper）

描述：Dto->Entity , Entity->Dto。

使用Entity作为接口参数，由于自身携带了属性，会导致接口参数复杂，会暴露实体信息及其关联，所以不建议使用Entity作为接口参数。

##### 不要重用输入DTO（不要嫌麻烦，粘贴复制即可），但是重用输出DTO

```csharp
public interface IUserAppService : IApplicationService
{
    Task CreateAsync(UserCreationDto input);
    Task UpdateAsync(UserUpdateDto input);
    Task ChangePasswordAsync(UserChangePasswordDto input);
}
```

##### 对象映射时，内部存在List<T>的处理方式

```
CreateMap<User, UserDto>()
    .ForMember(dest => dest.DptUserRels, opt => opt.MapFrom(src=>
        src.DptUserRels.Select<DptUserRel, DptUserRelDto>(x=>
            new DptUserRelDto() {
                DptId = x.DptId,
                UserId = x.UserId
            }
        )));

CreateMap<UserDto, User>()
    .ForMember(dest => dest.DptUserRels, opt => opt.MapFrom(src => 
        src.DptUserRels.Select<DptUserRelDto, DptUserRel>(x => 
            new DptUserRel()
            {
                DptId = x.DptId,
                UserId = x.UserId
            }
        )))
    .ForMember(dest => dest.Departments, opt => opt.Ignore());
```

##### ABP自定义AutoMapper

```
public class UserDtoToUserMapper : IObjectMapper<User, UserDto>, ITransientDependency
    {
        public UserDto Map(User source)
        {
            return new UserDto()
            {
                TenantId = source.TenantId,
                UserDisplayName = source.UserDisplayName,
                UserEmail = source.UserEmail,
                UserMobile = source.UserMobile,
                UserEnabled = source.UserEnabled,
                UserSync = source.UserSync,
                UserBuildinName = source.UserBuildinName,
                UserBuildinPassword = source.UserBuildinPassword,
                UserAdName = source.UserAdName,
                UserAadName = source.UserAadName,
                UserWeworkName = source.UserWeworkName,
                UserDingtalkName = source.UserDingtalkName,
                UserFeishuName = source.UserFeishuName,
                DptUserRels = source.DptUserRels.Select(x=>new DptUserRelDto() {
                    DptId = x.DptId,
                    UserId = x.UserId
                }).ToList(),
            };
        }

        public UserDto Map(User source, UserDto destination)
        {
            return destination;
        }
    }
```

注意：

1.不同的Entity之间存在双向导航，查询出来的数据转换为Json会可能出现深层循环，会报错，我们可以使用AutoMapper解决

2.Dto的对象不要来源于实体中，再写一个即可，因为可能出现深层循环问题或者类库引用问题

### 领域层

#### 加载方式

EF Core ： https://learn.microsoft.com/zh-cn/ef/core/querying/related-data/eager

ABP ： https://docs.abp.io/zh-Hans/abp/latest/Entity-Framework-Core

##### 预先加载

##### 显示加载

##### 延迟加载

``` 
ABP 要求 :

1.安装 Microsoft.EntityFrameworkCore.Proxies
2.配置如下 
Configure<AbpDbContextOptions>(options =>
{
    options.PreConfigure<MyCrmDbContext>(opts =>
    {
        opts.DbContextOptions.UseLazyLoadingProxies(); //启用延时加载
    });

    options.UseSqlServer();
});

3.导航属性和集合必须是virtual
```

#### 领域服务

命名前缀：...Manager

- 你实现了依赖于某些服务（如存储库或其他外部服务）的核心域逻辑.
- 你需要实现的逻辑与多个聚合/实体相关,因此它不适合任何聚合.

 #### 规约

继承Specification<T> 创建一个lambda表达式对实体或者linq添加条件判断

```

    public abstract class Specification<T> : ISpecification<T>
    {
        public virtual bool IsSatisfiedBy(T obj)
        {
            return ToExpression().Compile()(obj);
        }

        public abstract Expression<Func<T, bool>> ToExpression();

        public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
        {
            return specification.ToExpression();
        }
    }
```

#### 领域层构建组成

- **实体(Entity)**: [实体](https://docs.abp.io/zh-Hans/abp/latest/Entities)是种领域对象,它有自己的属性(状态,数据)和执行业务逻辑的方法.实体由唯一标识符(Id)表示,不同ID的两个实体被视为不同的实体.
- **值对象(Value Object)**: [值对象](https://docs.abp.io/zh-Hans/abp/latest/Value-Objects)是另外一种类型的领域对象,使用值对象的属性来判断两个值对象是否相同,而非使用ID判断.如果两个值对象的属性值全部相同就被视为同一对象.值对象通常是不可变的,大多数情况下它比实体简单.
- **聚合(Aggregate) 和 聚合根(Aggregate Root)**: [聚合](https://docs.abp.io/zh-Hans/abp/latest/Entities)是由**聚合根**包裹在一起的一组对象(实体和值对象).聚合根是一种具有特定职责的实体.
- **仓储(Repository)** (接口): [仓储](https://docs.abp.io/zh-Hans/abp/latest/Repositories)是被领域层或应用层调用的数据库持久化接口.它隐藏了DBMS的复杂性,领域层中只定义仓储接口,而非实现.
- **领域服务(Domain Service)**: [领域服务](https://docs.abp.io/zh-Hans/abp/latest/Domain-Services)是一种无状态的服务,它依赖多个聚合(实体)或外部服务来实现该领域的核心业务逻辑.
- **规约(Specification)**: [规约](https://docs.abp.io/zh-Hans/abp/latest/Specifications)是一种**强命名**,**可重用**,**可组合**,**可测试**的实体过滤器.
- **领域事件(Domain Event)**: [领域事件](https://docs.abp.io/zh-Hans/abp/latest/Event-Bus)是当领域某个事件发生时,通知其它领域服务的方式,为了解耦领域服务间的依赖.

#### 聚合根

 EF core 聚合根加载所有子集合？请参考EFcore加载关联实体(https://docs.abp.io/zh-Hans/abp/latest/Entity-Framework-Core#%E5%8A%A0%E8%BD%BD%E5%85%B3%E8%81%94%E5%AE%9E%E4%BD%93)

所有**子集合**对象必须被初始化



#### 实体

##### 描述

Entity<Guid> 和 Entity 的区别：

在仓储中继承Entity<Guid>的 IRepository<TEntity, TKey> 实现了简单的CRUD功能，继承Entity 的仓储无法使用与id相关的功能

##### 贫血模型

##### 充血模型

1.改变模型值的逻辑可以定义为方法。

```
public class Issue : AggregateRoot<Guid>
    {
        public Guid RepositoryId { get; private set; } //Never changes
        public string Title { get; private set; } //Needs validation
        public string Text { get; set; } //No validation
        public Guid? AssignedUserId { get; set; } //No validation
        public bool IsClosed { get; private set; } //Should change with CloseReason
        public IssueCloseReason? CloseReason { get; private set; } //Should change with IsClosed

        //...

        public void SetTitle(string title)
        {
            Title = Check.NotNullOrWhiteSpace(title, nameof(title));
        }

        public void Close(IssueCloseReason reason)
        {
            IsClosed = true;
            CloseReason = reason;
        }

        public void ReOpen()
        {
            IsClosed = false;
            CloseReason = null;
        }
    }
```



#### 实体逻辑

在实体中创建逻辑方法

#### 仓储

自定义仓储的实现：

```
public class PersonRepository : EfCoreRepository<MyDbContext, Person, Guid>, IPersonRepository
{
    public PersonRepository(IDbContextProvider<TestAppDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {

    }

    public async Task<Person> FindByNameAsync(string name)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Set<Person>()
            .Where(p => p.Name == name)
            .FirstOrDefaultAsync();
    }
}
```

###  工作单元（UOW，事务）

默认开启，进入action或者 继承ApplicationService会自动使用

继承IUnitOfWorkEnabled 或者使用 UnitOfWorkAttribute

_unitOfWorkManager.Current 可以判断当前是否使用UOW

### EF Core配置

###  缓存

一个缓存就可以

### 邮件

注意：多个租户，多个邮件地址

### 审计日志

### 多组织

### 系统集成、认证、授权（第三方授权）

系统集成:RSA

认证:JWT、cookie

第三方授权: （需要考虑内部认证授权方式和提供第三方的授权方式兼容）

#### identity server 4

#### 微软自带的认证和授权方式

#### OpenId connect 与Oauth2.0的区别及使用方式

OpenId connect  是基于Oauth2.0的一个认证和授权的协议

Oauth2.0目的是为了授权（https://www.ruanyifeng.com/blog/2019/04/oauth-grant-types.html）

### 前端主题与集成API





### 微服务

前端微服务应该怎么实现呢







邮件、缓存、blob 、虚拟文件目录



权限：

路由资源

报表、blob等资源（查看、编辑、删除）