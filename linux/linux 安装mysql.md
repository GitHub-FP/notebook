# yum命令如何安装mysqlhttps://m.php.cn/article/459508.html

1、查看系统自带的mysql是否已安装，执行命令：

```
`yum list installed | grep mysql`
```

2、如果已安装，则卸载，使用命令：

```
`yum -y remove mysql-libs.x86_64`
```

3、查看yum库上mysql的版本信息，命令：

```
`yum list | grep mysql 或 yum -y list mysql*`
```

4、使用yum安装mysql数据库，使用命令：

```
`yum -y install mysql-server mysql mysql-devel`
```

5、查看mysql版本信息，命令：

```
`rpm -qi mysql-server`
```

（相关教程推荐：[mysql教程](https://www.php.cn/mysql-tutorials.html)）

6、启动mysql，执行命令：

```
`service mysqld start`
```

如需开机启动则执行命令：

```
`chkconfig mysqld on`
```