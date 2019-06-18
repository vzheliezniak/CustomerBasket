using CustomerBasketProject.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBasketProject.Concrete
{
    public class RuleContainer : IRuleContainer
    {
        //Mocked rules container
        public string[] GetRules()
        {
            return new string[] {
                "RP-25D-SITB|2|>=|[OnProduct|Percent|RP-5NS-DITB|50|1|True|]",
                "RP-1TB-EITB|1|=|[OnProduct|FreeGift|RP-RPM-FIT|||True]",
                "RP-5NS-DITB|100|>=|[OnBasket|Percent||30||False]"
            };
        }
    }
}
