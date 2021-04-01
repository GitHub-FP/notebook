## CSS 英文、中文强制换行与不换行的代码

### *分类* [编程技术](https://www.runoob.com/w3cnote_genre/code)

- \1. **word-break:break-all;** 只对英文起作用，以字母作为换行依据
- \2. **word-wrap:break-word;** 只对英文起作用，以单词作为换行依据
- \3. **white-space:pre-wrap;** 只对中文起作用，强制换行
- \4. **white-space:nowrap;** 强制不换行，都起作用
- \5. **white-space:nowrap; overflow:hidden; text-overflow:ellipsis;** 不换行，超出部分隐藏且以省略号形式出现