##git reset

### 回滚到上一次提交时的状态

```git
git reset --hard HEAD^       // 将本地的仓库回滚到上一次提交时的状态，HEAD^指的是上一次提交。
git reset --hard fc232ae     // 将其回滚到 fc232ae commit 时的状态
```

### 取消暂存区的文件

```
git reset HEAD              // 取消所有暂存区的文件
git reset HEAD -- <file>    // 取消最近一次提交到版本库的文件到暂存区,改操作不影响工作区
```



