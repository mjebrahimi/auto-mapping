﻿namespace AutoMapping.Pattern2.Data.Entities
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}
