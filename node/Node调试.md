### Node调试

node --inspect

### 调试umi

#### 1. 在script中设置node调试脚本

![1677203779381](mdimg/Node%E8%B0%83%E8%AF%95/1677203779381.png)

```
"debugger":"node --inspect ./node_modules/umi/lib/forkedDev.js"
```

#### 2.在需要调试的地方设置debugger

```
 chainWebpack: (config) => {
    debugger
    // GraphQL Loader
    config.module
      .rule('mjs$')
      .test(/\.mjs$/)
      .include.add(/node_modules/)
      .end()
      .type('javascript/auto');
  },
```

#### 3.运行npm run debugger

#### 4.打开浏览器，打开F12，点击node图标

![1677215302303](mdimg/Node%E8%B0%83%E8%AF%95/1677215302303.png)