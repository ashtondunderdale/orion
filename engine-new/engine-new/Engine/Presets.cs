﻿namespace orion;

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
            string? objSymbol = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(objSymbol))
            {
                Display.Warning("Object symbol cannot be whitespace");
                continue;
            }

            if (objSymbol.Length > 1) 
            {
                Display.Warning("Object symbol must be one character");
                continue;
            }

            voxel.Symbol = objSymbol[0];

            break;
        }

        string colour = Display.Menu(Display.ColourMap.Keys.ToList(), "Select a colour for your custom object");
        ConsoleColor objColour;

        if (Display.ColourMap.TryGetValue(colour.ToLower(), out ConsoleColor mappedColor))
        {
            objColour = mappedColor;
        }
        else
        {
            objColour = ConsoleColor.White;
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

    public static void InspectPreset()
    {
        Console.Clear();

        Console.ForegroundColor = Display.Gray;
        Console.WriteLine("scenes\n\n");

        List<string?> presetObjects = Engine.ProjectContext!.PresetObjects.Select(obj => obj.Name).ToList();

        string sceneName = Display.Menu(presetObjects!, "choose a scene to inspect");

        if (sceneName == "") return;

        Voxel obj = Engine.ProjectContext!.PresetObjects.FirstOrDefault(obj => obj.Name == sceneName)!;

        Console.Clear();

        Console.Write($"{obj.Name}\n\n");
        Console.Write($"{obj.Symbol}\n\n");
        Console.Write($"{obj.Colour}\n\n");

        foreach (var script in obj.Scripts)
        {
            Console.WriteLine(script.Name);
        }

        Console.ReadKey();

        return;
    }

    public static void EditPreset() 
    { 
    
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
