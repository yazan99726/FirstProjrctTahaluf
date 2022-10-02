using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class CartProductItemDbRepository : IMarketingRepository<Firstcartproduct>
    {
        private readonly ModelContext modelContext;

        public CartProductItemDbRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }
        public void Add(Firstcartproduct intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var cartProductItem = find(Id);
            modelContext.Remove(cartProductItem);
            modelContext.SaveChanges();
        }

        public Firstcartproduct find(decimal Id)
        {
            var cart = modelContext.Firstcartproducts.Include(p=>p.Product).Include(c=>c.Cart).Include(u=>u.Cart.User).SingleOrDefault(c => c.Id == Id);
            return cart;
        }

        public IList<Firstcartproduct> List()
        {
            var cartProduct = modelContext.Firstcartproducts.Include(p => p.Product).Include(c => c.Cart).Include(u => u.Cart.User).ToList();
            return cartProduct;
        }

        public void Update(decimal Id, Firstcartproduct intity)
        {
            var cart = find(Id);
            modelContext.Update(intity);
            modelContext.SaveChanges();
        }
    }
}
