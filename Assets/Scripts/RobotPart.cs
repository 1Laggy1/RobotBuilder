using Unity.VisualScripting;
using UnityEngine;

public class RobotPart : MonoBehaviour
{
    [SerializeField]
    RobotPart_SO robotPart_SO;
    [SerializeField] 
    Animator partAnimator;
    [SerializeField]
    float YPartOffset;

    public virtual void PlayActionAnim()
    {
        if (partAnimator != null)
        {
            partAnimator.SetTrigger("Action");
        }
    }
    public virtual void SpawnPart()
    {
        this.transform.position += new Vector3(0, YPartOffset, 0);
    }
}
