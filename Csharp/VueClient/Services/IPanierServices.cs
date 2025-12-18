using VueClient.ViewModel;

namespace VueClient.Services
{
    public interface IPanierServices
    {
        PanierVM GetPanier(long clientId);

        void AddBurger(long clientId, long burgerId, int quantite);
        void AddMenu(long clientId, long menuId, int quantite);
        void AddComplement(long clientId, long complementId, int quantite);

        void RemoveItem(long panierItemId);

        void Clear(long clientId);
    }
}
