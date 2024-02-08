using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamUp : MonoBehaviour
{
    public GameObject shipVariantPrefab;

    public static float counter = 0f;

    private GameObject shipVariant;

    public void Beam()
    {
        // Spawns ship above object
        Vector3 spawnPosition = transform.position + new Vector3(0f, 12f, 1f);
        Quaternion spawnRotation = Quaternion.Euler(-90f, 0f, 0f);
        shipVariant = Instantiate(shipVariantPrefab, spawnPosition, spawnRotation);
        GameObject myObject = transform.parent.gameObject;

        counter += 1;

        if (counter == 28)
        {
            GameEnd.AllBeamed();
        }

        // Destroys both objects after 2 seconds
        Invoke("DestroyObjects", 2f);
    }

    void DestroyObjects()
    {
        GameObject obj = transform.parent.gameObject;

        if (obj != null)
        {
            Destroy(obj);
        }

        if (shipVariant != null)
        {
            Destroy(shipVariant);
        }
    }
}