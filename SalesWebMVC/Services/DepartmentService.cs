using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMVCContext _context;

        public DepartmentService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            //Retornar departamentos ordenados
            return _context.Department.OrderBy(d => d.Name).ToList();
        }
    }
}
