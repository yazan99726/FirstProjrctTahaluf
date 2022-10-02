using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class HomeDbRepository : IMarketingRepository<Firsthome>
    {
        private readonly ModelContext modelContext;

        public HomeDbRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }
        public void Add(Firsthome intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var home = find(Id);
            modelContext.Remove(home);
            modelContext.SaveChanges();
        }

        public Firsthome find(decimal Id)
        {
            var home = modelContext.Firsthomes.SingleOrDefault(h => h.Id == Id);
            return home;
        }

        public IList<Firsthome> List()
        {
            var home = modelContext.Firsthomes.ToList();
            return home;
        }

        public void Update(decimal Id, Firsthome intity)
        {
            
            modelContext.Firsthomes.Update(intity);
            modelContext.SaveChanges();
        }
    }
}
