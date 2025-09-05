using AutoMapper;
using SPC_Project.Model;
using SPC_Project.DTO;
namespace SPC_Project.Profiles
{
    public class DrugOrderProfile : Profile
    {
        public DrugOrderProfile()
        {

            CreateMap<DrugOrder, DTODrugOrderRead>();
            CreateMap<DTODrugOrderWrite, DrugOrder>();


        }

    }
}
