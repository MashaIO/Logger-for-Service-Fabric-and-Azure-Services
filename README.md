# AzureSerficeFabricAndAppServiceLogger
Azure Diagnositics for Service Fabric(ETW) and App Service(Tablestorage provider extendable)

This simple application will help in logging service fabric application with ETW which will finally log into table storage. Also this log can be used for app service also (where ever required).

# Patterns and Features
1. Strategy and factory pattern implmented to decouple app service log and service fabric log in applications.
2. ARM template to create/associate table storage to service fabric.
3. Custom event added to App service(Table storage) and service fabric(ETW)
4. Extendable provider added for app service so that provider can be changed as per requirement.
5. BufferEventlistner will help in listen to events and log it to the table storage.
6. In test folder show cased how to use the logger in both Service fabric and app service.
