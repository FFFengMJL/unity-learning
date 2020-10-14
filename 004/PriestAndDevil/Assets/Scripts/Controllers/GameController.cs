using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PriestsAndDevils
{
  public class GameController : MonoBehaviour, ISceneController, IUserAction
  {
    private Game game; // 裁判
    private GameActionManager actionManager; // 动作管理
    public CoastController leftCoast; // 左岸
    public CoastController rightCoast; // 右岸
    public BoatController boat; // 船
    public List<CharacterController> characters = new List<CharacterController>(6); // 最大能承载6个角色列表
    private GameGUI gui; // 场景界面

    void Awake()
    {
      Director.GetInstance().OnSceneWake(this); // 加载导演

      this.gui = gameObject.AddComponent<GameGUI>() as GameGUI; // 设置 gui
      this.actionManager = gameObject.AddComponent<GameActionManager>(); // 初始化动作管理

      this.game = new Game(boat.model, leftCoast.model, rightCoast.model); // 初始化裁判

      // 裁判类通知场景控制器游戏胜负。
      game.onChange += delegate
      {
        gui.result = game.result;
      };

      // LoadResources();
    }

    // 加载相关资源，生成游戏对象
    public void LoadResources()
    {
      // 加载河流
      GameObject river = Utils.Instantiate("Prefabs/Water", new Vector3(0, 0.5f, 0));
      river.name = "River";

      // 加载左岸
      GameObject leftCoastTemp = Utils.Instantiate("Prefabs/Stone", Coast.departure);
      this.leftCoast = leftCoastTemp.AddComponent<CoastController>();
      this.leftCoast.name = leftCoast.name = "LeftCoast";
      leftCoast.location = Location.Left;

      // 加载右岸
      GameObject rightCoastTemp = Utils.Instantiate("Prefabs/Stone", Coast.destination);
      this.rightCoast = rightCoastTemp.AddComponent<CoastController>();
      this.rightCoast.name = rightCoast.name = "RightCoast";
      this.rightCoast.location = Location.Right;

      // 加载船只
      GameObject boatTemp = Utils.Instantiate("Prefabs/Boat", Boat.departure);
      this.boat = boatTemp.AddComponent<BoatController>();
      this.boat.name = boat.name = "Boat";

      // 加载牧师
      for (int i = 0; i < 3; ++i)
      {
        GameObject priests = Utils.Instantiate("Prefabs/Priest", Coast.destination);
        this.characters[i] = priests.AddComponent<CharacterController>();
        priests.name = characters[i].name = "Priest" + i;
        characters[i].SetPosition(rightCoast.model.GetEmptyPosition());
        this.characters[i].GoAshore(rightCoast);

      }

      // 加载恶魔
      for (int i = 0; i < 3; ++i)
      {
        GameObject devils = Utils.Instantiate("Prefabs/Devil", Coast.destination);
        this.characters[i + 3] = devils.AddComponent<CharacterController>();
        devils.name = characters[i + 3].name = "Devil" + i;
        characters[i + 3].SetPosition(rightCoast.model.GetEmptyPosition());
        this.characters[i + 3].GoAshore(rightCoast);
      }
    }

    // 点击船只触发事件
    public void ClickBoat()
    {
      // 如果船只载客为空，那么船只没法移动
      if (boat.model.IsEmpty())
      {
        return;
      }

      this.boat.Move();
      actionManager.MoveBoat(boat);
      game.CheckWinner();
    }

    // 点击角色触发事件
    public void ClickCharacter(CharacterController character)
    {
      // 如果在船上，那么尝试上岸
      if (character.model.isOnboard)
      {
        boat.GoAshore(character);
        character.GoAshore((boat.location == Location.Right ? rightCoast : leftCoast));
        actionManager.MoveCharacter(character);
      }
      // 如果在岸上，那么尝试上传（需要考虑船是否满载，是否同岸）
      else
      {
        // 如果不同岸，那么什么都不干
        if (character.location != boat.location)
        {
          return;
        }
        // 如果满载
        else if (boat.model.GetEmptyIndex() == -1)
        {
          return;
        }

        // 上岸行为
        CoastController temp = (character.location == Location.Right ? rightCoast : leftCoast); ;
        temp.GoAboard(character);
        character.GoAboard(boat);
        actionManager.MoveCharacter(character);
      }

      // 判断是否赢
      game.CheckWinner();
    }

    // 重置游戏
    public void Reset()
    {
      gui.result = Result.GAMING; // 重置界面
      boat.Reset(); // 充值船的位置
      leftCoast.Reset(); // 重置（清空）左岸人物
      rightCoast.Reset(); // 重置右岸人物

      // 重置人物位置
      for (int i = 0; i < 6; ++i)
      {
        characters[i].Reset();
      }
    }

  }
}
