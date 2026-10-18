#!/usr/bin/env bash
set -euo pipefail
repo_root=$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")/.." && pwd)
export RH_PERSONA_SOURCE="$repo_root/Convertidor"
test_dir=$(mktemp -d "${TMPDIR:-/tmp}/rh-persona-salud.XXXXXXXX")
trap 'rm -rf -- "$test_dir"' EXIT
cat > "$test_dir/check.csproj" <<'PROJECT'
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup><OutputType>Exe</OutputType><TargetFramework>net10.0</TargetFramework><ImplicitUsings>enable</ImplicitUsings><Nullable>enable</Nullable></PropertyGroup>
  <ItemGroup>
    <Compile Include="$(RH_PERSONA_SOURCE)/Services/Rh/RhPersonaSalud.cs" Link="RhPersonaSalud.cs" />
    <Compile Include="$(RH_PERSONA_SOURCE)/Dtos/Rh/RhPersonaUpdateDto.cs" Link="RhPersonaUpdateDto.cs" />
  </ItemGroup>
</Project>
PROJECT
cat > "$test_dir/Program.cs" <<'CASES'
using System.Text.Json;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos.Rh;
int count = 0;
void Check(bool ok, string name) { if (!ok) throw new Exception(name); count++; Console.WriteLine("OK: " + name); }
var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
var omitted = JsonSerializer.Deserialize<RhPersonaUpdateDto>("{}", options)!;
Check(RhPersonaSalud.TryRead(omitted.EstadoSaludId, out var present, out var state, out _) && !present && state == null, "Omision conserva clasificacion");
var clear = JsonSerializer.Deserialize<RhPersonaUpdateDto>("{\"estadoSaludId\":null}", options)!;
Check(RhPersonaSalud.TryRead(clear.EstadoSaludId, out present, out state, out _) && present && state == null, "Null explicito limpia clasificacion");
foreach (var value in new[] { "1", "99999" }) {
 var dto = JsonSerializer.Deserialize<RhPersonaUpdateDto>("{\"estadoSaludId\":" + value + "}", options)!;
 Check(RhPersonaSalud.TryRead(dto.EstadoSaludId, out present, out state, out _) && present && state == int.Parse(value), "Acepta entero en rango " + value);
}
foreach (var value in new[] { "0", "-1", "100000", "1.2", "\"72\"", "true", "{}", "[]" }) {
 var dto = JsonSerializer.Deserialize<RhPersonaUpdateDto>("{\"estadoSaludId\":" + value + "}", options)!;
 Check(!RhPersonaSalud.TryRead(dto.EstadoSaludId, out _, out _, out var error) && error == "Estado de salud invalido.", "Rechaza " + value);
}
Console.WriteLine(count + " comprobaciones aprobadas sin Oracle");
CASES
dotnet run --project "$test_dir/check.csproj" -p:NuGetAudit=false
