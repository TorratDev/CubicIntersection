# Web API in .NET 6 with DDD, Swagger, Docker and Two Endpoints

This is a sample web API application built with .NET 6 using the Domain-Driven Design (DDD) pattern. It also features Swagger documentation, a Dockerfile for containerization, and two endpoints: basic and pipeline.

## Endpoints

### Basic

The `basic` endpoint calculates the cubic intersection of two lines. It takes the following parameters:

- `a1`: the slope of the first line
- `b1`: the y-intercept of the first line
- `c1`: the z-intercept of the first line
- `a2`: the slope of the second line
- `b2`: the y-intercept of the second line
- `c2`: the z-intercept of the second line

The response will be a JSON object with the following fields:

- `x`: the x-coordinate of the intersection point
- `y`: the y-coordinate of the intersection point
- `z`: the z-coordinate of the intersection point

Example request:

```json
{
  "first": {
    "dimensions": {
      "x": 2,
      "y": 2,
      "z": 2
    },
    "center": {
      "x": 0,
      "y": 0,
      "z": 0
    }
  },
  "second": {
    "dimensions": {
      "x": 2,
      "y": 2,
      "z": 2
    },
    "center": {
      "x": 0.5,
      "y": 0.5,
      "z": 0.5
    }
  }
```

Example response:

```json
{
  "AreTheyColliding": true,
  "IntersectedVolume": 3.375
}
```

