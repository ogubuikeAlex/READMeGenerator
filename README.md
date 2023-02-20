# ReadMe Generator

One of the greatest hassle for developers is writing readme's and documentation. 
We all really just want to design and build awesome applications.

ReadMe-Generator is a library that will automatically generate a detailed and well-formatted readme file for you.
Focus on writing the code, let ReadMe-Generator help you out with the documentation.

## Author

- [Girl King Alex](https://github.com/king-Alex-d-great)

## Badges

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)

## What ReadME Sections Get Generated?

- Project title and description
- Author's name and url to author's portfolio or social media page
- Tech Stack
- Code Documentation
- RoadMap

## Tech Stack

**C#, .NetStandard2.1**

## How Do I Get Started

First, install NuGet. Then, install ReadMeGenerator from the package manager console:

```C#
   NuGet\Install-Package ReadMeGenerator -Version 1.0.0
```

Or from the .NET CLI as:

```C#
   dotnet add package ReadMeGenerator --version 1.0.0
```

Finally, import into the file where you want to use it:

```C#
   using ReadMeGenerator;
```

## Doc Reference
_Tip: Using the readme generator works with some assembly attributes. For a cleaner code, you can optionally create a ```readme.cs``` file where you will store assembly based attributes._

### Table of Content
- **The Project Attribute** : Add Basic Project Details to ReadME
- **The Method Attribute** : Add Method Documentation to ReadME
- **The Environment Variable Attribute**: Add Environment Variables to ReadME
- **The TechStack Attribute** : Add TechStack to ReadME
- **The RoadMap Attribute** : Add RoadMap to ReadME
- **Generate Read Me** : Generate README File

#
### The `Project` Attribute: Add Basic Project Details to ReadME
The assembly-level project attribute allows you to add:
- Project Description
- Project title
- Author name
- Author portfolio URL
- How to deploy directive

#### How to use
Add this to the top level of either your `program.cs` or a dedicated `readme.cs` file:

```C#
   [assembly: Project
    (
        Description = "Automatically generate ReadMEs",
        AuthorProfileUrl = "https://www.linkedin.com/in/ogubuike-alex/",
        AuthorName = "Ogubuike Alex",
        ProjectName = "ReadMeGenerator",
        Deploy = "npm run deploy"       
    )
  ]

```
Do not forget to change the details to match yours though😅

| Parameter  |Description    |Is A Required Parameter                                            
| :--------- | :----- | :-
| `Description` | In few words tell us what your awesome project is about | Yes
| `AuthorProfileUrl` | This can be a link to your portfolio, github url or social medai page |No
| `AuthorName` | This is where you drop your legendary name | No
| `ProjectName` | This is the name of your project | Yes
| `Deploy` | Optional command for deploying your project| No

#
#
### The `Method` Attribute: Add Method Documentation to ReadME
This is a method-level attribute that allows you add:
- Method Description
- Input parameters
- Return Type
- How-to-use sample code

#### How to use 
Add this on top of the method to be documented:
- Method with two input strings
```C#
     
    [Method(
      output: typeof(Task<string>),
      input: new Type[] {typeof(int), typeof(string)},
      Description ="Get student name", 
      Example = "var result = await GetStudentName(1, "BASIC-ONE");")
      ]   
    public string GetStudentName(int id, string classCode)
    {
        //method body
    }

```
Do not forget to change the details to match yours though😅

| Parameter  |Description | Is a required parameter?                                                
| :--------- | :---------------------------------------------------------- |:-
| `Description` | What does the method do | No
| `Example` | String that shows how to use your method|No
| `output` | This is the your return type | Yes
|  `input` | An array of input parameter types | Yes

#
#
### The `EnvironmentVariable` attribute: Add Environment Variables to ReadME
The assembly-level environment attribute allows you to add environment variables that you want to appear in your readme document

#### How to use
Add this to the top level of either your `program.cs` or a dedicated `readme.cs` file:

```C#
   //declare like this
    [assembly: EnvironmentVariable(Key : "Stuff",  Value : "StuffValue")]
    //or like this
    [assembly: EnvironmentVariable("Two", "Number")]

```
Do not forget to change the details to match yours though😅

| Parameter  |Description | Is a required parameter?                                                
| :--------- | :---------------------------------------------------------- |:-
| `Key` | Environment Variable Key | Yes
| `Value` | Environment Variable Value|Yes

#
#
### The `TechStack` attribute: Add TechStack to ReadME
The assembly-level tech stack attribute allows you to add the tech stack for your awesome project

#### How to use
Add this to the top level of either your `program.cs` or a dedicated `readme.cs` file:

```C#
   [assembly: TechStack(".Net6.0", ".NetStandard2.1")]
```


| Parameter  |Description | Is a required parameter?                                                
| :--------- | :---------------------------------------------------------- |:-
| input | A comma-separated list of all the awesome tech you used| Yes

#
#
### The `RoadMap` attribute: Add RoadMap to ReadME
The assembly-level road map attribute allows you to add the road map for your project. 
##### Do you have awesome features or updates you wanna include in the project? Add it all here!

#### How to use
Add this to the top level of either your `program.cs` or a dedicated `readme.cs` file:

```C#
   [assembly: RoadMap("Add AI to the spice", "Add end-to-end test")]
```


| Parameter  |Description | Is a required parameter?                                                
| :--------- | :---------------------------------------------------------- |:-
| input | A comma-separated list of upcoming plans| Yes

#
#
### Generate Read Me : Generate README File
In your `program.cs` file.

- First import the library:

```C#
   using ReadMeGenerator;
```
- Next, get the assembly that contains code you want to document and then pass your assembly as parameter to `GenerateReadMe()` method

- Example:
```C#
   var assembly = Assembly.GetAssembly(typeof(readme.cs));
   GenerateReadMe(assembly);
```
- Run code to generate the readme file
- You can find the generated readme document in `..\bin\Debug\net7.0\ReadMe.Md` of current project.


## Roadmap

- Project-Type Based Generation
- Change generated text location 
- Section Customization
