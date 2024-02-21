namespace RBlazeLabs.Common.Models
{

    /// <summary>
    /// Simple registration data structure (Key + Name)
    /// </summary>
    /// <remarks>
    /// Creates a new instance of the structure
    /// </remarks>
    /// <param name="id">Id key value</param>
    /// <param name="name">Full name</param>
    public class SimpleIdentification(int? id, string name)
    {

        /// <summary>
        /// Id key value
        /// </summary>
        public int? Id { get; set; } = id;

        /// <summary>
        /// Full name
        /// </summary>
        public string Name { get; set; } = name;

    }

}
