using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class PaymentDbRepository : IMarketingRepository<Firstpayment>
    {
        private readonly ModelContext modelContext;

        public PaymentDbRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }
        public void Add(Firstpayment intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var card = find(Id);
            modelContext.Remove(card);
            modelContext.SaveChanges();
        }

        public Firstpayment find(decimal Id)
        {
            var card = modelContext.Firstpayments.Include(b=>b.Bank).SingleOrDefault(a => a.Cardnumber == Id.ToString());
            return card;
        }

        public IList<Firstpayment> List()
        {
            var card = modelContext.Firstpayments.ToList();
            return card;
        }

        public void Update(decimal Id, Firstpayment intity)
        {
            modelContext.Update(intity);
            modelContext.SaveChanges();
        }
    }
}
