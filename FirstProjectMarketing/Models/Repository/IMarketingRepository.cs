using System.Collections.Generic;

namespace FirstProjectMarketing.Models.Repository
{
    public interface IMarketingRepository<TIntity>
    {
        IList<TIntity> List();
        TIntity find(decimal Id);
        void Add(TIntity intity);
        void Update(decimal Id, TIntity intity);
        void Delete(decimal Id);
    }
}
