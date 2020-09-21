using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe.UI
{
  public class HomeController : MonoBehaviour
  {
    public Material material;
    // OnGUI is called when rendering and handling GUI events.
    void OnGUI()
    {
      // 这些属性和屏幕有关
      float height = Screen.height * 0.5f;
      float width = Screen.width * 0.5f;

      // 设置游戏标题
      float titleWidth = 200;
      float titleHeight = 100;
      float titleBaseX = width - titleWidth / 2;
      float titleBaseY = height - titleHeight * 2;
      GUIStyle titleStyle = new GUIStyle
      {
        fontSize = 50,
        fontStyle = FontStyle.Bold,
      };
      GUI.Label(new Rect(titleBaseX + 10, titleBaseY, titleWidth, titleHeight), "井 字 棋", titleStyle);

      // 设置制作人（
      GUI.Label(new Rect(titleBaseX + 50, titleBaseY + 80, titleWidth, titleHeight), "米家龙", new GUIStyle
      {
        fontSize = 30,
        fontStyle = FontStyle.Italic,
      });

      // 其他按钮的属性.
      float buttonWidth = 100;
      float buttonHeight = 50;
      float buttonBaseX = width - buttonWidth / 2;
      float buttonBaseY = height - buttonHeight / 2;

      // 单人模式 按钮
      if (GUI.Button(new Rect(buttonBaseX, buttonBaseY, buttonWidth, buttonHeight), "单人模式"))
      {
        SceneManager.LoadScene("SinglePlayerMode");
      }

      // 双人模式 按钮
      if (GUI.Button(new Rect(buttonBaseX, buttonBaseY + buttonHeight + 10, buttonWidth, buttonHeight), "双人模式"))
      {
        SceneManager.LoadScene("TwoPlayerMode");
      }

      // 退出
      if (GUI.Button(new Rect(buttonBaseX, buttonBaseY + buttonHeight * 2 + 20, buttonWidth, buttonHeight), "退出"))
      {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
      }
    }
  }
}
