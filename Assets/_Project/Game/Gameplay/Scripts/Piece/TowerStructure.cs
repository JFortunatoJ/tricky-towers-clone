using UnityEngine;

public abstract class TowerStructure : MonoBehaviour
{
    public int Id => gameObject.GetInstanceID();
}
