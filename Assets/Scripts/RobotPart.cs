using Unity.VisualScripting;
using UnityEngine;

public class RobotPart : MonoBehaviour
{
    [field: SerializeField]
    public RobotPart_SO robotPart_SO{ get; private set; }
    [SerializeField] 
    Animator partAnimator;
    void Start()
    {
        
        if (partAnimator != null) partAnimator.enabled = false;
    }

    public virtual void PlayActionAnim()
    {
        if (partAnimator != null)
        {
            partAnimator.enabled = true;
            partAnimator.SetTrigger("Action");
        }
    }
    
}
