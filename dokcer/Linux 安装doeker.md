https://www.runoob.com/docker/centos-docker-install.html

1. 安装所需的软件包 

```
sudo yum install -y yum-utils \
  device-mapper-persistent-data \
  lvm2
```

2.设置官方源地址

```
sudo yum-config-manager \
    --add-repo \
    https://download.docker.com/linux/centos/docker-ce.repo
```

3.安装 Docker Engine-Community

```
sudo yum install docker-ce docker-ce-cli containerd.io
```

4.启动 Docker

```
sudo systemctl start docker
```

