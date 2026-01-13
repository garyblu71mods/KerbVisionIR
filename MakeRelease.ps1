# KerbVision IR - Release Package Script
# This script creates a release-ready zip file

param(
    [string]$Version = "1.0.0"
)

$ErrorActionPreference = "Stop"

Write-Host "==================================" -ForegroundColor Cyan
Write-Host "KerbVision IR Release Builder" -ForegroundColor Cyan
Write-Host "Version: $Version" -ForegroundColor Cyan
Write-Host "==================================" -ForegroundColor Cyan
Write-Host ""

# Paths
$ProjectRoot = $PSScriptRoot
$ReleaseName = "KerbVisionIR-v$Version"
$ReleaseDir = Join-Path $ProjectRoot "Release"
$PackageDir = Join-Path $ReleaseDir $ReleaseName
$ZipPath = Join-Path $ReleaseDir "$ReleaseName.zip"

# Clean up old release
Write-Host "Cleaning up old release files..." -ForegroundColor Yellow
if (Test-Path $ReleaseDir) {
    Remove-Item $ReleaseDir -Recurse -Force
}
New-Item -ItemType Directory -Path $ReleaseDir | Out-Null
New-Item -ItemType Directory -Path $PackageDir | Out-Null

# Build the project
Write-Host "Building project..." -ForegroundColor Yellow
$MSBuild = "msbuild"
$BuildResult = & $MSBuild "$ProjectRoot\KerbVisionIR.csproj" /p:Configuration=Release /p:WarningLevel=0 /nologo /verbosity:minimal

if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed!" -ForegroundColor Red
    exit 1
}

Write-Host "Build successful!" -ForegroundColor Green
Write-Host ""

# Check if DLL was built
$DllPath = Join-Path $ProjectRoot "bin\Release\KerbVisionIR.dll"
if (-not (Test-Path $DllPath)) {
    Write-Host "DLL not found at: $DllPath" -ForegroundColor Red
    exit 1
}

# Create package structure
Write-Host "Creating package structure..." -ForegroundColor Yellow

$GameDataDest = Join-Path $PackageDir "GameData"
$ModDest = Join-Path $GameDataDest "KerbVisionIR"

New-Item -ItemType Directory -Path $GameDataDest | Out-Null
New-Item -ItemType Directory -Path $ModDest | Out-Null

# Copy GameData folder
Write-Host "Copying GameData files..." -ForegroundColor Yellow
$GameDataSource = Join-Path $ProjectRoot "GameData\KerbVisionIR"

# Copy directories
Copy-Item -Path (Join-Path $GameDataSource "Shaders") -Destination $ModDest -Recurse -Force
Copy-Item -Path (Join-Path $GameDataSource "PluginData") -Destination $ModDest -Recurse -Force

# Copy Icons if they exist
if (Test-Path (Join-Path $GameDataSource "Icons")) {
    Copy-Item -Path (Join-Path $GameDataSource "Icons") -Destination $ModDest -Recurse -Force
    Write-Host "Icons copied!" -ForegroundColor Green
}

# Copy Sounds if they exist
if (Test-Path (Join-Path $GameDataSource "Sounds")) {
    Copy-Item -Path (Join-Path $GameDataSource "Sounds") -Destination $ModDest -Recurse -Force
    Write-Host "Sounds copied!" -ForegroundColor Green
}

# Create Plugins directory and copy DLL
$PluginsDir = Join-Path $ModDest "Plugins"
New-Item -ItemType Directory -Path $PluginsDir | Out-Null
Copy-Item -Path $DllPath -Destination $PluginsDir -Force

# Copy version file
Copy-Item -Path (Join-Path $GameDataSource "KerbVisionIR.version") -Destination $ModDest -Force

Write-Host "GameData files copied successfully!" -ForegroundColor Green

