using UnityEngine;

public class FromTorsoPart : RobotPart
{
    [SerializeField]
    float YPartOffset;
    public virtual void AddOffset(Vector3 TorsoPosition)
    {
        this.transform.localPosition = new Vector3(0, YPartOffset, 0);
    }
}
