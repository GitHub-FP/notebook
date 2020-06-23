# [SameSite Cookie，防止 CSRF 攻击](https://www.cnblogs.com/ziyunfei/p/5637945.html)

因为 HTTP 协议是无状态的，所以很久以前的网站是没有登录这个概念的，直到网景发明 cookie 以后，网站才开始利用 cookie 记录用户的登录状态。cookie 是个好东西，但它很不安全，其中一个原因是因为 cookie 最初被设计成了允许在第三方网站发起的请求中携带，CSRF 攻击就是利用了 cookie 的这一“弱点”，如果你不了解 CSRF，请移步别的地方学习一下再来。

当我们在浏览器中打开 a.com 站点下的一个网页后，这个页面后续可以发起其它的 HTTP 请求，根据请求附带的表现不同，这些请求可以分为两大类：

\1. 异步请求（不会改变当前页面，也不会打开新页面），比如通过 <script>、<link>、<img>、<iframe> 等标签发起的请求，还有通过各种发送 HTTP 请求的 DOM API（XHR，fetch，sendBeacon）发起的请求。

\2. 同步请求（可能改变当前页面，也可能打开新页面），比如通过对 <a> 的点击，对 <form> 的提交，还有改变 location.href，调用 window.open() 等方式产生的请求。

上面说的同步和异步并不是正式术语，只是我个人的一种区分方式。

这些由当前页面发起的请求的 URL 不一定也是 a.com 上的，可能有 b.com 的，也可能有 c.com 的。我们把发送给 a.com 上的请求叫做第一方请求（first-party request），发送给 b.com 和 c.com 等的请求叫做第三方请求（third-party request），**第三方请求和第一方请求一样，都会带上各自域名下的 cookie**，所以就有了第一方 cookie（first-party cookie）和第三方 cookie（third-party cookie）的区别。上面提到的 CSRF 攻击，就是利用了第三方 cookie 。

