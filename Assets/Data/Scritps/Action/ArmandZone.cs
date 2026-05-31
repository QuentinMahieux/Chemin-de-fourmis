using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmandZone", menuName = "Scriptable Objects/Action/ArmandZone")]

public class ArmandZone : DefaultAction
{
    public AnimatorController animatorController;
    public override void Action(Sommet sommet)
    {
        sommet.defaultZone.animator.runtimeAnimatorController  = animatorController;
    }

    public override void PassifAction(Sommet sommet)
    {
        if(!sommet.defaultZone.animator.runtimeAnimatorController) sommet.defaultZone.animator.runtimeAnimatorController  = animatorController;

        
        int random = Random.Range(0, 3000);
        
        if(random == 1) sommet.defaultZone.animator.Play("Glitch");
        else if (random == 2) sommet.defaultZone.animator.Play("Look");
        else if (random == 3) sommet.defaultZone.animator.Play("Lgbt");
        else sommet.defaultZone.animator.Play("Idle");
    }
}
