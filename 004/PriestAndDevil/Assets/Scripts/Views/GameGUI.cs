using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PriestsAndDevils
{
  public class GameGUI : MonoBehaviour
  {
    public Result result;
    private IUserAction action;

    private const string title = "Priests and Devils";
    private const string author = "米家龙";
    private const string rule =
      @"蓝色: priests    红色: devils
3 priests and 3 devils want to go across the river.
The boat can only carries two persons each time.
One person must steer the boat.
Click on person or boat to make it move.
Priests get killed when less than devils on either side.
You should keep all priests alive!";

    private const string exit = "退出";
    private const string restart = "重置游戏";

    // Use this for initialization
    void Start()
    {
      result = Result.GAMING; // 初始化为未完成状态
      action = Director.GetInstance().currentSceneController as IUserAction;
    }

    void OnGUI()
    {

      // 标题的样式
      var textStyle = new GUIStyle()
      {
        fontSize = 40,
        alignment = TextAnchor.MiddleCenter
      };
      GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 280, 100, 50), title, textStyle); // 游戏标题
      GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 230, 100, 50), author, textStyle); // 游戏作者

      // 规则的样式
      var ruleStyle = new GUIStyle
      {
        fontSize = 20,
        fontStyle = FontStyle.Normal,
        alignment = TextAnchor.MiddleCenter
      };
      GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 110, 100, 50), rule, ruleStyle); // 游戏规则

      // 按钮的样式
      var buttonStyle = new GUIStyle("button")
      {
        fontSize = 15,
      };

      // 游戏结束
      if (result != Result.GAMING)
      {
        var text = result == Result.WIN ? "You Win!" : "You Lose!";
        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 50, 100, 50), text, textStyle);
      }

      // 重置游戏按钮
      if (GUI.Button(new Rect(Screen.width / 2 - 140, Screen.height / 2 + 140, 100, 40), restart, buttonStyle))
      {
        action.Reset();
      }

      // 退出游戏按钮
      if (GUI.Button(new Rect(Screen.width / 2 + 40, Screen.height / 2 + 140, 100, 40), exit, buttonStyle))
      {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
      }


    }

    // 使用射线捕捉来获取游戏对象
    void Update()
    {
      if (Input.GetButtonDown("Fire1") && this.result == Result.GAMING) // 判断按键
      {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
          var todo = hit.collider;
          var character = todo.GetComponent<CharacterController>(); // 获取角色
          if (character)                                            // 如果是角色
          {
            action.ClickCharacter(character);
          }
          else if (todo.transform.name == "Boat") // 如果不是，那么判断是否是船
          {
            action.ClickBoat();
          }
        }
      }
    }
  }
}
