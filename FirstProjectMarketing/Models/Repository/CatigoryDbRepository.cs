using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class CatigoryDbRepository : IMarketingRepository<Firstcatigory>
    {
        private readonly ModelContext modelContext;

        public CatigoryDbRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }
        public void Add(Firstcatigory intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var catigory = find(Id);
            modelContext.Remove(catigory);
            modelContext.SaveChanges();
        }

        public Firstcatigory find(decimal Id)
        {
            var catigory = modelContext.Firstcatigories.Include(c=>c.Store).SingleOrDefault(c => c.CatigoryId == Id);
            return catigory;
        }

        public IList<Firstcatigory> List()
        {
            var catigory = modelContext.Firstcatigories.Include(c=>c.Store).ToList();
            return catigory;
        }

        public void Update(decimal Id, Firstcatigory intity)
        {
            
            modelContext.Update(intity);
            modelContext.SaveChanges();
        }
    }
}
