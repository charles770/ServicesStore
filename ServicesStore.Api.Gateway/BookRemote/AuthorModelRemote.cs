using System;

namespace ServicesStore.Api.Gateway.BookRemote
{
    public class AuthorModelRemote
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string AuthorBookGuid { get; set; }

    }
}
