using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Orders.com.Web.MVC.ViewModels
{
    public class ViewModel<T> where T : new()
    {
        public ViewModel()
        {
            Entity = new T(); 
        }

        public T Entity { get; set; }
        public List<ValidationResult> Errors { get; set; } = new List<ValidationResult>();
    }
}