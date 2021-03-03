using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FimDeJogo : MonoBehaviour
{
   public Text message;

    GameManager gm;
   private void OnEnable()
   {
       Debug.Log("entrou debug");
       gm = GameManager.GetInstance();

       if(gm.vidas > 0)
       {
           message.text = "VOCÊ GANHOU!";
       }
       else
       {
           message.text = "VOCÊ PERDEU!";
       }
   }

   public void Voltar()
    {
        gm.ChangeState(GameManager.GameState.GAME);
    }
}
