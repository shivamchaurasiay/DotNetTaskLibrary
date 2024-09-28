using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetsTasks.Core.DataTables.Extension
{
    
    public enum EnumCourseType
    {
        [Display(Name = "--Select--")]
        None = 0,

        [Display(Name = "Graduate Course")]
        Graduate = 1,

        [Display(Name = "Post Graduate Course")]
        PostGraduate = 2,

        [Display(Name = "Diploma Course")]
        Diploma = 3
    }
    public enum EnumCourseCategory
    {
        [Display(Name = "--Select--")]
        None = 0,

        [Display(Name = "Regular")]
        Regular = 1,

        [Display(Name = "Private")]
        Private = 2,

        [Display(Name = "Part time")]
        PartTime = 3
    }
    public enum EnumDurationType
    {
        [Display(Name = "--Select--")]
        None = 0,

        [Display(Name = "Year")]
        Year = 1,

        [Display(Name = "Semester")]
        Semester = 2,

      
    }
    public enum EnumSubjectCategoryType
    {
        [Display(Name = "--Select--")]
        None = 0,

        [Display(Name = "Core")]
        Core = 1,

        [Display(Name = "Optional")]
        Optional = 2,


    }
}
