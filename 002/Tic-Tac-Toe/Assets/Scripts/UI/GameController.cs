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
      float width = Screen.width * 0.5f - 100;
      float height = Screen.height * 0.5f - 50;

      // 重置和回到主页按钮的属性
      float buttonWidth = 100;
      float buttonHeight = 50;

      // 回到主页 按钮
      float buttonBaseX = width - buttonWidth / 2;
      float buttonBaseY = height + buttonHeight * 2;
      if (GUI.Button(new Rect(buttonBaseX - 100, buttonBaseY - 55, buttonWidth, buttonHeight), "回到主页"))
      {
        OnPressBackButton();
        return;
      }

      // 重置棋盘 按钮
      if (GUI.Button(new Rect(buttonBaseX - 100, buttonBaseY, buttonWidth, buttonHeight), "重置棋盘"))
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
        GUI.Label(new Rect(width + 50, height - 75 - 50, 100, 100), msg, messageStyle);
      }

      // 生成棋盘
      int bHeight = 100;
      int bWidth = 100;
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
          var isPressed = GUI.Button(new Rect(width + i * bWidth + i * 5, height - 50 + j * bHeight + j * 5, bWidth, bHeight), token);
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