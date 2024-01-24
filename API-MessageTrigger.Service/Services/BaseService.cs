using API_MessageTrigger.Domain.Entities;
using API_MessageTrigger.Domain.Interfaces;
using FluentValidation;

namespace API_MessageTrigger.Service.Services
{
    public class BaseService<TEntity>(IBaseRepository<TEntity> baseRepository) : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _baseRepository = baseRepository;

        public TEntity Add<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            BaseService<TEntity>.Validate(obj, Activator.CreateInstance<TValidator>());
            _baseRepository.Insert(obj);
            return obj;
        }

        private static void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IList<TEntity> Get()
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public TEntity Update<TValidator>(TEntity obj) where TValidator : FluentValidation.AbstractValidator<TEntity>
        {
            throw new NotImplementedException();
        }
    }
}
