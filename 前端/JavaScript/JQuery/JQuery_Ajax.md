## Post

```
$("#root").click(function(){
  $.ajax({
    //请求方式
    type : "POST",
    //请求的媒体类型
    contentType: "application/json;charset=UTF-8",
    //请求地址
    url : "/service/api/HtmlEditor/TestPage",
    //数据，json字符串
    data : JSON.stringify({"id":15}),
    //请求成功
    success : function(result) {
      console.log(result);
    },
    //请求失败，包含具体的错误信息
    error : function(e){
      console.log(e.status);
      console.log(e.responseText);
    }
  });
});
```

## Get

```
$("#root").click(function(){
  $.ajax({
    //请求方式
    type : "GET",
    //请求的媒体类型
    //contentType: "application/json;charset=UTF-8",
    //请求地址
    url : "/service/api/HtmlEditor/TestPage?Id=15",
    //数据，json字符串
    //data : JSON.stringify({"Id":15}),
    //请求成功
    success : function(result) {
      debugger;
      console.log(result);
    },
    //请求失败，包含具体的错误信息
    error : function(e){
      console.log(e.status);
      console.log(e.responseText);
    }
  });
});
```

