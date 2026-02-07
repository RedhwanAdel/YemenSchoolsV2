$mappings = @{
    "./account.service" = "@core/services/account.service"
    "./acadmic-year.service" = "@features/school-dashboard/year/services/acadmic-year.service"
    "./student.service" = "@features/school-dashboard/student/services/student.service"
    "./teacher.service" = "@features/school-dashboard/teacher/services/teacher.service"
    
    "shared/models/teachers/teacher" = "@features/school-dashboard/teacher/models/teachers"
    "shared/models/student/student" = "@features/school-dashboard/student/models/student"
    
    # Add loose mapping for shared/models if generic
    "shared/models" = "@shared/models"
}

$files = Get-ChildItem -Path "src/app/features" -Recurse -Filter *.ts

foreach ($file in $files) {
    if ($file.Name -eq "fix_imports.ps1") { continue }
    
    $content = Get-Content $file.FullName
    $newContent = $content
    $modified = $false

    foreach ($key in $mappings.Keys) {
        $val = $mappings[$key]
        
        # Match imports containing the Key
        # We look for quote + (any path prefix) + Key + quote or path separator
        
        $keyRegex = [Regex]::Escape($key)
        # Try to find string ending with key
        
        # Simple string replace for "./account.service" if in quotes
        if ($key.StartsWith("./")) {
            # Check for exactly import ... from './account.service';
            $pattern = "['`"]" + $keyRegex + "['`"]"
            if ($newContent -match $pattern) {
                 $newContent = $newContent -replace $pattern, "'$val'"
                 $modified = $true
            }
        } else {
             # Match ../../shared/models/teachers/teacher
             # Regex: quote + (anything) + key + quote
             $pattern = "['`"].*" + $keyRegex + "['`"]"
             
             if ($newContent -match $pattern) {
                  $newContent = $newContent -replace $pattern, "'$val'"
                  $modified = $true
             }
        }
    }

    if ($modified) {
        $newContent | Set-Content $file.FullName
        Write-Host "Fixed $($file.FullName)"
    }
}
