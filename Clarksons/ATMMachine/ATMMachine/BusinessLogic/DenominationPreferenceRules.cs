using System.Collections.Generic;

namespace ATMMachine.BusinessLogic
{
    public class DenominationPreferenceRules
    {
        public List<DenominationType> PreferedDenominationTypes { get; }

        public DenominationPreferenceRules(List<DenominationType> preferedDenominationTypes)
        {
            PreferedDenominationTypes = preferedDenominationTypes;
        }
    }
}