# Copy documentation
Write-Host "Copying documentation..." -ForegroundColor Yellow
Copy-Item -Path (Join-Path $ProjectRoot "README.md") -Destination $PackageDir -Force
if (Test-Path (Join-Path $ProjectRoot "QUICKSTART.md")) {
    Copy-Item -Path (Join-Path $ProjectRoot "QUICKSTART.md") -Destination $PackageDir -Force
}
if (Test-Path (Join-Path $ProjectRoot "USERGUIDE.md")) {
    Copy-Item -Path (Join-Path $ProjectRoot "USERGUIDE.md") -Destination $PackageDir -Force
}
if (Test-Path (Join-Path $ProjectRoot "FEATURES.md")) {
    Copy-Item -Path (Join-Path $ProjectRoot "FEATURES.md") -Destination $PackageDir -Force
}
Copy-Item -Path (Join-Path $ProjectRoot "CHANGELOG.md") -Destination $PackageDir -Force
Copy-Item -Path (Join-Path $ProjectRoot "LICENSE") -Destination $PackageDir -Force

Write-Host "Documentation copied successfully!" -ForegroundColor Green
Write-Host ""

# Create zip file
Write-Host "Creating zip archive..." -ForegroundColor Yellow
if (Test-Path $ZipPath) {
    Remove-Item $ZipPath -Force
}

# Use .NET compression
Add-Type -Assembly "System.IO.Compression.FileSystem"
[System.IO.Compression.ZipFile]::CreateFromDirectory($PackageDir, $ZipPath)

Write-Host "Zip archive created: $ZipPath" -ForegroundColor Green
Write-Host ""

# Display package info
$ZipSize = (Get-Item $ZipPath).Length
$ZipSizeKB = [math]::Round($ZipSize / 1KB, 2)

# Calculate SHA256 hash for CKAN
Write-Host ""
Write-Host "Calculating SHA256 hash..." -ForegroundColor Yellow
$Hash = Get-FileHash -Path $ZipPath -Algorithm SHA256

Write-Host "==================================" -ForegroundColor Cyan
Write-Host "Release Package Complete!" -ForegroundColor Green
Write-Host "==================================" -ForegroundColor Cyan
Write-Host "Version:     $Version" -ForegroundColor White
Write-Host "Package:     $ReleaseName.zip" -ForegroundColor White
Write-Host "Size:        $ZipSizeKB KB" -ForegroundColor White
Write-Host "Location:    $ZipPath" -ForegroundColor White
Write-Host ""
Write-Host "SHA256 Hash (for CKAN):" -ForegroundColor Yellow
Write-Host $Hash.Hash -ForegroundColor White
Write-Host ""
Write-Host "Package contents:" -ForegroundColor Yellow
Write-Host "  - GameData/KerbVisionIR/" -ForegroundColor White
Write-Host "    - Plugins/KerbVisionIR.dll" -ForegroundColor White
Write-Host "    - Shaders/NightVision.shader" -ForegroundColor White
Write-Host "    - PluginData/" -ForegroundColor White
if (Test-Path (Join-Path $GameDataSource "Icons")) {
    Write-Host "    - Icons/" -ForegroundColor White
}
if (Test-Path (Join-Path $GameDataSource "Sounds")) {
    Write-Host "    - Sounds/" -ForegroundColor White
}
Write-Host "    - KerbVisionIR.version" -ForegroundColor White
Write-Host "  - README.md" -ForegroundColor White
if (Test-Path (Join-Path $ProjectRoot "QUICKSTART.md")) {
    Write-Host "  - QUICKSTART.md" -ForegroundColor White
}
if (Test-Path (Join-Path $ProjectRoot "USERGUIDE.md")) {
    Write-Host "  - USERGUIDE.md" -ForegroundColor White
}
if (Test-Path (Join-Path $ProjectRoot "FEATURES.md")) {
    Write-Host "  - FEATURES.md" -ForegroundColor White
}
Write-Host "  - CHANGELOG.md" -ForegroundColor White
Write-Host "  - LICENSE" -ForegroundColor White
Write-Host ""
Write-Host "=== Next Steps for CKAN Release ===" -ForegroundColor Cyan
Write-Host "1. Test the package locally" -ForegroundColor White
Write-Host "2. Create GitHub release with tag: v$Version" -ForegroundColor White
Write-Host "3. Upload $ReleaseName.zip to GitHub release" -ForegroundColor White
Write-Host "4. Submit KerbVisionIR.netkan to CKAN NetKAN repo" -ForegroundColor White
Write-Host "   (See CKAN-PUBLISHING-GUIDE.md for details)" -ForegroundColor White
Write-Host ""
Write-Host "Ready for distribution!" -ForegroundColor Green
Write-Host "==================================" -ForegroundColor Cyan
