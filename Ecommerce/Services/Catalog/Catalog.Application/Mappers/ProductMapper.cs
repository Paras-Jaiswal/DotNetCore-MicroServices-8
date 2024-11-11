using AutoMapper;

namespace Catalog.Application.Mappers
{
    public static class ProductMapper
    {
        /*
        so in a nutshell this product mapper class basically provides a centrally lazy initialized auto mapper configuration for the application 
        and it ensures that the mapper is configured correctly with the specified profile and that is where this configuration profile is taking this add 
        profile like add product mapping profile if you have other profiles also you can add that one and it's only initialized when needed like optimizing 
        resource users and initialization time as well so this approach simplifies object to object mapping within the application right so which improves 
        the maintainability and consistency as well.

         */
       public static IMapper Mapper => Lazy.Value;

        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
           {
               var config = new MapperConfiguration(cfg =>
               {
                   cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                   cfg.AddProfile<ProductMappingProfile>();
               });
               var mapper = config.CreateMapper();
               return mapper;

           });


    }
}
