## Technologies used
- `.NET` as main framework
- `MediatR` for mediating requests (controller <> business logic)
- `Entity Framework Core` for storage
- `XUnit` for unit tests
- `Visual Studio` for IDE

## What this Web API does
This Web API fetches data from [Data SF (San Francisco)](https://datasf.org) about Mobile Food Facility Permits and applies custom filters to it.

This API uses HTTP protocol for communication, and the responses are formatted in JSON.

## Why MediatR
MediatR makes API implementation simple and clean, avoiding the necessity of injecting several dependencies in controllers.
It is a well-established package with more than 144M downloads from [NuGet.org](https://www.nuget.org/packages/MediatR) and is constantly updated by its maintainers to the latest versions of .NET.

MediatR has a big community, and several articles about its use, implementation, best practices, and troubleshooting, being a great choice for a real-use API. 

## Why Entity Framework Core
Entity Framework Core is one of the most known Database ORM from .NET ecosystem.
Because it is kept by the Microsoft team, it is constantly up to date.

Even more than MediatR, Entity Framework Core has a big community that can help with a variety of issues.

---

## The cache necessity

This API integrates with an external API from [Data SF (San Francisco)](https://datasf.org).

For a critical integration with a third-party application, it's always a good idea to cache information when possible.
Third-party applications could face downtime (as exemplified below), slowness, and be expensive ($).

Adding a cache from our side makes access to information way faster and more reliable. Some points should be considered:
- Can I cache this information? Some information cannot be cached at all, because the current value needs to be shown in real time.
- What is a reasonable time to keep the cache (seconds, minutes, hours, days, etc.)? Some information needs to be updated more frequently than others.
- Can I invalidate/refresh my cache according to actions? Some APIs will both read and update information from the source of the cache. In situations like that, it is required that the cache be invalidated when the information is updated on the source.
- Will the cache be useful? Sometimes you are caching something so specific that nobody will perform the same query again in a reasonable time.

For this API, the cache was proven a great advantage in avoiding downtimes and slowness from the source.
The information is not something that needs to be seen in real time, but a deeper understanding of a reasonable cache expiration time is still necessary.

.

![](https://blipmediastore.blip.ai/public-medias/Media_a001d340-8da8-4f7f-9c6d-6d28ab03644e)
> Note from Data SF on September 15th, 2023
