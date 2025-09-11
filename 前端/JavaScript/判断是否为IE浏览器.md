# 1 判断IE浏览器与非IE 浏览器

  IE浏览器与非IE浏览器的区别是IE浏览器支持ActiveXObject，但是非IE浏览器不支持ActiveXObject。在IE11浏览器还没出现的时候我们判断IE和非IE经常是这么写的

```
function isIe(){
    return window.ActiveXObject ? true : false;
}123
```

  兼容IE11判断IE与非IE浏览器的方法  

```
function isIe(){
    return ("ActiveXObject" in window);
}123
```

# 2 判断IE6浏览器

  从IE7开始IE是支持XMLHttpRequest对象的，唯独IE6是不支持的。根据这个特性和前面判断IE的函数isIe()我们就知道怎么判断IE6了吧。判断方法如下

```
function isIe6() {
   // ie6是不支持window.XMLHttpRequest的
    return isIe() && !window.XMLHttpRequest;
 }1234
```

# 3 判断IE7浏览器

  因为从IE8开始是支持文档模式的，它支持document.documentMode。IE7是不支持的，但是IE7是支持XMLHttpRequest对象的。判断方法如下

```
function isIe7() {
   //只有IE8+才支持document.documentMode
   return isIe() && window.XMLHttpRequest && !document.documentMode;
 }1234
```

# 4 判断IE8浏览器

  在从IE9开始，微软慢慢的靠近标准,我们把IE678称为非标准浏览器，IE9+与其他如chrome,firefox浏览器称为标准浏览器。两者的区别其中有一个是

```
alert(-[1,]);//在IE678中打印的是NaN,但是在标准浏览器打印的是-11
```

  那么我们就可以根据上面的区别来判断是IE8浏览器。方法如下

```
function isIe8(){
   // alert(!-[1,])//->IE678返回NaN 所以!NaN为true 标准浏览器返回-1 所以!-1为false
  return isIe() &&!-[1,]&&document.documentMode;
}1234
```

# 5 判断其他浏览器

  

```
/****来自曾经项目中封装的公共类函数***/
//检测函数
var check = function(r) {
        return r.test(navigator.userAgent.toLowerCase());
 };
var statics = {
        /**
         * 是否为webkit内核的浏览器
         */
        isWebkit : function() {
          return check(/webkit/);
        },
        /**
         * 是否为火狐浏览器
         */
        isFirefox : function() {
          return check(/firefox/);
        },
        /**
         * 是否为谷歌浏览器
         */
        isChrome : function() {
          return !statics.isOpera() && check(/chrome/);
        },
        /**
         * 是否为Opera浏览器
         */
        isOpera : function() {
          return check(/opr/);
        },
        /**
         * 检测是否为Safari浏览器
         */
        isSafari : function() {
          // google chrome浏览器中也包含了safari
          return !statics.isChrome() && !statics.isOpera() && check(/safari/);
        }
};
```