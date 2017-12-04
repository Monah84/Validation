# Validation
A very simple way and easy to use validation library that validates any object

## How to use
Usually you have couple of classes like `Employee` or `Person` or `Car` and you want to validate these objects

1. First Step is to define your classes for example `Employee`

```csharp
    public class Employee
    {
        public string FirstName{get; set;}
        public string LastName {get; set;}
        public DateTime DateOfBirth { get; set;}
    }
```

2. Define the `EmployeeValidator` class which inherits from `Validator<Employee>` as 
    
 ```csharp
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
```

3. Comsume the code as

```csharp
    var emp = new Employee
    {
        FirstName = "John",
        LastName = "Adams",
        DateOfBirth = DateTime.Today.AddDays(10000);
    };
    var validate = emp.Validate();
    if(!validate.IsValid)
    {
        foreach(var error in validate.Errors)
        {
            Console.WriteLine("{0}: {1}",error.Key, error.Value);
        }         
    }
    
```

