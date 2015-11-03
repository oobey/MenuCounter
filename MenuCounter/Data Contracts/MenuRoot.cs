using System.Runtime.Serialization;

namespace MenuCounter.Data_Contracts
{
    /// <summary>
    /// Dummy object for JSON deserialization purposes.
    /// This menu root is just a shell Object holding an actual real Menu.
    /// </summary>
    [DataContract]
    public class MenuRoot
    {
        [DataMember(Name = "menu")]
        public MenuNode Menu;
    }
}
