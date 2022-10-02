using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class ContactUsDbRepository : IMarketingRepository<Firstcontactu>
    {
        private readonly ModelContext modelContext;

        public ContactUsDbRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }
        public void Add(Firstcontactu intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var contact = find(Id);
            modelContext.Remove(contact);
            modelContext.SaveChanges();
        }

        public Firstcontactu find(decimal Id)
        {
            var contact = modelContext.Firstcontactus.SingleOrDefault(c => c.Id == Id);
            return contact;
        }

        public IList<Firstcontactu> List()
        {
            var contact = modelContext.Firstcontactus.ToList();
            return contact;
        }

        public void Update(decimal Id, Firstcontactu intity)
        {
            
            modelContext.Update(intity);
            modelContext.SaveChanges();
        }
    }
}
