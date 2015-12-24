namespace Orders.com.Web.MVC.ViewModels
{
    public class ViewModel<T> where T : new()
    {
        public ViewModel()
        {
            Entity = new T(); 
        }

        public T Entity { get; set; }
    }
}