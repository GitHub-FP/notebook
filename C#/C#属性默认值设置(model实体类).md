##  如果是C#  6.0

```
public class Test{
 
public int X { get; set; } = 100;
 
public string Y { get; set; } = "test";
 
}
```

##  如果语法不支持，只能改回.net 2.0的写法。 

```
public class UserType
    {
        private int _UserType = 1;
        public int UserTypeID 
        {
            get
            {
                return this._UserType;
            }
            set
            {
                this._UserType = value;
            }
        }
}
```

