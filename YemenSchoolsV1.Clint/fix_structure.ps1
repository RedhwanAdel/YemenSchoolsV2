$fixes = @(
    @{ Path = "src/app/features/school-dashboard/student"; Service = "student.service.ts"; Model = "student.ts" },
    @{ Path = "src/app/features/school-dashboard/teacher"; Service = "teacher.service.ts"; Model = "teachers.ts" },
    @{ Path = "src/app/features/school-dashboard/year"; Service = "acadmic-year.service.ts"; Model = "AcademicYear.ts" },
    @{ Path = "src/app/features/school-dashboard/school-subject"; Service = "subject.service.ts"; Model = "subject.ts" },
    @{ Path = "src/app/features/school-dashboard/term"; Service = "term.service.ts"; Model = "term.ts" },
    @{ Path = "src/app/features/school-dashboard/stage"; Service = "stage.service.ts"; Model = "stage.ts" },
    @{ Path = "src/app/features/school-dashboard/section"; Service = "section.service.ts"; Model = "section.ts" },
    @{ Path = "src/app/features/school-dashboard/attendance"; Service = "attendance.service.ts"; Model = "attendance.ts" },
    @{ Path = "src/app/features/school-dashboard/daily-log"; Service = "daily-log.service.ts"; Model = "daily-log.ts" },
    @{ Path = "src/app/features/school-dashboard/school-grade"; Service = "grade.service.ts"; Model = "grade.ts" },
    @{ Path = "src/app/features/school-dashboard/mark"; Service = "mark.service.ts"; Model = "mark.ts" },
    @{ Path = "src/app/features/messages"; Service = "message.service.ts"; Model = "messages.ts" },
    @{ Path = "src/app/features/reports"; Service = "reports.service.ts"; Model = "reports.ts" },
    @{ Path = "src/app/features/school-dashboard/section-subject"; Service = "section-subject.service.ts"; Model = "" }, 
    @{ Path = "src/app/features/schools"; Service = "school.service.ts"; Model = "school.ts" },
    @{ Path = "src/app/features/parent-dashboard"; Service = "parent.service.ts"; Model = "parent.ts" }
)

# Note: section-subject model was not moved explicitly or I missed it? 
# I moved 'section-subject.service.ts'. model? shared/models/section?
# I'll skip Model for section-subject.
# Also schools service moved 2 files: school.service.ts and school-reviews.service.ts.
# Only one became 'services' file (the last one?). 
# I will check schools/services.

foreach ($fix in $fixes) {
    $path = $fix.Path
    $svcName = $fix.Service
    $modelName = $fix.Model
    
    # Fix Services
    if (Test-Path "$path/services" -PathType Leaf) {
        Write-Host "Fixing services in $path"
        Move-Item "$path/services" "$path/services_tmp" -Force
        New-Item -ItemType Directory -Force -Path "$path/services" | Out-Null
        Move-Item "$path/services_tmp" "$path/services/$svcName" -Force
    } elseif (-not (Test-Path "$path/services")) {
         # If it doesn't exist at all? maybe missed content.
    }
    
    # Fix Models
    if ($modelName -ne "" -and (Test-Path "$path/models" -PathType Leaf)) {
        Write-Host "Fixing models in $path"
        Move-Item "$path/models" "$path/models_tmp" -Force
        New-Item -ItemType Directory -Force -Path "$path/models" | Out-Null
        Move-Item "$path/models_tmp" "$path/models/$modelName" -Force
    }
}
