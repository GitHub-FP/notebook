### docker安装runner

#### linux安装

```
docker run -d --name gitlab-runner --restart always -v /srv/gitlab-runner/config:/etc/gitlab-runner -v /var/run/docker.sock:/var/run/docker.sock gitlab/gitlab-runner:latest
```

#### win安装

```
docker run -d --name gitlab-runner --restart always -v D:\SoftwareInstallation\docker\runner\config:/etc/gitlab-runner -v D:\SoftwareInstallation\docker\runner\docker.sock:/var/run/docker.sock  gitlab/gitlab-runner:latest
```

https://blog.csdn.net/qq_42997214/article/details/122490639

### 配置runner 

#### 进入容器

```
docker exec -it gitlab-runner bash
```

#### 注册命令

```
gitlab-runner register
```

#### 注册

```
输入Gitlab实例的地址(不能是127.0.0.1和localhost)
Please enter the gitlab-ci coordinator URL (e.g. https://gitlab.com )
http://xxx

输入token
Please enter the gitlab-ci token for this runner
xxx

输入Runner的描述
Please enter the gitlab-ci description for this runner
[hostname] my-runner

输入与Runner关联的标签
Please enter the gitlab-ci tags for this runner (comma separated):
my-tag,another-tag

输入Ruuner的执行者：
Please enter the executor: ssh, docker+machine, docker-ssh+machine, kubernetes, docker, parallels, virtualbox, docker-ssh, shell:
shell

退出:
exit
```

#### 添加clone_url (同一台电脑运行runner 和 gitlab 不添加找不到项目)

 ##### 找到 /etc/gitlab-runner/config.toml  文件

##### 添加一条信息 clone_url = "${局域网地址}"

![1651419273258](mdimg/docker%20%E5%AE%89%E8%A3%85runner/1651419273258.png)