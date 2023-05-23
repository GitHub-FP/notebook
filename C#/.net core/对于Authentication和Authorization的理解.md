

### 微软提供的认证和授权是不是独立使用的，需要两个一起使用，因为授权需要使用认证的功能，可以了解一下IAuthenticationHandler接口

```

builder.Services
    .AddControllersWithViews();
    //.AddMvcOptions(options => options.Filters.Add(new AuthorizeFilter()));

builder.Services.AddSingleton<
    IAuthorizationMiddlewareResultHandler, SampleAuthorizationMiddlewareResultHandler>();

builder.Services.AddAuthentication(options =>
{
    options.AddScheme<CustomCookieAuthenticationHandler>("CookieSchemeName", "default scheme");
    //options.DefaultAuthenticateScheme = "CookieSchemeName";
    //options.DefaultChallengeScheme = "CookieSchemeName";
    //options.DefaultForbidScheme = "CookieSchemeName";
    options.DefaultScheme = "CookieSchemeName";
});

builder.Services.AddAuthorization(option => {

    option.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    option.DefaultPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

    option.FallbackPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

    option.InvokeHandlersAfterFailure = true;

    option.AddPolicy("Name", policy => policy.RequireClaim(ClaimTypes.Name));
});

```

### 自定义IAuthenticationHandler接口

```
public class CustomCookieAuthenticationHandler : IAuthenticationHandler
    {
        public AuthenticationScheme _scheme;
        public HttpContext _context;

        public Task<AuthenticateResult> AuthenticateAsync()
        {
            var req = _context.Request.Query;
            var ticket = GetAuthTicket("test", "test");
            //return Task.FromResult(AuthenticateResult.Fail("未登陆"));
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        public AuthenticationTicket GetAuthTicket(string name, string role)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                 //new Claim(ClaimTypes.Name, name),
                 new Claim(ClaimTypes.Role, role),
            }, "My_Auth");

            var principal = new ClaimsPrincipal(claimsIdentity);
            return new AuthenticationTicket(principal, _scheme.Name);
        }

        public Task ChallengeAsync(AuthenticationProperties? properties)
        {
            _context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Task.CompletedTask;
        }

        public Task ForbidAsync(AuthenticationProperties? properties)
        {
            _context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return Task.CompletedTask;
        }

        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            _scheme = scheme;
            _context = context;
            return Task.CompletedTask;
        }
    }
```

#### AuthenticateAsync

每次认证都走这个方法，判断是否认证成功，如果成功返回一个AuthenticateResult.Success(ticket) ，告诉后面的授权是否认证，并且ticket携带Claim参数，提供授权判断是否符合授权

#### ChallengeAsync

在AuthenticateAsync之后，授权会判断是否认证成功，未认证成功走这个方法抛出异常

#### ForbidAsync

在AuthenticateAsync之后，授权会判断是否认证成功，如果成功但是没有权限走这个方法抛出异常。

