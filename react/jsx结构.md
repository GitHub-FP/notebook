### jsx结构

####   const div = <div></div>

![1605684741103](C:\Users\fengpan\AppData\Roaming\Typora\typora-user-images\1605684741103.png)

#### Loadable引入的懒加载JSX

```
const TagGroup = Loadable({ loader: () => import('../../pages/Tag/TagGroup/index.jsx'), loading: Loading, delay: 150 });
const TagGroupComponent= <TagGroup />
```

![1605684822367](C:\Users\fengpan\AppData\Roaming\Typora\typora-user-images\1605684822367.png)

