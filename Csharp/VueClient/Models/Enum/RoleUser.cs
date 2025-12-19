using NpgsqlTypes;
namespace VueClient.Models.Enum
{
    public enum RoleUser
    {
        [PgName("Gestionnaire")]
        Gestionnaire,
        [PgName("CLIENT")]
        CLIENT,
        [PgName("LIVREUR")]
        LIVREUR
    }
}
