using System.Text.Json;

namespace Convertidor.Data.Repository.Rh;

// Preserves omission independently of explicit null for older clients.
internal static class RhPersonaSalud
{
    public static bool TryRead(JsonElement value, out bool present, out int? estado, out string error)
    {
        present = value.ValueKind != JsonValueKind.Undefined;
        estado = null;
        error = string.Empty;
        if (!present || value.ValueKind == JsonValueKind.Null) return true;
        if (value.ValueKind != JsonValueKind.Number || !value.TryGetInt32(out var id) || id <= 0 || id > 99999)
        {
            error = "Estado de salud invalido.";
            return false;
        }
        estado = id;
        return true;
    }
}
