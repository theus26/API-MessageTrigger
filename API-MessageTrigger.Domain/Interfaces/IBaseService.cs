using API_MessageTrigger.Domain.Entities;
using FluentValidation;

namespace API_MessageTrigger.Domain.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        TEntity Add<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;
        TEntity GetInstanceNameByPhoneNumber(string phoneNumber);

     
    }
}
