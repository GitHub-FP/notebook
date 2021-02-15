##### 1.在使用namespace的store使用mapState获取state

```
方式1：
...mapState({
    searchInput: state => state.yourModuleName.searchInput,
  })

方式2： 
...mapState('yourModuleName',[
  'searchInput',
])
```

