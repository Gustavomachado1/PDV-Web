param([string]$Raiz = 'C:\Projetos\PDV-Web')
$ErrorActionPreference = 'Stop'
$fw = 'net10.0'

if (-not (Test-Path (Join-Path $Raiz 'src'))) { throw "Pasta src nao encontrada em $Raiz" }

$fonte = Get-ChildItem -Path $Raiz -Recurse -Directory -Filter fonte |
    Where-Object { Test-Path (Join-Path $_.FullName 'PdvWeb.Domain') } |
    Select-Object -First 1
if (-not $fonte) { throw "Pasta fonte (com PdvWeb.Domain dentro) nao encontrada em $Raiz" }
Write-Host "Usando fontes de: $($fonte.FullName)"

Set-Location $Raiz

if (-not (Get-ChildItem -Filter '*.sln*' -ErrorAction SilentlyContinue)) { dotnet new sln -n PDV-Web }
if (-not (Test-Path '.gitignore')) { dotnet new gitignore }

function Novo($tipo, $nome, $extra = @()) {
    $pasta = "src/$nome"
    if (Test-Path "$pasta/$nome.csproj") { Write-Host "$nome ja existe, pulando"; return }
    dotnet new $tipo -n $nome -o $pasta -f $fw @extra
    if ($LASTEXITCODE -ne 0) { throw "Falha ao criar $nome" }
}

Novo classlib PDV.Domain
Novo classlib PDV.Application
Novo classlib PDV.Infrastructure
Novo webapi   PDV.API
Novo blazor   PDV.Web @('--interactivity', 'Server')

Remove-Item src/PDV.Domain/Class1.cs, src/PDV.Application/Class1.cs, src/PDV.Infrastructure/Class1.cs -ErrorAction SilentlyContinue

$mapa = @{
    'PdvWeb.Domain'         = 'PDV.Domain'
    'PdvWeb.Application'    = 'PDV.Application'
    'PdvWeb.Infrastructure' = 'PDV.Infrastructure'
    'PdvWeb.Api'            = 'PDV.API'
}
foreach ($origem in $mapa.Keys) {
    $dirOrigem = Join-Path $fonte.FullName $origem
    if (-not (Test-Path $dirOrigem)) { continue }
    $dirDestino = Join-Path $Raiz ("src\" + $mapa[$origem])
    Get-ChildItem $dirOrigem -Recurse -File | ForEach-Object {
        $rel = $_.FullName.Substring($dirOrigem.Length).TrimStart('\')
        $destino = Join-Path $dirDestino $rel
        New-Item -ItemType Directory -Force -Path (Split-Path $destino) | Out-Null
        $conteudo = Get-Content $_.FullName -Raw -Encoding UTF8
        $conteudo = $conteudo -replace 'PdvWeb\.Api', 'PDV.API' -replace 'PdvWeb\.', 'PDV.'
        [System.IO.File]::WriteAllText($destino, $conteudo, (New-Object System.Text.UTF8Encoding($false)))
    }
}

dotnet sln add src/PDV.Domain/PDV.Domain.csproj src/PDV.Application/PDV.Application.csproj `
    src/PDV.Infrastructure/PDV.Infrastructure.csproj src/PDV.API/PDV.API.csproj src/PDV.Web/PDV.Web.csproj

dotnet add src/PDV.Application    reference src/PDV.Domain
dotnet add src/PDV.Infrastructure reference src/PDV.Application src/PDV.Domain
dotnet add src/PDV.API            reference src/PDV.Application src/PDV.Infrastructure

dotnet add src/PDV.Infrastructure package Microsoft.EntityFrameworkCore.SqlServer
dotnet add src/PDV.API package Microsoft.EntityFrameworkCore.Design
dotnet add src/PDV.API package Swashbuckle.AspNetCore

dotnet tool install --global dotnet-ef 2>$null
dotnet build

Write-Host ""
Write-Host "Proximo passo (criar o banco):"
Write-Host "dotnet ef migrations add Inicial -p src/PDV.Infrastructure -s src/PDV.API"
Write-Host "dotnet ef database update -p src/PDV.Infrastructure -s src/PDV.API"