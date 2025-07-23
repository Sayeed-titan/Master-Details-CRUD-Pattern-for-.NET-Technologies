// Master Entity
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public byte[] Photo { get; set; }
    public bool IsActive { get; set; }
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public EmployeeType Type { get; set; }
    public List<EmployeeSkill> EmployeeSkills { get; set; } = new List<EmployeeSkill>();
}

// Detail Entity
public class EmployeeSkill
{
    public int EmployeeId { get; set; }
    public int SkillId { get; set; }
    public int ProficiencyLevel { get; set; }
    public Employee Employee { get; set; }
    public Skill Skill { get; set; }
}

// Skill Catalog
public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
