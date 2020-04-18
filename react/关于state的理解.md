## 组件继承PureComponent

```
当state发生改变的时候会进行state浅比较，如果不相等就进行组件更新
```

## 组件继承Component

```
当state发生改变的时候会进行state更新，不管state是否相等
```

## 关于props 继承state的理解

```
如果组件的 props 是继承state的值，如果state没有更新，那么这个组件也不会更新
```

## 如果state的子组件发生更新是不会影响到父组件的

## setState 实现同步更新

![1587018843702](C:\Users\FP.LAPTOP-GU00DUT3\AppData\Roaming\Typora\typora-user-images\1587018843702.png)

## 关于diff 算法的理解

```
如上说法,虽然state 在相等的情况下发生了更新，但是不会影响到DOM 操作的修改。
因为diff算法是对DOM 的操作，通过虚拟DOM 比较去实现DOM的差异修改。
可以说：state为数据层，而diff为DOM层
```

