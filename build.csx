#! "netcoreapp2.0"
#r "nuget:NetStandard.Library,2.0"
#r "nuget:Newtonsoft.Json,10.0.3"

using System.IO;
using Newtonsoft.Json;

var dir = new DirectoryInfo(".");
var files = dir.GetFiles("*.*", SearchOption.AllDirectories);
var interested = files
    .Where(x => x.Name != "TEMPLATE.md")
    .Where(x => x.Name != "README.md")
    .Where(x => x.Name != "build.csx")
    .Where(x => !(x.FullName.Contains(".git") && x.Name != ".gitignore"))
    .Where(x => !x.FullName.Contains("Artifacts"))
    .Where(x => !x.FullName.Contains(".settings"))
    .Where(x => !x.FullName.Contains(".classpath"))
    .Where(x => !x.FullName.Contains(".project"))
    .Select(x => new { Path = x.FullName.Replace(dir.FullName, string.Empty).TrimStart('/'), x.Name });

var settings = interested.Select(x => new {
    name = x.Path,
    url = $"https://raw.githubusercontent.com/wk-j/configurations/master/{x.Path}",
    target = x.Path
});

var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
var template = File.ReadAllText("TEMPLATE.md");
var filled = template.Replace("{json}", json);
File.WriteAllText("README.md", filled);
