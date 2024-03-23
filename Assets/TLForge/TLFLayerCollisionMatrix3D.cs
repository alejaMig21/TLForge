using UnityEngine;

#if UNITY_EDITOR
public class TLFLayerCollisionMatrix3D : TLFLayerCollisionMatrix2D
{
    public override void IgnoreLayerCollision(int layer1, int layer2, bool collisionValue)
    {
        Physics.IgnoreLayerCollision(layer1, layer2, collisionValue);
    }
}
#endif