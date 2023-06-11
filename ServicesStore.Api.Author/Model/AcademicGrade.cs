using System;
using System.ComponentModel.DataAnnotations;

namespace ServicesStore.Api.Author.Model
{
    public class AcademicGrade
    {
        [Key]
        public int AcademicGradeId { get; set; }
        public string Name { get; set; }
        public string AcademicCenter { get; set; }
        public DateTime? GradeDate { get; set; }
        public int AuthorBookId { get; set; }
        public AuthorBook AuthorBook { get; set; }
        public string AcademicGradeGuid { get; set; }

    }
}
