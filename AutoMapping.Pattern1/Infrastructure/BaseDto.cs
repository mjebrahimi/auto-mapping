using AutoMapper;
using AutoMapping.Pattern1.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace AutoMapping.Pattern1.Infrastructure
{
    public abstract class BaseDto<TDto, TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        [Display(Name = "ردیف")]
        public TKey Id { get; set; }

        /// <summary>
        /// Maps this dto to a new entity object.
        /// </summary>
        public TEntity ToEntity()
        {
            return Mapper.Map<TEntity>(CastToDerivedClass(this));
        }

        /// <summary>
        /// Maps this dto to an exist entity object.
        /// </summary>
        public TEntity ToEntity(TEntity entity)
        {
            return Mapper.Map(CastToDerivedClass(this), entity);
        }

        /// <summary>
        /// Maps the specified entity to a new dto object.
        /// </summary>
        public static TDto FromEntity(TEntity model)
        {
            return Mapper.Map<TDto>(model);
        }

        protected TDto CastToDerivedClass(BaseDto<TDto, TEntity, TKey> baseInstance)
        {
            return Mapper.Map<TDto>(baseInstance);
        }
    }

    public abstract class BaseDto<TDto, TEntity> : BaseDto<TDto, TEntity, int>
        where TDto : class, new()
        where TEntity : BaseEntity<int>, new()
    {

    }
}
