## 一、CSRF 攻击是什么？

Cookie 往往用来存储用户的身份信息，恶意网站可以设法伪造带有正确 Cookie 的 HTTP 请求，这就是 CSRF 攻击。

举例来说，用户登陆了银行网站`your-bank.com`，银行服务器发来了一个 Cookie。

> ```bash
> Set-Cookie:id=a3fWa;
> ```

用户后来又访问了恶意网站`malicious.com`，上面有一个表单。

> ```markup
> <form action="your-bank.com/transfer" method="POST">
>   ...
> </form>
> ```

用户一旦被诱骗发送这个表单，银行网站就会收到带有正确 Cookie 的请求。为了防止这种攻击，表单一般都带有一个随机 token，告诉服务器这是真实请求。

> ```markup
> <form action="your-bank.com/transfer" method="POST">
>   <input type="hidden" name="token" value="dad3weg34">
>   ...
> </form>
> ```

这种第三方网站引导发出的 Cookie，就称为第三方 Cookie。它除了用于 CSRF 攻击，还可以用于用户追踪。

比如，Facebook 在第三方网站插入一张看不见的图片。

> ```markup
> <img src="facebook.com" style="visibility:hidden;">
> ```

浏览器加载上面代码时，就会向 Facebook 发出带有 Cookie 的请求，从而 Facebook 就会知道你是谁，访问了什么网站。