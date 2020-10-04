# 牧师与魔鬼项目文档

- [牧师与魔鬼项目文档](#牧师与魔鬼项目文档)
  - [要求](#要求)
  - [游戏对象和动作表](#游戏对象和动作表)
    - [游戏对象](#游戏对象)
    - [动作表](#动作表)
  - [设计职责](#设计职责)
    - [导演](#导演)
    - [场记](#场记)
  - [MVC 结构设计](#mvc-结构设计)

## 要求

阅读以下游戏脚本:

> Priests and Devils
>
> Priests and Devils is a puzzle game in which you will help the Priests and Devils to cross the river within the time limit. There are 3 priests and 3 devils at one side of the river. They all want to get to the other side of this river, but there is only one boat and this boat can only carry two persons each time. And there must be one person steering the boat from one side to the other side. In the flash game, you can click on them to move them and click the go button to move the boat to the other direction. If the priests are out numbered by the devils on either side of the river, they get killed and the game is over. You can try it in many > ways. Keep all priests alive! Good luck!

程序需要满足的要求：

- [Play the game](http://www.flash-game.net/game/2535/priests-and-devils.html)。
- 列出游戏中提及的事物（Objects）。
- 用表格列出玩家动作表（规则表），注意，动作越少越好。
- 请将游戏中对象做成预制。
- 在 GenGameObjects 中创建长方形、正方形、球及其色彩代表游戏中的对象。
- 使用 C# 集合类型有效组织对象。
- 整个游戏仅主摄像机 和 一个 Empty 对象， **其他对象必须代码动态生成**！！！。整个游戏不许出现 Find 游戏对象， SendMessage 这类突破程序结构的**通讯耦合**语句。**违背本条准则，不给分**。
- 请使用课件架构图编程，**不接受非 MVC 结构程序**。
- 注意细节，例如：船未靠岸，牧师与魔鬼上下船运动中，均不能接受用户事件！

## 游戏对象和动作表

### 游戏对象

游戏对象构建如下表：

| 游戏对象 |           预设对象           | 对象形状/类型 |
| :------: | :--------------------------: | :-----------: |
|   牧师   | ![牧师](./img/priestPre.png) |    Sphere     |
|   恶魔   | ![恶魔](./img/devilPre.png)  |     Cube      |
|    船    |   ![船](./img/boatPre.png)   |     Cube      |
|   河岸   | ![河岸](./img/stonePre.png)  |     Cube      |
|   河流   | ![河流](./img/waterPre.png)  |     Cube      |

### 动作表

|       玩家动作        | 游戏对象具体行为                           |
| :-------------------: | :----------------------------------------- |
| 点击角色（牧师/恶魔） | 船上的角色——上岸                           |
|                       | 岸上的角色——如果船没满，则上船；满则无操作 |
|       点击船只        | 船只移动                                   |

## 设计职责

根据课堂内容，我们可以将职责换分为如下三个：
- 导演 * 1，是具体类型（`Director`），负责控制场景切换
- 场景控制 * n（在该游戏中为1），是抽象类型（`ISenceController`），负责每个场景的布景、演员上下场和动作管理等
- 观众 * 1，是抽象类型（`IUserAction`），负责和场景控制交互

后两者的接口为：
```csharp
namespace PriestsAndDevils
{
  public interface ISceneController
  {
    void LoadResources(); // 加载资源，生成游戏对象
  }

  public interface IUserAction
  {
    void ClickBoat();                           // 点击船只事件
    void ClickCharacter(CharacterController c); // 点击角色事件
    void Reset();                               // 重置游戏事件
  }
}
```

### 导演

导演，具体代码如下：
```csharp
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PriestsAndDevils
{
  public class Director : System.Object
  {
    private static Director instance;                             // 导演必须为单例
    public ISceneController currentSceneController { get; set; }  // 当前场记
    public bool running { get; set; }                             // 游戏是否正在运行
    public int fps // 帧数
    {
      get
      {
        return Application.targetFrameRate;
      }
      set
      {
        Application.targetFrameRate = value;
      }
    }

    // 获取单例
    public static Director GetInstance()
    {
      return instance ?? (instance = new Director());
    }
  }
}
```

### 场记

由于本游戏只有一个场景，因此只需要创建一个场记类，将该场记命名为 `GameController` ，代码如下：
```csharp
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PriestsAndDevils
{
  // 游戏结果，判断是未结束（正在进行）还是输赢
  public enum Result
  {
    GAMING, // 正在进行
    WINNER, // 赢了
    LOSER, // 输了
  }

  public class GameController : MonoBehaviour, ISceneController, IUserAction
  {
    public CoastController leftCoast;   // 左岸
    public CoastController rightCoast;  // 右岸
    public BoatController boat;         // 船
    public List<CharacterController> characters = new List<CharacterController>(6); // 最大能承载6个角色列表
    private GameGUI gui; // 场景界面

    void Awake()
    {
      Director director = Director.GetInstance(); // 获取导演
      director.currentSceneController = this; // 设置当前场记

      this.gui = gameObject.AddComponent<GameGUI>() as GameGUI; // 设置 gui

      LoadResources();
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
        this.characters[i].GoAshore(rightCoast);

      }

      // 加载恶魔
      for (int i = 0; i < 3; ++i)
      {
        GameObject devils = Utils.Instantiate("Prefabs/Devil", Coast.destination);
        this.characters[i + 3] = devils.AddComponent<CharacterController>();
        devils.name = characters[i + 3].name = "Devil" + i;
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
      boat.Move();
      gui.result = CheckWinner(); // 移动完之后需要进行机制判断
    }

    // 点击角色触发事件
    public void ClickCharacter(CharacterController character)
    {
      // 如果在船上，那么尝试上岸
      if (character.model.isOnboard)
      {
        boat.GoAshore(character);
        character.GoAshore((boat.location == Location.Right ? rightCoast : leftCoast));
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
      }

      // 更新场景显示.
      gui.result = CheckWinner();
    }

    // 重置游戏
    public void Reset()
    {
      gui.result = Result.GAMING; // 重置界面
      boat.Reset();               // 充值船的位置
      leftCoast.Reset();          // 重置（清空）左岸人物
      rightCoast.Reset();         // 重置右岸人物

      // 重置人物位置
      for (int i = 0; i < 6; ++i)
      {
        characters[i].Reset();
      }
    }

    // 判断游戏是否结束
    private Result CheckWinner()
    {
      Result result = Result.GAMING; // 储存结果

      // 获取两岸角色构成，用于判断结果
      int leftPriests = leftCoast.model.GetCharacterAmount()[0];
      int leftDevils = leftCoast.model.GetCharacterAmount()[1];
      int rightPriests = rightCoast.model.GetCharacterAmount()[0];
      int rightDevils = rightCoast.model.GetCharacterAmount()[1];

      // 左岸全部人到齐了，游戏胜利
      if (leftPriests + leftDevils == 6)
      {
        result = Result.WINNER;
      }

      // 船在右边，增加船上人员构成
      if (boat.location == Location.Right)
      {
        rightPriests += boat.model.GetCharacterAmount()[0];
        rightDevils += boat.model.GetCharacterAmount()[1];
      }
      // 船在左边，增加船上人员构成
      else
      {
        leftPriests += boat.model.GetCharacterAmount()[0];
        leftDevils += boat.model.GetCharacterAmount()[1];
      }

      // 判断人员构成，看是否输掉游戏
      if ((rightPriests < rightDevils && rightPriests > 0) ||
          (leftPriests < leftDevils && leftPriests > 0))
      {
        result = Result.LOSER;
      }
      return result;
    }
  }
}
```

## MVC 结构设计