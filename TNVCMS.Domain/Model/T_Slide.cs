//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TNVCMS.Domain.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_Slide
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string ImagePath { get; set; }
        public Nullable<bool> Enable { get; set; }
        public Nullable<int> GroupID { get; set; }
    
        public virtual T_SlideGroup T_SlideGroup { get; set; }
    }
}