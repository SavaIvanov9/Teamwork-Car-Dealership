using Dealership.DataSeed.Contracts;
using System;
using System.Collections.Generic;
using Dealership.DataSeed.Models;
using System.Xml;

namespace Dealership.XmlFilesProcessing.Readers
{
    public class XmlEmployeeReader : IEmployeeReader
    {
        private const string EmployeesFilePath = @"..\..\..\Data\Xml\DbSeed\Employees.xml";

        private const string EmployeeTag = "Employee";

        private const string FirstNameTag = "FirstName";

        private const string LastNameTag = "LastName";

        private const string PidTag = "Pid";

        private const string PhoneTag = "Phone";

        private const string ShopNameTag = "Name";

        private const string HireDateTag = "HireDate";

        private const string SalaryTag = "Salary";

        private const string PositionTag = "Position";

        private const string AddressTag = "Address";

        private const string StreetTag = "Street";

        private const string ZipCodeTag = "ZipCode";

        private const string CityTag = "City";

        private const string CountryTag = "Country";

        private const string ShopTag = "Shop";

        public IEnumerable<EmployeeDto> ReadEmployees()
        {
            var employees = new List<EmployeeDto>();
            using (var xmlReader = XmlReader.Create(EmployeesFilePath))
            {
                while (xmlReader.Read())
                {
                    var nodeName = xmlReader.Name;
                    if (nodeName == EmployeeTag)
                    {
                        var employee = this.ReadEmployee(xmlReader);

                        employees.Add(employee);
                    }
                }
            }

            return employees;
        }

        private EmployeeDto ReadEmployee(XmlReader xmlReader)
        {
            var employee = new EmployeeDto();

            while (xmlReader.Read() && xmlReader.Name != EmployeeTag)
            {
                this.FillEmployeePersonalData(employee, xmlReader);

                this.FillEmployeeWorkinData(employee, xmlReader);
            }

            return employee;
        }

        private void FillEmployeeWorkinData(EmployeeDto employee, XmlReader xmlReader)
        {
            var nodeName = xmlReader.Name;
            if (nodeName == HireDateTag)
            {
                var hireDate = DateTime.Parse(this.ReadValue(xmlReader));
                employee.HireDate = hireDate;
            }
            else if (nodeName == SalaryTag)
            {
                var salary = int.Parse(this.ReadValue(xmlReader));
                employee.Salary = salary;
            }
            else if (nodeName == PositionTag)
            {
                var position = this.ReadPosition(xmlReader);
                employee.Position = position;
            }
            else if (nodeName == ShopTag)
            {
                var shop = this.ReadShop(xmlReader);
                employee.Shop = shop;
            }
        }

        private void FillEmployeePersonalData(EmployeeDto employee, XmlReader xmlReader)
        {
            var nodeName = xmlReader.Name;
            if (nodeName == FirstNameTag)
            {
                var firstName = this.ReadValue(xmlReader);
                employee.FirstName = firstName;
            }
            else if (nodeName == LastNameTag)
            {
                var lastName = this.ReadValue(xmlReader);
                employee.LastName = lastName;
            }
            else if (nodeName == PidTag)
            {
                var pid = this.ReadValue(xmlReader);
                employee.Pid = pid;
            }
            else if (nodeName == PhoneTag)
            {
                var phone = this.ReadValue(xmlReader);
                employee.Phone = phone;
            }
            else if (nodeName == AddressTag)
            {
                var address = this.ReadAddress(xmlReader);
                employee.Address = address;
            }
        }

        private AddressDto ReadAddress(XmlReader xmlReader)
        {
            var address = new AddressDto();
            while (xmlReader.Read() && xmlReader.Name != AddressTag)
            {
                var nodeName = xmlReader.Name;
                if (nodeName == StreetTag)
                {
                    var street = this.ReadValue(xmlReader);
                    address.Street = street;
                }
                else if (nodeName == ZipCodeTag)
                {
                    var zipCode = this.ReadValue(xmlReader);
                    address.ZipCode = zipCode;
                }
                else if (nodeName == CityTag)
                {
                    var city = this.ReadCity(xmlReader);
                    address.City = city;
                }
                else if (nodeName == CountryTag)
                {
                    var country = this.ReadCountry(xmlReader);
                    address.Country = country;
                }
            }

            return address;
        }

        private CountryDto ReadCountry(XmlReader xmlReader)
        {
            var country = new CountryDto();

            var name = this.ReadValue(xmlReader);

            country.Name = name;

            return country;
        }

        private CityDto ReadCity(XmlReader xmlReader)
        {
            var city = new CityDto();

            var name = this.ReadValue(xmlReader);

            city.Name = name;

            return city;
        }

        private ShopDto ReadShop(XmlReader xmlReader)
        {
            var shop = new ShopDto();
            while (xmlReader.Read() && xmlReader.Name != ShopTag)
            {
                var nodeName = xmlReader.Name;
                if (nodeName == ShopNameTag)
                {
                    var name = this.ReadValue(xmlReader);
                    shop.Name = name;
                }
                else if (nodeName == AddressTag)
                {
                    var address = this.ReadAddress(xmlReader);
                    shop.Address = address;
                }
            }

            return shop;
        }

        private PositionDto ReadPosition(XmlReader xmlReader)
        {
            var position = new PositionDto();

            var name = ReadValue(xmlReader);

            position.Name = name;

            return position;
        }

        private string ReadValue(XmlReader xmlReader)
        {
            xmlReader.Read();

            var value = xmlReader.Value;

            xmlReader.Read();

            return value;
        }
    }
}
