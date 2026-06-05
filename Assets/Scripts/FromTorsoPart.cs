using UnityEngine;

public class FromTorsoPart : RobotPart
{
    [SerializeField]
    float YPartOffset;
    public virtual void AddOffset(Vector3 TorsoPosition)
    {
        this.transform.position = new Vector3(0, TorsoPosition.y + YPartOffset, 0);
    }
}
