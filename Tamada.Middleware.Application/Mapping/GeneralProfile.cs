using AutoMapper; 

namespace Tamada.Middleware.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<QueryAccountStatus, AccountStatus>()
                .ForMember(dest => dest.ac_stat_block, opt => opt.MapFrom(src => ConvertToBool(src.ac_stat_block??"")))
                .ForMember(dest => dest.ac_stat_frozen, opt => opt.MapFrom(src => ConvertToBool(src.ac_stat_frozen ?? "")))
                .ForMember(dest => dest.ac_stat_no_dr, opt => opt.MapFrom(src => ConvertToBool(src.ac_stat_no_dr ?? "")))
                .ForMember(dest => dest.ac_stat_dormant, opt => opt.MapFrom(src => ConvertToBool(src.ac_stat_dormant ?? "")));
            //.ReverseMap();
          
            CreateMap<QueryAccountValidation, AccountValidation>()
                .ForMember(dest => dest.ac_stat_block, opt => opt.MapFrom(src => ConvertToBool(src.ac_stat_block??"")))
                .ForMember(dest => dest.ac_stat_frozen, opt => opt.MapFrom(src => ConvertToBool(src.ac_stat_frozen ?? "")))
                .ForMember(dest => dest.ac_stat_no_dr, opt => opt.MapFrom(src => ConvertToBool(src.ac_stat_no_dr ?? "")))
                .ForMember(dest => dest.ac_stat_dormant, opt => opt.MapFrom(src => ConvertToBool(src.ac_stat_dormant ?? "")));

            CreateMap<QueryAccountValidationWithOTP, AccountValidationWithOTP>()
               .ForMember(dest => dest.ac_stat_block, opt => opt.MapFrom(src => ConvertToBool(src.ac_stat_block ?? "")))
               .ForMember(dest => dest.ac_stat_frozen, opt => opt.MapFrom(src => ConvertToBool(src.ac_stat_frozen ?? "")))
               .ForMember(dest => dest.ac_stat_no_dr, opt => opt.MapFrom(src => ConvertToBool(src.ac_stat_no_dr ?? "")))
               .ForMember(dest => dest.ac_stat_dormant, opt => opt.MapFrom(src => ConvertToBool(src.ac_stat_dormant ?? "")));
            //.ReverseMap();
            CreateMap<EmailRequest, Email>().ReverseMap();
            CreateMap<SMSRequest, SMS>().ReverseMap();


        }

        private bool ConvertToBool(string value)
        { 
            return value == "Y";
        }
    }

}
