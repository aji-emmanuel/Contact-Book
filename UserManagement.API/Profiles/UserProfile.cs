using AutoMapper;

namespace UserManagement.API.Profiles
{
    // Contains profiles for mapping models to data transfer objects (DTOs) and vice versa
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<DTOs.RegRequestDTO, Model.User>();

            CreateMap<Model.User, DTOs.LoginResponseDTO>()
                .ForMember(dest => dest.Name, option => option.MapFrom(src =>
                            $"{src.FirstName} {src.LastName}"));

            CreateMap<Model.User, DTOs.GetUserDTO>()
                .ForMember(dest => dest.Name, option => option.MapFrom(src =>
                            $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Address,
                            option => option.MapFrom(src => $"{src.StreetAddress}, {src.City}, {src.State}."))
                .ForMember(dest => dest.DateOfBirth,
                            option => option.MapFrom(src => src.DateOfBirth.ToShortDateString()));
        }
    }
}
