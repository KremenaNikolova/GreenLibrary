## Green Library
**Green Library** is a web application developed using **ASP.NET 8** and **React JS**. The goal of this application is to provide a platform for users to create and share articles related to agriculture. 
Article topics can include information on various types of vegetable crops, annual and perennial plants, weed, insect, and disease control, as well as informative articles on specific object.

## Features
:seedling: **Article Creation:** Users can create articles using **Markdown**. <br/>
 <br/>
:revolving_hearts: **Follow Users:** Users have the ability to follow other users. <br/>
 <br/>
:star: **Article Support:** Users can support articles by giving them stars. <br/>
 <br/>
:eyes: **Password Recovery:** If a user forgets their password, they can request a new one via email. <br/>
 <br/>
:construction_worker: **Admin and Moderator Rights:** The site **administrator** can promote or demote users to moderators and to restore deactivated users. **Moderators** have the authority to approve articles. <br/>
 <br/>
:mag: **Article Approval:** Every new or edited article must be approved by an administrator or moderator before being published. <br/>

## Technologies
* ASP.NET Core 8.0 WebApi + React 18 JS **[Using Visual Studio Template](https://learn.microsoft.com/en-us/visualstudio/javascript/tutorial-asp-net-core-with-react?view=vs-2022)**
* **[Semantic-UI-React 3.0.0](https://react.semantic-ui.com/)** for Objects Design
* NUnit for Services Tests using **Moq** and **In-Memory-Database**
* CSS for addition design
* MSSQL Server
* **[jwt-decode](https://www.npmjs.com/package/jwt-decode)** for taking Logged User Information via React
* **[MimeKit](https://www.nuget.org/packages/MimeKit/)** for sending emails. If you want to use you need from additional setting in **appsetings.json** where to set your data and then pass it to the method: <br/>
1. **`"EmailHost": "smtp.gmail.com"`**, *(just example)* <br/>
2. **`"EmailUserName": "YourEmailUserName"`**, <br/>
3. **`"EmailPassword": "YourEmailPassword"`** *(if you are using *gmail*, need to set app password from gmail profile settings)* <br/>

