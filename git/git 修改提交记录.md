## 修改最后一次注释https://www.jianshu.com/p/098d85a58bf1

如果你只想修改最后一次注释（就是最新的一次提交），那好办：
 `git commit --amend`
 出现有注释的界面（你的注释应该显示在第一行）， 输入`i`进入修改模式，修改好注释后，按`Esc`键 退出编辑模式，输入`:wq`保存并退出。ok，修改完成。
 例如修改时编辑界面的图：

![img](https:////upload-images.jianshu.io/upload_images/10019540-7544f6a6883728d3.png?imageMogr2/auto-orient/strip|imageView2/2/w/671/format/webp)

## 修改某一次注释https://www.jianshu.com/p/098d85a58bf1

1. 输入：
    `git rebase -i HEAD~2`
    最后的数字2指的是显示到倒数第几次  比如这个输入的2就会显示倒数的两次注释（最上面两行）

   ![img](https:////upload-images.jianshu.io/upload_images/10019540-0db9f307d45630e4.png?imageMogr2/auto-orient/strip|imageView2/2/w/775/format/webp)

   显示倒数两次的commit注释.png

   

2. 你想修改哪条注释 就把哪条注释前面的`pick`换成`edit`。方法就是上面说的编辑方式：`i`---编辑，把`pick`换成`edit`---`Esc`---`:wq`.

3. 然后：（接下来的步骤Terminal会提示）
    `git commit --amend`

4. 修改注释，保存并退出后，输入：
    `git rebase --continue`
    

   ![img](https:////upload-images.jianshu.io/upload_images/10019540-00d3c9acbce99abe.png?imageMogr2/auto-orient/strip|imageView2/2/w/471/format/webp)

   提示输入的命令.png

   

其实这个原理我的理解就是先版本回退到你想修改的某次版本，然后修改当前的commit注释，然后再回到本地最新的版本

### 