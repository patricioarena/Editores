using AutoMapper;

namespace ApiBackend
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration() : this("MyProfile")
        {
        }
        protected AutoMapperProfileConfiguration(string profileName) : base(profileName)
        {
            //CreateMap<Credenciales, CredencialesModel>();
            //CreateMap<CredencialesModel, Credenciales>();
        }

    }
}