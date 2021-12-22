# SQL Server解析Json格式数据

[SQL Server解析Json格式数据]: https://blog.csdn.net/qq_33246702/article/details/104483027

```sql
/****** Script for SelectTopNRows command from SSMS  ******/
SELECT mu1.[MenuId]
      ,mu1.[MenuParentId]
	  ,mu2.MenuName as ParentName
	  ,Json_Value (mu2.[MenuName],'$."zh-CN"') as '父菜单名称中文'
	  ,Json_Value (mu2.[MenuName],'$."en-US"') as '父菜单名称英文'
      ,Json_Value (mu1.[MenuName],'$."zh-CN"') as '菜单名称中文'
	  ,Json_Value (mu1.[MenuName],'$."en-US"') as '菜单名称英文'
      ,mu1.[MenuProducer]
      ,mu1.[MenuContactDetails]
	  ,pr.PbiReportName	as '报表名称'
	  ,pr.PbiGroupId
	  ,pr.PbiReportId
  FROM [dbo].[Ptl_Menu] mu1
  left join [dbo].[Ptl_Menu] mu2 on mu1.MenuParentId = mu2.MenuId
  left join [dbo].[Ptl_PbiReport] pr on pr.PbiReportId = Json_Value (mu1.[MenuExtend],'$."pbiReportId"')
  where mu1.[MenuType]='PbiReport'

```

