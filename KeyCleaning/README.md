# Key Cleaning

- Setup a simple project in azure

```
az login
az account list --output table
az account set --subscription 'Visual Studio Professional Subscription'
az group create --location australiaeast --name azrg-aue-demo
az keyvault create --resource-group azrg-aue-demo --name azkv-demo
az keyvault secret set --vault-name azkv-demo --name demo-secret --value 1234
az ad sp create-for-rbac -n KeyScanningDemo --skip-assignment
```

- Gather the credentials from the result which can be used in the code

```
{
  "appId": "<APP ID>",
  "displayName": "KeyScanningDemo",
  "password": "<PASSWORD>",
  "tenant": "<TENANT>"
}
```

Create the policy so the account can access Azure

```
az keyvault set-policy --name azkv-demo --spn <APP ID> --secret-permissions backup delete get list set
az keyvault secret show --name demo-secret --vault-name azkv-demo
```

Build and Run
```
cd TestApp
dotnet build
dotnet run
```

- Update the program with the details

```
git add .
git commit -m "updated with a key"
git push
```
- Note that the password can be seen in the repository file.
- time passes.....  The error is noted and the code updated.
```
git add .
git commit -m "removed the key"
git push
```
- Check and see that the password is still available in the GIT history for the project.
- Edit the replacements.txt file with the values you want removed from history
- Remove the values from the program file, as this is history based
- Next run the replacement text to remove 

```
java -jar ..\..\tools\bfg-1.14.0.jar  --replace-text ..\replacements.txt -fi *.cs --no-blob-protection ..\..\.git
git reflog expire --expire=now --all
git gc --prune=now --aggressive
git pull
git push
```

- Check your Git History to find the value removed
