查找端口

netstat -ano | findstr 3342

查找端口信息

tasklist | findstr 6848

杀死端口

taskkill /pid 19416 -f

查看nginx

tasklist /fi "imagename eq nginx.exe"