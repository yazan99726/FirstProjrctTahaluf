using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class ReviewProductRepository : IMarketingRepository<FirstReviewProduct>
    {
        private readonly ModelContext modelContext;

        public ReviewProductRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }
        public void Add(FirstReviewProduct intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var review = find(Id);
            modelContext.Remove(review);
            modelContext.SaveChanges();
        }

        public FirstReviewProduct find(decimal Id)
        {
            var review = modelContext.FirstReviewProducts.Include(r => r.Product).Include(u => u.User).SingleOrDefault(a => a.Id == Id);
            return review;
        }

        public IList<FirstReviewProduct> List()
        {
            var review = modelContext.FirstReviewProducts.Include(r=>r.Product).Include(u=>u.User).ToList();
            return review;
        }

        public void Update(decimal Id, FirstReviewProduct intity)
        {
            modelContext.Update(intity);
            modelContext.SaveChanges();
        }
    }
}
