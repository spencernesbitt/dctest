using System.Collections.Generic;

namespace designcrowd.spencer.logic
{
    public class DayBasedPublicHolidayRuleValidator: IValidator<DayBasedPublicHolidayRule>
    {
        List<string> _validationErrors;
        public bool Validate(DayBasedPublicHolidayRule rule){
            _validationErrors = new List<string>();

            if (rule.MonthOfYear == 0)
                _validationErrors.Add("The MonthOfYear property must be greater than 0");
            else if (rule.MonthOfYear > 12)
                _validationErrors.Add("The MonthOfYear property must be no greater than 12");
            
            if (_validationErrors.Count > 0 )                
                return false;
            else    
                return true;
        }

        public IList<string> ValidationErrors => _validationErrors;
    }
}