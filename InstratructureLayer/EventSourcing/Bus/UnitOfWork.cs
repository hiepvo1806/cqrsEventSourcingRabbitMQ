using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstratructureLayer.EventSourcing.Bus
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            try {
                return _context.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                var k = e;
                return false;
            }
            
        }
    }
}
