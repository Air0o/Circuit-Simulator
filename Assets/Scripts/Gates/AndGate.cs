using UnityEditor;
using UnityEngine;

public class AndGate : MonoBehaviour, IGate
{
    public Node input_a, input_b, output;

    public GameObject prefab;

    private void Start() {
        FindAnyObjectByType<GateSimulationManager>().AddGate(this);
        Invoke("Init", .1f);
    }

    void Init()
    {
        output.ChangeState(input_a.state && input_b.state);
    }

    void IGate.UpdateGateState()
    {
        output.ChangeState(input_a.state && input_b.state);
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
    private void OnDestroy() {try
        {
            GameObject.FindAnyObjectByType<GateSimulationManager>().RemoveGate(this);
        } catch{};
    }

    public void SetGatePrefab(GameObject prefab)
    {
        this.prefab = prefab;
    }
}
