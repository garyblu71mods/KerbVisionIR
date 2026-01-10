# Build script for KerbVision IR
# This script helps build the project even without KSP installed

param(
    [string]$KSPPath = "",
    [string]$Configuration = "Release"
)

Write-Host "==================================" -ForegroundColor Cyan
Write-Host "KerbVision IR Build Script" -ForegroundColor Cyan
Write-Host "==================================" -ForegroundColor Cyan
Write-Host ""

# Try to find KSP
$kspLocations = @(
    "C:\Program Files (x86)\Steam\steamapps\common\Kerbal Space Program",
    "C:\Program Files\Steam\steamapps\common\Kerbal Space Program",
    "D:\SteamLibrary\steamapps\common\Kerbal Space Program",
    "E:\SteamLibrary\steamapps\common\Kerbal Space Program"
)

if ($KSPPath -eq "") {
    Write-Host "Searching for KSP installation..." -ForegroundColor Yellow
    
    foreach ($loc in $kspLocations) {
        if (Test-Path "$loc\KSP_x64_Data\Managed\Assembly-CSharp.dll") {
            $KSPPath = $loc
            Write-Host "Found KSP at: $KSPPath" -ForegroundColor Green
            break
        }
    }
    
    if ($KSPPath -eq "") {
        Write-Host "ERROR: Could not find KSP installation!" -ForegroundColor Red
        Write-Host ""
        Write-Host "Please specify KSP path manually:" -ForegroundColor Yellow
        Write-Host '  .\Build.ps1 -KSPPath "C:\Path\To\Kerbal Space Program"' -ForegroundColor White
        Write-Host ""
        Write-Host "Or install KSP to one of these locations:" -ForegroundColor Yellow
        foreach ($loc in $kspLocations) {
            Write-Host "  - $loc" -ForegroundColor White
        }
        exit 1
    }
} else {
    if (-not (Test-Path "$KSPPath\KSP_x64_Data\Managed\Assembly-CSharp.dll")) {
        Write-Host "ERROR: Invalid KSP path: $KSPPath" -ForegroundColor Red
        Write-Host "Could not find Assembly-CSharp.dll" -ForegroundColor Red
        exit 1
    }
    Write-Host "Using KSP at: $KSPPath" -ForegroundColor Green
}

Write-Host ""
Write-Host "Building configuration: $Configuration" -ForegroundColor Cyan
Write-Host ""

# Build the project
$msbuildArgs = @(
    "KerbVisionIR.csproj",
    "/p:Configuration=$Configuration",
    "/p:KSPRoot=`"$KSPPath`"",
    "/v:minimal",
    "/nologo"
)

Write-Host "Running MSBuild..." -ForegroundColor Yellow
& msbuild $msbuildArgs

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "==================================" -ForegroundColor Green
    Write-Host "BUILD SUCCESSFUL!" -ForegroundColor Green
    Write-Host "==================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "DLL Location: bin\$Configuration\KerbVisionIR.dll" -ForegroundColor White
    Write-Host "Copied to: GameData\KerbVisionIR\Plugins\" -ForegroundColor White
    Write-Host ""
    Write-Host "Next steps:" -ForegroundColor Cyan
    Write-Host "1. Copy GameData\KerbVisionIR to your KSP GameData folder" -ForegroundColor White
    Write-Host "2. Launch KSP and test the mod" -ForegroundColor White
    Write-Host ""
} else {
    Write-Host ""
    Write-Host "==================================" -ForegroundColor Red
    Write-Host "BUILD FAILED!" -ForegroundColor Red
    Write-Host "==================================" -ForegroundColor Red
    Write-Host ""
    Write-Host "Check the errors above and fix them." -ForegroundColor Yellow
    Write-Host ""
    exit 1
}
