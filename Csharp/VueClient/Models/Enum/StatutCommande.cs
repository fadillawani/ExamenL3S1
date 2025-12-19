using NpgsqlTypes;

namespace VueClient.Models.Enum
{
    public enum StatutCommande
    {
        [PgName("EN_ATTENTE")]
        EN_ATTENTE,
        [PgName("ANNULEE")]
        ANNULEE,
        [PgName("TERMINEE")]
        TERMINEE,
        [PgName("VALIDEE")]
        VALIDEE
    }
}
