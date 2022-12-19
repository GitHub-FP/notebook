参考文档：https://developer.okta.com/blog/2020/10/23/how-to-authenticate-with-saml-in-aspnet-core-and-csharp



## 描述

###  AuthnRequest 

按压缩、base64编码、URL encode的顺序处理 

###  SAMLResponse 

base64编码



## SAML 步骤

### 1.安装nuget包

> 必须使用此版本

```
dotnet add package ITfoxtec.Identity.Saml2 --version 4.0.8
dotnet add package ITfoxtec.Identity.Saml2.MvcCore --version 4.0.8
```

### 2.appsettings.json,添加“Saml2”的信息

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Saml2": {
    "IdPMetadata": "<?xml version=\"1.0\" encoding=\"UTF-8\"?><md:EntityDescriptor entityID=\"http://www.okta.com/exk77t9zilbcRXff15d7\" xmlns:md=\"urn:oasis:names:tc:SAML:2.0:metadata\"><md:IDPSSODescriptor WantAuthnRequestsSigned=\"false\" protocolSupportEnumeration=\"urn:oasis:names:tc:SAML:2.0:protocol\"><md:KeyDescriptor use=\"signing\"><ds:KeyInfo xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\"><ds:X509Data><ds:X509Certificate>MIIDqDCCApCgAwIBAgIGAYRksWXLMA0GCSqGSIb3DQEBCwUAMIGUMQswCQYDVQQGEwJVUzETMBEG A1UECAwKQ2FsaWZvcm5pYTEWMBQGA1UEBwwNU2FuIEZyYW5jaXNjbzENMAsGA1UECgwET2t0YTEU MBIGA1UECwwLU1NPUHJvdmlkZXIxFTATBgNVBAMMDGRldi04OTQ4ODU1MTEcMBoGCSqGSIb3DQEJ ARYNaW5mb0Bva3RhLmNvbTAeFw0yMjExMTEwMzE2MzhaFw0zMjExMTEwMzE3MzhaMIGUMQswCQYD VQQGEwJVUzETMBEGA1UECAwKQ2FsaWZvcm5pYTEWMBQGA1UEBwwNU2FuIEZyYW5jaXNjbzENMAsG A1UECgwET2t0YTEUMBIGA1UECwwLU1NPUHJvdmlkZXIxFTATBgNVBAMMDGRldi04OTQ4ODU1MTEc MBoGCSqGSIb3DQEJARYNaW5mb0Bva3RhLmNvbTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoC ggEBANfkaQ/AWFLjoxB7g/lX0ijd0cv1WUcMDF7XKgPItJy7HX1JdYz0IIe98M9fMQpLK9VemrTV UvyG46r3R56xMGK06+t47/17HYp3g+u8Ciie4zA573wE0gqWTYP0ogRmAyHMQmM9N+Ns2LAkZUHz Hwo3TpP9476pRKh+XmFhLzWSFCyoypU+L8wSLWd2c6+kG4/GrYscnMm2kjnbVkq+5BfjMYW2lw9C c5zbNLipEUTIxhrGTyYorWNYKaTAnTNnqIxInu2+/uC/EH/2oDnZObJLo6dzbZVprcYXu57OSZKC CUmXI9KPtrQWvFWr+8qC+R8hVkpmD6OOEjdWPUgxtF0CAwEAATANBgkqhkiG9w0BAQsFAAOCAQEA EznSBxeAUGxPjm9ThJjcf4pl0f7Zr8cMBJwMp3llB0PQ7Qwoews32tnPRfXqtkslv+RvrvJwiehJ UZVi9bPb7XrYPf+nRSWjHv1fA+azMhR9oHXdQRUOd3YaHlKSiK7zV6BVXuVe60U8JDDn7FdhtIlY Fq+4NfzY8NW9R+06unJESPhAOLsF7Y8hckvFxCqYs7tO/lG4m2JYLTWTK+vFxVphmyM/DvuuODsz CaqhTukqJYaHWewiq0MzIaA28aUTyq6wqnl5NL+MvXMDCjTtBK8DwMfc2MMOfYYLa5LOTEJ8emba krVttKFY1GtVfHRbIVeanrlmSigUfUMclFs9FQ==</ds:X509Certificate></ds:X509Data></ds:KeyInfo></md:KeyDescriptor><md:NameIDFormat>urn:oasis:names:tc:SAML:1.1:nameid-format:unspecified</md:NameIDFormat><md:NameIDFormat>urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress</md:NameIDFormat><md:SingleSignOnService Binding=\"urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST\" Location=\"https://dev-89488551.okta.com/app/dev-89488551_fengpantest_1/exk77t9zilbcRXff15d7/sso/saml\"/><md:SingleSignOnService Binding=\"urn:oasis:names:tc:SAML:2.0:bindings:HTTP-Redirect\" Location=\"https://dev-89488551.okta.com/app/dev-89488551_fengpantest_1/exk77t9zilbcRXff15d7/sso/saml\"/></md:IDPSSODescriptor></md:EntityDescriptor>",
    "Issuer": "http://www.okta.com/exk77t9zilbcRXff15d7",
    "SignatureAlgorithm": "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256",
    "CertificateValidationMode": "ChainTrust",
    "RevocationMode": "NoCheck",
    "AudienceRestricted": false
  },
  //系统配置
  "SysSettings": {
    //数据库连接字符串
    //"DatabaseConnStr": "data source=.\\SQLSERVER2017;database=BIPortalV2_stg;user id=user1; pwd=1qaz!QAZ; MultipleActiveResultSets=true;"
  }
}

