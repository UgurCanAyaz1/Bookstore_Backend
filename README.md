# Clone project to local

```bash
git clone https://github.com/UgurCanAyaz1/Bookstore_Backend.git
```

# Installation

## Run following commands

```bash
dotnet run # this will start the backend server
```

# Now go to 
http://localhost:5234/Swagger/index.html to see the endpoints

# Important Points!
## Must be run together with "bookstore_frontend" project

Link: https://github.com/UgurCanAyaz1/bookstore_frontend

## DBBackup Folder consists a database which is ready to use 

## Database has following users:

| User Name   | Password    |
| ----------- | ----------- |
| admin       | admin       |
| user        | user        |

where admin has the admin functionality and user is a default user.

## Testing Stripe Payment Api

To test the stripe payment api, you use one of the provided test cards, which can be found at 

https://docs.stripe.com/testing

One of the test cards that can be used is following:

5555555555554444 with any 3 digit for CVC and any future date for date section.
