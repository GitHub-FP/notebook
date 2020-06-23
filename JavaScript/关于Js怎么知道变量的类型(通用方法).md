## 关于Js怎么知道变量的类型(通用方法)

```
Object.prototype.toString.call({})
```

```
Object.prototype.toString.call({})== '[object Array]';
false

Object.prototype.toString.call({});
"[object Object]"

Object.prototype.toString.call(true);
"[object Boolean]"

let a; Object.prototype.toString.call(a);
"[object Undefined]"
```

