using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Models.Repository
{
    public class TestimonailDbRepossitory : IMarketingRepository<Firsttestimonial>
    {
        private readonly ModelContext modelContext;

        public TestimonailDbRepossitory(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }
        public void Add(Firsttestimonial intity)
        {
            modelContext.Add(intity);
            modelContext.SaveChanges();
        }

        public void Delete(decimal Id)
        {
            var testimonial = find(Id);
            modelContext.Remove(testimonial);
            modelContext.SaveChanges();
        }

        public Firsttestimonial find(decimal Id)
        {
            var testimonial = modelContext.Firsttestimonials.Include(u => u.User).SingleOrDefault(t => t.Id == Id);
            return testimonial;
        }

        public IList<Firsttestimonial> List()
        {
            var testimonial = modelContext.Firsttestimonials.Include(u=>u.User).ToList();
            return testimonial;
        }

        public void Update(decimal Id, Firsttestimonial intity)
        {
            var testimonial = find(Id);
            modelContext.Update(intity);
            modelContext.SaveChanges();
        }
    }
}
