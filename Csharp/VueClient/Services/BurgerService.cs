namespace VueClient.Data
{
    using Microsoft.EntityFrameworkCore;
    using VueClient.Models;

    public class BurgerService
    {
        private readonly AppDbContext _context;

        public BurgerService(AppDbContext context)
        {
            _context = context;
        }

        public Burger? GetBurgerById(int id)
        {
            return _context.burger.Find(id);
        }


        public IEnumerable<Burger> GetAllBurgers()
        {
            return _context.burger.ToList();
        }
    }
}