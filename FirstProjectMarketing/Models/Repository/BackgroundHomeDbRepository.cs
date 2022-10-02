using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class BackgroundHomeDbRepository : IMarketingRepository<Firsthomeimage>
    {
        private readonly ModelContext modelContext;

        public BackgroundHomeDbRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }
        public void Add(Firsthomeimage intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var bgHome = find(Id);
            modelContext.Remove(bgHome);
            modelContext.SaveChanges();
        }

        public Firsthomeimage find(decimal Id)
        {
            var bgHome = modelContext.Firsthomeimages.SingleOrDefault(b => b.Id == Id);
            return bgHome;
        }

        public IList<Firsthomeimage> List()
        {
            var bgHome = modelContext.Firsthomeimages.ToList();
            return bgHome;
        }

        public void Update(decimal Id, Firsthomeimage intity)
        {
            
            modelContext.Update(intity);
            modelContext.SaveChanges();
        }
    }
}
