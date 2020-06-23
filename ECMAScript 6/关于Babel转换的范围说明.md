## 关于Babel转换的范围说明

```
	Babel 默认只转换新的 JavaScript 句法，而不转换新的 API ，比如 Iterator、Generator、Set、Maps、Proxy、Reflect、Symbol、Promise 等全局对象，以及一些定义在全局对象上的方法（比如 Object.assign）都不会转码。为了解决这个问题，我们使用一种叫做 Polyfill（代码填充，也可译作兼容性补丁） 的技术。
```

