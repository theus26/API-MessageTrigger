using API_MessageTrigger.Domain.Entities;
using API_MessageTrigger.Domain.Interfaces;
using FluentValidation;

namespace API_MessageTrigger.Service.Services
{
    public class BaseService<TEntity>(IBaseRepository<TEntity> baseRepository) : IBaseService<TEntity> where TEntity : BaseEntity
    {
        public TEntity Add<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            baseRepository.Insert(obj);
            return obj;
        }

        private static void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }

        public TEntity GetByNumber(string phoneNumber) => baseRepository.GetByNumber(phoneNumber);

    
    }
}
