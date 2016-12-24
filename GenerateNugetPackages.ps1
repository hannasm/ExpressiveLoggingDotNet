if (!(get-command 'Get-Project' -errorAction SilentlyContinue)) {
	throw ('this script is intended to be run from the nuget package manager console inside visual studio which has access to visual studio DTE');
}

$project = Get-Project | select -First 1
$build = $dte.Solution.SolutionBuild;
if (!$dte.Solution.SolutionBuild) {
	throw ('unable to locate solution build component from visual studio environment');
}

$oldCfg = $dte.Solution.SolutionBuild.ActiveConfiguration
$newCfg = $dte.Solution.SolutionBuild.SolutionConfigurations | where {$_.Name -eq 'Release'}
$newCfg.Activate();

$build.Clean($true);
$build.Build($true);

$solutionDir = split-path $dte.Solution.FullName -parent;
$metadataPath = join-path $solutionDir 'metadata.xml';
if (!(test-path $metadataPath)) {
	throw ('unable to find solution metadata at ' + $metadataPath);
}
$metadata = [xml](get-content $metadataPath);
$metadata = $metadata.solution;
if (!$metadata) {
	throw ('metadata file  at ' + $metadataPath + ' is missing root solution node');
}

nuget pack 'ExpressiveLogging.Merged/Package.nuspec' -Symbol -Prop Configuration=Release
nuget pack 'ExpressiveLogging.PowershellV5Logging/Package.nuspec' -Symbol -Prop Configuration=Release

$oldCfg.Activate();

# vim: set expandtab ts=2 sw=2: