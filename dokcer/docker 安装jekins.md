

参考文档

https://www.jianshu.com/p/2efe272cd77c

### 拉取jenkins

```
docker pull jenkins/jenkins
```



### 运行jenkins

```
docker run -d -p 8080:8080 --name jenkins -v D:\docker_volumejenkins\jenkins_home:/usr/bin/docker -v D:\docker_volumejenkins\jenkins_home:/var/run/docker.sock -v D:\docker_volumejenkins\jenkins_home:/var/jenkins_home jenkins/jenkins
```



### 找密码

```
docker exec -it 7e1543f7695f bash

cat /var/jenkins_home/secrets/initialAdminPassword

```

![1678437192878](mdimg/docker%20%E5%AE%89%E8%A3%85jekins/1678437192878.png)

### 设置账号和密码

admin

1qaz!QAZ