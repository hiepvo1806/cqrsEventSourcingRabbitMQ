# cqrsEventSourcingRabbitMQ
I.Descriptions:
- Hi everyone, this is a sample project using microservices architect, which came from my own experience with reallife project and research. Although it still has some bugs,some side effect but it still can be useful while you have to design a system using microservice architect. If you have any question, please post it here. I will try to answer all the questions.

II. Techstacks
  1. CQRS 
  2. Event Sourcing: MediatR
  3. Service bus : RabbitMQ
  4. SignalR
  5. Gateway framework: Ocelot
  6. Client side : 
    6.1. AngularJs 6
    6.2. React

III. Roadmap
16/9/2018: 
  Before this point, in June, I've updated the project using these : cqrs,eventsourcing, angularjs 6, rabbitmq based on my research. After a few months later working in a real microservice project, I've spotted some interesting points ,which will be listed below. The solution is updated later by changing some point of the architect, I am currently working on these so be patient.
    1.using a event sourcing architecture, how can we check an entity that is conflict with other entities in business logic or keys?
      - the solution is keeping a event table and others current state tables in the write project
      - the read project will contain the denormalized version of these records
    2.when client sends a request for updating the records, then what will be in the reponse?
      - using event sourcing so we must accept that there will be a little delay between the old and new state of data. In this case, we can have a couple of solutions which has pros and cons : 
        2.1 Return the data of the new record state after updating the write stack: use when the delay in syncing between write and read is small, so when user refresh the client page, they will see the new data.
        2.2 Redirect the user to the readstack with an indicator, like job guid: the readstack receives the request, check whether the record in the job is updated, if true : return the data, if false : keep the request waiting or reponse something depend on the business logic.
        2.3 Using some sync mechanism between the client and the backend : signalR
      

IV.How to run:
- restore nuget package.
- update db with EF core migration
- run the script in IntrastructureLayer/SetupScript for posgres and rabbitMQ
- build then set startup project to both PresentationLayer and Consumer (this is the logging console application for getting RabbitMQ messages )


