## 关于SVG设置背景图片IE10和11不能设置位置的问题

```
问题不在于CSS，而在于SVG。SVG将增长以填充整个元素框的背景（如预期的那样）。SVG规模如何成为控制因素：
在<svg>元素上设置viewBox ="0 0 width height"（以像素为单位）属性，并删除其width和height属性。您还需要在svg元素上设置prepareAspectRatio ="xMinYMid"（x /垂直左对齐，y /水平居中）。至少与Internet Explorer 10和11兼容。
```

```
<svg viewbox ="0 0 64 64"
    reserveAspectRatio="xMinYMid">
	…
</svg>
```

