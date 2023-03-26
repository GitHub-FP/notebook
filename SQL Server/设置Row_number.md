### ROW_NUMBER

```
SELECT ROW_NUMBER() over(order by al.SubjectId) as Id, al.* from (
SELECT [UserId] as SubjectId
	  ,'User' as SubjectType
      ,[UserName] as Name
  FROM [dbo].[Ptl_User]
  union
SELECT [UserGroupId] as SubjectId
	  ,'UserGroup' as SubjectType
      ,[UserGroupName] as Name
  FROM [dbo].[Ptl_UserGroup]
) al
```



