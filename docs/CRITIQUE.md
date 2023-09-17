# 🍔 Food Facilities Web API Critique

## What could be added

Two artifacts are missing on this project to it be considered fully production-ready:

- A log system. For example, a log package connected with Grafana or Datalust Seq.
- Integration tests. Important to validate correct dependency injection and test coverage for controllers.

## What could be improved

### Coordinates distances

Some requisites on the assessment were open to interpretation. Depending on what exactly the customer's necessity is, the search by coordinates could be improved in two ways.
The current distance is calculated by using the distance between two points on a Cartesian plane.

1. For a city size, a Cartesian plane solution is enough. If bigger distances are added to the API scope (country, worldwide), an improved formula considering the Earth curvature should be used instead.

2. If the distance needs to consider the path a person would walk, an integration with a map API provider (Google Maps API, Here Maps API, etc.) should be used instead.

In both scenarios, more complexity would be added to the application.
The formula considering the Earth curvature would require more processing, while integration with a map API provider would add more time to the request and costs ($) to keep the service running.

### DataSF API token

For this project usage expectation it was not necessary to get an API token from Data SF.

For a production solution that may require higher API throttling limits, an API token must be generate as described on [SODA Developes: Application Tokens](https://dev.socrata.com/docs/app-tokens.html) and added on request headers.

## Scaling

This Web API was created having in mind the increase of code base.
The idea of using the package MediatR is to make it easy for the developer to add more endpoints to the API.

You may notice that the API caches the data retrieved from the Data San Francisco API using a memory cache.
This can be reasonable when you have only one instance of the API running, but not for a multiple instance scenario (for example load balancing).

The fact the cache is stored locally on each instance could cause data inconsistency. When the cache from one instance refreshes, while the cache from the other instance is persisted, you can have scenarios of different responses being generated for the same input.

For a multiple instance scenario, a centralized cache engine should be used instead, like **Redis**.
