using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class UserDbRepository : IMarketingRepository<Firstuser>
    {
        private readonly ModelContext modelContext;

        public UserDbRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }

        public void Add(Firstuser intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var user = find(Id);
            modelContext.Remove(user);
            modelContext.SaveChanges();
        }

        public Firstuser find(decimal Id)
        {
            var user = modelContext.Firstusers.SingleOrDefault(r => r.UserId == Id);
            return user;
        }

        public IList<Firstuser> List()
        {
            var user = modelContext.Firstusers.ToList();
            return user;
        }

        public void Update(decimal Id, Firstuser intity)
        {
            modelContext.Update(intity);
            modelContext.SaveChanges();
        }
    }
}
