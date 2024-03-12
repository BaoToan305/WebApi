namespace CreateApiProject.ModelDto
{
    public class EmployeeDto
    {
        public int employee_id { get; set; }
        public string employee_name { get; set; } = string.Empty;
        public string employee_email { get; set; } = string.Empty;
        public string employee_phone { get; set; } = string.Empty;
        public string employee_address { get; set; } = string.Empty;
        public decimal employee_salary { get; set; }
        public string employee_position { get; set; } = string.Empty;
        public int employee_user_id { get; set; }
        public int department_room_id { get; set; }
    }
}
