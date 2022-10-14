### 新建/更新实体

```
添加迁移
dotnet ef migrations add init

通过迁移更新数据库
dotnet ef database update
```



### 删除数据库

```
删除数据库
dotnet ef database drop
```



### 数据库回滚到某个迁移文件->删除回滚的迁移文件

```
1.指定回滚的文件名称，数据库即可回滚到这个迁移文件的历史，会删除之后的迁移指令
dotnet ef database update sys

2. If '0', all migrations will be reverted.删除所有的表
dotnet ef database update 0

3.更新最后一个迁移文件
dotnet ef database update

4.删除回滚的迁移文件
dotnet ef migrations remove

注意：数据库根据迁移文件回滚后，删除不要的迁移文件（dotnet ef migrations remove），重新生成新的迁移文件即可(切记不要手动删除migration文件)
```









