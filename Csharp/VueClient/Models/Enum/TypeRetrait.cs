using NpgsqlTypes;
namespace VueClient.Models.Enum
{
    public enum TypeRetrait
    {
        [PgName("LIVRAISON")]
        LIVRAISON,
        [PgName("SUR_PLACE")]
        SUR_PLACE,
        [PgName("A_EMPORTER")]
        A_EMPORTER
    }
}
