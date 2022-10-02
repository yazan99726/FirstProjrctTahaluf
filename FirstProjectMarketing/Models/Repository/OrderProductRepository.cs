using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class OrderProductRepository : IMarketingRepository<Firstorderproduct>
    {
        private readonly ModelContext modelContext;

        public OrderProductRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }
        public void Add(Firstorderproduct intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var orderProduct = find(Id);
            modelContext.Remove(orderProduct);
            modelContext.SaveChanges();
        }

        public Firstorderproduct find(decimal Id)
        {
            var orderProduct = modelContext.Firstorderproducts.Include(o=>o.Order).Include(p=>p.Product).SingleOrDefault(a => a.Id == Id);
            return orderProduct;
        }

        public IList<Firstorderproduct> List()
        {
            var orderProduct = modelContext.Firstorderproducts.Include(o => o.Order).Include(p => p.Product).Include(u=>u.Order.User).Include(p => p.Product.Catigory).Include(p => p.Product.Catigory.Store).ToList();
            return orderProduct;
        }

        public void Update(decimal Id, Firstorderproduct intity)
        {
            modelContext.Update(intity);
            modelContext.SaveChanges();
        }
    }
}
