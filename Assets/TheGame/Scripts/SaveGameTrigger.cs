using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameTrigger : MonoBehaviour
{

    public string TriggerId = "";

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Jetzt speichern.");
        var savegame = SaveGameData.current;

        if (savegame.lastTriggerId != TriggerId)
        {
            savegame.lastTriggerId = TriggerId;
            savegame.save();
        }
        else
        {
            Debug.Log($"Der Speicherpunkt {TriggerId} hat bereits gespeichert. Überspringe Speichern.");
        }
    }

    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject != this.gameObject)
        {
            Gizmos.color = Color.magenta;
            Matrix4x4 oldMatrix = Gizmos.matrix;

            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);

            Gizmos.matrix = oldMatrix;
        }
    }
}
