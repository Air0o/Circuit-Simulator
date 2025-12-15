using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GateSimulationManager : MonoBehaviour
{
    public delegate void UpdateGate();
    public event UpdateGate OnGateUpdateCall;
    List<Wire> wires = new();
    List<IGate> gates = new();

    public void AddWire(Wire w)
    {
        wires.Add(w);
    }

    public void AddGate(IGate g)
    {
        gates.Add(g);
    }
    public void RemoveWire(Wire w)
    {
        wires.Remove(w);
    }

    public void RemoveGate(IGate g)
    {
        gates.Remove(g);
    }

    private void Start() {
        StartCoroutine(Step());
    }

    IEnumerator Step()
    {
        while (true)
        {
            for(int i = 0; i < gates.Count; i++)
            {
                gates.ElementAt(i).UpdateGateState();
            }
            for(int i = 0; i < wires.Count; i++)
            {
                wires.ElementAt(i).UpdateWireState();
            }

            yield return null;
        }
    }
}
