using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebAppConsuminAPI.Models
{  [Table("Emp")]
    public class Emp
    {    [Key]
        public int Id { get; set; }
        public string Fname  { get; set; }
        public string LName { get; set; }
        public string Designation{ get; set; }
        public double Salary { get; set; }
        public string  Zone { get; set; }
        public string Departmet{ get; set; }
    }
}