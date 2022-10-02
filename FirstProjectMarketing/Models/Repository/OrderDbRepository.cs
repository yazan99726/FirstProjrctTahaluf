using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class OrderDbRepository : IMarketingRepository<Firstorder>
    {
        private readonly ModelContext modelContext;

        public OrderDbRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }
        public void Add(Firstorder intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var order = find(Id);
            modelContext.Remove(order);
            modelContext.SaveChanges();
        }

        public Firstorder find(decimal Id)
        {
            var order = modelContext.Firstorders.SingleOrDefault(o => o.OrderId == Id);
            return order;
        }

        public IList<Firstorder> List()
        {
            var order = modelContext.Firstorders.ToList();
            return order;
        }

        public void Update(decimal Id, Firstorder intity)
        {
            var order = find(Id);
            modelContext.Update(order);
            modelContext.SaveChanges();
        }
    }
}
