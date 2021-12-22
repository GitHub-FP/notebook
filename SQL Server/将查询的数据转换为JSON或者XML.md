### 将查询的字段变为 JSON

方式一：

```sql
SELECT UserId FROM [dbo].[Ptl_UserTenantRel] FOR JSON PATH

结果:
[{"UserId":"7dba8ea5-b5c0-408e-8de5-06857b5c826e"},{"UserId":"7dba8ea5-b5c0-408e-8de5-06857b5c826e"}]
```

### 将查询的字段变为XML

方式一：

```sql
SELECT UserId FROM [dbo].[Ptl_UserTenantRel] FOR XML PATH

结果:
<row><UserId>7dba8ea5-b5c0-408e-8de5-06857b5c826e</UserId></row><row><UserId>7dba8ea5-b5c0-408e-8de5-06857b5c826e</UserId></row>
```

方式二：

```
SELECT ','+UserId+','+TenantId+',' FROM [dbo].[Ptl_UserTenantRel] FOR XML PATH('')

结果：
,7dba8ea5-b5c0-408e-8de5-06857b5c826e,0a52d4e0-b4de-4969-8415-92bd4071ee1a,,7dba8ea5-b5c0-408e-8de5-06857b5c826e,1b864883-4e02-4bf6-aa3f-042c64ffe29f,
```

