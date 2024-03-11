namespace orion;

internal class Presets
{
    public static void CreatePresetObject()
    {
        Voxel voxel = new();
        string? objName;

        while (true) 
        {
            Console.Clear();

            Console.ForegroundColor = Display.Gray;
            Console.WriteLine("enter a name for your object\n\n  >");

            Console.ForegroundColor = Display.DarkGray;
            Console.SetCursorPosition(4, 2);
            objName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(objName))
            {
                Display.Warning("object name can not be empty");
                continue;
            }

            if (Engine.ProjectContext!.PresetObjects.Any(scene => scene.Name == objName))
            {
                Display.Warning("an object with that name already exists");
                continue;
            }

            voxel.Name = objName;

            break;
        }

        while (true)
        {
            Console.Clear();

            Console.ForegroundColor = Display.Gray;
            Console.WriteLine("enter the symbol for your object\n\n  >");

            Console.ForegroundColor = Display.DarkGray;
            Console.SetCursorPosition(4, 2);
            char objSymbol = Console.ReadKey().KeyChar;

            if (char.IsWhiteSpace(objSymbol) || objSymbol == '\t')
            {
                Display.Warning("Object symbol cannot be whitespace.");
                continue;
            }

            voxel.Symbol = objSymbol;

            break;
        }

        string colour = Display.Menu(new List<string>() { "red", }, "select a colour for your custom object");
        ConsoleColor objColour;

        switch (colour) 
        {
            case "red":
                objColour = ConsoleColor.Red;
                break;

            default: 
                objColour = ConsoleColor.White;
                break;
        }

        voxel.Colour = objColour;

        while (true)
        {
            string script = Display.Menu(new List<string>() { "movement", "collider", "dynamic colour", "dynamic symbol", "pseudo collider" }, "select scripts for your custom object");

            if (script == "") break;

            switch (script)
            {
                case "collider":
                    voxel.Scripts.Add(new Collider());
                    break;

                case "movement":
                    voxel.Scripts.Add(new Collider());
                    break;

                case "dynamic colour":
                    voxel.Scripts.Add(new Collider());
                    break;

                case "dynamic symbol":
                    voxel.Scripts.Add(new DynamicSymbol());
                    break;

                case "pseudo collider":
                    voxel.Scripts.Add(new PseudoCollider());
                    break;
            }

            Display.Message($"\nAdded {script} to {objName}");
        }

        Engine.ProjectContext.PresetObjects.Add(voxel);
    }

    public static void DeletePresetObject() 
    {
        List<string> presets = Engine.ProjectContext!.PresetObjects.Select(obj => obj.Name).ToList()!;
        string preset = Display.Menu(presets, "choose the preset to delete");

        Voxel presetToDelete = Engine.ProjectContext.PresetObjects.Where(obj => obj.Name == preset).FirstOrDefault()!;
        Engine.ProjectContext!.PresetObjects.Remove(presetToDelete);

        Display.Message($"preset {preset} has been deleted");
    }
}
