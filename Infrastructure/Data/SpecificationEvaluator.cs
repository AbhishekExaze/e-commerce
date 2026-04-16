using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
    {
        if (spec.Includes != null)
        {
            inputQuery = spec.Includes.Aggregate(inputQuery, (current, include) => current.Include(include));
        }
        if (spec.Criteria != null)
        {
            inputQuery = inputQuery.Where(spec.Criteria);
        }
        if (spec.OrderBy != null)
        {
            inputQuery = inputQuery.OrderBy(spec.OrderBy);
        }
        if (spec.OrderByDescending != null)
        {
            inputQuery = inputQuery.OrderByDescending(spec.OrderByDescending);
        }
        if (spec.IsDistinct)
        {
            inputQuery = inputQuery.Distinct();
        }
        if (spec.IsPagingEnabled)
        {
            inputQuery = inputQuery.Skip(spec.Skip).Take(spec.Take);
        }
        return inputQuery;
    }
    public static IQueryable<TResult> GetQuery<TSpec,TResult>(IQueryable<T> inputQuery, ISpecification<T, TResult> spec)
    {
        if(spec.OrderByDescending!=null)        {
            inputQuery = inputQuery.OrderByDescending(spec.OrderByDescending);
        }
        var query = inputQuery as IQueryable<TResult>;
        if (spec.Select != null)
        {
            query = inputQuery.Select(spec.Select);
        }
        if (spec.IsDistinct)
        {
            query = query?.Distinct();
        }
        if (spec.IsPagingEnabled)
        {
            query = query?.Skip(spec.Skip).Take(spec.Take);
        }
        return query ?? inputQuery.Cast<TResult>();
    }
}
