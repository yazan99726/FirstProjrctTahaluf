using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class RolesDbRepository : IMarketingRepository<Firstrole>
    {
        private readonly ModelContext modelContext;

        public RolesDbRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }

        public void Add(Firstrole intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var role = find(Id);
            modelContext.Remove(role);
            modelContext.SaveChanges();
        }

        public Firstrole find(decimal Id)
        {
            var role = modelContext.Firstroles.SingleOrDefault(r=>r.RoleId == Id);
            return role;
        }

        public IList<Firstrole> List()
        {
            var roles = modelContext.Firstroles.ToList();
            return roles;
        }

        public void Update(decimal Id, Firstrole intity)
        {
            var role = find(Id);
            modelContext.Update(role);
            modelContext.SaveChanges();
        }
    }
}
