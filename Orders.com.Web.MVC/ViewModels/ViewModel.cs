using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Orders.com.Web.MVC.ViewModels
{
    public class ViewModel<T>
    {
        public T Entity { get; set; }
        public IEnumerable<ValidationResult> Errors { get; set; } = Enumerable.Empty<ValidationResult>();
    }
}