防止 CSRF 攻击的办法已经有 CSRF token 校验和 Referer 请求头校验。为了从源头上解决这个问题，Google 起草了[一份草案](https://tools.ietf.org/html/draft-west-first-party-cookies-07)来改进 HTTP 协议，那就是为 Set-Cookie 响应头新增 SameSite 属性，它用来标明这个 cookie 是个“同站 cookie”，同站 cookie 只能作为第一方 cookie，不能作为第三方 cookie。SameSite 有两个属性值，分别是 Strict 和 Lax，下面分别讲解：

### SameSite=Strict：

严格模式，表明这个 cookie 在任何情况下都不可能作为第三方 cookie，绝无例外。比如说假如 b.com 设置了如下 cookie：

```
Set-Cookie: foo=1; SameSite=Strict
Set-Cookie: bar=2
```

你在 a.com 下发起的对 b.com 的任意请求中，foo 这个 cookie 都不会被包含在 Cookie 请求头中，但 bar 会。举个实际的例子就是，假如淘宝网站用来识别用户登录与否的 cookie 被设置成了 SameSite=Strict，那么用户从百度搜索页面甚至天猫页面的链接点击进入淘宝后，淘宝都不会是登录状态，因为淘宝的服务器不会接受到那个 cookie，其它网站发起的对淘宝的任意请求都不会带上那个 cookie。

### SameSite=Lax：

宽松模式，比 Strict 放宽了点限制：假如这个请求是我上面总结的那种同步请求（改变了当前页面或者打开了新页面）且同时是个 GET 请求（因为从语义上说 GET 是读取操作，比 POST 更安全），则这个 cookie 可以作为第三方 cookie。比如说假如 b.com 设置了如下 cookie：

```
Set-Cookie: foo=1; SameSite=Strict
Set-Cookie: bar=2; SameSite=Lax
Set-Cookie: baz=3
```

当用户从 a.com 点击链接进入 b.com 时，foo 这个 cookie 不会被包含在 Cookie 请求头中，但 bar 和 baz 会，也就是说用户在不同网站之间通过链接跳转是不受影响了。但假如这个请求是从 a.com 发起的对 b.com 的异步请求，或者页面跳转是通过表单的 post 提交触发的，则 bar 也不会发送。

## 该用哪种模式？

该用哪种模式，要看你的需求。比如你的网站是一个少数人使用的后台管理系统，所有人的操作方式都是从自己浏览器的收藏夹里打开网址，那我看用 Strict 也无妨。如果你的网站是微博，用了 Strict 会这样：有人在某个论坛里发了帖子“快看这个微博多搞笑 http://weibo.com/111111/aaaaaa”，结果下面人都回复“打不开啊”；如果你的网站是淘宝，用了 Strict 会这样：某微商在微博上发了条消息“新百伦正品特卖5折起 https://item.taobao.com/item.htm?id=1111111”，结果点进去顾客买不了，也就是说，这种超多用户的、可能经常需要用户从别的网站点过来的网站，就不适合用 Strict 了。

假如你的网站有用 iframe 形式嵌在别的网站里的需求，那么连 Lax 你也不能用，因为 iframe 请求也是一种异步请求。或者假如别的网站有使用你的网站的 JSONP 接口，那么同样 Lax 你也不能用，比如天猫就是通过淘宝的 JSONP 接口来判断用户是否登录的。

有时安全性和灵活性就是矛盾的，需要取舍。

## 和浏览器的“禁用第三方 cookie”功能有什么区别？ 

主流浏览器都有禁用第三方 cookie 的功能，它和 SameSite 有什么区别？我能总结 3 点：

\1. 该功能是由用户决定是否开启的，是针对整个浏览器中所有 cookie 的，即便有些浏览器可以设置域名白名单，那最小单位也是域名；而 SameSite 是由网站决定是否开启的，它针对的是某个网站下的单个 cookie。

\2. 该功能同时禁用第三方 cookie 的读和写，比如 a.com 发起了对 b.com 的请求，这个请求完全不会有 Cookie 请求头，同时假如这个请求的响应头里有 Set-Cookie: foo=1，foo 这个 cookie 也不会被写进浏览器里；而 SameSite 只禁用读，比如 b.com 在用户浏览器下已经写入了个 SameSite cookie foo，当 a.com 请求 b.com 时，foo 肯定不会被发送过去，但 b.com 在这个请求的响应里又返回了： Set-Cookie: bar=1; SameSite=Strcit，这个 bar 会成功写入浏览器的 cookie 里。

\3. 该功能不会把我上面说的那种同步请求（改变了当前页面或者打开了新页面）算在第三方请求里，因此也不会拦截对应的 cookie。

## 到底怎样才算第三方请求？

我上面说的原话是：当一个请求本身的 URL 和它的发起页面的 URL 不属于同一个站点时，这个请求就算第三方请求。那么怎样算是同一个站点？是我们经常说的同源（same-origin）吗，cross-origin 的两个请求就不属于同一个站点？显然不是的，foo.a.com 和 bar.a.com 是不同源的，但很有可能是同一个站点的，a.com 和 a.com:8000 是不同源的，但它俩绝对是属于同一个站点的，浏览器在判断第三方请求时用的判断逻辑并不是同源策略，而是用了 Public Suffix List 来判断。

有些同学可能会这么想：一个域名可以用逗号分成多个字段，如果两个域名的最后两个字段都是相同的，那它们就是同一个站点的，比如 foo.a.com 和 bar.a.com 就是。但是 sina.com.cn 和 sohu.com.cn 也满足这个条件啊，它们绝对不是同一个网站吧，那是不是说浏览器需要维护一份列表来记录所有国家颁布的二级域名啊，但是不仅国家可以开放三级域名给不同的网站使用，普通的网站也可能会，比如新浪就开放 *.sinaapp.com 三级域名注册，foo.sinaapp.com 和 bar.sinaapp.com 是两个不同的网站，那 sinaapp.com 也应该加入那个列表中，以及 github.io 等等。

Mozilla 很久之前就将自己维护的[这个域名后缀列表](https://publicsuffix.org/list/public_suffix_list.dat)放到了 github 上，起名为 Public Suffix List，里面不仅有 IANA 颁布的顶级域名，众多二级域名，还有三级域名比如 compute.amazonaws.com，甚至四级域名比如 compute.amazonaws.com.cn，判断两个 URL 是不是同一个网站的，只要判断两个 URL 的域名的 public suffix（按能匹配到的最长的算）以及它前面的那个字段（后面用 public suffix+1 指代）是否都相同，是的话就是同一个站点的，否则不是。比如 www.sina.com.cn 的 public suffix+1 是 sina.com.cn，www.sohu.com.cn 的 public suffix+1 是 sohu.com.cn， 两者不一样，所以不属于同一个站点；再比如 nanzhuang.taobao.com 的 public suffix+1 是 taobao.com，nvzhuang.taobao.com 的 public suffix+1 也是 taobao.com，那么它俩就是同一个站点的。

Public Suffix List 最初被 Firefox 用在限制 Set-Cookie 响应头的 Domain 属性上的， Domain 不能设置成一个比自己网站的 public suffix+1 还高层级的域名，比如 foo.w3c.github.io 就不能设置 Set-Cookie: foo=1; Domain=github.io，最高只能设置成 Set-Cookie: bar=1; Domain=w3c.github.io，现在其它浏览器也都在用同样的列表做同样的限制。DOM API 里的 document.domain 后来也加上了这个限制。有些浏览器还用这个列表来高亮地址栏上的 URL 中的 public suffix+1 部分（Firefox 和 IE 有用，Chrome 是高亮了整个域名），此外浏览器们还用该列表干一些其它琐事，比如将历史网址按不同站点排列等等。

浏览器们会定期同步这份列表，比如 Chrome 是在每个正式版本发布之前同步一次。

## 后台语言的支持程度

目前还没有哪个后台语言的 API 支持了 SameSite 属性，比如 php 里的 [setcookie 函数](http://php.net/manual/zh/function.setcookie.php)，或者 java 里的 [java.net.HttpCookie 类](https://docs.oracle.com/javase/8/docs/api/java/net/HttpCookie.html)，如果你想使用 SameSite，需要使用更底层的 API 直接修改 Set-Cookie 响应头。Node.js 本来就没有专门设置 cookie 的 API，只有通用的 [setHeader 方法](https://nodejs.org/docs/latest-v6.x/api/http.html#http_response_setheader_name_value)，不过 Node.js 的框架 Express 已经[支持了 SameSite](https://expressjs.com/en/changelog/4x.html)。

## 使用 document.cookie 测试

如果觉得开 http 服务测试 SameSite cookie 比较麻烦的话，你也可以使用 document.cookie 来代替，比如 document.cookie="foo=1;SameSite=Strict"，为 document.cookie 赋值和使用 Set-Cookie 响应头的效果几乎一摸一样，除了不能读取和设置带 HttpOnly 属性的 cookie 以外。 