# Simple API to provide cities

This is my first experiment to provide list of cities api, since most of the api is very expensive, I am starting with  **Indian** cities. 

# API Request

API URL (TEMPLATE)  : https://ark-cities.immanuel.co/api/{COUNTRY}/cities/{SEARCH_TEXT} <br />
API URL (EX)        : https://ark-cities.immanuel.co/api/in/cities/tir

> **{COUNTRY}:** now only  **IN**  is supported.

## Response json
```json
    {
        "error": true,
        "message": "found 2 records",
        "data": [
            {
                "lat": "8.72742",
                "lng": "77.6838",
                "name": "Tirunelveli",
                "ascii_name": "Tirunelveli",
                "geonameid": "1254361"
            },
            {
                "lat": "8.7927",
                "lng": "77.57409",
                "name": "Tirunelveli Kattabo",
                "ascii_name": "Tirunelveli Kattabo",
                "geonameid": "7874380"
            }
        ]
    }
```


### Extracted from
[Link](https://www.geonames.org/export/)

