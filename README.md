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
A monolith, which is basically a non‑distributed application. That means that all logic is contained in one application, not in several microservices
It is the oldest architecture around, but it's still something you should consider when designing an application. And that's because there are benefits to it. 
A monolith is very easy to deploy. It's just one project instead of potentially dozens of microservices.

