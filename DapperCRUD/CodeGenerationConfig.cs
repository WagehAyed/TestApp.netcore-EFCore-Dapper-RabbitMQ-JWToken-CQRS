using DapperCRUD.Models;
using Mapster;

namespace DapperCRUD
{
    public class CodeGenerationConfig : ICodeGenerationRegister
    {
        public void Register(Mapster.CodeGenerationConfig config)
        {
            config.AdaptTo("[name]Model")
                .ForType<Person>()
                .ForType<Address>();

            config.GenerateMapper("[name]mapper")
                .ForType<Person>()
                .ForType<Address>();
        }
    }
}
