param ($path, $solutionDir);

$refactorPath = join-path $path 'AssemblyRefactoring.dll';
import-module $refactorPath;

$metadataPath = join-path $solutionDir 'metadata.xml';
if (!(test-path $metadataPath)) {
	throw ('unable to find solution metadata at ' + $metadataPath);
}
$metadata = [xml](get-content $metadataPath);
$metadata = $metadata.solution;
if (!$metadata) {
	throw ('metadata file  at ' + $metadataPath + ' is missing root solution node');
}

$verString = ('' + $metadata.version) -replace '\.', '_';
$verString = $verString.Substring(0, $verString.IndexOf('_'));  # only want major version

$targetDll = join-path $path 'ExpressiveLogging.PowershellV5Logging.dll';
$newDll = join-path $path ('ExpressiveLogging.V' + $verString + '.PowershellV5Logging.dll');
$targetPdb = join-path $path 'ExpressiveLogging.PowershellV5Logging.pdb';
$oldNS = 'ExpressiveLogging';
$newNS = 'ExpressiveLogging.V' + $verString;
$oldAssemRef = $oldNS;
$newAssemRef = $newNS;
$oldAssem = 'ExpressiveLogging.PowershellV5Logging';
$newAssem = 'ExpressiveLogging.V' + $verString + '.PowershellV5Logging';

$x = new-object AssemblyRefactoring.AssemblyRefactoringContext $targetDll

$x.RenameNamespace($oldNS, $newNS);
$x.RenameAssemblyReference($oldAssemRef, $newAssemRef);
$x.RenameAssemblyName($oldAssem,$newAssem);
$x.Save( $newDll )

remove-item -Force $targetDll;
remove-item -Force $targetPdb;