Task("Run").Does(() => {
    var project = "src/Deconstruct/Deconstruct.csproj";
    StartProcess("dotnet", new ProcessSettings {
        Arguments = $"run --project {project} --configuration Release"
    });  
});

var target = Argument("target", "default");
RunTarget(target);