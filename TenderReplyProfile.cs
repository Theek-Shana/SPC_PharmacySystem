using AutoMapper;
using SPC_Project.Model;
using SPC_Project.DTO;


namespace SPC_Project.Profiles
{
    public class TenderReplyProfile : Profile
    {
        public TenderReplyProfile()
        {
            CreateMap<TenderReplys, DTOTenderReplyRead>();
            CreateMap<DTOTenderReplyWrite, TenderReplys>();
        }
    }

   
}
