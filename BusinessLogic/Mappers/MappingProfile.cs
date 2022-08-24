using AutoMapper;
using BusinessLogic.Dtos.PageDtos;
using Entity.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mappers
{
    public class MappingProfile: Profile
    {

        public MappingProfile()
        {
            #region Page
            CreateMap<Page, PageDto>().ReverseMap();
            #endregion
        }

    }
}
