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
            // Human is the Player 1, the AI is the Player 2.
            if (mechanics.GetTurn())
            {
                if (isPressed)
                {
                    mechanics.SetHistory(i, j);
                }
            }
            else
            {
                mechanics.AIMove();
            }
        }
    }
}
