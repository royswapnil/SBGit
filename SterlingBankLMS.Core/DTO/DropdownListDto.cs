namespace SterlingBankLMS.Core.DTO
{
    public class BaseDropdownListBaseDTO<T>
    {
        public T Id { get; set; }
        public string Name { get; set; }
    }

    public class DepartmentDropdownListDto : BaseDropdownListBaseDTO<int> { }

    public class GroupDropdownListDto : BaseDropdownListBaseDTO<int> { }

    public class RegionDropdownListDto : BaseDropdownListBaseDTO<int> { }

    public class BranchDropdownListDto : BaseDropdownListBaseDTO<int> { }

    public class LineManagerDropdownListDto : BaseDropdownListBaseDTO<int> {
        public string LineManagerFirstName { get; set; }
        public string LineManagerLastName { get; set; }
        public string DepartmentName { get; set; }
    }
}