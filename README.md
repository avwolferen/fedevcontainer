# Run in dev container

1. Open bash
2. Run command
   ```sh
   dotnet run --project $CODESPACE_VSCODE_FOLDER/AspireSample/AspireSample.AppHost
   ```
3. Open the dashboard

The api service ran into an error. Use GitHub Copilot to get this fixed in the DataSetInitializer.

```prompt
Add a couple extra recipes that are secret. Oh, and please make sure you add the preparations to the current recipes because it results in a null reference exception.
```

Commit changes.


4. Open the apiservice
5. Navigate to the swagger ui, /swagger
6. Call the postcode action
7. valid postalcode: 8266AJ housenumber 19
8. Call the badpostalcode action
9. enter any postalcode, enter any housenumber but make sure to add a form of SQL injection to it. e.g.   1' or 1=1--
10. ![image](https://github.com/user-attachments/assets/9c6e31c5-0fed-464f-9812-5b2f5c57393a)




# Getting started with .NET Aspire and Dev Containers

This is a repository template to streamline the process of getting started with .NET Aspire using Dev Containers in both Visual Studio Code and GitHub Codespaces. Please refer to our product documentation on how to use these repository templates to get started.

- [.NET Aspire and GitHub Codespaces](https://learn.microsoft.com/dotnet/aspire/get-started/github-codespaces)
- [.NET Aspire and Visual Studio Code Dev Containers](https://learn.microsoft.com/dotnet/aspire/get-started/dev-containers)

> [!NOTE]
> Once you have created your repository from this template please remember to review the included files such as `LICENSE`, `CODE_OF_CONDUCT.md`, `SECURITY.md` and this `README.md` file to ensure they are appropriate for your circumstances.

# Code of Conduct

This project has adopted the code of conduct defined by the Contributor Covenant
to clarify expected behavior in our community.

For more information, see the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).
