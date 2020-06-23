# SQL中EXISTS的用法

##用法:
​		EXISTS用于检查子查询是否至少会返回一行数据，该子查询实际上并不返回任何数据，而是返回值True或False
​		EXISTS 指定一个子查询，检测 行 的存在。

##  NOT EXISTS 

​		 NOT EXISTS 的作用与 EXISTS 正好相反。如果子查询没有返回行，则满足了 NOT EXISTS 中的 WHERE 子句。 