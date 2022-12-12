namespace ShootingRange
{
    public class EmployeeBusinessLogic : IEmployeeDataAccessDependency
    {
        public IEmployeeDataAccess EmployeeDataAccess { get; set; }

        public Employee GetEmployeeDetails(int id)
        {
            return EmployeeDataAccess.GetEmployeeDetails(id);
        }

        public void SetDependency(IEmployeeDataAccess employeeDataAccess)
        {
            EmployeeDataAccess = employeeDataAccess;
        }
    }
}
