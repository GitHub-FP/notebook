## [EFCore 迁移命令移除外键](https://www.cnblogs.com/lludcmmcdull/p/13502567.html)

### 1.生成一个实体扩展工具，标记那个外键需要删除

```

    public static class EntityTypeBuilderExtensions
    {
        public static EntityTypeBuilder<T> RemoeForeignKey<T>(this EntityTypeBuilder<T> builder, string name) where T : class
        {
            var tableName = builder.Metadata.FindAnnotation("Relational:TableName").Value.ToString();
            Console.WriteLine("tableName:"+tableName);
            RemoveForeignKeysHelper.RemoveForeignKeys.AddOrUpdate(tableName, new List<string> { name }, (value, values) => {
                values.Add(name);
                return values.Distinct().ToList();
            });
            return builder;
        }
    }
```

### 2. 通过外键标记，指定删除外键

```
public class RemoveForeignKeysHelper
    {
        //定义个全局变量，用来存储需要移除的级联属性
        internal static ConcurrentDictionary<string, List<string>> RemoveForeignKeys = new ConcurrentDictionary<string, List<string>>();

        public static void ExecuForeignKeys(CreateTableOperation operation)
        {
            if (RemoveForeignKeys.TryGetValue(operation.Name, out List<string> columns))
            {
                operation.ForeignKeys
                    .Where(item => item.Columns.Intersect(columns).Count() > 0)
                    .ToList()
                    .ForEach(item => operation.ForeignKeys.Remove(item));
            }
        }
    }
```

### 3.继承MigrationsModelDiffer类型删除出指定的外键

```
[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<挂起>")]
    public class MigrationsModelDifferWithoutForeignKey : MigrationsModelDiffer
    {
        public MigrationsModelDifferWithoutForeignKey
            ([NotNull] IRelationalTypeMappingSource typeMappingSource,
            [NotNull] IMigrationsAnnotationProvider migrationsAnnotations,
            [NotNull] IChangeDetector changeDetector,
            [NotNull] IUpdateAdapterFactory updateAdapterFactory,
            [NotNull] CommandBatchPreparerDependencies commandBatchPreparerDependencies)
            : base(typeMappingSource, migrationsAnnotations, changeDetector, updateAdapterFactory, commandBatchPreparerDependencies)
        {
        }

        public override IReadOnlyList<MigrationOperation> GetDifferences(IRelationalModel? source, IRelationalModel? target)
        {
            var operations = base.GetDifferences(source, target)
                .Where(op => !(op is AddForeignKeyOperation))
                .Where(op => !(op is DropForeignKeyOperation))
                .ToList();

            foreach (var operation in operations.OfType<CreateTableOperation>()) {
                //ExecuForeignKeys
                RemoveForeignKeysHelper.ExecuForeignKeys(operation);
            }
           //operation.ForeignKeys?.Clear();

            return operations;
        }
    }
```

### 4.将MigrationsModelDifferWithoutForeignKey类替换EF Core的MigrationsModelDiffer类

在DbContextFactory的类中注入，替换MigrationsModelDiffer类

```
services.AddDbContext<MyDbContext>(options =>
　　{
　　　　options.UseSqlServer(Default);
　　　　options.ReplaceService<IMigrationsModelDiffer, MigrationsModelDifferWithoutForeignKey>();
　　});
```

ABP如下：

![1662897034386](mdimg/EFCore%20%E8%BF%81%E7%A7%BB%E5%91%BD%E4%BB%A4%E7%A7%BB%E9%99%A4%E5%A4%96%E9%94%AE/1662897034386.png)





### 参考文档：

https://www.cnblogs.com/lludcmmcdull/p/13502567.html

http://t.zoukankan.com/guodf-p-9682201.html

