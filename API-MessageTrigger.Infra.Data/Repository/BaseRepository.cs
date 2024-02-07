using API_MessageTrigger.Domain.Entities;
using API_MessageTrigger.Domain.Interfaces;
using API_MessageTrigger.Infra.Data.Context;

namespace API_MessageTrigger.Infra.Data.Repository
{
    public class BaseRepository<TEntity>(MessageTriggerContext mySqlContext) : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly MessageTriggerContext _mySqlContext = mySqlContext;

        public void Insert(TEntity obj)
        {
            _mySqlContext.Set<TEntity>().Add(obj);
            _mySqlContext.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _mySqlContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _mySqlContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _mySqlContext.Set<TEntity>().Remove(Select(id));
            _mySqlContext.SaveChanges();
        }

        public IList<TEntity> Select() =>
            _mySqlContext.Set<TEntity>().ToList();

        public TEntity Select(int id) =>
            _mySqlContext.Set<TEntity>().Find(id);

        public TEntity GetInstanceEvolutionByGetPhoneNumber(string numberPhone)
        {
            return _mySqlContext.Set<TEntity>().FirstOrDefault(x => x.PhoneNumber == numberPhone);
        }
    }
}
