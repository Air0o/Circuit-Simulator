using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GateButton : MonoBehaviour
{
    public GameObject prefab;
    
    public TextMeshProUGUI gate_name_text;

    private void Start()
    {
        gate_name_text.text = prefab.name;
    }

    public void Clicked()
    {
        Debug.Log($"Selected {prefab.name}");
        PlayerInteractionManager.SelectGate(prefab);
    }
}
