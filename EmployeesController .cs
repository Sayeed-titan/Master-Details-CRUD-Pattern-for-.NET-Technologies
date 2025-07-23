[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMasterDetailsRepository<Employee, EmployeeSkill> _repository;

    public EmployeesController(IMasterDetailsRepository<Employee, EmployeeSkill> repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public IActionResult Create([FromForm] EmployeeDto dto)
    {
        var employee = new Employee
        {
            Name = dto.Name,
            Age = dto.Age,
            Photo = ImageHelper.ConvertToBytes(dto.PhotoFile),
            Skills = dto.SkillIds
        };

        var id = _repository.Create(employee, dto.SkillIds);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var employee = _repository.GetById(id);
        return Ok(new EmployeeResponse
        {
            Id = employee.Id,
            Name = employee.Name,
            PhotoBase64 = ImageHelper.ToBase64String(employee.Photo),
            Skills = employee.EmployeeSkills.Select(es => es.Skill.Name)
        });
    }
}
