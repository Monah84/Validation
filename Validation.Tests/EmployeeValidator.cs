using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.Tests
{
    public class EmployeeValidator : Validator<Employee>
    {
        public override void Validate(Employee item)
        {
            if (item == null)
                AddError("", "The object is null");
            else
            {
                if (string.IsNullOrWhiteSpace(item.FirstName))
                    AddError(nameof(item.FirstName), "First Name is required");
                else if (item.FirstName.Length > 50)
                    AddError(nameof(item.FirstName), "First Name should not exceed 50 characters");


                if (string.IsNullOrWhiteSpace(item.LastName))
                    AddError(nameof(item.LastName), "Last Name is required!");
                else if (item.LastName.Length > 50)
                    AddError(nameof(item.LastName), "Last Name should not exceed 50 characters");

                if (item.DateOfBirth == DateTime.MinValue)
                    AddError(nameof(item.DateOfBirth), "Date Of Birth should has a valid value");
                else if (item.DateOfBirth > DateTime.Today)
                    AddError(nameof(item.DateOfBirth), "Date Of Birth cannot be in future");
                else if (DateTime.Today.Subtract(item.DateOfBirth).TotalDays / 365.25 < 18 && !string.IsNullOrEmpty(item.FirstName))
                    AddError(nameof(item.DateOfBirth), "{0} should be at least 18 years old", item.FirstName);
            }
        }
    }
}
