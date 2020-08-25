### git cherry-pick {commit-id}

 cherry-pick： 选择合并一个或者多个commit，比如要从A分支合并某个commit到B分支，A—>B,步骤如下

step 1: A分支下，git log获取某个commit-id
step 2: 切换到B分支，git checkout B(执行该命令前一定要保证A分支没有未提交的改动，或未stash的改动)
step 3: B分支下，执行 git cherry-pick {commit-id}
step 4: 执行git pull git push 将改动提交到远程分支 示例：我在A分支修改了某个文件，需要同时提交到A分支和B分支，操作如下 A分支下

```
git cherry-pick {commit-id}
```