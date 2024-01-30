namespace engine_new.Misc;

public class Message
{
    public const string InvalidInputError = "\nInvalid Input.";
    public const string CommandDoesNotExistWarning = "\nCommand does not exist.";

    public const string ObjectOutOfBoundsError = "\nObject is not within terminal x, y bounds";

    public static string ObjectReferenceCanNotBeEmptyWarning(string obj)
    {
        return $"\nCan not use empty reference for type '{obj}' ";
    }

    public static string ObjectAlreadyExistsWarning(string obj)
    {
        return $"\nThe type '{obj}' already exists with this name.";
    }

    public static string ObjectDoesNotExistsWarning(string obj)
    {
        return $"\nThe type '{obj}' does not exist.";
    }
}
