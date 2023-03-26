### proxy_pass

####  规则一 ：

【ip或者域名】:【端口】,最终代理的地址为  【ip或者域名】:【端口】+Location+Url

```
server {

    listen 5000;
    server_name www.liushuyu.top;

    location  /ccc/test/ {
        proxy_pass http://119.91.100.32:6000;
    }
}
访问的URL：http://www.liushuyu.top:5000/ccc/test/aaa/bbb.txt
代理的结果：http://www.liushuyu.top:6000/ccc/test/aaa/bbb.txt
```



#### 规则二

```
server {

    listen 5000;
    server_name www.liushuyu.top;

    location  /ccc/test/ {
        proxy_pass http://119.91.100.32:6000/;
    }
}
访问的URL：http://www.liushuyu.top:5000/ccc/test/aaa/bbb.txt
代理的结果：http://www.liushuyu.top:6000/aaa/bbb.txt
```

> 注意:如果proxy_pass后面有Url（/也是Url），则会删除掉访问Url中有的location部分，如规则二所示