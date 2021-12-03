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
  13. 

