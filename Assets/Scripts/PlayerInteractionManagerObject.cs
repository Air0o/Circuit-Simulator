using UnityEngine;

public class PlayerInteractionManagerObject : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerInteractionManager.is_deleting = !PlayerInteractionManager.is_deleting;
        }
    }
}
