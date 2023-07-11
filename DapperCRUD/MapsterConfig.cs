using DapperCRUD.Models;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace DapperCRUD
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<Person, PersonDTO>
                .NewConfig()
                .Map(des => des.FullName, src => $"{src.FirstName} {src.LastName}")
                .Map(des => des.Age,
                src => DateTime.Now.Year - src.DateOfBirth.Value.Year,
            srcCond => srcCond.DateOfBirth.HasValue)
            .Map(dest => dest.FullAddress,
            src => $"{src.Address.Street} {src.Address.PostCode} - {src.Address.City}");


            //TypeAdapterConfig<Person,PersonDTO>
            //  .NewConfig()
            //  .Ignore(dest=> dest.Title);

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly()) ;

            TypeAdapterConfig<Person, PersonDTO>.ForType()
                .BeforeMapping((src, result) =>
                {
                    Console.WriteLine("We Start");
                })
                .AfterMapping((src, result) => {
                    Console.WriteLine("We Finish");
                });
               
        }
    }
}