```

### 2.在startup.cs 中添加以下代码

#### ConfigureServices:

```C#
services.Configure<Saml2Configuration>(Configuration.GetSection("Saml2"));
services.Configure<Saml2Configuration>(saml2Configuration =>
{
    saml2Configuration.AllowedAudienceUris.Add(saml2Configuration.Issuer);

    
    var entityDescriptor = new EntityDescriptor();
    entityDescriptor.ReadIdPSsoDescriptor(Configuration["Saml2:IdPMetadata"]);
    if (entityDescriptor.IdPSsoDescriptor != null)
    {
        saml2Configuration.SingleSignOnDestination = entityDescriptor.IdPSsoDescriptor.SingleSignOnServices.First().Location;
        saml2Configuration.SignatureValidationCertificates.AddRange(entityDescriptor.IdPSsoDescriptor.SigningCertificates);
    }
    else
    {
        throw new Exception("IdPSsoDescriptor not loaded from metadata.");
    }
});
services.AddSaml2();
```

#### Configure

```C#
app.UseSaml2();
```

### 3.设置控制器

```C#
[AllowAnonymous]
[Route("Auth")]
public class SamlController : Controller
{
    const string relayStateReturnUrl = "ReturnUrl";
    private readonly Saml2Configuration config;

    public SamlController(IOptions<Saml2Configuration> configAccessor)
    {
        config = configAccessor.Value;
    }

        
    /// <summary>
    /// 获取登录地址
    /// </summary>
    /// <param name="returnUrl"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [Route("Login")]
    public IActionResult Login(string returnUrl = null)
    {
        var binding = new Saml2RedirectBinding();
        binding.SetRelayStateQuery(new Dictionary<string, string> { { relayStateReturnUrl, returnUrl ?? Url.Content("~/") } });

        return binding.Bind(new Saml2AuthnRequest(config)).ToActionResult();

    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [AllowAnonymous]
    [Route("Saml")]
    public async Task<IActionResult> AssertionConsumerService()
    {
        var binding = new Saml2PostBinding();
        var saml2AuthnResponse = new Saml2AuthnResponse(config);
        var z = Request.ToGenericHttpRequest();
        //z.Form["SAMLResponse"] = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmN...";            // 验证有效性
        binding.ReadSamlResponse(z, saml2AuthnResponse);
        if (saml2AuthnResponse.Status != Saml2StatusCodes.Success)
        {
            throw new Exception($"SAML Response status: {saml2AuthnResponse.Status}");
        }
        binding.Unbind(Request.ToGenericHttpRequest(), saml2AuthnResponse);
            
        return Ok("sadsd");
    }
}
```

