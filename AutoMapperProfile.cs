using AutoMapper;
using SPC_Project.DTO;
using SPC_Project.Model;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Define mapping configurations
        CreateMap<DTOSPCTenderRequestWrite, TenderRequest>();
        CreateMap<TenderRequest, DTOSPCTenderRequestRead>();
    }
}

 
