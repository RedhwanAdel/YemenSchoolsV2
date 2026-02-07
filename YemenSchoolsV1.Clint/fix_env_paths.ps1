$files = Get-ChildItem -Path "src/app/features" -Recurse -Filter *.ts

foreach ($file in $files) {
    if ($file.Name -eq "fix_env_paths.ps1") { continue }
    
    $content = Get-Content $file.FullName
    $hasEnv = $content | Select-String "environments/environment"
    
    if ($hasEnv) {
        # Calculate depth relative to src/app/features
        # Path: src/app/features/...
        # Split by path separator and count
        
        # We need to reach src/environments/environment
        # File is at: X/Y/Z/file.ts
        # We need to go up enough times to reach 'src'
        # 'src' contains 'environments'
        
        # Determine strict relative path
        $relativePathTokens = $file.FullName.Split([System.IO.Path]::DirectorySeparatorChar)
        $srcIndex = $relativePathTokens.IndexOf("src")
        $fileIndex = $relativePathTokens.Count - 1
        
        # Depth from file to 'src'
        # if file is src/app/features/file.ts
        # depth to src: 3 (../features -> ../app -> ../src)
        
        $depth = $fileIndex - $srcIndex
        
        
        # Construct correct prefix
        $prefix = "../" * ($depth - 1)
        
        # Replace logic
        $newContent = $content -replace "import \{ environment \} from ['.`"](\.\./)+environments/environment['.`"];", "import { environment } from '$prefix" + "environments/environment';"
        
        if ($newContent -ne $content) {
             # Only write if changed (avoid touching files unnecessarily, though simple compare might be enough)
             # Wait, array comparison in PS is tricky.
             # Let's compare joined string or assume verify later.
             # Actually -ne works on array of strings by filtering? No.
             
             $newContent | Set-Content $file.FullName
             Write-Host "Fixed env path in $($file.Name) (Depth: $depth)"
        }
    }
}
