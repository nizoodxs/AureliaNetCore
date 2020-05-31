# AureliaNetCore
aurelia for front end and dot net core for api

The api is written with net core 3.1. It includes a single controller for REST endpoint for a virtual company managing applicants. 
The dot net (api) part contains three different projects for data persistent layer, model/model validation and web api rest endpoints.
The primary focus for this project is on these technologies

**BackEnd**
1. Swashbuckle v5 => for ap description. The location set for this project is "localhost:50001/swagger". 
    It contains definition for the api endpoints for the http verbs get,post,put and delete.
    Example data are present SwaggerUI using "https://github.com/mattfrear/Swashbuckle.AspNetCore.Filters"
2. Fluentvalidation for model validation.
3. Netcore logging implementation via serilog. Used serilog rolling file sink. 
   The name for rolling file sink is configurable in the applicationsettings.json
4. Net core Entity Framework in memory database.
5. CORS policy implementation

**FrontEnd**
1. Front end forms using aurelia.js with event triggers
2. All codes are in typescript.
3. Webpack implementation
4. Bootstrap form renderer for aurelia
5. Front end fluent style validation using aurelia-validation
6. External api consumption using http-fetch-client


Coming up:
1 i18n => i18next implementation for aurelia
