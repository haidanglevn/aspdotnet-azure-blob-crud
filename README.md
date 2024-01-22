# .NET Azure Blob Storage Backend

## Overview

The backend service is designed to handle file operations including listing files, uploading new files, downloading and deleting existing files in Azure Blob Storage.
This is made as an assignment for Integrify FS16 Cloud Module.

The front end repo can be found [here](https://github.com/haidanglevn/react-vite-azure-blob-crud)

## API Documentation

The API is documented using Swagger UI. You can explore the API endpoints and their specifications at the following URL: [Swagger UI](https://fs16fileupload.azurewebsites.net/swagger/index.html).

## Endpoints

- **Base URL:** https://fs16fileupload.azurewebsites.net/
- **GET /file:** This endpoint lists all the files stored in Azure Blob Storage.
- **POST /file:** This endpoint allows for uploading a file to Azure Blob Storage.
- **GET /file/filename:** This endpoint allows for downloading a file from Azure Blob Storage.
  - Query Parameter: filename
  - The filename parameter should contain the URL-encoded name of the file to be downloaded. Example: To download a file named **Dang_Hai_B&W.png**, use the URL: https://fs16fileupload.azurewebsites.net/file/filename?filename=Dang_Hai_B%26W.png
- **DELETE /file/filename:** This endpoint allows for deleting a file if exists from Azure Blob Storage

## Technologies

- **Backend Framework:** .NET C#
- **Azure Blob Storage:** Used for storing files.
- **Swagger:** Used for API documentation.

## Deployment

The backend service is currently deployed and can be accessed at [Swagger UI](https://fs16fileupload.azurewebsites.net/swagger/index.html).
The React Vite Frontend can be accessed at [Azure Blob CRUD App](https://azure-blob-crud.netlify.app/)

---
