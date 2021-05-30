#### 1.创建一个springboot项目，`选择web模块`，删除src、 .mvn 、 mvnw 、 mvnw.cmd文件或者文件夹

#### 2.在当前项目上添加mavean项目（controller、service、dao、model）

#### 3.安装相应的依赖

#### 4.在在当前模块的pom.xml中使用其他模块的package，添加如下类似配置：

```
<dependency>
    <groupId>com.example</groupId>
    <artifactId>dao</artifactId>
    <version>0.0.1-SNAPSHOT</version>
    <scope>compile</scope>
</dependency>
```

#### 5.报错找不到main class ，当前模块没有主方法，需要在当前模块的pom.xml中添加如下类似配置：

```
    <build>
        <plugins>
            <plugin>
                <groupId>org.springframework.boot</groupId>
                <artifactId>spring-boot-maven-plugin</artifactId>
                <configuration>
                    <mainClass>com.fengpan.ParentApplication</mainClass>
                </configuration>
                <executions>
                    <execution>
                        <goals>
                            <goal>repackage</goal>
                        </goals>
                    </execution>
                </executions>
            </plugin>
        </plugins>
    </build>
```

#### 6.多个模块不能在相同的位置同时使用application.properties

