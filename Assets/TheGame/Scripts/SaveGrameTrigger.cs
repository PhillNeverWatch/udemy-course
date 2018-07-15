using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGrameTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Jetzt speichern.");
        var savegame = new SaveGameData();
        savegame.save();
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
