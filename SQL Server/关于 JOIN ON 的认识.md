# [SQL中INNER JOIN、LEFT JOIN、RIGHT JOIN、FULL JOIN区别](https://www.cnblogs.com/yanglang/p/8780722.html)

sql中的连接查询有inner join(内连接）、left join(左连接)、right join（右连接）、full join（全连接）四种方式，它们之间其实并没有太大区别，仅仅是查询出来的结果有所不同。 
例如我们有两张表： 
![这里写图片描述](https://img-blog.csdn.net/20150603222647340)

Orders表通过外键Id_P和Persons表进行关联。

### 1.inner join，在两张表进行连接查询时，只保留两张表中完全匹配的结果集。

我们使用inner join对两张表进行连接查询，sql如下：

```
1 SELECT Persons.LastName, Persons.FirstName, Orders.OrderNo
2 FROM Persons
3 INNER JOIN Orders
4 ON Persons.Id_P=Orders.Id_P
5 ORDER BY Persons.LastName
查询结果集： 
```

![这里写图片描述](https://img-blog.csdn.net/20150603222827804)

此种连接方式Orders表中Id_P字段在Persons表中找不到匹配的，则不会列出来。

### 2.left join,在两张表进行连接查询时，会返回左表所有的行，即使在右表中没有匹配的记录。

我们使用left join对两张表进行连接查询，sql如下：

```
1 SELECT Persons.LastName, Persons.FirstName, Orders.OrderNo
2 FROM Persons
3 LEFT JOIN Orders
4 ON Persons.Id_P=Orders.Id_P
5 ORDER BY Persons.LastName
查询结果如下： 
```

*![这里写图片描述](https://img-blog.csdn.net/20150603223638605) 
可以看到，左表（Persons表）中LastName为Bush的行的Id_P字段在右表（Orders表）中没有匹配，但查询结果仍然保留该行。*

### 3.right join,在两张表进行连接查询时，会返回右表所有的行，即使在左表中没有匹配的记录。

我们使用right join对两张表进行连接查询，sql如下：

```
1 SELECT Persons.LastName, Persons.FirstName, Orders.OrderNo
2 FROM Persons
3 RIGHT JOIN Orders
4 ON Persons.Id_P=Orders.Id_P
5 ORDER BY Persons.LastName
```

 *查询结果如下：*

![这里写图片描述](https://img-blog.csdn.net/20150603224352995) 
Orders表中最后一条记录Id_P字段值为65，在左表中没有记录与之匹配，但依然保留。

### 4.full join,在两张表进行连接查询时，返回左表和右表中所有没有匹配的行。

我们使用full join对两张表进行连接查询，sql如下：

```
1 SELECT Persons.LastName, Persons.FirstName, Orders.OrderNo
2 FROM Persons
3 FULL JOIN Orders
4 ON Persons.Id_P=Orders.Id_P
5 ORDER BY Persons.LastName
```

 

查询结果如下： 
![这里写图片描述](https://img-blog.csdn.net/20150603224604636) 
查询结果是left join和right join的并集。

这些连接查询的区别也仅此而已。