using System.Collections;
using System.Collections.Generic;
using System.IO;
using GameDevProfi.Utils;
using UnityEngine;

[System.Serializable]
public class SaveGameData
{

    public Vector3 playerPosition = Vector3.zero;

    private static string getFilename()
    {
        return Application.persistentDataPath + "/savegame.xml";
    }

    public void save()
    {
        Debug.Log($"Speichere Spielstand {getFilename()}");

        Player p = Component.FindObjectOfType<Player>();
        playerPosition = p.transform.position;

        string xml = XML.Save(this);
        
        File.WriteAllText(getFilename(), xml);

        Debug.Log(xml);
    }
}
