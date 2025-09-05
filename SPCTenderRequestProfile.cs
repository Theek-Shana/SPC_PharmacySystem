using AutoMapper;
using SPC_Project.DTO;
using SPC_Project.Model;

namespace SPC_Project.Profiles
{
    public class SPCTenderRequest : Profile
    {
        public SPCTenderRequest()

        {
            CreateMap<TenderRequest, DTOSPCTenderRequestRead>();
            CreateMap<DTOSPCTenderRequestWrite,TenderRequest>();
        }
    }
}
