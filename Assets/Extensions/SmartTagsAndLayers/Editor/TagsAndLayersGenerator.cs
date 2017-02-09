using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

namespace SmartTagsAndLayers
{
    [InitializeOnLoad]
    public class TagsAndLayersGenerator
    {
        #region Settings Variables

        private static bool autoGenerate = true;
        private static float generateBuffer = 1f;
        private static string scriptFolderPathAbsolute = Application.dataPath + "/Extensions/SmartTagsAndLayers/Scripts/";

        #endregion

        #region Variables

        private static bool generateLayersWait = false;
        private static double generateLayersAt = 0f;
        private static string[] layersBuffer = new string[0];

        private static bool generateTagsWait = false;
        private static double generateTagsAt = 0f;
        private static string[] tagsBuffer = new string[0];

        #endregion

        #region Constructor

        static TagsAndLayersGenerator()
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode)
            {
                EditorApplication.update += Update;

                // Load buffers if classes are the same
                if (Layers.List.Count == UnityEditorInternal.InternalEditorUtility.layers.Length)
                {
                    bool loadLayersBuffer = true;
                    for (int i = 0; i < Layers.List.Count; i++)
                    {
                        if (Layers.List[i].Name != UnityEditorInternal.InternalEditorUtility.layers[i])
                        {
                            loadLayersBuffer = false;
                            break;
                        }
                    }
                    if (loadLayersBuffer)
                        layersBuffer = UnityEditorInternal.InternalEditorUtility.layers;
                }
                if (Tags.List.Count == UnityEditorInternal.InternalEditorUtility.tags.Length)
                {
                    bool loadTagsBuffer = true;
                    for (int i = 0; i < Tags.List.Count; i++)
                    {
                        if (Tags.List[i] != UnityEditorInternal.InternalEditorUtility.tags[i])
                        {
                            loadTagsBuffer = false;
                            break;
                        }
                    }
                    if (loadTagsBuffer)
                        tagsBuffer = UnityEditorInternal.InternalEditorUtility.tags;
                }
            }
        }

        #endregion

        #region Update

        private static void Update()
        {
            // Layers
            if (autoGenerate && !Enumerable.SequenceEqual(layersBuffer, UnityEditorInternal.InternalEditorUtility.layers))
            {
                layersBuffer = UnityEditorInternal.InternalEditorUtility.layers;
                generateLayersWait = true;
                generateLayersAt = EditorApplication.timeSinceStartup + generateBuffer;
            }
            if (generateLayersWait && generateLayersAt < EditorApplication.timeSinceStartup)
            {
                generateLayersWait = false;
                GenerateLayers();
            }

            // Tags
            if (autoGenerate && !Enumerable.SequenceEqual(tagsBuffer, UnityEditorInternal.InternalEditorUtility.tags))
            {
                tagsBuffer = UnityEditorInternal.InternalEditorUtility.tags;
                generateTagsWait = true;
                generateTagsAt = EditorApplication.timeSinceStartup + generateBuffer;
            }
            if (generateTagsWait && generateTagsAt < EditorApplication.timeSinceStartup)
            {
                generateTagsWait = false;
                GenerateTags();
            }
        }

        #endregion

        #region Menu Items

        [MenuItem("Tools/Smart Tags and Layers/Generate Layers", false, 0)]
        public static void MenuItemGenerateLayers()
        {
            GenerateLayers();
        }

        [MenuItem("Tools/Smart Tags and Layers/Generate Tags", false, 0)]
        public static void MenuItemGenerateTags()
        {
            GenerateTags();
        }

        #endregion

        #region Layer Methods

        private static void GenerateLayers()
        {
            // Create folders if necessary
            if (!Directory.Exists(scriptFolderPathAbsolute))
                Directory.CreateDirectory(scriptFolderPathAbsolute);

            // Create the file
            File.WriteAllText(scriptFolderPathAbsolute + "Layers.cs", GenerateLayersFile());

            // Refresh asset database
            AssetDatabase.Refresh();
        }

        private static string GenerateLayersFile()
        {
            string output = "";

            output += "// This class is auto-generated DO NOT MODIFY\n";
            output += "// Use Tools >> Smart Tags and Layers >> Generate Layers on the menu to update this class\n";
            output += "\n";
            output += "using System.Collections.Generic;\n";
            output += "\n";
            output += "namespace SmartTagsAndLayers\n";
            output += "{\n";
            output += "\tpublic static class Layers\n";
            output += "\t{\n";
            output += "\t\t#region Variables\n";
            output += "\t\t\n";

            for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.layers.Length; i++)
            {
                output += "\t\tpublic static Layer " + Clean(UnityEditorInternal.InternalEditorUtility.layers[i]) + " = new Layer(\"" + UnityEditorInternal.InternalEditorUtility.layers[i] + "\");\n";
            }

            output += "\t\t\n";
            output += "\t\t#endregion\n";
            output += "\t\n";
            output += "\t\t#region List\n";
            output += "\t\t\n";
            output += "\t\tpublic static List<Layer> List = new List<Layer>()\n";
            output += "\t\t{\n";

            for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.layers.Length; i++)
            {
                output += "\t\t\t" + Clean(UnityEditorInternal.InternalEditorUtility.layers[i]) + ",\n";
            }

            output += "\t\t};\n";
            output += "\t\t\n";
            output += "\t\t#endregion\n";
            output += "\t}\n";
            output += "}";

            return output;
        }

        #endregion

        #region Tag Methods

        private static void GenerateTags()
        {
            // Create folders if necessary
            if (!Directory.Exists(scriptFolderPathAbsolute))
                Directory.CreateDirectory(scriptFolderPathAbsolute);

            // Create the file
            File.WriteAllText(scriptFolderPathAbsolute + "Tags.cs", GenerateTagsFile());

            // Refresh asset database
            AssetDatabase.Refresh();
        }

        private static string GenerateTagsFile()
        {
            string output = "";

            output += "// This class is auto-generated DO NOT MODIFY\n";
            output += "// Use Tools >> Smart Tags and Layers >> Generate Tags on the menu to update this class\n";
            output += "\n";
            output += "using System.Collections.Generic;\n";
            output += "\n";
            output += "namespace SmartTagsAndLayers\n";
            output += "{\n";
            output += "\tpublic static class Tags\n";
            output += "\t{\n";
            output += "\t\t#region Variables\n";
            output += "\t\t\n";

            for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++)
            {
                output += "\t\tpublic const string " + Clean(UnityEditorInternal.InternalEditorUtility.tags[i]) + " = \"" + UnityEditorInternal.InternalEditorUtility.tags[i] + "\";\n";
            }

            output += "\t\t\n";
            output += "\t\t#endregion\n";
            output += "\t\n";
            output += "\t\t#region List\n";
            output += "\t\t\n";
            output += "\t\tpublic static List<string> List = new List<string>()\n";
            output += "\t\t{\n";

            for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++)
            {
                output += "\t\t\t" + Clean(UnityEditorInternal.InternalEditorUtility.tags[i]) + ",\n";
            }

            output += "\t\t};\n";
            output += "\t\t\n";
            output += "\t\t#endregion\n";
            output += "\t}\n";
            output += "}";

            return output;
        }

        #endregion

        #region String Extension

        public static string Clean(string _value)
        {
            string output = "";
            bool nextUpper = true;

            for (int i = 0; i < _value.Length; i++)
            {
                bool nextUpperBuffer = nextUpper;
                nextUpper = false;

                if (_value[i] == ' ')
                {
                    nextUpper = true;
                    continue;
                }

                if (nextUpperBuffer)
                    output += Char.ToUpper(_value[i]);
                else
                    output += _value[i];
            }

            return output;
        }

        #endregion
    }
}