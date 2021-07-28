```
UPDATE nu 
SET nu.[NoticeRead] = 1
    ,nu.[NoticeReadTime] = SYSDATETIMEOFFSET()
from [dbo].[Ptl_NoticeUser] nu
INNER JOIN [dbo].[Ptl_Notice] n ON nu.NoticeId = n.NoticeId and n.[NoticeType]='Message'
WHERE nu.[UserId] = 'z'
```

