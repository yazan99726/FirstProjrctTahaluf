using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class AboutUsDbRepository : IMarketingRepository<Firstaboutu>
    {
        private readonly ModelContext modelContext;

        public AboutUsDbRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }
        public void Add(Firstaboutu intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var about = find(Id);
            modelContext.Remove(about);
            modelContext.SaveChanges();
        }

        public Firstaboutu find(decimal Id)
        {
            var about = modelContext.Firstaboutus.SingleOrDefault(a => a.Id == Id);
            return about;
        }

        public IList<Firstaboutu> List()
        {
            var about = modelContext.Firstaboutus.ToList();
            return about;
        }

        public void Update(decimal Id, Firstaboutu intity)
        {
            modelContext.Update(intity);
            modelContext.SaveChanges();
        }
    }
}
