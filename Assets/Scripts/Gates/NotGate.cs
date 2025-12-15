using UnityEditor;
using UnityEngine;

public class NotGate : MonoBehaviour, IGate
{
    public Node input, output;

    public GameObject prefab;
    private void Start()
    {
        FindAnyObjectByType<GateSimulationManager>().AddGate(this);
        Invoke("Init", .1f);
    }

    void Init()
    {
        output.ChangeState(!input.state);
    }

    void IGate.UpdateGateState()
    {
        output.ChangeState(!input.state);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(!PlayerInteractionManager.is_placing_gate && !PlayerInteractionManager.is_editing_a_wire)
            {
                PlayerInteractionManager.SelectGate(prefab);
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        try
        {
            GameObject.FindAnyObjectByType<GateSimulationManager>().RemoveGate(this);
        }
        catch { }
        ;
    }
    public void SetGatePrefab(GameObject prefab)
    {
        this.prefab = prefab;
    }
}
