using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Human_Resource_Management.Models;
using Human_Resource_Management.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Human_Resource_Management.Service
{
    public class EmployeeService : IEmployee
    {
        private ManagementContext _mgmtContext;

        public EmployeeService(ManagementContext managementContext)
        {
            _mgmtContext = managementContext;
        }

        public bool CreateEmployee(EmployeeViewModel employeeViewModel)
        {

            try
            {

                HashSet<string> addressSet = null;
                HashSet<string> phoneNumberSet = null;

                Employee employee = new Employee();
                employee.Name = employeeViewModel.EmployeeName;
                employee.Joined_Date = employeeViewModel.Joined_Date;
                // employee.Leave_Date = employeeViewModel.Joined_Date;
                employee.Salary = employeeViewModel.Salary;
                employee.SubCompany = _mgmtContext.SubCompany.Where(c => c.Id == employeeViewModel.SubCompanyId).ToList()[0];
                employee.Positions = _mgmtContext.Position.Where(c => c.Id == employeeViewModel.PositionId).ToList()[0];

                //checking optional phone numbers list is not null && empty
                //adding unique phone number only
                if (employeeViewModel.PhoneNumbers != null && employeeViewModel.PhoneNumbers.Count() > 0)
                {
                    phoneNumberSet = new HashSet<string>();
                    foreach (string addr in employeeViewModel.PhoneNumbers)
                    {
                        if (!String.IsNullOrEmpty(addr))
                        {
                            phoneNumberSet.Add(addr);
                        }
                    }
                }


                //checking optional address list is not null && empty
                //adding unique address only
                if (employeeViewModel.Addresses != null && employeeViewModel.Addresses.Count() > 0)
                {
                    addressSet = new HashSet<string>();
                    foreach (string phn in employeeViewModel.Addresses)
                    {
                        if (!String.IsNullOrEmpty(phn))
                        {
                            addressSet.Add(phn);
                        }
                    }
                }

                List<Phone> phones = new List<Phone>();
                List<Address> addresses = new List<Address>();

                Phone phone = new Phone();
                phone.Phone_Number = employeeViewModel._PhoneNumber;
                phone.Employee = employee;
                phones.Add(phone);

                Address address = new Address();
                address.Location = employeeViewModel._Address;
                address.Employee = employee;
                addresses.Add(address);


                if (phoneNumberSet != null)
                {
                    foreach (string phn in phoneNumberSet)
                    {
                        phones.Add(new Phone
                        {
                            Phone_Number = phn,
                            Employee = employee
                        });
                    }
                }

                if (addressSet != null)
                {
                    foreach (string addr in addressSet)
                    {
                        addresses.Add(new Address
                        {
                            Location = addr,
                            Employee = employee
                        });
                    }
                }

                employee.Addresses = addresses;
                employee.Phones = phones;

                _mgmtContext.Add(employee);
                _mgmtContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message " + ex.Message);
                return false;
            }

        }

        public List<Employee> GetAll(string position, string subCompany)
        {

            List<Employee> employees = _mgmtContext.Employee.Include(e => e.Addresses).Include(e => e.Positions).Include(e => e.SubCompany).Include(e => e.Phones).ToList();

            if (!String.IsNullOrEmpty(position))
            {
                employees = employees.Where(e => e.Positions.Position_Type == position).ToList();
            }

            if (!String.IsNullOrEmpty(subCompany))
            {
                employees = employees.Where(e => e.SubCompany.Name == subCompany).ToList();
            }

            return employees;
        }

        public IQueryable GetPositionsQuery()
        {
            var averageQuery = from m in _mgmtContext.Position select m.Position_Type;

            return averageQuery.Distinct();
        }

        public IQueryable GetSubCompaniesQuery()
        {
            var averageQuery = from m in _mgmtContext.SubCompany select m.Name;

            return averageQuery.Distinct();
        }

        public List<Position> GetPositions()
        {
            return _mgmtContext.Position.ToList();
        }

        public List<SubCompany> GetSubCompanies()
        {
            return _mgmtContext.SubCompany.ToList();
        }

        public EmployeeViewModel GetEmployeeViewModel(int empId)
        {
            try
            {


                Employee employee = _mgmtContext.Employee.Where(e => e._EmpId == empId).Include(e => e.Addresses).Include(e => e.Positions).Include(e => e.SubCompany).Include(e => e.Phones).ToList()[0];
                EmployeeViewModel employeeViewModel = new EmployeeViewModel();
                employeeViewModel.EmployeeName = employee.Name;
                employeeViewModel.Joined_Date = employee.Joined_Date;
                employeeViewModel.Salary = employee.Salary;
                employeeViewModel.Id = employee._EmpId;
                employeeViewModel.SubCompanyId = employee.SubCompany.Id;
                employeeViewModel.PositionId = employee.Positions.Id;
                employeeViewModel.Addresses = new List<string>();
                employeeViewModel.PhoneNumbers = new List<string>();

                int i = 0;
                foreach (Address address in employee.Addresses)
                {
                    if (i == 0)
                    {
                        employeeViewModel._Address = address.Location;
                    }
                    else
                    {
                        employeeViewModel.Addresses.Add(address.Location);
                    }
                    i++;
                }

                int j = 0;
                foreach (Phone phone in employee.Phones)
                {
                    if (j == 0)
                    {
                        employeeViewModel._PhoneNumber = phone.Phone_Number;
                    }
                    else
                    {
                        employeeViewModel.PhoneNumbers.Add(phone.Phone_Number);
                    }
                    j++;
                }
                return employeeViewModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message " + ex.Message);
                return null;
            }

        }

        public bool Update(EmployeeViewModel employeeViewModel)
        {
            try
            {
                HashSet<string> addressSet = new HashSet<string>();
                HashSet<string> phoneNumberSet = new HashSet<string>();

                Employee employee = _mgmtContext.Employee.Where(e => e._EmpId == employeeViewModel.Id).ToList()[0];
                employee.Name = employeeViewModel.EmployeeName;
                employee.Joined_Date = employeeViewModel.Joined_Date;
                employee.Salary = employeeViewModel.Salary;
                employee.SubCompany = _mgmtContext.SubCompany.Where(s => s.Id == employeeViewModel.SubCompanyId).ToList()[0];
                employee.Positions = _mgmtContext.Position.Where(p => p.Id == employeeViewModel.PositionId).ToList()[0];

                List<Address> addresses = _mgmtContext.Address.Where(a => a.Employee._EmpId == employeeViewModel.Id).ToList();
                List<Phone> phones = _mgmtContext.Phone.Where(p => p.Employee._EmpId == employeeViewModel.Id).ToList();

                phoneNumberSet.Add(employeeViewModel._PhoneNumber);
                addressSet.Add(employeeViewModel._Address);

                //checking phone numbers list is not null && empty
                //adding only unique phone number only
                if (employeeViewModel.PhoneNumbers != null && employeeViewModel.PhoneNumbers.Count() > 0)
                {

                    foreach (string addr in employeeViewModel.PhoneNumbers)
                    {
                        if (!String.IsNullOrEmpty(addr))
                        {
                            phoneNumberSet.Add(addr);
                        }
                    }
                }

                //checking address list is not null && empty
                //adding unique address only
                if (employeeViewModel.Addresses != null && employeeViewModel.Addresses.Count() > 0)
                {
                    foreach (string phn in employeeViewModel.Addresses)
                    {
                        if (!String.IsNullOrEmpty(phn))
                        {
                            addressSet.Add(phn);
                        }
                    }
                }

                int i = 0;
                try
                {
                    foreach (string phn in phoneNumberSet)
                    {
                        phones[i].Phone_Number = phn;
                        i++;
                    }

                    if (phoneNumberSet.Count() < phones.Count())
                    {
                        phones.RemoveRange(i, phones.Count() - i);
                    }

                }
                catch (ArgumentOutOfRangeException aor)
                {
                    Console.WriteLine("Message " + aor.Message);

                    if (phoneNumberSet.Count() > phones.Count())
                    {
                        while (i < phoneNumberSet.Count())
                        {
                            phones.Add(new Phone
                            {
                                Phone_Number = phoneNumberSet.ElementAt(i),
                                Employee = employee
                            });
                            i++;
                        }
                    }
                }

                employee.Phones = phones;

                int j = 0;

                try
                {
                    foreach (string addr in addressSet)
                    {
                        addresses[j].Location = addr;
                        j++;
                    }

                    if (addressSet.Count() < addresses.Count())
                    {
                        addresses.RemoveRange(j, addresses.Count() - j);
                    }

                }
                catch (ArgumentOutOfRangeException aor)
                {
                    Console.WriteLine("Message  " + aor.Message);

                    if (addressSet.Count() > addresses.Count())
                    {
                        while (j < addressSet.Count())
                        {
                            addresses.Add(new Address
                            {
                                Location = addressSet.ElementAt(j),
                                Employee = employee
                            });
                            j++;
                        }
                    }
                }

                employee.Addresses = addresses;

                _mgmtContext.Update(employee);
                _mgmtContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message at the end " + ex.Message);
                return false;
            }

        }

        public Employee GetEmployee(int id)
        {

            return _mgmtContext.Employee.Where(e => e._EmpId == id).Include(e => e.Addresses).Include(e => e.Positions)
                               .Include(e => e.SubCompany).Include(e => e.Phones).Include(e => e.EmployeeProjects)
                               .ThenInclude(e => e.Project)
                               .ToList()[0];
        }

        public bool Delete(Employee employee)
        {
            try
            {
                _mgmtContext.Remove(employee);
                _mgmtContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception  " + ex.Message);
                return false;
            }
        }
    }
}
