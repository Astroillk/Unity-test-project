using UnityEngine;

[CreateAssetMenu(fileName = "KeyBinds", menuName = "Keys", order = 1)]
public class KeyBinds : ScriptableObject
{
    [Header("Movement Keys")] 
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
}
