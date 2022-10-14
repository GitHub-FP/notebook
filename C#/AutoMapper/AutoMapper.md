### AutoMapper 使用介绍

```
CreateMap<GroupDto, Group>()
            .ForMember(dest => dest.GroupMembers, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => src.TenantId))
            .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.GroupName));
```

