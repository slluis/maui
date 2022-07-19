
# MAUI Templates

## Building / Testing

Add the local artifacts to the NuGet.config:

```xml
<add key="LocalMauiTemplates" value="./artifacts" />
```

```dotnetcli
dotnet pack Microsoft.Maui.sln
dotnet new -i artifacts\Microsoft.Maui.Templates.*.nupkg
# then just in the maui folder, so you get a NuGet.config
mkdir foo
cd foo
dotnet new maui-mobile
```
