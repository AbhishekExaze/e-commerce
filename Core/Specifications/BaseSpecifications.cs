using System;
using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecifications<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
    protected BaseSpecifications():this(null)
    {
    }
    public Expression<Func<T, bool>>? Criteria => criteria;

    public List<Expression<Func<T, object>>> Includes => new List<Expression<Func<T, object>>>();

    public Expression<Func<T, object>>? OrderBy {get; private set; }

    public Expression<Func<T, object>>? OrderByDescending {get; private set; }

    public bool IsDistinct {get;private set; }

    public int Take {get;private set; }

    public int Skip {get;private set; }

    public bool IsPagingEnabled {get;private set; }

    public IQueryable<T> ApplyCriteria(IQueryable<T> inputQuery)
    {
        if (Criteria != null)
        {
            inputQuery = inputQuery.Where(Criteria);
        }
        return inputQuery;
    }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }
    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }
    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}
public class BaseSpecifications<T, TResult>(Expression<Func<T, bool>>? criteria) : BaseSpecifications<T>(criteria), ISpecification<T, TResult>
{
    protected BaseSpecifications():this(null!)
    {
    }
    public Expression<Func<T, TResult>>? Select { get; private set; }
    protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
    {
        Select = selectExpression;
    }
}