using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Validation.Tests
{
    [TestClass]
    public class EmployeeTests
    {
        [TestMethod]
        public void ValidEmployeeTest()
        {
            var emp = new Employee
            {
                FirstName = "John",
                LastName = "Adams",
                DateOfBirth = new DateTime(1995, 1, 1)
            };

            var validate = emp.Validate();
            Assert.AreEqual(validate.IsValid, true);
        }

        [TestMethod]
        public void AgeTest()
        {
            var emp = new Employee
            {
                FirstName = "John",
                LastName = "Adams",
                DateOfBirth = new DateTime(2007, 1, 1)
            };
            var validate = emp.Validate();
            Assert.AreEqual(validate[nameof(emp.DateOfBirth)], string.Format("{0} should be at least 18 years old", emp.FirstName));
        }

        [TestMethod]
        public void DateInFutureTest()
        {
            var emp = new Employee
            {
                FirstName = "John",
                LastName = "Adams",
                DateOfBirth = DateTime.Today.AddDays(100)
            };
            var validate = emp.Validate();
            Assert.AreEqual(validate[nameof(emp.DateOfBirth)], "Date Of Birth cannot be in future");
        }

        [TestMethod]
        public void DateNotValidTest()
        {
            var emp = new Employee
            {
                FirstName = "John",
                LastName = "Adams",
            };
            var validate = emp.Validate();
            Assert.AreEqual(validate[nameof(emp.DateOfBirth)], "Date Of Birth should has a valid value");
        }

        [TestMethod]
        public void FirstNameLengthTest()
        {
            var emp = new Employee
            {
                FirstName = "John          Something to test its length         XYZ",
                LastName = "Adams",
            };
            var validate = emp.Validate();
            Assert.AreEqual(validate[nameof(emp.FirstName)], "First Name should not exceed 50 characters");
        }
    }
}