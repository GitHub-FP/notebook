### 3.5.35-community

docker 运行

```
docker pull neo4j:3.5.35-community
```

```
docker run -d --name neo4j_name -p 7474:7474 -p 7687:7687 -v  D:\SoftwareInstallation\docker\neo4j\data:/data -v D:\SoftwareInstallation\docker\neo4j\logs:/logs -v D:\SoftwareInstallation\docker\neo4j\conf:/var/lib/neo4j/conf -v D:\SoftwareInstallation\docker\neo4j\import:/var/lib/neo4j/import neo4j:3.5.35-community
```

```
CREATE (n1:class {name: "大英Ⅱ"}) 
CREATE (n2:class {name: "大英Ⅲ"}) 
CREATE (n3:Class {name:'大英Ⅳ'}) 
CREATE (m1:profession {name:'计算机专业'})
CREATE (m2:profession {name:'软件工程'})
CREATE (m3:profession {name:'大数据'})
CREATE (m4:profession {name:'信管'})
CREATE (n1)-[r1:BASIC]->(n2)
CREATE (n2)-[r2:BASIC]->(n3)
CREATE (m1)-[r3:REQUIRE]->(n1)
CREATE (m2)-[r4:REQUIRE]->(n1)
CREATE (m3)-[r5:REQUIRE]->(n1)
CREATE (m4)-[r6:REQUIRE]->(n1)
RETURN n1, n2,n3,m1,m2,m3,m4
```

```
MATCH (m3:profession {name:'大数据'})-[*1..34]->(result) return result
```

### Latest

```
docker run -d --name neo4j_latest -p 7474:7474 -p 7687:7687 -v  D:\SoftwareInstallation\docker\neo4jlatest\data:/data -v D:\SoftwareInstallation\docker\neo4jlatest\logs:/logs -v D:\SoftwareInstallation\docker\neo4jlatest\conf:/var/lib/neo4j/conf -v D:\SoftwareInstallation\docker\neo4jlatest\import:/var/lib/neo4j/import neo4j:latest
```

