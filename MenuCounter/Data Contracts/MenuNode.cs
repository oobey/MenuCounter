using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MenuCounter.Data_Contracts
{
    /// <summary>
    /// "Menu" object as implicitly defined by coding exercise instructions.
    /// A Menu has a Header and a list of Items to be summed.
    /// </summary>
    [DataContract]
    public class MenuNode
    {
        [DataMember(Name = "header")]
        public string Header;

        [DataMember(Name = "items")]
        public IEnumerable<Item> Items;
    }
}
