using NpgsqlTypes;
namespace VueClient.Models.Enum
{
    public enum TypeComplement
    {
        [PgName("FRITE")]
        FRITE,       
        [PgName("BOISSON")]
        BOISSON
        
    }
}
