using H3Project.Data.Context;
using H3Project.Data.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class EmployeeRepository
{
    private readonly CinemaDbContext _dbContext;

    public EmployeeRepository(CinemaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _dbContext.Employees.ToListAsync();
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        return await _dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
    }

    public async Task AddEmployeeAsync(Employee employee)
    {
        await _dbContext.Employees.AddAsync(employee);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
        _dbContext.Employees.Update(employee);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        var employee = await _dbContext.Employees.FindAsync(id);
        if (employee != null)
        {
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
        }
    }
}
