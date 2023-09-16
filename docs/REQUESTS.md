# 🍔 Food Facilities Web API Requests

This Web API is a REST API. All the requests are based on HTTP protocol, and use JSON as data transfer objects. Each endpoint will be described below:

A swagger is also available at the path `/swagger/index.html` once the Web API is running.

## Get Permits by Applicant

### Request
**Method**: `GET` | **Path**: `/api/permits/applicants/{applicant}?status={status}`

`applicant`: Required
`status`: Optional

<details>
  <summary>Request example (click to expand)</summary>

  ```curl
  curl -X 'GET' \
    'http://webapi.url/api/permits/addresses/Street%20Meet?status=REQUESTED' \
    -H 'accept: */*'
  ```
</details>

### Response:
**Status**: 200

**Body**:
```json
[
  {
    "id": "1621287",
    "applicant": "Street Meet",
    "facilityType": "Truck",
    "cnn": "13057001",
    "locationDescription": "VALENCIA ST: 14TH ST to ROSA PARKS LN (300 - 337)",
    "address": "306 VALENCIA ST",
    "blockLot": "3546001",
    "block": "3546",
    "lot": "001",
    "currentPermit": "22MFF-00033",
    "status": "REQUESTED",
    "x": "6006013.135",
    "y": "2107762.89",
    "latitude": 37.767855269354264,
    "longitude": -122.42248440416165,
    "schedule": "http://bsm.sfdpw.org/PermitsTracker/reports/report.aspx?title=schedule&report=rptSchedule&params=permit=22MFF-00033&ExportPDF=1&Filename=22MFF-00033_schedule.pdf",
    "received": "20220602",
    "priorPermit": "0",
    "expirationDate": "2021-01-15T00:00:00.000"
  }
]
```

---

## Get Permits by Address

### Request
**Method**: `GET` | **Path**: `/api/permits/addresses/{address}`

`address`: Required

<details>
  <summary>Request example (click to expand)</summary>

  ```curl
  curl -X 'GET' \
    'http://localhost:5000/api/permits/addresses/HOWARD' \
    -H 'accept: */*'
  ```
</details>

### Response:
**Status**: 200

**Body**:
```json
[
  {
    "id": "1658693",
    "applicant": "Bonito Poke",
    "facilityType": "Truck",
    "cnn": "107000",
    "locationDescription": "01ST ST: HOWARD ST to TEHAMA ST (200 - 231)",
    "address": "505 HOWARD ST",
    "blockLot": "3736183",
    "block": "3736",
    "lot": "183",
    "currentPermit": "22MFF-00071",
    "status": "REQUESTED",
    "x": "6013786.26714",
    "y": "2114936.80369",
    "latitude": 37.78798864899528,
    "longitude": -122.39610066847152,
    "schedule": "http://bsm.sfdpw.org/PermitsTracker/reports/report.aspx?title=schedule&report=rptSchedule&params=permit=22MFF-00071&ExportPDF=1&Filename=22MFF-00071_schedule.pdf",
    "received": "20221109",
    "priorPermit": "0",
    "expirationDate": "2022-11-15T00:00:00.000"
  }
]
```

----


## Get Permits by Coordinate

### Request
**Method**: `GET` | **Path**: `/api/permits/latitude/{latitude}/longitude/{longitude}?status={status}`

`latitude`: Required
`longitude`: Required
`status`: Optional

<details>
  <summary>Request example (click to expand)</summary>

  ```curl
  curl -X 'GET' \
    'http://localhost:5000/api/permits/latitude/37.71/longitude/-122.47?status=REQUESTED' \
    -H 'accept: */*
  ```
</details>

### Response:
**Status**: 200

**Body**:
```json
[
  {
    "id": "1163792",
    "applicant": "SOHOMEI, LLC",
    "facilityType": "Truck",
    "cnn": "12583000",
    "locationDescription": "THOMAS MORE WAY: BROTHERHOOD WAY \\ CHUMASERO DR to SAN FRANCISCO GOLF CLUB RD (1 - 99)",
    "address": "1 THOMAS MORE WAY",
    "blockLot": "7380027",
    "block": "7380",
    "lot": "027",
    "currentPermit": "18MFF-0028",
    "status": "REQUESTED",
    "x": "5991430.02662",
    "y": "2086996.77488",
    "latitude": 37.710003334289446,
    "longitude": -122.47141191312888,
    "schedule": "http://bsm.sfdpw.org/PermitsTracker/reports/report.aspx?title=schedule&report=rptSchedule&params=permit=18MFF-0028&ExportPDF=1&Filename=18MFF-0028_schedule.pdf",
    "received": "20180521",
    "priorPermit": "1",
    "expirationDate": "2019-07-15T00:00:00.000"
  }
]
```
