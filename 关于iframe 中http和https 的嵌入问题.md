## 关于iframe 中http和https 的嵌入问题

### https里面不能引用http页面

​		浏览器默认是不允许在HTTPS里面引用HTTP页面的，ie下面会弹出提示框提示是否显示不安全的内容，一般都会弹出提示框，用户确认后才会继续加载，但是chrome下面直接被block掉，只在控制台打出信息。 

### http 可以嵌入https /http