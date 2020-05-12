### useEffect 相当于：

### 同步方法 ：useLayoutEffect

```
componentDidMount	组件已经挂载
componentDidUpdate	组件已经更新
componentWillUnmount  组件将要被卸载
```

### 解绑功能详解

###（1）每次都会执行destruction();

```react
useEffect(()=>{
    document.title = "hello";	//componentDidMount/componentDidUpdate时候执行
	return destruction();		//解绑,会在组件再次渲染的时候执行，去卸载副作用
})
```

###（2）组件销毁时执行 destruction()

```react
useEffect(()=>{
    document.title = "hello";
	return destruction();		//解绑		
},[])							//仅在组件挂载和卸载时执行
```

###（3）count 发生变化就会执行 destruction()

```react
useEffect(()=>{
    document.title = count;		
	return destruction();		//解绑
},[count])						//每次count发生变化的时候执行，useEffect内必须使用count
								//count必须是useState
```

*注意：以上将解绑函数取消掉，就表示没有解绑函数，不执行解绑操作。useEffect的其他操作照常按照以上规则执行*