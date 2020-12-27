using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace SalesWebMVC.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMVCContext _context;

        public DepartmentService(SalesWebMVCContext context)
        {
            _context = context;
        }

        //Criado para popular os selects
        public async Task<List<Department>> FindAllAsync() //Async é uma recomendação para assincrona
        {
            //Retornar departamentos ordenados
            return await _context.Department.OrderBy(d => d.Name).ToListAsync();
        }
    }
}
