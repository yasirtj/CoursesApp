//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoursesApp.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            this.Course_Units = new HashSet<Course_Units>();
            this.Trainee_Courses = new HashSet<Trainee_Courses>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public System.DateTime Creation_Date { get; set; }
        public string Description { get; set; }
        public string Image_ID { get; set; }
        public int Category_Id { get; set; }
        public Nullable<int> Trainer_id { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Trainer Trainer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course_Units> Course_Units { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Trainee_Courses> Trainee_Courses { get; set; }
    }
}
