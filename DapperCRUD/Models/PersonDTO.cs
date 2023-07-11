using System;

namespace DapperCRUD.Models
{
    public class PersonDTO
    {
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public int? Age { get; set; }
        public string? FullAddress { get;set;}
        public string?  AddressStreet  { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
