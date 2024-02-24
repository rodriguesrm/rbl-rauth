namespace RBlazeLabs.Architecture.Exceptions
{

    /// <summary>
    /// Exception thrown when the entity to be recorded is not valid for 
    /// the operation, check the entity's notifications.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="InvalidEntityException"/>
    /// class with a specified error message.
    /// </remarks>
    /// <param name="entityName">Entity name</param>
    public class InvalidEntityException(string entityName) 
        : Exception($"The entity {entityName} is invalid to insert or update in database context")
    {
    }
}
