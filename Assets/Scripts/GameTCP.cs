using System;
using System.IO;
using UnityEngine;

public class GameTCP
    {
    public static string readFileText(string fileName)
    {
        try
        {
            //Debug.Log(Main.res + "/" + fileName);
            TextAsset textAsset = (TextAsset)Resources.Load(Main.res + "/" + fileName, typeof(TextAsset));

            if (textAsset != null)
            {
                StringReader reader = new StringReader(textAsset.text);
                string firstLine = reader.ReadLine();
                return firstLine != null ? firstLine : string.Empty;
            }
            else
            {
                Debug.LogWarning("TextAsset not found: " + fileName);
                return string.Empty;
            }
        }
        catch (IOException)
        {
            return string.Empty;
        }
    }
}

