using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Realisiert das Öffnen der Tür mit einem Schalter.
/// </summary>
public class DoorSwitch : MonoBehaviour
{
    /// <summary>
    /// Animator auf den Tür-Mesch zum Öffnen der Tür.
    /// </summary>
    public Animator DoorAnimator;

    /// <summary>
    /// Zeiger auf das Mesh, das die Lichter an der Türschalter-Konsole darstellt.
    /// </summary>
    public MeshRenderer MeshRenderer;

    private void Awake()
    {
        SaveGameData.onSave += saveMe;
        SaveGameData.onLoad += loadMe;
    }

    private void saveMe(SaveGameData saveGameData)
    {
        saveGameData.doorIsOpen = DoorAnimator.GetBool("isOpen");
    }

    private void loadMe(SaveGameData saveGameData)
    {
        if (saveGameData.doorIsOpen)
            openTheDoor();
    }

    private void OnDestroy()
    {
        SaveGameData.onLoad -= loadMe;
        SaveGameData.onSave -= saveMe;
    }

    /// <summary>
    /// Steuert die Tür mit der Schaltkonsole, wenn die Fire1-Taste (Strg) gedrückt wird.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetAxisRaw("Fire1") != 0f && !DoorAnimator.GetBool("isOpen"))
        {
            openTheDoor();
        }
    }

    /// <summary>
    /// Öffnet die Tür und synchronisiert Türobjekt und Schalter.
    /// </summary>
    private void openTheDoor()
    {
        DoorAnimator.SetBool("isOpen", true);

        Material[] materials = MeshRenderer.materials;

        Material tempMaterial = materials[2];

        materials[2] = materials[1];
        materials[1] = tempMaterial;

        MeshRenderer.materials = materials;
    }
}
