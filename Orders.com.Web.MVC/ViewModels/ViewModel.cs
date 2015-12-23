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
        public IEnumerable<ValidationResult> Errors { get; set; } = Enumerable.Empty<ValidationResult>();
    }
}