using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class BankDbRepository : IMarketingRepository<Firstbank>
    {
        private readonly ModelContext modelContext;

        public BankDbRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }
        public void Add(Firstbank intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var banc = find(Id);
            modelContext.Remove(banc);
            modelContext.SaveChanges();     
        }

        public Firstbank find(decimal Id)
        {
            var banc = modelContext.Firstbanks.Include(b => b.Firstpayments).SingleOrDefault(a => a.AccountNumber == Id.ToString());
            return banc;
        }

        public IList<Firstbank> List()
        {
            var banc = modelContext.Firstbanks.ToList();
            return banc;
        }

        public void Update(decimal Id, Firstbank intity)
        {
            modelContext.Update(intity);
            modelContext.SaveChanges();
        }
    }
}
