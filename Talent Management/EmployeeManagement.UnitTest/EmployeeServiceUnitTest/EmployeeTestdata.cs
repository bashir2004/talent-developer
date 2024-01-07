using EmployeeManagement.API.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.UnitTest.EmployeeServiceUnitTest
{
    public class EmployeeTestdata : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new CreateEmployeeViewModel { Username = "JohnDoe",PhoneNumber = "123456789", Email = "someone@email.com",SkillSet = "Net, Angular", Hobbies = "playing games" } };
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
