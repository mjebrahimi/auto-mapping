using AutoMapper;
using AutoMapping.Pattern1.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace AutoMapping.Pattern1.Infrastructure
{
    public abstract class BaseDto<TDto, TEntity, TKey>
         where TDto : class, new()
         where TEntity : BaseEntity<TKey>, new()
    {
        [Display(Name = "ردیف")]
        public TKey Id { get; set; }

        /// <summary>
        /// Maps the specified view model to a entity object.
        /// </summary>
        public TEntity ToEntity()
        {
            return Mapper.Map<TEntity>(CastToDerivedClass(this));
        }

        public TEntity ToEntity(TEntity entity)
        {
            return Mapper.Map(CastToDerivedClass(this), entity);
        }

        public static TDto FromEntity(TEntity model)
        {
            return Mapper.Map<TDto>(model);
        }

        #region Private
        protected TDto CastToDerivedClass(BaseDto<TDto, TEntity, TKey> baseInstance)
        {
            return Mapper.Map<TDto>(baseInstance);
        }
        #endregion
    }

    public abstract class BaseDto<TDto, TEntity> : BaseDto<TDto, TEntity, int>
        where TDto : class, new()
        where TEntity : BaseEntity<int>, new()
    {

    }
}
