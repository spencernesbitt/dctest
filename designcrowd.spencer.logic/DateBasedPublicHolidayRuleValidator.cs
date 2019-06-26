using System.Collections.Generic;

namespace designcrowd.spencer.logic
{
    public class DateBasedPublicHolidayRuleValidator: IValidator<DateBasedPublicHolidayRule>
    {
        List<string> _validationErrors;
        public bool Validate(DateBasedPublicHolidayRule rule){
            _validationErrors = new List<string>();

            if (rule.DayOfMonth == 0)
                _validationErrors.Add("The DayOfMonth property must be greater than 0");
            else if (rule.DayOfMonth > 31)
                _validationErrors.Add("The DayOfMonth property must be no greater than 31");
            
            if (rule.MonthOfYear == 0)
                _validationErrors.Add("The MonthOfYear property must be greater than 0");
            else if (rule.MonthOfYear > 12)
                _validationErrors.Add("The MonthOfYear property must be no greater than 12");

            if (rule.MonthOfYear == 2 && rule.DayOfMonth > 28)
                _validationErrors.Add("For February holidays the DayOfMonth property must be no greater than 28");
            
            if(rule.DayOfMonth > 30 && (rule.MonthOfYear == 4 || rule.MonthOfYear == 6 || rule.MonthOfYear == 9 || rule.MonthOfYear == 11))
                _validationErrors.Add("For April, June, September and November holidays the DayOfMonth property must be no greater than 28");
        
            if (_validationErrors.Count > 0 )                
                return false;
            else    
                return true;
        }

        public IList<string> ValidationErrors => _validationErrors;
    }
}