using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe.UI
{
    public class TwoPlayerModeController : GameController
    {
        void Awake()
        {
            // Initialize.
            mechanics = new Mechanics.Basic();
        }

        public override void AfterRenderButton(int i, int j, Mechanics.Player player, bool isPressed)
        {
            if (mechanics.GetPlaying() && isPressed)
            {
                mechanics.SetHistory(i, j);
            }
        }
    }
}
