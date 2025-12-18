namespace VueClient.ViewModel
{
    public class PanierVM
    {
        public long PanierId { get; set; }
        public List<PanierItemVM> Items { get; set; } = new();
        public double Total => Items.Sum(i => i.PrixTotal);
    }

   
}
