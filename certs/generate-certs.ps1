<#
.SYNOPSIS
    Generates HTTPS certificates for local development.

.DESCRIPTION
    Creates:
      - src/ClientApp/ssl/cert.pem and key.pem  (Angular dev server)
      - localhost.pfx                             (ASP.NET Core backend)

    Prerequisites: OpenSSL must be installed and on PATH.
    On Windows: available via Git for Windows, Chocolatey, or Windows native.

.EXAMPLE
    .\generate-certs.ps1 -Password "YourPfxPassword"
#>

param(
    [Parameter(Mandatory = $true)]
    [string]$Password
)

$Root = Split-Path $PSScriptRoot
$SslDir = Join-Path $Root "src\ClientApp\ssl"
$CertPem = Join-Path $SslDir "cert.pem"
$KeyPem  = Join-Path $SslDir "key.pem"
$PfxPath = Join-Path $Root "localhost.pfx"

# Create ssl directory if needed
New-Item -ItemType Directory -Path $SslDir -Force | Out-Null

Write-Host "Generating self-signed certificate for localhost..." -ForegroundColor Cyan

# Generate private key and self-signed certificate with SAN
openssl req -x509 -newkey rsa:4096 -sha256 -days 365 -nodes `
    -keyout $KeyPem `
    -out $CertPem `
    -subj "/CN=localhost" `
    -addext "subjectAltName=DNS:localhost,IP:127.0.0.1"

if ($LASTEXITCODE -ne 0) {
    Write-Error "OpenSSL certificate generation failed. Is OpenSSL installed?"
    exit 1
}

Write-Host "Exporting as PFX for ASP.NET Core backend..." -ForegroundColor Cyan

openssl pkcs12 -export `
    -out $PfxPath `
    -inkey $KeyPem `
    -in $CertPem `
    -passout "pass:$Password"

if ($LASTEXITCODE -ne 0) {
    Write-Error "PFX export failed."
    exit 1
}

Write-Host ""
Write-Host "Done!" -ForegroundColor Green
Write-Host "  cert.pem -> $CertPem" -ForegroundColor Gray
Write-Host "  key.pem  -> $KeyPem"  -ForegroundColor Gray
Write-Host "  PFX      -> $PfxPath" -ForegroundColor Gray
Write-Host ""
Write-Host "Add to your .env file:" -ForegroundColor Yellow
Write-Host "  ASPNETCORE_Kestrel__Certificates__Default__Password=$Password" -ForegroundColor Yellow
Write-Host "  ASPNETCORE_Kestrel__Certificates__Default__Path=/https/localhost.pfx" -ForegroundColor Yellow
