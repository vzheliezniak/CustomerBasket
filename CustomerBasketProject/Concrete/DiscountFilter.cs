using CustomerBasketProject.Abstract;
using CustomerBasketProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CustomerBasketProject.Concrete
{
    delegate Func<BasketEntryModel, bool> RuleCondition(string productId, int amount);
    public class DiscountFilter: IDiscountFilter
    {
        private readonly Dictionary<string, RuleCondition> DiscountCondition;
        public DiscountFilter()
        {
            DiscountCondition = new Dictionary<string, RuleCondition> {
                { ">=", GreaterThanOrEqual},
                { ">", GreaterThan},
                { "<=", LessThanOrEqual},
                { "=", Equal}
            };
        }

        public bool CheckRuleCondition(BasketModel basket, string operatorSymbol, string productId, int  amount)
        {
            DiscountCondition.TryGetValue(operatorSymbol, out RuleCondition rule);
            var res = basket.BasketContent.Where(rule.Invoke(productId, amount)).ToList();

            return res.Any();
        }
             
        private Func<BasketEntryModel, bool> GreaterThanOrEqual(string productId, int amount)
        {
            var parameter = Expression.Parameter(typeof(BasketEntryModel), "basketContent");
            var amountComparison = Expression.GreaterThanOrEqual(Expression.Property(parameter, Type.GetType("CustomerBasketProject.Models.BasketEntryModel").GetProperty("Product.Quantity")), Expression.Constant(amount));
            var idComparison = GetProductIdEvaluation(productId);
            var comparison = Expression.And(amountComparison, idComparison);
           
            return Expression.Lambda<Func<BasketEntryModel, bool>>(comparison, parameter).Compile();
        }

        private Func<BasketEntryModel, bool> GreaterThan(string productId, int amount)
        {
            var parameter = Expression.Parameter(typeof(BasketEntryModel), "basketContent");
            var amountComparison = Expression.GreaterThan(Expression.Property(parameter, Type.GetType("CustomerBasketProject.Models.BasketEntryModel").GetProperty("Product.Quantity")), Expression.Constant(amount));
            var idComparison = GetProductIdEvaluation(productId);
            var comparison = Expression.And(amountComparison, idComparison);
            return Expression.Lambda<Func<BasketEntryModel, bool>>(comparison, parameter).Compile();
        }

        private Func<BasketEntryModel, bool> LessThanOrEqual(string productId, int amount)
        {
            var parameter = Expression.Parameter(typeof(BasketEntryModel), "basketContent");
            var amountComparison = Expression.LessThanOrEqual(Expression.Property(parameter, Type.GetType("CustomerBasketProject.Models.BasketEntryModel").GetProperty("Product.Quantity")), Expression.Constant(amount));
            var idComparison = GetProductIdEvaluation(productId);
            var comparison = Expression.And(amountComparison, idComparison);

            return Expression.Lambda<Func<BasketEntryModel, bool>>(comparison, parameter).Compile();
        }

        private Func<BasketEntryModel, bool> Equal(string productId, int amount)
        {
            var parameter = Expression.Parameter(typeof(BasketEntryModel), "basketContent");
            var amountComparison = Expression.Equal(Expression.Property(parameter, Type.GetType("CustomerBasketProject.Models.BasketEntryModel").GetProperty("Product.Quantity")), Expression.Constant(amount));
            var idComparison = GetProductIdEvaluation(productId);
            var comparison = Expression.And(amountComparison, idComparison);
            return Expression.Lambda<Func<BasketEntryModel, bool>>(comparison, parameter).Compile();
        }

        private Expression GetProductIdEvaluation(string productId)
        {
            var parameter = Expression.Parameter(typeof(BasketEntryModel), "basketContent");
            return Expression.Equal(Expression.Property(parameter, Type.GetType("CustomerBasketProject.Models.BasketEntryModel").GetProperty("Product.Id")), Expression.Constant(productId));
        }
    }
}
