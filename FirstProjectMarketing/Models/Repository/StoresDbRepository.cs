using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class StoresDbRepository : IMarketingRepository<Firststore>
    {
        private readonly ModelContext modelContext;

        public StoresDbRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }
        public void Add(Firststore intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var store = find(Id);
            modelContext.Remove(store);
            modelContext.SaveChanges();
        }

        public Firststore find(decimal Id)
        {
            var store = modelContext.Firststores.SingleOrDefault(s => s.StoreId == Id);
            return store;
        }

        public IList<Firststore> List()
        {
            var store = modelContext.Firststores.ToList();
            return store;
        }

        public void Update(decimal Id, Firststore intity)
        {
            //var store = find(Id);
            modelContext.Update(intity);
            modelContext.SaveChanges();
        }
    }
}
