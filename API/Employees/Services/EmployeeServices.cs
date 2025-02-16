using MonApi.API.Employees.DTOs;
using MonApi.API.Employees.Models;
using MonApi.API.Employees.Extensions;
using MonApi.API.Employees.Repositories;
using Microsoft.EntityFrameworkCore.Scaffolding;
using MonApi.API.Employees.Filters;
using MonApi.API.Passwords.Repositories;
using MonApi.API.Passwords.Extensions;
using MonApi.API.Suppliers.DTOs;
using MonApi.Shared.Exceptions;
using MonApi.Shared.Pagination;


namespace MonApi.API.Employees.Services;

public class EmployeeServices : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPasswordRepository _passwordRepository;

    public EmployeeServices(IEmployeeRepository employeeRepository, IPasswordRepository passwordRepository)
    {
        _employeeRepository = employeeRepository;
        _passwordRepository = passwordRepository;
    }


    public async Task<ReturnEmployeeDto> GetEmployeeByIdAsync(int id)
    {
        Console.WriteLine("GetEmployeeByIdAsync");
        ReturnEmployeeDto employee =
            await _employeeRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");
        if (employee.DeletionTime != null) throw new Exception("Employee deleted");

        return employee;
    }

    public async Task<ReturnEmployeeDto> AddEmployeeAsync(CreateEmployeeDto createEmployeeDto)
    {
        Console.WriteLine("adding new employee");
        if (await _employeeRepository.AnyAsync(s => s.Email == createEmployeeDto.Email))
            throw new ArgumentException("Employee already exist");

        var newPassword = createEmployeeDto.MapToPasswordModel();
        var createdPassword = await _passwordRepository.AddAsync(newPassword);

        var newEmployee = createEmployeeDto.MapToEmployeeModel(createdPassword);
        Console.WriteLine("creating new employee");

        await _employeeRepository.AddAsync(newEmployee);


        ReturnEmployeeDto newEmployeeDetails = await _employeeRepository.FindAsync(newEmployee.EmployeeId) ??
                                               throw new KeyNotFoundException("Id not found");
        Console.WriteLine("retrieving employee details");

        return newEmployeeDetails;
    }

    public async Task<ReturnEmployeeDto> UpdateEmployeeAsync(int id, UpdateEmployeeDto employee)
    {
        var model = await _employeeRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");

        if (model.DeletionTime != null) throw new Exception("Employee deleted");

        var updateEmployee = employee.MapToEmployeeModel(id);

        updateEmployee.PasswordId = model.PasswordId;

        await _employeeRepository.UpdateAsync(updateEmployee);
        ReturnEmployeeDto newModifiedEmployeeDetails =
            await _employeeRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");


        return newModifiedEmployeeDetails;
    }

    public async Task<ReturnEmployeeDto> SoftDeleteEmployeeAsync(int id)
    {
        ReturnEmployeeDto employee =
            await _employeeRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");
        if (employee.DeletionTime != null) throw new Exception("Employee deleted");

        var foundPassword = await _passwordRepository.FindAsync(employee.PasswordId)
                            ?? throw new NullReferenceException("Can't find password");
        if (foundPassword.DeletionTime != null)
            throw new SoftDeletedException("Password already deleted");
        
        foundPassword.DeletionTime = DateTime.UtcNow;
        await _passwordRepository.UpdateAsync(foundPassword);

        Employee employeeToDelete = employee.MapToEmployeeModel(id);

        employeeToDelete.DeletionTime = DateTime.UtcNow;

        employee.DeletionTime = employeeToDelete.DeletionTime;

        await _employeeRepository.UpdateAsync(employeeToDelete);

        return employee;
    }

    public async Task<PagedResult<ReturnEmployeeDto>> GetAllEmployeesAsync(EmployeeQueryParameters queryParameters)
    {
        var employee = await _employeeRepository.GetAll(queryParameters);
        return employee;
    }
}