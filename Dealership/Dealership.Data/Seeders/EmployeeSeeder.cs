using Dealership.DataSeed.Contracts;
using System.Collections.Generic;
using Dealership.DataSeed.Models;
using Dealership.Data.Contracts;
using Dealership.Models.Models.XmlSource;
using System;
using System.Linq;

namespace Dealership.Data.Seeders
{
    public class EmployeeSeeder : IEmployeeSeeder
    {
        private readonly IDealershipData data;

        public EmployeeSeeder(IDealershipData data)
        {
            this.data = data;
        }

        public bool IsDataSeeded()
        {
            var isDataSeeded = this.data.Employees.Any();
            return isDataSeeded;
        }

        public void Seed(IEnumerable<EmployeeDto> employees)
        {
            this.SeedPositions(employees);

            this.SeedCountries(employees);

            this.SeedCities(employees);

            this.SeedAddresses(employees);

            this.SeedShops(employees);

            this.SeedEmployees(employees);
        }

        private void SeedPositions(IEnumerable<EmployeeDto> employees)
        {
            foreach (var employee in employees)
            {
                var positionName = employee.Position.Name;
                var containsPosition = this.data.Positions.Local.Where(p => p.Name == positionName).Count() > 0;
                if (!containsPosition)
                {
                    var position = new Position()
                    {
                        Name = positionName
                    };

                    this.data.Positions.Add(position);
                }
            }

            data.SaveChanges();
        }

        private void SeedCountries(IEnumerable<EmployeeDto> employees)
        {
            foreach (var employee in employees)
            {
                var countryName = employee.Address.Country.Name;
                var containsCountry = this.data.Countries.Local.Where(c => c.Name == countryName).Count() > 0;
                if (!containsCountry)
                {
                    var country = new Country()
                    {
                        Name = countryName
                    };

                    this.data.Countries.Add(country);
                }
            }

            data.SaveChanges();
        }

        private void SeedCities(IEnumerable<EmployeeDto> employees)
        {
            foreach (var employee in employees)
            {
                var cityName = employee.Address.City.Name;
                var containsCity = this.data.Cities.Local.Where(c => c.Name == cityName).Count() > 0;
                if (!containsCity)
                {
                    var countryName = employee.Address.Country.Name;
                    var country = this.data.Countries.FirstOrDefault(c => c.Name == countryName);

                    var city = new City()
                    {
                        Name = cityName,
                        Country = country
                    };

                    country.Cities.Add(city);
                    this.data.Cities.Add(city);
                }
            }

            data.SaveChanges();
        }

        private void SeedAddresses(IEnumerable<EmployeeDto> employees)
        {
            foreach (var employee in employees)
            {
                var employeeAddress = employee.Address;
                var employeeStreet = employeeAddress.Street;
                var containsEmployeeAddress = this.data.Addresses.Local.Where(a => a.Street == employeeStreet).Count() > 0;
                if (!containsEmployeeAddress)
                {
                    SeedAddress(employeeAddress);
                }

                var shopAddress = employee.Shop.Address;
                var shopStreet = shopAddress.Street;
                var containsShopAddress = this.data.Addresses.Local.Where(a => a.Street == shopStreet).Count() > 0;
                if (!containsShopAddress)
                {
                    SeedAddress(shopAddress);
                }
            }

            data.SaveChanges();
        }

        private void SeedAddress(AddressDto addressDto)
        {
            var cityName = addressDto.City.Name;
            var city = this.data.Cities.FirstOrDefault(c => c.Name == cityName);

            var zipCode = addressDto.ZipCode;
            var street = addressDto.Street;

            var address = new Address()
            {
                Street = street,
                ZipCode = zipCode,
                City = city
            };

            city.Addresses.Add(address);
            this.data.Addresses.Add(address);
        }

        private void SeedShops(IEnumerable<EmployeeDto> employees)
        {
            foreach (var employee in employees)
            {
                var shopName = employee.Shop.Name;
                var containsShop = this.data.Shops.Local.Where(sh => sh.Name == shopName).Count() > 0;
                if (!containsShop)
                {
                    var addressStreet = employee.Shop.Address.Street;
                    var address = this.data.Addresses.FirstOrDefault(a => a.Street == addressStreet);

                    var shop = new Shop()
                    {
                        Name = shopName,
                        Address = address
                    };

                    address.Shops.Add(shop);
                    this.data.Shops.Add(shop);
                }
            }

            data.SaveChanges();
        }

        private void SeedEmployees(IEnumerable<EmployeeDto> employees)
        {
            foreach (var employee in employees)
            {
                var employeePid = employee.Pid;
                var containsEmployee = this.data.Employees.Local.Where(e => e.Pid == employeePid).Count() > 0;
                if (!containsEmployee)
                {
                    var addressStreet = employee.Address.Street;
                    var address = this.data.Addresses.FirstOrDefault(a => a.Street == addressStreet);

                    var positionName = employee.Position.Name;
                    var position = this.data.Positions.FirstOrDefault(p => p.Name == positionName);

                    var shopName = employee.Shop.Name;
                    var shop = this.data.Shops.FirstOrDefault(sh => sh.Name == shopName);

                    var employeeEnity = new Employee()
                    {
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Pid = employeePid,
                        Phone = employee.Phone,
                        HireDate = employee.HireDate,
                        Salary = employee.Salary,
                        Address = address,
                        Position = position,
                        Shop = shop
                    };

                    address.Employees.Add(employeeEnity);
                    position.Employees.Add(employeeEnity);
                    shop.Employees.Add(employeeEnity);

                    this.data.Employees.Add(employeeEnity);
                }
            }

            data.SaveChanges();
        }
    }
}
