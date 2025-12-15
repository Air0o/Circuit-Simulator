using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class Wire : MonoBehaviour
{
    public Node node_a, node_b;
    List<Vector3> points = new();
    public bool is_editing_wire;

    Camera main_camera => Camera.main;
    LineRenderer line_renderer;

    private void Start()
    {

        InitLineRenderer();
        PlayerInteractionManager.is_editing_a_wire = true;
        PlayerInteractionManager.edited_wire = this;
        is_editing_wire = true;


        FindAnyObjectByType<GateSimulationManager>().AddWire(this);
    }

    void OnDestroy()
    {
        try
        {
            FindAnyObjectByType<GateSimulationManager>().RemoveWire(this);
        }
        catch { }
        ;
    }

    void Update()
    {
        if (is_editing_wire)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                is_editing_wire = false;
                PlayerInteractionManager.is_editing_a_wire = false;
                Destroy(gameObject);
            }
            line_renderer.positionCount = points.Count + 1;
            line_renderer.SetPositions(points.ToArray());
            line_renderer.SetPosition(points.Count, FixPosition(main_camera.ScreenToWorldPoint(Input.mousePosition)));

            if (Input.GetMouseButtonDown(0))
            {
                AddPoint(main_camera.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }

    public void FinishedEditing(Node node_b)
    {
        is_editing_wire = false;
        this.node_b = node_b;

        UpdateWireState();
    }

    public void UpdateWireState()
    {
        if (is_editing_wire) return;
        if (node_a == null || node_b == null)
        {
            Destroy(gameObject);
            return;
        }
        node_b.ChangeState(node_a.state);
    }

    public void AddPoint(Vector3 pos)
    {
        if (points.Count == 0)
        {
            pos = FixPosition(transform.position);
        }
        else
        {
            pos = FixPosition(pos);
        }

        points.Add(pos);
        line_renderer.positionCount = points.Count;
        line_renderer.SetPositions(points.ToArray());
    }
    public Vector3 FixPosition(Vector3 pos)
    {
        return new Vector3(pos.x, pos.y, 0);
    }
    void InitLineRenderer()
    {
        line_renderer = gameObject.AddComponent<LineRenderer>();
        line_renderer.material = Resources.Load("Wire Material") as Material;
        line_renderer.startColor = Color.black;
        line_renderer.endColor = Color.black;
        line_renderer.startWidth = .1f;
        line_renderer.endWidth = .1f;
        line_renderer.numCornerVertices = 2;
    }
}
