using System.Collections;
using System.Collections.Generic;
using System.IO;
using GameDevProfi.Utils;
using UnityEngine;

[System.Serializable]
public class SaveGameData
{

    public Vector3 playerPosition = Vector3.zero;

    public bool doorIsOpen = false;

    public delegate void SaveHandler(SaveGameData saveGameData);

    public static event SaveHandler onSave;
    public static event SaveHandler onLoad;

    private static string getFilename()
    {
        return Application.persistentDataPath + "/savegame.xml";
    }

    public void save()
    {
        Debug.Log($"Speichere Spielstand {getFilename()}");

        Player p = Component.FindObjectOfType<Player>();
        playerPosition = p.transform.position;

        onSave?.Invoke(this);

        string xml = XML.Save(this);
        
        File.WriteAllText(getFilename(), xml);

        Debug.Log(xml);
    }

    public static SaveGameData load()
    {
        if (!File.Exists(getFilename()))
            return new SaveGameData();
        
        var saveGameData =  XML.Load<SaveGameData>(File.ReadAllText(getFilename()));

        Player p = Component.FindObjectOfType<Player>();
        p.transform.position = saveGameData.playerPosition;

        onLoad?.Invoke(saveGameData);

        return saveGameData;
    }
}
