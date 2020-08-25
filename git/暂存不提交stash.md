#### git stash

应用场景

- 有部分代码是多余的，想保留以后查看，但是又不想提交到远程分支
- 在进行多分支开发时，比如你在A分支上开发，但是突然发现B分支上有个bug需要修复，以前往往会把A分支上开发一半的功能本地commit，切换到B分支修复bug，然后再切换回A分支继续开发，这样往往log上会有大量不必要的记录。现在可以使用`git stash`将你当前未提交到本地（和服务器）的代码推入到Git的栈中，放心切换到B分支修复代码，完事儿后切换回A分支使用`git stash apply`将以前一半的工作应用回来；

添加暂存保留

```
git stash
```

 实际应用中推荐给每个stash加一个message，用于记录版本

```
git stash save "添加console.log"
```

 查看所有的缓存

```
git stash list
```

如果想将修改的内容重新释放出来

```
git stash apply stash@{x}			将编号x的缓存释放出来,记录不会被删除
git stash apply						当前分支的最后一次缓存的内容释放出来,记录不会被删除
git stash pop  						当前分支的最后一次缓存的内容释放出来,记录会被删除
```

如果你实在实在不想要本地的更改了，可以清除使用如下命令清除所有的stash栈

```
git stash clear
```

当然也可以使用`git stash drop`移除指定的stash栈

```
git stash drop stash@{1}
```