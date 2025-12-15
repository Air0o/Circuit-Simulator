

using UnityEngine;

public class GatePreview : MonoBehaviour
{
    Camera main_camera;
    private void Start() {
        main_camera = Camera.main;
    }
    void Update()
    {
        if (PlayerInteractionManager.is_placing_gate)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PlayerInteractionManager.UnselectGate();
                return;
            }
            if (Input.GetMouseButtonDown(0))
            {
                PlayerInteractionManager.PlaceGate();
                return;
            }
            PlayerInteractionManager.gate_preview_instance.transform.position = FixPosition(main_camera.ScreenToWorldPoint(Input.mousePosition));
        }
    }
    public Vector3 FixPosition(Vector3 pos)
    {
        return new Vector3(pos.x, pos.y, 0);
    }
}