using AutoMapper;
using CollectionsAndLinq.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.Services.Abstract
{
    public class BaseService
    {
        private protected readonly IMapper _mapper;
        private protected readonly ProjectDbContext _context;

        public BaseService(IMapper mapper, ProjectDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
    }
}
