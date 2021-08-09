Microservice template using microsoft latest .Net 5

#TODO: Explain
* Microservices

   The microservices architecture takes this same approach and extends it to the loosely coupled services which can be developed, deployed, and maintained independently. Each of   these services is responsible for a discrete task and can communicate with other services through simple APIs to solve a larger complex business problem. The services are independent of each other and run in their own processes.
   ![image](https://user-images.githubusercontent.com/26070637/128610776-2d46cd4b-fdd0-4c1b-a8e5-4d6771e47bb7.png)


* Why Microservice template

   In microservice architecture style, we are expected to divide domain into several sub domain and each act as independent service. It is quite repeative work to setup service following best practices. To improve turn around time and effectively create service in matter of few mins. This template help you create microservice following domain driven design, command and query responsiblilty segregation and service bus to send events, so that other microservice remain in sync. 
   Solution is templatized, so you can easily replace place holder "__NAME__" and create personalized service in no time.
   
* How to use?

   To replace __NAME__ with your brand name of solution
  
      * Install warmup using cinst warmup
      * Add replacement token
            * C:\ProgramData\chocolatey\lib\warmup\bin
            * Add TextReplace section
                  * <add find="__NAME__" replace="ProjectName"/>
            * Or use below command
                  * warmup addTextReplacement warmup ProjectName
       * To get template project command
            * Maintain template project at C:\CODE\_templates\base
            * Create destination folder as "D:\XYZ"
            * Run command to get customized project
                  * warmup base ProjectName
* Components

   Service : Named as "__NAME__", act as microservice based on Microsoft lastest .net 5. 
* Communication between microservices
* Create microservice from template solution
* Benefits
* Pending Items
   - Unit Tests
   - API Auth using AAD 
