namespace AddressBook
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Person
    {
        [Display(Name = "Id")]
        public int Id { get; set; }


        [Display(Name = "Name_CH")]
        [Required]
        [StringLength(200)]
        public string Name_CH { get; set; }


        [Display(Name = "Name_EN")]
        [Required]
        [StringLength(200)]
        public string Name_EN { get; set; }


        [Display(Name = "UserNo")]
        [Required]
        [StringLength(200)]
        public string UserNo { get; set; }


        [Display(Name = "Extension_Num")]
        [StringLength(200)]
        public string Extension_Num { get; set; }


        [Display(Name = "Dep")]
        [Required]
        [StringLength(200)]
        public string Dep { get; set; }


        [Display(Name = "Type")]
        [Required]
        [StringLength(200)]
        public string Type { get; set; }
    }
}
