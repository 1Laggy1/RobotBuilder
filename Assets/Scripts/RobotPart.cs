using Unity.VisualScripting;
using UnityEngine;

public class RobotPart : MonoBehaviour
{
    [field: SerializeField]
    public RobotPart_SO robotPart_SO{ get; private set; }
    [SerializeField] 
    Animator partAnimator;
    

    public virtual void PlayActionAnim()
    {
        if (partAnimator != null)
        {
            partAnimator.SetTrigger("Action");
        }
    }
    
}
