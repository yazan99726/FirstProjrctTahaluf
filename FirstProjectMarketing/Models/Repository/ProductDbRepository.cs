using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class ProductDbRepository : IMarketingRepository<Firstproduct>
    {
        private readonly ModelContext modelContext;

        public ProductDbRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }
        public void Add(Firstproduct intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var product = find(Id);
            modelContext.Remove(product);
            modelContext.SaveChanges();
        }

        public Firstproduct find(decimal Id)
        {
            var product = modelContext.Firstproducts.Include(c=>c.Catigory).Include(c => c.Catigory.Store).SingleOrDefault(p => p.ProductId == Id);
            return product;
        }

        public IList<Firstproduct> List()
        {
            var product = modelContext.Firstproducts.Include(c=>c.Catigory).Include(c => c.Catigory.Store).ToList();
            return product;
        }

        public void Update(decimal Id, Firstproduct intity)
        {
            modelContext.Update(intity);
            modelContext.SaveChanges();
        }
    }
}
