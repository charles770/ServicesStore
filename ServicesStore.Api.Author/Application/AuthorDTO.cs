using ServicesStore.Api.Author.Model;
using System.Collections.Generic;
using System;

namespace ServicesStore.Api.Author.Application
{
    public class AuthorDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string AuthorBookGuid { get; set; }
    }
}
