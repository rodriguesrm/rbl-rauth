namespace RBlazeLabs.Common.Models
{

    /// <summary>
    /// Creating and changing data author data structure
    /// </summary>
    /// <param name="date">Occurrence date</param>
    /// <param name="id">Author id key value</param>
    /// <param name="name">Author full name</param>
    public class AuditAuthor(DateTime date, int id, string name)
    {


        /// <summary>
        /// Occurrence date
        /// </summary>
        public DateTime Date { get; set; } = date;

        /// <summary>
        /// Author key value
        /// </summary>
        public int Id { get; set; } = id;

        /// <summary>
        /// Author full name
        /// </summary>
        public string Name { get; set; } = name;

    }

}
