# npm install安装固定版本号以及package.json中版本号详解

#### 在npm中安装固定的版本号package，只需要在其后加 ‘@版本号’

```
npm install three@0.102.1
```

#### Node.js中package.json中库的版本号详解：
```text
1、 ~ 匹配最近的小版本依赖包，比如~1.2.3会匹配所有1.2.x版本，但是不包括1.3.0
2、^ 匹配最新的大版本依赖包，比如^1.2.3会匹配所有1.x.x的包，包括1.3.0，但是不包括2.0.0
3、* 意味着安装最新版本的依赖包
4、不带标志，则意味着指定版本的依赖包
```

