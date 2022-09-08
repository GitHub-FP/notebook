### docker 安装 gitlab

docker pull gitlab/gitlab-ce

#### docker 运行gitlab

#### linux

```
docker run -d --name gitlabce --restart always -p 80:80 -p 443:443 -p 222:22 -v /data/docker/gitlab/etc:/etc/gitlab -v /data/docker/gitlab/data:/var/opt/gitlab -v /data/docker/gitlab/log:/var/log/gitlab gitlab/gitlab-ce
```

#### win

```
docker run -d --name gitlabce --restart always -p 9999:80 -p 4443:443 -p 222:22 -v D:\SoftwareInstallation\docker\gitlab\etc:/etc/gitlab -v D:\SoftwareInstallation\docker\gitlab\data:/var/opt/gitlab -v D:\SoftwareInstallation\docker\gitlab\log:/var/log/gitlab gitlab/gitlab-ce
```

#### 修改root密码

```
进入gitlab：
docker exec -it 88d303fd34ce bash

修改密码：
gitlab-rails console

u=User.where(id:1).first

u.password='12345678'

u.password_confirmation='12345678'

u.save

退出gitlab
exit
exit

重启gitlab
docker restart gitlab
```

![img](mdimg/gitlabe/8666493ee82245a4a14982728ce8bb99.png) 

#### 克隆地址是一串字符串，需要在/data/docker/gitlab/etc/gitlab.rb新增一条数据（只写域名，不要加端口）

```
external_url 'http://192.168.92.130'
```

![1652628609063](mdimg/docker%20%E5%AE%89%E8%A3%85%20gitlab/1652628609063.png)

### CI/CD代码模板

```
image: node:14.15.3

cache:
    paths:
      - node_modules
      - build

stages: 
    - install
    - build
    - artft
    - deploy

install: 
    stage: install
    timeout: 3h 30m
    tags: 
        - dockerrunner
    script: 
        - echo "hello"
        - node -v
        - npm -v
        - npm install
build: 
    stage: build
    timeout: 3h 30m
    tags: 
        - dockerrunner
    script: 
        - npm run wb
artft:
    stage: artft
    script: echo 'The man who has made up his mind to win will never say “impossible”.'
    tags: 
        - dockerrunner
    artifacts:
        paths: 
            - build/
deploy: 
    image: docker:stable
    stage: deploy
    script: 
        - echo 'deploy'
        - docker build -t test-runner-docker:v1 .
        - docker images
    tags: 
        - dockerrunner


```

