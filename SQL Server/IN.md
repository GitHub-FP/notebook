# IN

用法: IN 操作符允许您在 WHERE 子句中规定多个值。 

```
SELECT column_name(s)
FROM table_name
WHERE column_name IN (value1,value2,...);
```

**IN 与 = 的异同**

-  相同点：均在WHERE中使用作为筛选条件之一、均是等于的含义
-  不同点：IN可以规定多个值，等于规定一个值

**IN**

```sql
例子1:
SELECT column_name(s)
FROM table_name
WHERE column_name IN (value1,value2,...);

例子2:
SELECT * FROM [BIPortalV2].[dbo].[Authorization]
WHERE ResType 
in (select ResType from [BIPortalV2].[dbo].[Authorization] where ResType='Tenant')
```

**=**

```
SELECT column_name(s)
FROM table_name
WHERE column_name=value1;
```

**in 与 = 的转换**

```
select * from Websites where name in ('Google','菜鸟教程');
```

可以转换成 **=** 的表达：

```
select * from Websites where name='Google' or name='菜鸟教程';
```