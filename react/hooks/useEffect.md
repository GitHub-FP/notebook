### useEffect 相当于：

```
componentDidMount	组件已经挂载
componentDidUpdate	组件已经更新
```

### 解绑功能详解

####（1）每次都会执行destruction();

```react
useEffect(()=>{
	return destruction();		解绑
})
```

####（2）组件销毁时执行 destruction()

```react
useEffect(()=>{
	return destruction();		解绑
},[])
```

####（3）count 发生变化就会执行 destruction()

```react
useEffect(()=>{
	return destruction();		解绑
},[count ])
```



