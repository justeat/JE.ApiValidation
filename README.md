# Build Scripts

## Getting Started

1. Make sure the sample works

```
git clone git@github.je-labs.com:sys-automation/deploymentscripts.git c:\justeat\bootstrapping\deploymentscripts
git clone git@github.je-labs.com:sys-automation/buildscripts.git  c:\justeat\bootstrapping\buildscripts
git clone git@github.je-labs.com:sys-automation/JE.ApiValidation.Library.git c:\_github\JE.ApiValidation.Library

cd c:\_github\JE.ApiValidation.Library

.\build.ps1
.\package.ps1
.\deploy.ps1
```

2. Add your application code
  1. solution file should be at the root of the project
  2. projects should be in the src folder

3. Update the buildscripts and deployscripts configuration
    1. [deploy/manifest.json](deploy/manifest.json)
    2. [deploy/secure](deploy/secure) - add your connection strings, or delete this folder
    3. [deploy/configs](deploy/configs) - add your app config, or delete this folder
    4. [version.txt](version.txt) - your application version number in the format `^\d+\.\d+\.\d+$`

4. Use the [Build Scripts Master](http://ci.je-labs.com/admin/editBuild.html?id=template:NetBuildScriptsMaster) to create a Team City Build Configuration

