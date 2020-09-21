using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe.UI
{
  public class SinglePlayerModeController : GameController
  {
    void Awake()
    {
      // Initialize.
      mechanics = new Mechanics.SinglePlayerMode();
    }

    public override void AfterRenderButton(int i, int j, Mechanics.Player player, bool isPressed)
    {
      if (!mechanics.GetPlaying())
      {
        return;
      }
      // 按照顺序判定
      if (mechanics.GetTurn())
      {
        if (isPressed)
        {
          mechanics.SetHistory(i, j);
          //   mechanics.CheckWin();
        }
      }
      else if (mechanics.CheckWin() != Mechanics.Player.First)
      {
        mechanics.AIMove();
      }
    }
  }
}
