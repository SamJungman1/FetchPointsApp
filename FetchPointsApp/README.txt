This page includes startup documentation for the application FetchPointsApp
If this application is to be run in visual studio, this can just be loaded and ran as a standard visual studio solution.
Otherwise, this application can be loaded using any application or development environment that can compile and run C# code. Start with Startup.cs and everything should launch properly from there.

To test the endpoints, any api testing application can be used (Ex. postman). I would disable ssl certification verification to test.
The endpoints can be hit on https://localhost on port 44358. For ease of use, I have pasted the three endpoints and their type below

POST: https://localhost:44358/api/points/addtransaction - Add Transaction
POST: https://localhost:44358/api/points/spendpoints - Spend Points
GET:  https://localhost:44358/api/points/pointsbalance - Check Balance of Payers
