# cqrsEventSourcingRabbitMQ
Descriptions:
- EventSourcing using MediatR, then publish the entity update events to RabbitMQ, using a console app to log

Stacks :
- ASP NetCore 2
- Entity Framework Core 2
- Postgresql
- CQRS
- RabbitMQ 
- MediatR

How to run:
- restore nuget package.
- update db with EF core migration
- run the script in IntrastructureLayer/SetupScript for posgres and rabbitMQ
- build then set startup project to both PresentationLayer and Consumer (this is the logging console application for getting RabbitMQ messages )


