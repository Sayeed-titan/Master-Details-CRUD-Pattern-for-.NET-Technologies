public class EmployeeService
{
    // CREATE
    public int CreateEmployee(Employee employee, List<int> skillIds)
    {
        using var transaction = context.Database.BeginTransaction();
        try
        {
            // Handle image conversion
            if (employee.PhotoFile != null)
                employee.Photo = ImageHelper.ConvertToBytes(employee.PhotoFile);
            
            context.Employees.Add(employee);
            context.SaveChanges();
            
            // Add details
            foreach (var skillId in skillIds)
            {
                context.EmployeeSkills.Add(new EmployeeSkill 
                {
                    EmployeeId = employee.Id,
                    SkillId = skillId,
                    ProficiencyLevel = 3 // Default value
                });
            }
            
            context.SaveChanges();
            transaction.Commit();
            return employee.Id;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    // READ
    public Employee GetEmployeeWithSkills(int id)
    {
        return context.Employees
            .Include(e => e.EmployeeSkills)
                .ThenInclude(es => es.Skill)
            .FirstOrDefault(e => e.Id == id);
    }

    // UPDATE
    public void UpdateEmployee(Employee employee, List<int> newSkillIds)
    {
        using var transaction = context.Database.BeginTransaction();
        try
        {
            // Handle image update
            if (employee.PhotoFile != null)
                employee.Photo = ImageHelper.ConvertToBytes(employee.PhotoFile);
            
            // Update master
            context.Entry(employee).State = EntityState.Modified;
            
            // Sync details
            var existingSkills = context.EmployeeSkills
                .Where(es => es.EmployeeId == employee.Id)
                .ToList();
                
            // Remove old skills
            context.EmployeeSkills.RemoveRange(existingSkills);
            
            // Add new skills
            foreach (var skillId in newSkillIds)
            {
                context.EmployeeSkills.Add(new EmployeeSkill 
                {
                    EmployeeId = employee.Id,
                    SkillId = skillId
                });
            }
            
            context.SaveChanges();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}
