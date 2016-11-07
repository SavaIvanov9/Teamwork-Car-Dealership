using Dealership.DataSeed.Contracts;
using System.Collections.Generic;
using Dealership.DataSeed.Models;
using Dealership.Data.Contracts;
using Dealership.Models.Models.XmlSource;
using System.Linq;

namespace Dealership.Data.Seeders
{
    public class EmployeeSeeder : IEmployeeSeeder
    {
        private readonly IDealershipData data;
        private readonly IDealershipRepository<Employee> employees;
        private readonly IDealershipRepository<Position> positions;
        private readonly IDealershipRepository<Country> countries;
        private readonly IDealershipRepository<City> cities;
        private readonly IDealershipRepository<Address> addresses;
        private readonly IDealershipRepository<Shop> shops;

        public EmployeeSeeder(IDealershipData data,
            IDealershipRepository<Employee> employees,
            IDealershipRepository<Position> positions,
            IDealershipRepository<Country> countries,
            IDealershipRepository<City> cities,
            IDealershipRepository<Address> addresses,
            IDealershipRepository<Shop> shops)
        {
            this.data = data;
            this.employees = employees;
            this.positions = positions;
            this.countries = countries;
            this.cities = cities;
            this.addresses = addresses;
            this.shops = shops;
        }

        public bool IsDataSeeded()
        {
            var isDataSeeded = this.employees.Any();
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
                var containsPosition = this.positions.Local.Where(p => p.Name == positionName).Any();
                if (!containsPosition)
                {
                    var position = new Position()
                    {
                        Name = positionName
                    };

                    this.positions.Add(position);
                }
            }

            data.SaveChanges();
        }

        private void SeedCountries(IEnumerable<EmployeeDto> employees)
        {
            foreach (var employee in employees)
            {
                var countryName = employee.Address.Country.Name;
                var containsCountry = this.countries.Local.Where(c => c.Name == countryName).Any();
                if (!containsCountry)
                {
                    var country = new Country()
                    {
                        Name = countryName
                    };

                    this.countries.Add(country);
                }
            }

            data.SaveChanges();
        }

        private void SeedCities(IEnumerable<EmployeeDto> employees)
        {
            foreach (var employee in employees)
            {
                var cityName = employee.Address.City.Name;
                var containsCity = this.cities.Local.Where(c => c.Name == cityName).Any();
                if (!containsCity)
                {
                    var countryName = employee.Address.Country.Name;
                    var country = this.countries.FirstOrDefault(c => c.Name == countryName);

                    var city = new City()
                    {
                        Name = cityName,
                        Country = country
                    };

                    country.Cities.Add(city);
                    this.cities.Add(city);
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
                var containsEmployeeAddress = this.addresses.Local.Where(a => a.Street == employeeStreet).Any();
                if (!containsEmployeeAddress)
                {
                    SeedAddress(employeeAddress);
                }

                var shopAddress = employee.Shop.Address;
                var shopStreet = shopAddress.Street;
                var containsShopAddress = this.addresses.Local.Where(a => a.Street == shopStreet).Any();
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
            var city = this.cities.FirstOrDefault(c => c.Name == cityName);

            var zipCode = addressDto.ZipCode;
            var street = addressDto.Street;

            var address = new Address()
            {
                Street = street,
                ZipCode = zipCode,
                City = city
            };

            city.Addresses.Add(address);
            this.addresses.Add(address);
        }

        private void SeedShops(IEnumerable<EmployeeDto> employees)
        {
            foreach (var employee in employees)
            {
                var shopName = employee.Shop.Name;
                var containsShop = this.shops.Local.Where(sh => sh.Name == shopName).Any();
                if (!containsShop)
                {
                    var addressStreet = employee.Shop.Address.Street;
                    var address = this.addresses.FirstOrDefault(a => a.Street == addressStreet);

                    var shop = new Shop()
                    {
                        Name = shopName,
                        Address = address
                    };

                    address.Shops.Add(shop);
                    this.shops.Add(shop);
                }
            }

            data.SaveChanges();
        }

        private void SeedEmployees(IEnumerable<EmployeeDto> employees)
        {
            foreach (var employee in employees)
            {
                var employeePid = employee.Pid;
                var containsEmployee = this.employees.Local.Where(e => e.Pid == employeePid).Any();
                if (!containsEmployee)
                {
                    var addressStreet = employee.Address.Street;
                    var address = this.addresses.FirstOrDefault(a => a.Street == addressStreet);

                    var positionName = employee.Position.Name;
                    var position = this.positions.FirstOrDefault(p => p.Name == positionName);

                    var shopName = employee.Shop.Name;
                    var shop = this.shops.FirstOrDefault(sh => sh.Name == shopName);

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

                    this.employees.Add(employeeEnity);
                }
            }

            data.SaveChanges();
        }
    }
}
