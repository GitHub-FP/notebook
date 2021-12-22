### [linux下运行、停止jar包](https://www.cnblogs.com/whalesea/p/13578824.html)

一、后台运行jar

```
[root@VM-0-4-centos java]# nohup java -jar /home/spring_one_demo-0.0.1-SNAPSHOT.jar > spring.log 2>&1 &
```

上述命令会使jar包在后台运行，用户退出也不会终止程序。

其中：

末尾的&，使用指定后台运行

nohup命令表示，系统后台不挂断地运行命令，退出终端不会影响程序的运行。不加这个命令，即使使用&，在退出远程连接后还是终止程序。

\> spring.log,是nohup的相关命令，表示将原本会打印在控制台的文件打印到spring.log里。该文件如果未指定路径，会在当前目录生成。

2>&1，同样是nohup相关,表示将标准错误 2 重定向到标准输出 &1 ，标准输出 &1 再被重定向输入到 runoob.log 文件中。如果不加这一命令会生成如下提示：

```
[root@VM-0-4-centos java]# nohup: ignoring input and redirecting stderr to stdout
```

二、终止jar程序

1、首先找到该jar在运行时产生的进程号pid

a、在运行成功时会自动返回一个pid,如下图5509既是。

![img](mdimg/linux%E4%B8%8B%E8%BF%90%E8%A1%8C%E3%80%81%E5%81%9C%E6%AD%A2jar%E5%8C%85/1278594-20200828174228555-989510967.png)

 

 b、根据jar所占用端口（如果有端口占用），如下图5509既是。

```
[root@VM-0-4-centos java]# netstat -nlp | grep :80
```

![img](mdimg/linux%E4%B8%8B%E8%BF%90%E8%A1%8C%E3%80%81%E5%81%9C%E6%AD%A2jar%E5%8C%85/1278594-20200828174349614-1410391545.png)

 

 c、根据java程序查找

```
[root@VM-0-4-centos java]# ps -ef | grep java
```

![img](mdimg/linux%E4%B8%8B%E8%BF%90%E8%A1%8C%E3%80%81%E5%81%9C%E6%AD%A2jar%E5%8C%85/1278594-20200828174543952-937195131.png)

 

 2、根据进程号pid，结束进程

```
[root@VM-0-4-centos java]# kill -9 5509
```

检查运行结果：

![img](mdimg/linux%E4%B8%8B%E8%BF%90%E8%A1%8C%E3%80%81%E5%81%9C%E6%AD%A2jar%E5%8C%85/1278594-20200828174725478-126615947.png)