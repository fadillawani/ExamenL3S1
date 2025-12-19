using NpgsqlTypes;
namespace VueClient.Models.Enum
{
    public enum StatutLivraison
    {
        [PgName("EN_ATTENTE")]
        EN_ATTENTE,
        [PgName("EN_COURS")]
        EN_COURS,
        [PgName("TERMINEE")]
        TERMINEE
    }
}
