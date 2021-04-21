# Shrink-Ray: The URL Shortener
![enter image description here](https://img.shields.io/github/workflow/status/Abhid15/shrink-ray/.NET)

Shrink ray is an ASP.NET Core 3.X  based Web API that can shorten long URLs and store then in a MongoDB backend. Shrink Ray apart is based on C# and comes with 3 principle endpoints, global error handling and rich logging for better diagnosis.  With Shrink-Ray, you can :

 - **Get** all the  Short Url objects
 - **Create** a new Short URL 
 - **Navigate** or **Fetch** the Details of particular Short Url

## The Endpoints

### Get 
The Get endpoint is  the standard Get All endpoint of Shrink-Ray. It takes no arguments and returns all the Short Url objects from the MongoDB Cluster.

### Post
The Post endpoint is the Shrink-Ray endpoint to create Short URL. It takes the standard URL as an input and returns the Short URL and some other metadata. 

### GetSpecific
The GetSpecific endpoint doubles as the active endpoint that redirects Short URL to actual URL and also a get a specific Short URL object. This is really useful when the application needs to redirect to a specific link or use metadata to bind to any DOM element.

## How to Set Shrink-Ray Locally

### Pre-requisites


### Step 1 : Clone Repository
 
