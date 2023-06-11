using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServicesStore.Api.Author.Model
{
    public class AuthorBook
    {
        [Key]
        public int AuthorBookId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set;}
        public ICollection<AcademicGrade> ListAcademicGrade { get; set; }    
        public string AuthorBookGuid { get; set; }
    }
}
