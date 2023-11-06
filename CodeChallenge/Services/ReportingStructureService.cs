using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public ReportingStructure GetReportingStructureById(String id)
        {
            Employee employee = _employeeRepository.GetById(id);
            ReportingStructure reportingStructure = new ReportingStructure();

            if (employee != null)
            {
                reportingStructure.Employee = employee;

                if (employee.DirectReports != null && employee.DirectReports.Count > 0)
                {
                    reportingStructure.NumberOfReports = GetNumberOfDirectReports(employee.DirectReports);
                    return reportingStructure;
                }
            }

            return reportingStructure;
        }

        private int GetNumberOfDirectReports(List<Employee> directReports)
        {
            int numberOfDirectReports = 0;
            if (directReports != null && directReports.Count > 0)
            {
                numberOfDirectReports += directReports.Count;

                foreach(Employee emp in directReports)
                {
                    Employee fullEmpData = _employeeRepository.GetById(emp.EmployeeId);
                    numberOfDirectReports += GetNumberOfDirectReports(fullEmpData.DirectReports);
                }

                return numberOfDirectReports; 
            }

            return numberOfDirectReports;
        }
    }
}
