using System.Linq;
using TMPro;
using UnityEngine;

public class Variable : MonoBehaviour, IPlaceable
{
    public Node output;

    public TextMeshProUGUI text;

    bool just_placed = false;

    void Start()
    {
        int total_var_count = FindObjectsByType<Variable>(FindObjectsSortMode.None).Length;

        text.text = $"{(char)('A' + total_var_count-1)}";
    }

    public void Clicked()
    {
        if (just_placed)
        {
            just_placed = false;
            return;
        }

        output.SwitchState();

        if (output.state)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.white;
        }
    }

    public void Place()
    {
        
        just_placed = true;
    }
}
