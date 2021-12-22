# [SQL事务](https://www.cnblogs.com/zhaoyl9/p/11472615.html)

## 了解事务和锁

**事务**：保持逻辑数据一致性与可恢复性，必不可少的利器。

**锁**：多用户访问同一数据库资源时，对访问的先后次序权限管理的一种机制，没有他事务或许将会一塌糊涂，不能保证数据的安全正确读写。

**死锁**：是数据库性能的重量级杀手之一，而死锁却是不同事务之间抢占数据资源造成的。

一个事务中可以包含多个DML语句，一个DDL语句或者一个DCL语句。

事务中的语句要么全部执行，要么全部不执行。

书面解释：**事务具有原子性，一致性，隔离性，持久性（ACID）**

- A 原子性：事务必须是一个自动工作的单元，要么全部执行，要么全部不执行。
- C 一致性：事务把数据库从一个一致状态带入到另一个一致状态，事务结束的时候，所有的内部数据都是正确的。
- I 隔离性：并发多个事务时，一个事务的执行不受其他事务的影响。
- D 持久性：事务提交之后，数据是永久性的，不可再回滚，不受关机等事件的影响。

事务在如下情况终止：

1. 遇到**rollback** 或**commit**命令
2. 遇到DDL或者DCL语句.
3. 系统发生错误，崩溃或者退出。

然而在SQL Server中事务被分为3类常见的事务：

- **自动提交事务：**是SQL Server默认的一种事务模式，每条Sql语句都被看成一个事务进行处理，你应该没有见过，一条Update 修改2个字段的语句，只修该了1个字段而另外一个字段没有修改。。
- **显式事务：**T-sql标明，由Begin Transaction开启事务开始，由Commit Transaction 提交事务、Rollback Transaction 回滚事务结束。
- **隐式事务：**使用Set IMPLICIT_TRANSACTIONS ON 将将隐式事务模式打开，不用Begin Transaction开启事务，当一个事务结束，这个模式会自动启用下一个事务，只用Commit Transaction 提交事务、Rollback Transaction 回滚事务即可

## 显式事务的应用

常用语句就四个。

- Begin Transaction：标记事务开始。
- Commit Transaction：事务已经成功执行，数据已经处理妥当。
- Rollback Transaction：数据处理过程中出错，回滚到没有处理之前的数据状态，或回滚到事务内部的保存点。
- Save Transaction：事务内部设置的保存点，就是事务可以不全部回滚，只回滚到这里，保证事务内部不出错的前提下。

[![复制代码](mdimg/SQL%E4%BA%8B%E5%8A%A1/copycode.gif)](javascript:void(0);)

