# Shrink-Ray: The URL Shortener
![enter image description here](https://img.shields.io/github/workflow/status/Abhid15/shrink-ray/.NET)
![GitHub commit activity](https://img.shields.io/github/commit-activity/m/Abhid15/shrink-ray)
![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/Abhid15/shrink-ray)

Shrink ray is an ASP.NET Core 3.X  based Web API that can shorten long URLs and store then in a MongoDB backend. Shrink Ray apart is based on C# and comes with 3 principle endpoints, global error handling and rich logging for better diagnosis.  With Shrink-Ray, you can :

 - **Get** all the  Short Url objects
 - **Create** a new Short URL 
 - **Navigate** or **Fetch** the Details of particular Short Url

## The Endpoints

### Get  `/api/v{version}/ShrinkRayUrl`

The Get endpoint is  the standard Get All endpoint of Shrink-Ray. It takes no arguments and returns all the Short Url objects from the MongoDB Cluster.

### Post `/api/v{version}/ShrinkRayUrl`
The Post endpoint is the Shrink-Ray endpoint to create Short URL. It takes the standard URL as an input and returns the Short URL and some other metadata. 

> Request Schema :

```
{
  "longURL": "https://docs.docker.com/docker-for-windows/install/"
}
```

> Response Schema
```json
{
  "message": "Saved successfully",
  "success": true,
  "shrinkRayUrlModel": "http://localhost:8080/api/v1/ShrinkRayUrl/GetSpecific?shorturl=MbJ0T",
  "model": {
    "shortURL": "MbJ0T",
    "longURL": "https://docs.docker.com/docker-for-windows/install/",
    "createDate": "2021-04-21T18:09:43.0243045+00:00",
    "id": {
      "timestamp": 1619028583,
      "machine": 4026397,
      "pid": -18411,
      "increment": 11691947,
      "creationTime": "2021-04-21T18:09:43Z"
    }
  }
}
```

### GetSpecific `/api/v{version}/ShrinkRayUrl/GetSpecific`
The GetSpecific endpoint doubles as the active endpoint that redirects Short URL to actual URL and also a get a specific Short URL object. This is really useful when the application needs to redirect to a specific link or use metadata to bind to any DOM element.

> Response Schema
```json
{
  "shortURL": "MbJ0T",
  "longURL": "https://docs.docker.com/docker-for-windows/install/",
  "createDate": "2021-04-21T18:09:43.024Z",
  "id": {
    "timestamp": 1619028583,
    "machine": 4026367,
    "pid": -18411,
    "increment": 11691947,
    "creationTime": "2021-04-21T18:09:43Z"
  }
}
```

## How to Run Shrink-Ray Locally

### Pre-requisites

 - [Visual Studio 2019](https://visualstudio.microsoft.com/)
 - [ASP.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet/3.1)
 - [ASP.NET SDK 5.0.202](https://dotnet.microsoft.com/download)
 - [MongoDB Atlas Cluster for Database](https://www.mongodb.com/)
 - [Tortoise Git for Cloning Repo](https://tortoisegit.org/) (Any Git Shell equivalent will do.)
 - [Docker to Dockerize the API](https://docs.docker.com/docker-for-windows/install/)

#### Step 1 : Clone Repository
Clone Repository using Tortoise Git. Or using [Github CLI](https://cli.github.com/):

    gh repo clone Abhid15/shrink-ray
    
#### Step 2 : Ensure all Package dependencies are loaded and build passes

Open command prompt from Visual Studio or command prompt from the solution folder and run :

    dotnet build
Alternatively, use Visual Studio GUI to do the same.

#### Step 3 : Create MongoDB Atlas Cluster and link it to your Solution
MongoDB atlas provides a great alternative to having a cumbersome DB architecture. And nosql working perfectly for our purpose. Please follow the steps below to create your own cluster and link it to the local Solution.

 1. Go to [https://cloud.mongodb.com/user#/atlas/login](https://cloud.mongodb.com/user#/atlas/login) page, register if not registered before or login if you already registered.
 2. Click Create a cluster button, give your cluster an informative name for this project, select a cloud provider. Go with free tier.  :wink:
 3. Create a new user for you cluster access. It is not a good practice to use admin account.
 4. Also, if not done earlier, go to network access and Allow Access from Anywhere.
 5. After your cluster is ready, click Connect Your Application tab, Select `C#` as driver and `2.5 or Later` as Version. Note the connection string somewhere, we will be using it later in our API project.
 6. Finally, add the connection string from connect to short-url section of your created mongo db cloud cluster to `appsettings.Development.json`.

### Step 4: Run the API and Test Endpoints from Swagger

Swagger will start-up on [http://localhost:XXXX/swagger/index.html](http://localhost:XXXX/swagger/index.html) once you run the application. You can test the endpoints once that happened. If you encounter any errors, please go to `C:\temp` to go through the logs. 

### Optional Step 5: Dockerize your App

A docker file is provided with the application.  A brief explanation of the Dockerfile is :
 - Each dockerfile should start with `FROM` keyword. `FROM` sets the
   base image for dockerfile, you can either start from scratch or set a
   previous image for base image. 
 - `WORKDIR` command sets the current
   working directory. 
 - `EXPOSE` command exposes the ports we are going to    use. Here we
   will be using port 80 and going to map to another port    when
   running container.
 - In the second block, we are setting working    directory as `src` to
   working directory running restore command to    restore dependencies.
   Then building the API project by calling `RUN`   `dotnet build`.
 - Next block, we are publishing the project.  And In    the final
   block, setting the entry point of the container.

Next, we build and run the container. We do that with the following two commands. Use  a terminal or a command prompt from the solution folder to run these commands:
To Build: `docker build -t shrinkry:v1` 
`-t` parameter sets the tag name of the container, we name it as “shrinkry” and `:v1`  part is the version number.

To Run: `docker run -p 8080:80 shrinkry:v1`
`-p` parameter sets the port to be mapped, in this case we are mapping 80 (whi ch is exposed in dockerfile) to `8080`.

If everything goes according to plan, all your endpoints including Swagger should now be available at http://localhost:8080 . You can use the Docker GUI to see your container specs.
 
![image](https://user-images.githubusercontent.com/82752202/115612733-35592580-a309-11eb-87e9-b123b1c038b8.png)

Retrace any steps you feel have gone wrong and follow the tutorial carefully to understand every step! Happy Coding!
