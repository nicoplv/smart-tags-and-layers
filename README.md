# Smart Tags and Layers
A tags and layers helper for Unity3D

## Installation
Import the [package](https://github.com/nicoplv/smart-tags-and-layers/raw/master/SmartTagsAndLayers.unitypackage) on your project and that's all ... you have access easily to all the tags and layers on your code.

## Usage for Layers
Get the mask of the layer named "Awesome Name":
```C#
SmartTagsAndLayers.Layers.AwesomeName.Mask
```

Get the index of the layer named "Awesome Name":
```C#
SmartTagsAndLayers.Layers.AwesomeName.Get
```

Get the name of the layer named "Awesome Name":
```C#
SmartTagsAndLayers.Layers.AwesomeName.Name
```

Get a list of all the layers available on the project:
```C#
SmartTagsAndLayers.Layers.List
```

## Usage for Tags
Get the mask of the tag "Awesome Name":
```C#
SmartTagsAndLayers.Tags.AwesomeName
```

Get a list of all the tags available on the project:
```C#
SmartTagsAndLayers.Tags.List
```

## Settings
You can disable the auto-generation of the layer list by editing the file [TagsAndLayersGenerator](https://github.com/nicoplv/smart-tags-and-layers/blob/master/Assets/Extensions/SmartTagsAndLayers/Editor/TagsAndLayersGenerator.cs) like that:
```C#
private static bool autoGenerate = false;
```

You can change the folder path of the generated list by editing the file [TagsAndLayersGenerator](https://github.com/nicoplv/smart-tags-and-layers/blob/master/Assets/Extensions/SmartTagsAndLayers/Editor/TagsAndLayersGenerator.cs) like that:
```C#
private static string scriptFolderPathAbsolute = Application.dataPath + "YourPath";
```

## Others
A button to force the generation of lists is created on the Unity menu at Tools >> SmartTagsAndLayers.