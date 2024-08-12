# Put this script in your solution's root folder
# Run it from PowerShell

# Use the current directory as the project directory
$projectDir = Get-Location

# Use a fixed name for the output file in the current directory
$outputFile = Join-Path $projectDir "code_context.txt"

# Remove the output file if it exists
if (Test-Path $outputFile) {
    Remove-Item $outputFile
}

# List of directories to look for (adjust these based on your .NET project structure)
$directories = @("Controllers", "Models", "Views", "Services", "Data", "Repository", "Endpoints")

# List of file types to ignore
$ignoreFiles = @("*.ico", "*.png", "*.jpg", "*.jpeg", "*.gif", "*.svg", "*.css", "*.min.js")

# Recursive function to read files and append their content
function Read-Files($dir) {
    Get-ChildItem $dir -Recurse | ForEach-Object {
        if ($_.PSIsContainer) {
            # If it's a directory, do nothing (we're already recursing)
        }
        elseif ($_.PSIsContainer -eq $false) {
            # Check if the file type should be ignored
            $shouldIgnore = $false
            foreach ($ignorePattern in $ignoreFiles) {
                if ($_.Name -like $ignorePattern) {
                    $shouldIgnore = $true
                    break
                }
            }

            # If the file type should not be ignored, append its relative path and content to the output file
            if (-not $shouldIgnore) {
                $relativePath = $_.FullName.Substring($projectDir.Path.Length + 1)
                Add-Content $outputFile "// File: $relativePath"
                Get-Content $_.FullName | Add-Content $outputFile
                Add-Content $outputFile ""
            }
        }
    }
}

# Call the recursive function for each specified directory in the project directory
foreach ($dir in $directories) {
    $fullPath = Join-Path $projectDir $dir
    if (Test-Path $fullPath) {
        Read-Files $fullPath
    }
}

Write-Host "Code context has been written to $outputFile"