using UnityEngine;

public class ButtonManager : MonoBehaviour
{
   public ObjetifData queen;
   public ObjetifData reserveSheet;
   
   public void ObjetifNewAnt()
   {
      Sommet goal = RechercheProfondeurGraph.instance.FindSommet(reserveSheet.id);
      Sommet home = RechercheProfondeurGraph.instance.FindSommet(queen.id);
      if (goal && home) LevelManager.instance.ManipuleAnt(goal, home);
   }
}
