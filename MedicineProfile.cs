using AutoMapper;
using SPC_Project.Model;
using SPC_Project.DTO;

namespace SPC_Project.Profiles
{
    public class MedicineProfile : Profile
    {
        public MedicineProfile()

        {
            CreateMap<Medicine, DTOMedicineRead>();
            CreateMap<DTOMedicineWrite, Medicine>();
        } 


    }
}

