namespace SterlingBankLMS.Web.ViewModels
{
    public class BaseDropdownListModelBaseDDLVM<T>
    {
        public T Id { get; set; }
        public string Name { get; set; }
    }

    public class DropdownListModel : BaseDropdownListModelBaseDDLVM<int> { }
}