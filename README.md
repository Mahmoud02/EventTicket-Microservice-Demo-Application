# EventTicket-Microservice-Demo-Application
This Demo Project presents how to architect a microservice-based application and show how to organize the communication between different microservices 
as well as the different front-ends.

## The Business Case
Eventticket is a company that is selling tickets for events like concerts and musicals. In this Demo, we're building a complete solution that handles ticket sales. 
1. On the website, users can view events and filter them by category. 
2. They can also check out the details for an event and order tickets for it. 
3. The tickets are placed in a shopping basket. 
4. And when done shopping, users can pay for the tickets in the basket.  
5. there are three **distinct pieces of functionality**
   - the event catalog, 
   - the shopping basket 
   - handling payment for an order. 
6. There are also some **non functional requirements** for this application. 
   - Certain events are hugely popular, especially on the day where the tickets become available for the first time. 
   - That's why Eventticket needs the app to scale in a flexible way. 
   - They don't want to pay for computing. resources on a slow day, but need high availability on a busy one.
   - And only the parts of the application that receive the higher load should scale up when needed. 
   - Each part of the application should be deployed independently from the rest of the parts using continuous delivery. 
   - One of the reasons is that there will be separate teams on different locations working on each part in their own pace. 
   - The functionality of this solution is not fixed, so it must be easy to add functionality. 
   - And last but not least, once an order is placed, the chance of it getting lost must be a smallest possible. 
   - We want both customers and the boss to be happy. 

## Why a Microservices Architecture?
### With microservices architecture, these services are small and micro. Why is that? 
- Well, the smaller a component is, the easier it is to produce and maintain. At the same time, when you put all these small components together, you get something great. 
- Each service is independent. When a new version of a service is ready and it can't be deployed because other services has to change first, the service is not independent. 
- Maybe the most important aspect of using a microservices architecture is that multiple teams can work separately on each service. 
- The independency of a microservice makes that possible, and the size of it makes it manageable. 
- Finally, each service must be responsible for exactly one task. In other words, it must be one distinct component, not do other things on the side.
- Once you have identified the components for your application, it can be as easy as creating a microservice for each of them.

## From Monolith to Microservices
- A monolith, which is basically a non‑distributed application. That means that all logic is contained in one application, not in several microservices.    
- It is the oldest architecture around, but it's still something you should consider when designing an application. And that's because there are benefits to it:     
  1. A monolith is very easy to deploy. 
  2. It's just one project instead of potentially dozens of microservices.  
  3. That makes it also easier to test. 
  4. Coordinating many microservices to do **an integration test**, for example, is way harder than having to deal with just one thing.
- A monolith is well known. Every  developer starts with building monoliths because they are easy to comprehend. 
- A monolith typically doesn't do HTTP calls or other calls over the wire except to the database perhaps. All calls are to **internal classes**, which make it faster. 
- When building a monolith using separate components using the solid principles, internal modules can be independent from others too, **But let's face it, we developers tend to violate the modular structure of an application**
  1. implementing all kinds of cross‑calls to modules. 
  2. We start out with good intentions, but end up with a need for a rewrite because the architecture turned to spaghetti. 
  3. With microservices, there is a physical separation between services, so this problem is less likely to occur. 
- **when monoliths get bigger**, they get more and more complex. No matter how much you do your best to structure the code using layers and other patterns, you'll eventually come to the point where if you fix or add something in one part of the application, some other part of the application breaks. This can be mitigated writing unit tests, but you will end up with tons of them, making the application even more complex and harder to maintain.
- Scaling out a monolith is possible, but you can only scale the whole thing, which can make scaling out very costly an/or inefficient.
- updating to new technologies is also costly and time consuming with monoliths. Let's say we want to update a monolith to a new major version of .NET. Since we can't update piece by piece, we have to fix compatibility issues for the application as a whole. 
- a monolith is hard to work on with multiple teams. The hardest part about creating software is the way people collaborate. The more people, the harder it is to coordinate the work effort.
- Deployment of new versions is also an issue here because a new release can only be done when every team is ready, so a release must be highly coordinated, making deployment hard and continuous deployment very difficult.

## Reasons Not to Use Microservices
- If the application will remain small, In these cases, I would totally recommend using a monolith. 
- The biggest downside of using microservices architecture is that understanding all parts together.
- the whole application is harder, while dividing up the application into small microservices, each individual microservice is easier to get. But the more microservices you have, all communicating using synchronous and asynchronous communication, the harder it will get to really know how the application sticks together as a whole, keeping track of which message is received by which microservice and where messages went, for example, is hard.
- Deploying and monitoring microservices is also no walk in the park and will require a high degree of automation to get it right each time. 
- A team must take care of the automation for the microservices it is responsible for, requiring more skills than just coding in the team. 
- And finding out the boundaries of a microservice is a hard and continuous process that requires discipline.
- It's easy to start out with the microservices architecture, but end up with a distributed monolith. The teams should evaluate constantly if a microservice is still a microservice independent of the others. 
- Also, maintaining referential integrity like we use in a relational database is not possible across microservices. 
- monoliths are hard to maintain because of their complexity, and microservices are hard to deploy and monitor because of their complexity.
- **The complexities around microservices are harder to tame** than the complexities around monoliths. Choose microservices when you have a really good reason to. If not, use monolithic architecture.

