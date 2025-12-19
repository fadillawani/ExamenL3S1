using NpgsqlTypes;
namespace VueClient.Models.Enum
{
    public enum MoyenPaiement
    {
        [PgName("WAVE")]
        WAVE,
        [PgName("OM")]
        OM
    }
}
