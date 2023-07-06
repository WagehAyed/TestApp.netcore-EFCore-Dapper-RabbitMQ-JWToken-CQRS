namespace TestJWTApi.Dtos
{
    public class CreateGenereDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
