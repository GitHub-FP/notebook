#### 1.在main class (controller)模块中添加如下配置，删除父工程和其他模块的如下配置

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
        <!--        <resources>-->
        <!--            <resource>-->
        <!--                <directory>src/main/java</directory>-->
        <!--                <includes>-->
        <!--                    <include>**/*.xml</include>-->
        <!--                </includes>-->
        <!--            </resource>-->
        <!--            <resource>-->
        <!--                <directory>src/main/resources</directory>-->
        <!--            </resource>-->
        <!--        </resources>-->
    </build>
```

#### 2.在maven中找到父工程选择package打包

![1613469121227](C:\Users\fengpan\AppData\Roaming\Typora\typora-user-images\1613469121227.png)

