### 1.登录腾讯云服务器

### 2.安装openssl（腾讯云centos自带不需要安装）

### 3.生成证书https://blog.csdn.net/ZY_FlyWay/article/details/125616927

```
openssl genrsa -out server.key 2048

openssl req -new -sha256 -x509 -days 365 -key server.key -out server.crt

按照步骤一步步填写，国家是CN 大写。其他的随便填
```



