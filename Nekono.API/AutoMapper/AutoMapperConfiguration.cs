using AutoMapper;

namespace Nekono.API.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });

            config.AssertConfigurationIsValid();
        }
    }
}