## Deploying Microservices: Containers 
Because we want to automate deployment and scale a microservice, it's important to have it as portable as possible.  
Wouldn't it be great if we could stick every microservice with all its dependencies, including .NET Core, in a package that can move from one system to another in a safe and reliable way? Well, we can.  
That is what the **container** is, and **Docker** is a tool that helps with building and running containers. 

## Microservice Communication
Communication between different services can be done in different ways.  
####  If we take a step back and think of a "regular application" built with C#, so not built with microservices in mind that is, what do we have in the end?  
  We have probably a large set of classes or components. They interact with one another through, indeed, method calls, so, we instantiate a new object and then we call a method   on that object, all regular stuff.  
  It could be that we bring in a separate component through, for example, a NuGet package, and although that code lives in a different package, it's still using the same flow.     **We'll always rely on method calls.** 
#### If we now shift gears and start thinking of how a microservices application is working?
it's indeed going to be a set of pretty much standalone applications really, probably APIs.  
So, while in a regular application, the calls all happen in memory of the executing application, that's going to shift when moving to microservices.  
We'll need inter‑process communication options to let the different applications or microservices communicate with each other.  
In general, we can distinguish two types of communication that we'll have between our different services, **synchronous and asynchronous communication**. 
#### synchronous communication
- is a  communication where we have a request which is sent by the sender. 
- The request is received by a target service, this will trigger the execution of code on that target service. 
- Perhaps that service needs to search for a given ticket, in our case. Once found, it will send back a response to the original sender. 
- All the time in between, the sender has been waiting for a response. 
- The sending service cannot continue with the work until a response is received from the target.
- For inter‑service communication, so, between different microservices, gRPC is a good choice since it provides higher speeds.
- synchronous communication might not always be, well, the best solution
  1. If you have been paying close attention, one thing that's clearly visible is that all services performing communication this way have a lot of knowledge about each other. 
  2. When a shopping basket comes with a discount service, it really needs to know about the discount service, and then it will make a direct call,in our case.
  3. maybe this is already triggering some kind of alarm in your brain. We are indeed creating tight coupling between our different microservices, which is far from ideal, and is in fact breaking one of the premises of using microservices architecture in the first place.
  4. the communication we have done(ex: between Basket Service and Discount Service) so far has always been point to point, one service to the other.
  5. When using microservices, we'll want to have multiple services to get notified about something happening in one service. Creating one too many implementations are going to be pretty hard to create this way.
  6. If we need to do this with **synchronous communication**, every time a new service will be added and it needs to be updated about something happening in the system, we'll have to make changes to the code to call into that new service as well. 
  7. That is far from great. If one microservice will be responsible to update many others making direct communication calls, this will introduce a possible bottleneck in the system since a lot of messages might need to go through, and this could potentially take a long time.
  8. So adding new services becomes harder and harder, and potentially, over time, could stress the system in places where we didn't notice in the beginning.
  9. Finally, since we'll have one service calling the next, and that one onto the next one, and so on, and so on, errors that happened down the chain will be hard to catch. 
  10. Indeed, if we just rely on synchronous communication between our different microservices, so where one microservice will call into the next one, and that one will call in the next one, you get the picture, we will end up with a very long chain of synchronous calls. 
  11. Now, not only will this take a long time while the caller, possibly the client application even, needs to wait. Another issue might be that if one service fails, it will break the entire chain. 
  12. The entire system is only as strong as the weakest link in the chain. So while synchronous microservices communication will work for simple calls, like the one we have between the shopping basket and the discount microservice, it might not work for larger systems where we have a lot of microservices working together, triggering each other to execute an action. 
  
#### Asynchronous communication
The asynchronous in asynchronous communication that we're looking at here comes from the fact that we're not having a request/response cycle where the sender is waiting. 
Now in fact, the asynchronous part will focus mostly on the fact that we have a broker and messages will be sent between different microservices. The broker will be the facilitator for this.  
in another way, The c message sender usually doesn't wait for a response. It just sends the message to **message broker**..

## Asynchronous Messaging with RabbitMQ
RabbitMQ is one of the leading open source messaging platforms available to enterprise developers today. RabbitMQ has proven to be a stable, robust, and highly scalable solution, and is powering some of our largest enterprises.  

