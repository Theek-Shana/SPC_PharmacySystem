using AutoMapper;
using SPC_Project.Model;
using SPC_Project.DTO;
namespace SPC_Project.Profiles
{
    public class SPCSupplierProfile: Profile
    {
          public SPCSupplierProfile()

        {
            CreateMap<SPCSupplier, DTOSPCSuplierRead>();
            CreateMap<DTOSPCSupplierWrite, SPCSupplier>();
        }
    }
}
