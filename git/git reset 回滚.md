### Git Reset hard误操作回滚恢复代码


昨天晚上做项目的时候，误操作将Git服务器上的代码Reset hard回到了之前的分支上，导致一天写好的代码找不到了。本以为已经没有办法找回原来的代码了。从网上搜了下，发现可以进行回滚操作。

#### 一、选择.git文件夹所在文件夹 

如图所示即SteamPipelineManagement文件夹

![img](mdimg/git%20reset%20%E5%9B%9E%E6%BB%9A/20170303093635066.png)

#### 二、选择SteamPipelineManagement文件夹，右键选择 Git Bash Here，弹出



#### 三、输入git reset --hard *

注意：这里的*代表你想要恢复的log号。log号可以在.git文件夹中找到，如图：

 ![img](mdimg/git%20reset%20%E5%9B%9E%E6%BB%9A/20170303094008731.png) 

我想恢复liubaobin_branch中的分支，则打开liubaobin_branch文件，如图：

 ![img](mdimg/git%20reset%20%E5%9B%9E%E6%BB%9A/20170303094302329.png) 

蓝色标注的即为log号。

#### 四、输入命令行后，show log即可看到之前reset hard丢失的分支已经恢复了。

请注意，log日志文件据说只会保留30天，如果想要恢复，请尽快恢复文件。



原文链接：https://blog.csdn.net/u011450490/article/details/60119210