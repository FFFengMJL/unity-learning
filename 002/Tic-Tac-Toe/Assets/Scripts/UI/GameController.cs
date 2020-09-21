using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe.UI
{
  public abstract class GameController : MonoBehaviour
  {
    // It controls the logic of the game.
    protected Mechanics.Basic mechanics;

    // OnGUI is called when rendering and handling GUI events.
    void OnGUI()
    {

      // 重置和回到主页按钮的长宽
      float buttonWidth = 100;
      float buttonHeight = 50;

      // 棋盘单个按钮的长宽
      int bHeight = 100;
      int bWidth = 100;
      int margin = 5;
      float width = Screen.width * 0.5f - bHeight * 1.5f - margin;
      float height = Screen.height * 0.5f - 100;

      // 回到主页 按钮
      float buttonBaseX = Screen.width / 2;
      float buttonBaseY = Screen.height / 2 + bHeight * 1.5f + margin * 3;
      if (GUI.Button(new Rect(buttonBaseX - bWidth * 1.5f - margin, buttonBaseY, buttonWidth, buttonHeight), "回到主页"))
      {
        OnPressBackButton();
        return;
      }

      // 重置棋盘 按钮
      if (GUI.Button(new Rect(buttonBaseX + bWidth / 2 + margin, buttonBaseY, buttonWidth, buttonHeight), "重置棋盘"))
      {
        OnPressResetButton();
        return;
      }

      // Check if there is a winner.
      var winner = mechanics.CheckWin();
      if (winner != Mechanics.Player.Unfinished)
      {
        string msg = winner == Mechanics.Player.First ? "Player1 (X) Wins!" :
                                winner == Mechanics.Player.Second ? "Player2 (O) Wins!" : "Tie!";
        GUIStyle messageStyle = new GUIStyle
        {
          fontSize = 25,
          fontStyle = FontStyle.BoldAndItalic,
        };
        messageStyle.normal.textColor = Color.red;
        // 展示结果
        if (msg == "Tie!")
        {
          GUI.Label(new Rect(width + 135, height - 75 - 50, 100, 100), msg, messageStyle);
        }
        else
        {
          GUI.Label(new Rect(width + 50, height - 75 - 50, 100, 100), msg, messageStyle);
        }
      }

      for (int i = 0; i < 3; ++i)
      {
        for (int j = 0; j < 3; ++j)
        {
          var player = mechanics.GetHistory(i, j);
          string token;
          if (player == Mechanics.Player.First)
          {
            token = "X";
          }
          else if (player == Mechanics.Player.Second)
          {
            token = "O";
          }
          else
          {
            token = "";
          }
          // 设置棋盘块的位置
          var isPressed = GUI.Button(new Rect(width + i * (bWidth + margin), height - 50 + j * (bHeight + margin), bWidth, bHeight), token);
          // 避免多次点击带来的错误
          if (player == Mechanics.Player.Unfinished)
          {
            AfterRenderButton(i, j, player, isPressed);
          }
        }
      }
    }

    public abstract void AfterRenderButton(int i, int j, Mechanics.Player player, bool isPressed);

    // 回到主页
    void OnPressBackButton()
    {
      SceneManager.LoadScene("Home");
    }

    // 重置棋盘
    void OnPressResetButton()
    {
      mechanics.Reset();
    }
  }
}