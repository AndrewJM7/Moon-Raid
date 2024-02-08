using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;

    [SerializeField]    
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;

    private PlayerUI playerUI;
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        // Clears message string
        playerUI.UpdateText(string.Empty);

        // Creating ray shooting outwards from camera centre
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);  
        RaycastHit hit; // Stores collision information
        if (Physics.Raycast(ray, out hit, distance, mask))
        {
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.message);
                if (inputManager.onFoot.Interact.triggered)
                {
                   interactable.BaseInteraact();
                }
            }
            
        }

    }
}
