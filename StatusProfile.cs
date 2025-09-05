using AutoMapper;
using SPC_Project.Model;
using SPC_Project.DTO;
namespace SPC_Project.Profiles
{
    public class StatusProfile : Profile
    {
        public StatusProfile()
        {
            CreateMap<Status, DTOStatusRead>();
            CreateMap<DTOStatusWrite, Status>();
        }
    }
}
