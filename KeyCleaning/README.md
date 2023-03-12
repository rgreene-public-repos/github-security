# Key Cleaning

This demo is designed to show how a developer can easily commit their internal cloud keys to code without realising the security consequences.  When the key is part of history is then becomes a difficult process to re-write history.  Although this process works, the recommendation is that this is only needed when the key is a 3rd party <i>difficult to change</i> code.  For the majority of cases the developer should mearly rotate or regenerate a new key.

This demonstrates the process using an Azure KeyVault, so you will need a valid [login and profile](https://signup.azure.com/) in Azure; however the process will be similar for most services.

- Setup a simple project in Azure and obtain a key

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

Create the policy so the account can access Azure from the console application

```
az keyvault set-policy --name azkv-demo --spn <APP ID> --secret-permissions backup delete get list set
az keyvault secret show --name demo-secret --vault-name azkv-demo
```

Build and Run the console application to confirm everything is working and the key is being read from the KeyVault.

```
cd TestApp
dotnet build
dotnet run
```

- Update and commit the program code with the access key details included.

```
git add .
git commit -m "updated with a key"
git push
```
- Note that the password can be seen in the repository file.
- Oh No!, that was a mistake, lets correct it by removing remove the key from the current version of the code.  Great, problem solved ... but is it?

```
git add .
git commit -m "removed the key"
git push
```

- Time passes in our daily life.....  This could be weeks or months later; until someone notices that the key was added by mistake into the code and it's still in the history.  The discovery was due to using (GitLeaks)[https://gitleaks.io/].  This command line tool will allows developers to search any repository for over 140 known key types, while also giving you the ability to add your own.

```
../../gitleaks detect -v
```

- Double check that you can also see the password is still available in the GIT history for the project.
- Normal process is just to regenerate a new key and all will be good again; however in this case we say that this process is not possible right away.  It is however **still needed** for security as you never know who may have found that key or has the git history of your project on their local PC.
- So we need to clean this Edit the replacements.txt file with the values you want removed from history
- Remove the values from the program file, as this is history based
- Next run the replacement text to remove 

```
java -jar ..\..\tools\bfg-1.14.0.jar  --replace-text ..\replacements.txt -fi *.cs --no-blob-protection ..\..\.git
git reflog expire --expire=now --all
git gc --prune=now --aggressive
git pull
git push
```

- Check your Git History to find the value removed also retest with GitLeaks.

```
../../gitleaks detect -v
```
