using AutoMapper;
using SPC_Project.Model;
using SPC_Project.DTO;
namespace SPC_Project.Profiles
{
    public class PharmacyDrugsProfile : Profile
    {
        public PharmacyDrugsProfile()
        {
            CreateMap<PharmacyDrugs, DTOPharmacyDrugsRead>();
            CreateMap<DTOPharmacyDrugsWrite, PharmacyDrugs>();
        }
    }
}
