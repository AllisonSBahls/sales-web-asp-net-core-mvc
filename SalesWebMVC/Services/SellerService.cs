using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Services.Exceptions;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        //Readonly Define que a dependencia não poode ser alterada
        private readonly SalesWebMVCContext _context;

        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            //Acessa a fonte de dados relacionado a tabela de vendedos e converter em uma lista
            return await _context.Seller.ToListAsync();
        }

        //Inserir novo vendedor
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            //Verificação se existe um vendendo X igual ao id do objeto passo
            if (!hasAny)
            {
                throw new NotFoundException("Id not Found");
            }
            //Usando o try catch para capturar um erro do banco de dados
         
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            //caso ocorre essa excessão
            catch (DbUpdateConcurrencyException e)
            {
                /*Vai ser feito o lançamento de uma outra excessão a nivel de Serviço
                Aqui esta sendo feito a interceptção de uma exceção de nivel de acesso a dados
                e re-lançando usando uma exceção de nivel de serviço
                dessa forma o controlador so lida com exceções da camada de serviço
                conforme a arquivetura MVC*/
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
