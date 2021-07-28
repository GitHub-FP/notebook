### X-Frame-Options

```
X-Frame-Options: deny
X-Frame-Options: sameorigin
X-Frame-Options: allow-from https://example.com/

deny
表示该页面不允许在 frame 中展示，即便是在相同域名的页面中嵌套也不允许。
sameorigin
表示该页面可以在相同域名页面的 frame 中展示。
allow-from uri
表示该页面可以在指定来源的 frame 中展示。
```

#### .net core 前端配置

```
<?xml version="1.0" encoding="UTF-8"?>
<configuration>
  <system.webServer>
  
  	// 这里配置X-Frame-Options
	<httpProtocol>
        <customHeaders>
          <add name="X-Frame-Options" value="sameorigin" />
        </customHeaders>
  	</httpProtocol>
  	
    <rewrite>
      <rules>
        <rule name="page" patternSyntax="Wildcard">
          <match url="page/*" />
          <action type="Rewrite" url="index.html" />
        </rule>
      </rules>
    </rewrite>
    <security>
      <requestFiltering>
        <!--限制请求最大长度2000000000Bypt=2G-->
        <requestLimits maxAllowedContentLength="2000000000" /> 
      </requestFiltering>
    </security>
        <httpErrors>
            <remove statusCode="401" subStatusCode="-1" />
        </httpErrors>
  </system.webServer>
</configuration>
```