```
---开启事务
begin tran
--错误捕捉机制，看好啦，这里也有的。并且可以嵌套。
begin try  
   --语句正确
   insert into lives (Eat,Play,Numb) values ('猪肉','足球',1)
   --Numb为int类型，出错
   insert into lives (Eat,Play,Numb) values ('猪肉','足球','abc')
   --语句正确
   insert into lives (Eat,Play,Numb) values ('狗肉','篮球',2)
end try
begin catch
   select Error_number() as ErrorNumber,  --错误代码
          Error_severity() as ErrorSeverity,  --错误严重级别，级别小于10 try catch 捕获不到
          Error_state() as ErrorState ,  --错误状态码
          Error_Procedure() as ErrorProcedure , --出现错误的存储过程或触发器的名称。
          Error_line() as ErrorLine,  --发生错误的行号
          Error_message() as ErrorMessage  --错误的具体信息
   if(@@trancount>0) --全局变量@@trancount，事务开启此值+1，他用来判断是有开启事务
      rollback tran  ---由于出错，这里回滚到开始，第一条语句也没有插入成功。
end catch
if(@@trancount>0)
commit tran  --如果成功Lives表中，将会有3条数据。

--表本身为空表，ID ,Numb为int 类型，其它为nvarchar类型
select * from lives
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![img](mdimg/SQL%E4%BA%8B%E5%8A%A1/1146926-20190906141337598-1906122818.png)

## 事务设置保存点

利用save transaction  和rollback transaction 语句，

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
---开启事务
begin tran
--错误捕捉机制，看好啦，这里也有的。并且可以嵌套。
begin try    
   --语句正确
   insert into lives (Eat,Play,Numb) values ('猪肉','足球',1)   
    --加入保存点
   save tran pigOneIn
   insert into lives (Eat,Play,Numb) values ('猪肉','足球',2)
   insert into lives (Eat,Play,Numb) values ('狗肉','篮球',3)
end try
begin catch
   select Error_number() as ErrorNumber,  --错误代码
          Error_severity() as ErrorSeverity,  --错误严重级别，级别小于10 try catch 捕获不到
          Error_state() as ErrorState ,  --错误状态码
          Error_Procedure() as ErrorProcedure , --出现错误的存储过程或触发器的名称。
          Error_line() as ErrorLine,  --发生错误的行号
          Error_message() as ErrorMessage  --错误的具体信息
   if(@@trancount>0) --全局变量@@trancount，事务开启此值+1，他用来判断是有开启事务
      rollback tran   
end catch
if(@@trancount>0)
rollback tran pigOneIn 

--表本身为空表，ID ,Numb为int 类型，其它为nvarchar类型
select * from lives
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![img](mdimg/SQL%E4%BA%8B%E5%8A%A1/1146926-20190906141619400-671003331.png)

注：事务保存点以上的都将影响，当提交事务以后，只有保存点之前的语句被执行。

**事务保存点示例：**

在SQL Server中使用rollback会回滚所有的未提交事务状态，但是有些时候我们只需要回滚部分语句，把不需要回滚的语句提到事务外面来，虽然是个方法，但是却破坏了事务的ACID。

SQL中使用事务保存点，即可解决这个问题。

SQL 事务中存在错误信息 进行Catch 回滚事务时

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
begin try
    begin tran A
    insert into dbo.lives ( Eat, Play, Numb, times ) values ( 'A', '', 0, getdate() )
    
    select 1/0    --错误信息
    save tran B_Point
    insert into dbo.lives ( Eat, Play, Numb, times ) values ( 'B', '', 0, getdate() )
    
    save tran C_Point
    insert into dbo.lives ( Eat, Play, Numb, times ) values ( 'C', '', 0, getdate() )
    
    rollback tran B_Point    --回滚事务点B_Point 即事务点下的部分都回滚
    select 1
    commit tran A    --提交整个事务信息
end try
begin catch
    select 2
    rollback tran B_Point    --回滚事务点B_Point 即事务点下的部分都回滚
    commit tran A    --提交整个事务信息
end catch
go

select * from dbo.lives
go
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![img](mdimg/SQL%E4%BA%8B%E5%8A%A1/1146926-20190906151024764-1877465873.png)

 ![img](mdimg/SQL%E4%BA%8B%E5%8A%A1/1146926-20190906151042585-2111501997.png)

 SQL回滚局部信息时

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
begin try
    begin tran A
    insert into dbo.lives ( Eat, Play, Numb, times ) values ( 'A', '', 0, getdate() )
    
    --select 1/0    --错误信息
    save tran B_Point
    insert into dbo.lives ( Eat, Play, Numb, times ) values ( 'B', '', 0, getdate() )
    
    save tran C_Point
    insert into dbo.lives ( Eat, Play, Numb, times ) values ( 'C', '', 0, getdate() )
    
    rollback tran B_Point    --回滚事务点B_Point 即事务点下的部分都回滚
    select 1
    commit tran A    --提交整个事务信息
end try
begin catch
    select 2
    rollback tran B_Point    --回滚事务点B_Point 即事务点下的部分都回滚
    commit tran A    --提交整个事务信息
end catch
go

select * from dbo.lives
go
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![img](mdimg/SQL%E4%BA%8B%E5%8A%A1/1146926-20190906151112668-845113590.png)

回滚保存点B时 即保存点以下部分均要回滚,

注：使用保存点 无论try 或 catch 代码块 除提交或回滚保存点外，都要COMMIT或 ROLLBACK完整事务。

使用场景:当操作数据时前校验数据成本太高且数据出错率不高时 可采用.eg:用户下单 检查库存信息是否>0时 可以设置库存量需>=0的约束 当更新库时信息小于0即出错 进行事务回滚 并查询返回当前库存信息

## 使用set xact_abort

设置 xact_abort on/off ， 指定是否回滚当前事务，为on时如果当前sql出错，回滚整个事务，为off时如果sql出错回滚当前sql语句，其它语句照常运行读写数据库。

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
delete lives  --清空数据
set xact_abort off
begin tran 
    --语句正确
   insert into lives (Eat,Play,Numb) values ('猪肉','足球',1)   
   --Numb为int类型，出错,如果1234..那个大数据换成'132dsaf' xact_abort将失效
   insert into lives (Eat,Play,Numb) values ('猪肉','足球',12345646879783213)
   --语句正确
   insert into lives (Eat,Play,Numb) values ('狗肉','篮球',3)
commit tran
select * from lives
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![img](mdimg/SQL%E4%BA%8B%E5%8A%A1/1146926-20190906163224123-639268824.png)

为on时，结果集为空，因为运行是数据过大溢出出错，回滚整个事务。