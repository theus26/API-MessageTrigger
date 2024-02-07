using API_MessageTrigger.Domain.Entities;

namespace API_MessageTrigger.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        void Insert(TEntity obj);

        void Update(TEntity obj);

        void Delete(int id);

        IList<TEntity> Select();

        TEntity Select(int id);
        TEntity GetInstanceEvolutionByGetPhoneNumber(string numberPhone);
    }
}
