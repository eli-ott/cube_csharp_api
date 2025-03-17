using MonApi.API.Employees.DTOs;
using MonApi.API.Employees.Models;
using MonApi.API.Passwords.Models;
using MonApi.API.Roles.Extensions;

namespace MonApi.API.Employees.Extensions;

public static class EmployeeExtensions
{
    public static Employee MapToEmployeeModel(this CreateEmployeeDto createEmployeeDto, Password password)
    {
        return new Employee()
        {
            FirstName = createEmployeeDto.FirstName,
            LastName = createEmployeeDto.LastName,
            Email = createEmployeeDto.Email,
            Phone = createEmployeeDto.Phone,
            RoleId = createEmployeeDto.RoleId,
            PasswordId = password.PasswordId
        };
    }
    
    public static Employee MapToEmployeeModel(this ReturnEmployeeDto returnEmployeeDto, int id)
    {
        return new Employee()
        {
            EmployeeId = id,
            FirstName = returnEmployeeDto.FirstName,
            LastName = returnEmployeeDto.LastName,
            Email = returnEmployeeDto.Email,
            Phone = returnEmployeeDto.Phone,
            RoleId = returnEmployeeDto.Role.RoleId,
            PasswordId =  returnEmployeeDto.Password != null ? returnEmployeeDto.Password.PasswordId : returnEmployeeDto.PasswordId
        };
    }
    
    public static Employee MapToEmployeeModel(this UpdateEmployeeDto updateEmployeeDto, int id)
    {
        return new Employee()
        {
            EmployeeId = id,
            FirstName = updateEmployeeDto.FirstName,
            LastName = updateEmployeeDto.LastName,
            Email = updateEmployeeDto.Email,
            Phone = updateEmployeeDto.Phone,
            Role = updateEmployeeDto.Role.MapToRoleModel()
        };
    }
}