$files = Get-ChildItem -Path "src/app/features" -Recurse -Filter *.ts
$emptyFiles = @()
foreach ($file in $files) {
    if ($file.Length -lt 50) {
        $emptyFiles += $file.FullName
        Write-Host "Empty/Small File found: $($file.FullName) - Size: $($file.Length)"
    }
}
if ($emptyFiles.Count -eq 0) {
    Write-Host "No empty files found."
}
