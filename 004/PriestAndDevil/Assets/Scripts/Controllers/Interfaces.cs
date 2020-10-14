using System.Collections;
using System.Collections.Generic;

using UnityEngine;

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