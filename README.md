# CubicIntersection

[![Build and Test with Coverage](https://github.com/TorratDev/CubicIntersection/actions/workflows/dotnet.yml/badge.svg)](https://github.com/TorratDev/CubicIntersection/actions/workflows/dotnet.yml)

[![Coverage Status](https://coveralls.io/repos/github/TorratDev/CubicIntersection/badge.svg?branch=master)](https://coveralls.io/github/TorratDev/CubicIntersection?branch=master)

## Web API in .NET 6 with DDD

This is a sample web API application built with .NET 6 using the Domain-Driven Design (DDD) pattern.

[Web](https://cubicintersection-production.up.railway.app)

### Endpoints

- https://cubicintersection-production.up.railway.app/api/basic
- https://cubicintersection-production.up.railway.app/api/pipeline

#### Process

The endpoint calculates the cubic intersection of two cubic objects. It takes the following parameters:

- `first`: the first cubic object
  - `dimensions`: the dimension of the object
  - `center`: the center point of the object
  
- `second`: the second cubic object
  - `dimensions`: the dimension of the object
  - `center`: the center point of the object

The response will be a JSON object with the following fields:

- AreTheyColliding: Flag that tell if the two object are collinding
- IntersectedVolume: Value of the intersected volume between the two objects.

##### Request:

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

##### Response:

```json
{
  "AreTheyColliding": true,
  "IntersectedVolume": 3.375
}
```