RabbitMQ is available from rabbitmq. com. RabbitMQ implements the AMQP protocol, which stands for the Advanced Message Queue Protocol. The RabbitMQ server itself is written in the Erlang programming language, which was initially designed for the telecoms industry by Ericsson.  

The RabbitMQ server is a message broker that acts as a message coordinator for the applications that you want to integrate together. This means that you can give your systems a common platform for sending and receiving messages.

Messages sent into RabbitMQ will be persisted to this to make sure they are not lost if the server ever has to be restarted; and RabbitMQ is the facility to send message delivery acknowledgements to the sender of the message so they are safe in the knowledge that their message was received and persisted.  

Then we have routing. The way RabbitMQ works is by sending your messages through to exchanges before they are persisted onto a queue. There are many types of exchanges that enable you to perform routing, but you can also perform even more complex routing by binding exchanges together.  

there is the management's web user interface. RabbitMQ comes of a management interface that runs in the browser. This interface lets you manage users, permissions, queues, exchanges, bindings, and much more.  

## Client-to-microservice Communication
- Direct client‑to‑microservice communication isn't always the best solution there is. Assume that your next task is adding a mobile application, like a Flutter application.  
- If we use direct client‑to‑microservice communication, it means the Flutter app will now need to directly call the services as well.  
- the logic that we have in the Web application will also need to be written in the Flutter application. 
- While that's not great, we can't just duplicate it, because mobile apps have different needs than a server‑side  application. 
- **We have exposed on our microservices HTTP endpoints that we can connect with from Website or from Flutter.** 
- It might be so that your microservices will expose an endpoint that is using a protocol that cannot be used from client‑side technologies.   
- We are introducing tight coupling between the client and the services. The client needs to know a lot about services, and it also needs to make calls directly into the services. 
- If we change something to the API service of our microservices, this can have an impact on the client applications as well.  
 - Now, if client‑side apps need to directly invoke the microservices, so basically, into the heart of our microservices architecture, we definitely need to think about security implications this brings with it. 
 - If all our microservices are directly exposed to the outside world, this means we need to put strict measures in place for all services individually.  
 - the client‑side applications, due to tight coupling, could be impacted by changes on our microservices. If we make changes on the API calls, so internally for our services, the apps using these API endpoints will also be impacted. 
 - if we have a client‑side application such as an Angular or a Xamarin application using our microservices directly, it may be so that a lot of individual round trips will be required, which can have an impact on the performance of the application. 
 -  
 ## Gateway
 Introducing the solution, a gateway. What we'll be bringing in is basically a new service that sits in front of our microservices, providing a single point of entry to the services. It's now basically a controlled access we are exposing. Only through this entry can client apps, even the server‑side ones, access the microservices. Because now all requests go through this one service, it can also contain shared functionality. Things like authentication can now definitely be added on this one service, the gateway, that is, and thus all services are now protected in one go. Adding a gateway is extra work, but it's mostly useful if your application is somewhat larger. 
 
 First, there we go again, tight coupling. Indeed, just like we had possible coupling between the clients and the microservices, we can now also start having tight coupling between the gateway and the services. Now since it is still just limited to one layer, in this case, the impact, in my humble opinion, is a bit less. However, adding a gateway means that you are introducing an extra layer. That comes with some downsides in turn, such as speed and basically also an extra layer to develop, to test. Your debug sessions might also get slightly more complex, so it's definitely something to keep in mind. Now the gateway is the entry point to the services, and it's in fact also a service. It needs to be developed, and in a larger application, it's typically owned by a team. It could become even a development bottleneck and perhaps, more importantly, scaling. Since all interactions with the microservices are now going through this gateway.  
 ### Adding Different Clients and Gateways
But with a gateway in place, one can start to wonder if this is the ideal situation. Sure, if we only have an MVC application, then we can tailor the gateway to what that MVC application will need. But, what if we decide to bring in a mobile application? Inherently, a Xamarin application works differently. Perhaps the Xamarin app needs the data returned by the gateway in a different way. Then maybe the Xamarin app will need to make different calls to the gateway and start puzzling different responses together.
All of these, well, so‑called clients might prefer answers from the gateway in a different way. We have already established that adding all these different requests in one single gateway is far from ideal. You'd be creating a new monolith on top of all your microservices. Instead, what I think is a better approach is simply introducing different gateways specific for the client. 
What we are doing here is indeed creating a different back end for different front ends. That's why this approach is often referred to as the BFF, the back end for front end. This offers the highest level of flexibility while still hiding the real microservices from the front ends. Indeed, some code might be duplicated, but again, that outweighs the disadvantages we'd get otherwise.
 
