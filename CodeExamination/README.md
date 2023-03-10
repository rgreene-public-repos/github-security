# CodeQL 
Setup the two simple demo projects.

```
pushd CodeExamination
cd TestApp
dotnet build
dotnet run
popd 
```
React application
```
pushd CodeExamination
cd react-app
npm install
npm run build
npm run start
popd 
```

- Download the latest CodeQL Bundle from https://github.com/github/codeql-action/releases.
- Extract tar file "codeql" folder to folder "CodeExamination"
- Verify the installation and paths by reviewing the languages returned.  You should not see any errors, however the list of available languages may change over time. 
```
cd CodeExamination
codeql/codeql resolve languages 
```

- Run the examination processing by creating the database and executing the selected queries.  **Note**; there are times when you'll want to delete the database from the file system as the "--overwrite" flag does not always work.
- Check the various pack queries available to you in the folder "codeql\qlpacks\codeql"
    - C# is "csharp-queries\[version]"
    - Javascript "javascript-queries\[version]

## CSharp TestApp example
```
## Genreate the Database ##
codeql/codeql database create TestAppDB --source-root=TestApp --language=csharp --overwrite

## Run the Security features in SARIF format ##
codeql/codeql database analyze TestAppDB --threads=1  --format=sarif-latest --output=results-csharp.sarif "codeql\qlpacks\codeql\csharp-queries\0.5.4\Security Features"

## Run the Security features in CSV format note queries are already executed ##
codeql/codeql database analyze TestAppDB --threads=1  --format=CSV --output=results-csharp.csv "codeql\qlpacks\codeql\csharp-queries\0.5.4\Security Features"

## Run General code qulaity check in CSV format ##
codeql/codeql database analyze TestAppDB --threads=1  --format=CSV --output=results-csharp.csv "codeql\qlpacks\codeql\csharp-queries\0.5.4\Bad Practices"

## Run General code qulaity check in CSV format ##
codeql/codeql database analyze TestAppDB --threads=4  --format=CSV --output=results-csharp.csv "codeql\qlpacks\codeql\csharp-queries\0.5.4\Likely Bugs"

```

## React TestApp
```
codeql/codeql database create react-appDB --source-root=react-app --language=javascript --overwrite

codeql/codeql database analyze react-appDB --threads=4  --format=CSV --output=results-javascript.csv "codeql\qlpacks\codeql\javascript-queries\0.5.4\Security"
```

The results are retuned in a newly generated CSV file.
