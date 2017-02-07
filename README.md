# Smart Layers
A layer helper for Unity3D

## Installation
Import the [package](https://github.com/nicoplv/smart-layers/raw/master/SmartLayers.unitypackage) on your project and that's all ... you have access easily to all the layers on your code.

## Usage
Get the mask of the layer named "Awesome Name":
```C#
SmartLayers.Layers.AwesomeName.Mask
```

Get the index of the layer named "Awesome Name":
```C#
SmartLayers.Layers.AwesomeName.Get
```

Get the name of the layer named "Awesome Name":
```C#
SmartLayers.Layers.AwesomeName.Name
```

Get a list of all the layers available on the project:
```C#
SmartLayers.Layers.List
```

## Settings
You can disable the auto-generation of the layer list by editing the file [LayersGenerator](https://github.com/nicoplv/smart-layers/blob/master/Assets/Extensions/SmartLayers/Editor/LayersGenerator.cs) like that:
```C#
private static bool autoGenerate = false;
```

You can change the folder path of the generated list by editing the file [LayersGenerator](https://github.com/nicoplv/smart-layers/blob/master/Assets/Extensions/SmartLayers/Editor/LayersGenerator.cs) like that:
```C#
private static string scriptFolderPathAbsolute = Application.dataPath + "YourPath";
```

## Others
A button to force the generation of the list is created on the Unity menu at Tools >> SmartLayers >> Generator.
