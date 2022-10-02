using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class FavoriteDbRepository : IMarketingRepository<Firstfavorite>
    {
        private readonly ModelContext modelContext;

        public FavoriteDbRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }
        public void Add(Firstfavorite intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var fav = find(Id);
            modelContext.Remove(fav);
            modelContext.SaveChanges();
        }

        public Firstfavorite find(decimal Id)
        {
            var fav = modelContext.Firstfavorites.Include(c => c.User).Include(p=>p.Product).SingleOrDefault(c => c.Id == Id);
            return fav;
        }

        public IList<Firstfavorite> List()
        {
            var favorite = modelContext.Firstfavorites.Include(c => c.User).Include(p=>p.Product).ToList();
            return favorite;
        }

        public void Update(decimal Id, Firstfavorite intity)
        {
            modelContext.Update(intity);
            modelContext.SaveChanges();
        }
    }
}
