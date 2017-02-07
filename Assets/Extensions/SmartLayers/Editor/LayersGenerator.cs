using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

namespace SmartLayers
{
    [InitializeOnLoad]
    public class LayersGenerator
    {
        #region Settings Variables

        private static bool autoGenerate = true;
        private static float generateBuffer = 1f;
        private static string scriptFolderPathAbsolute = Application.dataPath + "/Extensions/SmartLayers/Scripts/";

        #endregion

        #region Variables

        private static bool waitGenerate = false;
        private static double generateAt = 0f;
        private static string[] layersBuffer = new string[0];

        #endregion

        #region Constructor

        static LayersGenerator()
        {
            EditorApplication.update += Update;
        }

        #endregion

        #region Auto Generate

        private static void Update()
        {
            if (autoGenerate && !Enumerable.SequenceEqual(layersBuffer, UnityEditorInternal.InternalEditorUtility.layers))
            {
                layersBuffer = UnityEditorInternal.InternalEditorUtility.layers;
                waitGenerate = true;
                generateAt = EditorApplication.timeSinceStartup + generateBuffer;
            }
            if (waitGenerate && generateAt < EditorApplication.timeSinceStartup)
            {
                waitGenerate = false;
                Debug.Log("Generate");
                Generate();
            }
        }

        #endregion

        #region Menu Item

        [MenuItem("Tools/SmartLayers/Generate", false, 0)]
        public static void MenuItemGenerate()
        {
            Generate();
        }

        #endregion

        #region Generate Method

        private static void Generate()
        {
            // Create folders if necessary
            if (!Directory.Exists(scriptFolderPathAbsolute))
                Directory.CreateDirectory(scriptFolderPathAbsolute);

            // Create the file
            File.WriteAllText(scriptFolderPathAbsolute + "Layers.cs", GenerateLayers());

            // Refresh asset database
            AssetDatabase.Refresh();
        }

        private static string GenerateLayers()
        {
            string output = "";

            output += "// This class is auto-generated DO NOT MODIFY\n";
            output += "// Use Tools >> SmartLayers >> Generator on the menu to update this class\n";
            output += "\n";
            output += "using System.Collections.Generic;\n";
            output += "\n";
            output += "namespace SmartLayers\n";
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