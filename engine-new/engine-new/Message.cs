namespace engine_new;

public class Message
{
    public static String InvalidInputWarning = "\nInvalid Input.";
    public static String CommandDoesNotExistWarning = "\nCommand does not exist.";

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
