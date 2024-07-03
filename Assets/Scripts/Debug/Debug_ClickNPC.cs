using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_ClickNPC : MonoBehaviour
{
    void Start()
    {
        EnsureColliderExists();
    }

    void OnMouseDown()
    {
        // Output to the Unity Console when the model is clicked
        Debug.Log("Model clicked");
    }

    private void EnsureColliderExists()
    {
        Collider collider = GetComponent<Collider>();
        if (collider == null)
        {
            // Add a BoxCollider if no collider is found
            gameObject.AddComponent<BoxCollider>();
            Debug.Log("No collider found. BoxCollider has been added automatically.");
        }
    }
}
