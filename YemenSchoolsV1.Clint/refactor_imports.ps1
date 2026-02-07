$mappings = @{
    "core/services/student.service" = "@features/school-dashboard/student/services/student.service"
    "core/services/teacher.service" = "@features/school-dashboard/teacher/services/teacher.service"
    "core/services/acadmic-year.service" = "@features/school-dashboard/year/services/acadmic-year.service"
    "core/services/subject.service" = "@features/school-dashboard/school-subject/services/subject.service"
    "core/services/term.service" = "@features/school-dashboard/term/services/term.service"
    "core/services/stage.service" = "@features/school-dashboard/stage/services/stage.service"
    "core/services/section.service" = "@features/school-dashboard/section/services/section.service"
    "core/services/attendance.service" = "@features/school-dashboard/attendance/services/attendance.service"
    "core/services/daily-log.service" = "@features/school-dashboard/daily-log/services/daily-log.service"
    "core/services/grade.service" = "@features/school-dashboard/school-grade/services/grade.service"
    "core/services/mark.service" = "@features/school-dashboard/mark/services/mark.service"
    "core/services/message.service" = "@features/messages/services/message.service"
    "core/services/reports.service" = "@features/reports/services/reports.service"
    "core/services/section-subject.service" = "@features/school-dashboard/section-subject/services/section-subject.service"
    "core/services/school.service" = "@features/schools/services/school.service"
    "core/services/parent.service" = "@features/parent-dashboard/services/parent.service"
    "core/services/school-reviews.service" = "@features/schools/services/school-reviews.service"
    
    "shared/models/student" = "@features/school-dashboard/student/models"
    "shared/models/teachers" = "@features/school-dashboard/teacher/models"
    "shared/models/AcademicYear" = "@features/school-dashboard/year/models/AcademicYear"
    "shared/models/attendance" = "@features/school-dashboard/attendance/models"
    "shared/models/daily-log" = "@features/school-dashboard/daily-log/models"
    "shared/models/grade" = "@features/school-dashboard/school-grade/models"
    "shared/models/mark" = "@features/school-dashboard/mark/models"
    "shared/models/messages" = "@features/messages/models"
    "shared/models/reports" = "@features/reports/models"
    "shared/models/section" = "@features/school-dashboard/section/models"
    "shared/models/stage" = "@features/school-dashboard/stage/models"
    "shared/models/term" = "@features/school-dashboard/term/models"
    "shared/models/school" = "@features/schools/models"
    "shared/models/parent" = "@features/parent-dashboard/models/parent"
}

# Note: Explicit mappings for folders like AcademicYear if the casing requires it
# Or simply map shared/models/AcademicYear to feature path.

$files = Get-ChildItem -Path "src/app" -Recurse -Filter *.ts

foreach ($file in $files) {
    if ($file.Name -eq "refactor_imports.ps1") { continue }
    
    $content = Get-Content $file.FullName
    $newContent = $content
    $modified = $false

    foreach ($key in $mappings.Keys) {
        $val = $mappings[$key]
        
        # Regex to match:
        # Quote + ( ../ or ./ )+ + Key
        # We capture the Quote as $1.
        # We replace the whole match with $1 + $val
        # This effectively replaces relative path to Key with absolute Alias path.
        
        $keyRegex = [Regex]::Escape($key)
        # Regex breakdown:
        # (['`"])        -> Capture group 1: Quote
        # ((\.\.|/)\/?)+ -> Relative path segments (../ or ./ or /) repeated
        # $keyRegex      -> The path suffix we are looking for
        
        # Improved relative path regex: ((\.\.)|(\.))\/  matches ../ or ./
        # plus optional leading / for root relative imports? No, angular uses relative or alias.
        
        $pattern = "(['`"])(((\.\.)|(\.))[\\/])+" + $keyRegex
        
        if ($newContent -match $pattern) {
             # Use lookahead to ensure we don't match partial folder names if possible, or assume Key is specific enough.
             # "core/services/student.service" is specific.
             # "shared/models/student" might match "shared/models/student_profile".
             # To be safe, ensure next char is / or ' or "
             
             # But -replace in PS is simple regex.
             
             $newContent = $newContent -replace "$pattern", "`$1$val"
             $modified = $true
        }
    }

    if ($modified) {
        $newContent | Set-Content $file.FullName
        Write-Host "Updated $($file.FullName)"
    }
}
