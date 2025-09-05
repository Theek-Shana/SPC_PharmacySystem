using AutoMapper;
using SPC_Project.Model;
using SPC_Project.DTO;
namespace SPC_Project.Profiles
{
    public class ConfirmTenderProfile : Profile
    {
        public ConfirmTenderProfile() 
        {

            CreateMap<ConfirmTender, DTOConfirmRead>();
            CreateMap<DTOConfirmWrite, ConfirmTender>();
        }

    }
    
}
