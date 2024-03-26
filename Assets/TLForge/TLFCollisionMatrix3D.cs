using UnityEngine;

public class TLFCollisionMatrix3D : TLFCollisionMatrix2D
{
    public override void IgnoreLayerCollision(int layer1, int layer2, bool collisionValue)
    {
        Physics.IgnoreLayerCollision(layer1, layer2, collisionValue);
    }
}