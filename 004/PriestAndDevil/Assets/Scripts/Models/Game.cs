using System;
using UnityEngine;

namespace PriestsAndDevils
{
  // 游戏结果，判断是未结束（正在进行）还是输赢
  public enum Result
  {
    GAMING,
    WIN,
    LOSE,
  }

  public class Game
  {

    public Result result = Result.GAMING; // 游戏结果。
    private Boat boat;
    private Coast leftCoast;
    private Coast rightCoast;
    // 用于通知场景控制器游戏的胜负。
    public event EventHandler onChange;
    // 根据传入的控制器生成裁判类。
    public Game(Boat boat, Coast leftCoast, Coast rightCoast)
    {
      this.boat = boat;
      this.leftCoast = leftCoast;
      this.rightCoast = rightCoast;
    }

    // It determines whether the player wins the game.
    public void CheckWinner()
    {
      result = Result.GAMING;

      // 计算成员组成
      int leftPriests = leftCoast.GetCharacterAmount()[0];
      int leftDevils = leftCoast.GetCharacterAmount()[1];
      int rightPriests = rightCoast.GetCharacterAmount()[0];
      int rightDevils = rightCoast.GetCharacterAmount()[1];

      // 如果所有牧师都在边上
      if (leftPriests + leftDevils == 6)
      {
        result = Result.WIN;
      }
      // 船在右边
      if (boat.location == Location.Right)
      {
        rightPriests += boat.GetCharacterAmount()[0];
        rightDevils += boat.GetCharacterAmount()[1];
      }
      else // 船左边
      {
        leftPriests += boat.GetCharacterAmount()[0];
        leftDevils += boat.GetCharacterAmount()[1];
      }
      // 失败
      if ((rightPriests < rightDevils && rightPriests > 0) ||
          (leftPriests < leftDevils && leftPriests > 0))
      {
        result = Result.LOSE;
      }
      // 通知场景控制器。
      onChange?.Invoke(this, EventArgs.Empty);
    }
  }
}