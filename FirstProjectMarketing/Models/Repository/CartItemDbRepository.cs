using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class CartItemDbRepository : IMarketingRepository<Firstcart>
    {
        private readonly ModelContext modelContext;

        public CartItemDbRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }
        public void Add(Firstcart intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var cart = find(Id);
            modelContext.Remove(cart);
            modelContext.SaveChanges();
        }

        public Firstcart find(decimal usrtId)
        {
            var cart = modelContext.Firstcarts.Include(u=>u.User).SingleOrDefault(c => c.UserId == usrtId);
            return cart;
        }

        //public Firstcart findByUserId(decimal userId)
        //{
        //    var cart = modelContext.Firstcarts.Include(u => u.User).SingleOrDefault(c => c.UserId == userId);
        //    return cart;
        //}

        public IList<Firstcart> List()
        {
            var cart = modelContext.Firstcarts.ToList();
            return cart;
        }

        public void Update(decimal Id, Firstcart intity)
        {
            var cart = find(Id);
            modelContext.Update(intity);
            modelContext.SaveChanges();
        }
    }
}
