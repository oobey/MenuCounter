using System.Runtime.Serialization;

namespace MenuCounter.Data_Contracts
{
    /// <summary>
    /// "Item" object as implicitly defined by coding exercise instructions.
    /// Any Item with a Label will have its ID added to the sum for its parent Menu.
    /// </summary>
    [DataContract]
    public class Item
    {
        [DataMember(Name = "id")]
        public int ID;

        [DataMember(Name = "label")]
        public string Label;
    }
}
