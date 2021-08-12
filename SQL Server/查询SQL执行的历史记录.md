### 查询SQL执行的历史记录

注意：SQL的历史记录必须是创建数据库有勾选完整的类型

```
SELECT *
FROM (
	SELECT TOP 1000 
	ST.text AS text, --执行的sql
	QS.execution_count AS '执行次数', 
	QS.total_elapsed_time AS '耗时', 
	QS.total_logical_reads AS '逻辑读取次数', 
	QS.total_logical_writes AS '逻辑写入次数', 
	QS.total_physical_reads AS '物理读取次数', 
	QS.creation_time AS '执行时间'
	FROM sys.dm_exec_query_stats QS
		CROSS APPLY sys.dm_exec_sql_text(QS.sql_handle) ST
	WHERE QS.creation_time BETWEEN '2021-08-05 10:00:00' AND '2021-08-05 14:00:00'		-- 时间段
	ORDER BY QS.total_elapsed_time DESC
) z
WHERE z.text LIKE '%DELETE FROM %'		-- 模糊搜素
```

