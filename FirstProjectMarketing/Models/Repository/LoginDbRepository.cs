using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class LoginDbRepository : IMarketingRepository<Firstlogin>
    {
        private readonly ModelContext modelContext;

        public LoginDbRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }
        public void Add(Firstlogin intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var login = find(Id);
            modelContext.Remove(login);
            modelContext.SaveChanges();
        }

        public Firstlogin find(decimal Id)
        {
            var login = modelContext.Firstlogins.SingleOrDefault(l => l.LoginId == Id);
            return login;
        }

        public IList<Firstlogin> List()
        {
            var login = modelContext.Firstlogins.Include(x => x.Role).Include(u=>u.User).ToList();
            return login;
        }

        public void Update(decimal Id, Firstlogin intity)
        {
            var login = find(Id);
            modelContext.Update(intity);
            modelContext.SaveChanges();
        }
    }
}
