using AutoMapper;
using CreateApiProject.Models;
using CreateApiProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CreateApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository employeeRepository;
        private readonly ILogger<EmployeeController> logger;
        private readonly IMapper mapper;

        public EmployeeController(EmployeeRepository employeeRepository, ILogger<EmployeeController> logger,
            IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet("get-emp")]
        public async Task<ActionResult<List<Employee>>> GetAllEmployee()
        {
            IEnumerable<Employee> emp = await employeeRepository.SpGetAllEmployee();
            if(emp == null) { return BadRequest(); }           
            return Ok(mapper.Map<List<Employee>>(emp));

        }
    }
}
