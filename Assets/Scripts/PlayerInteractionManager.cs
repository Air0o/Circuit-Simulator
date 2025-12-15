using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public interface IPlaceable
{
    public void Place();
}

public static class PlayerInteractionManager
{
    public static bool is_editing_a_wire = false;
    public static Wire edited_wire;

    public static bool is_placing_gate = false;
    public static GameObject selected_gate_prefab, gate_preview_instance;   

    public static bool is_deleting = false;

    public static void SelectGate(GameObject gate_prefab)
    {
        if(is_editing_a_wire) return;
        if (is_placing_gate)
        {
            if (gate_prefab == selected_gate_prefab) return;
            UnselectGate();
        }

        is_placing_gate = true;
        selected_gate_prefab = gate_prefab;
        gate_preview_instance = GameObject.Instantiate(selected_gate_prefab);
    }
    public static void UnselectGate()
    {
        is_placing_gate = false;
        GameObject.Destroy(gate_preview_instance);
        gate_preview_instance = null;
        selected_gate_prefab = null;
    }
    
    public static void PlaceGate()
    {
        foreach(Component component in gate_preview_instance.GetComponents<Component>())
        {
            if(component is IPlaceable)
            {
                ((IPlaceable)component).Place();
            }
            if(component is IGate)
            {
                ((IGate)component).SetGatePrefab(selected_gate_prefab);
            }
        }

        is_placing_gate = false;
        gate_preview_instance = null;
        selected_gate_prefab = null;        
    }
}