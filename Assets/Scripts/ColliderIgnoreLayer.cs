using UnityEngine;

public class ColliderIgnoreLayer : MonoBehaviour
{
    void OnMouseEnter()
    {
        int layerToIgnore = LayerMask.NameToLayer("IgnoreRaycast");
        if (gameObject.layer == layerToIgnore)
        {
            return;
        }
    }
}