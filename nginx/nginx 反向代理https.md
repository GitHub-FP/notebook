### nginx 反向代理https.md

```

worker_processes  1;

events {
    worker_connections  1024;
}

http {
    include       mime.types;
    default_type  application/octet-stream;

    log_format  main  '$remote_addr - $remote_user [$time_local] "$request" '
                      '$status $body_bytes_sent "$http_referer" '
                      '"$http_user_agent" "$http_x_forwarded_for"'
					  '"$upstream_bytes_sent" "$upstream_bytes_received" "$upstream_connect_time"';


    sendfile        on;
    server {
        listen       443 ssl;
        server_name  localhost;

        ssl_certificate      server.crt;
        ssl_certificate_key  server.key;
	

        location / {
			proxy_pass         https://biportalv2.chinacloudsites.cn;
			proxy_set_header   Host biportalv2.chinacloudsites.cn;
			proxy_set_header X-Requested-With XMLHttpRequest;
			proxy_set_header X-Real-IP $remote_addr;
			proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
		
		}
    }

}

```



> 注意：
>
> 1.ssl证书可以是自签名证书
>
> 2.Host改为代理之后的域名
>
> 3.https 反向代理可以不需要443端口，其他端口也可以
>
> 4.https也不需要有证书支持