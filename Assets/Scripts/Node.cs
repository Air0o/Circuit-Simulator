using System.Collections.Generic;
using UnityEngine;

public enum NodeType{
    Input,
    Output
}

public class Node : MonoBehaviour
{
    public NodeType type;
    public bool state = false;
    new SpriteRenderer renderer;

    public List<Wire> connected_wires = new();

    void Start()
    {
        ChangeState(false);
    }

    void OnMouseDown()
    {
        if (PlayerInteractionManager.is_editing_a_wire)
        {
            PlayerInteractionManager.is_editing_a_wire = false;
            
            Wire new_wire = PlayerInteractionManager.edited_wire;
            new_wire.FinishedEditing(this);
            new_wire.AddPoint(transform.position);

            connected_wires.Add(new_wire);
        }
        else
        {
            GameObject wire_obj = new GameObject("Wire");
            wire_obj.transform.position = transform.position;
            wire_obj.transform.SetParent(transform);
            Wire new_wire = wire_obj.AddComponent<Wire>();
            new_wire.node_a = this;
            connected_wires.Add(new_wire);
        }
    }

    public void ChangeState(bool state)
    {
        this.state = state;
        if(renderer == null)
        {
            renderer = GetComponentInChildren<SpriteRenderer>();
        }
        renderer.color = state ? Color.red : Color.darkGray;
    }

    public void SwitchState()
    {
        ChangeState(!state);
    }
}
