using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class DiscountDbRepository : IMarketingRepository<Firstdiscount>
    {
        private readonly ModelContext modelContext;

        public DiscountDbRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }
        public void Add(Firstdiscount intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var discount = find(Id);
            modelContext.Remove(discount);
            modelContext.SaveChanges();
        }

        public Firstdiscount find(decimal Id)
        {
            var discount = modelContext.Firstdiscounts.Include(p=>p.Product).SingleOrDefault(d => d.DiscountId == Id);
            return discount;
        }

        public IList<Firstdiscount> List()
        {
            var discount = modelContext.Firstdiscounts.ToList();
            return discount;
        }

        public void Update(decimal Id, Firstdiscount intity)
        {
            modelContext.Update(intity);
            modelContext.SaveChanges();
        }
    }
}
