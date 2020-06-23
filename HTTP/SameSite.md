## SameSite 属性

Cookie 的`SameSite`属性用来`**限制第三方 Cookie**`，从而减少安全风险。

它可以设置三个值。

> - Strict
> - Lax
> - None

### 2.1 Strict

`Strict`最为严格，完全禁止第三方 Cookie，跨站点时，任何情况下都不会发送 Cookie。换言之，只有**`当前网页的 URL 与请求目标一致`**，才会带上 Cookie。

> ```bash
> Set-Cookie: CookieName=CookieValue; SameSite=Strict;
> ```

这个规则过于严格，可能造成非常不好的用户体验。比如，当前网页有一个 GitHub 链接，用户点击跳转就不会带有 GitHub 的 Cookie，跳转过去总是未登陆状态。

### 2.2 Lax

`Lax`规则稍稍放宽，大多数情况也是不发送第三方 Cookie，但是导航到目标网址的 Get 请求除外。

> ```markup
> Set-Cookie: CookieName=CookieValue; SameSite=Lax;
> ```

导航到目标网址的 GET 请求，只包括三种情况：链接，预加载请求，GET 表单。详见下表。

| 请求类型  | 示例           | 正常情况    | Lax         |
| --------- | -------------- | ----------- | ----------- |
| 链接      | ``             | 发送 Cookie | 发送 Cookie |
| 预加载    | ``             | 发送 Cookie | 发送 Cookie |
| GET 表单  | ``             | 发送 Cookie | 发送 Cookie |
| POST 表单 | ``             | 发送 Cookie | 不发送      |
| iframe    | ``             | 发送 Cookie | 不发送      |
| AJAX      | `$.get("...")` | 发送 Cookie | 不发送      |
| Image     | ``             | 发送 Cookie | 不发送      |

设置了`Strict`或`Lax`以后，基本就杜绝了 CSRF 攻击。当然，前提是用户浏览器支持 SameSite 属性。

### 2.3 None

Chrome 计划将`Lax`变为默认设置。这时，网站可以选择显式关闭`SameSite`属性，将其设为`None`。不过，前提是必须同时设置`Secure`属性（Cookie 只能通过 HTTPS 协议发送），否则无效。

下面的设置无效。

> ```bash
> Set-Cookie: widget_session=abc123; SameSite=None
> ```

下面的设置有效。

> ```bash
> Set-Cookie: widget_session=abc123; SameSite=None; Secure
> ```