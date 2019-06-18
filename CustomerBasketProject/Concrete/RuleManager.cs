using CustomerBasketProject.Abstract;
using CustomerBasketProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBasketProject.Concrete
{
    public class RuleManager : IRuleManager
    {
        private readonly IRuleContainer _ruleContainer;

        public RuleManager()
        {
            _ruleContainer = new RuleContainer();
        }

        public decimal ApplyDiscounts(BasketModel basket)
        {
            throw new NotImplementedException();
        }

        public List<DiscountModel> GetApplicableDiscounts(BasketModel basket)
        {
            throw new NotImplementedException();
        }

        private List<DiscountModel> ParseRules(string [] rules)
        {
            throw new NotImplementedException();
        }
    }
}
