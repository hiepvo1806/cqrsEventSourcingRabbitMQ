using AutoMapper;
using InstratructureLayer.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstratructureLayer.EventSourcing.Bus
{
    public abstract class CommandHandler
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IMediatorHandler _bus;
        protected readonly IMapper _mapper;

        public CommandHandler(IUnitOfWork uow,
            IMediatorHandler bus,
            IMapper mapper
            )
        {
            _uow = uow;
            _bus = bus;
            _mapper = mapper;
        }

        public bool Commit()
        {
            if (_uow.Commit()) return true;
            return false;
        }
    }
}
