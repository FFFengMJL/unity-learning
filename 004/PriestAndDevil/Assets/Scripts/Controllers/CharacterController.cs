using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PriestsAndDevils
{
  public class CharacterController : Moveable
  {
    public Character model = new Character();
    public new string name { get { return model.name; } set { model.name = value; } }

    // 用于表示左岸还是右岸
    public Location location { get { return model.location; } set { model.location = value; } }
    private Vector3 nowDestination;


    // 上船时调用
    public void GoAboard(BoatController boat)
    {
      this.location = boat.location;
      this.model.isOnboard = true;
      this.transform.parent = boat.transform;

      this.nowDestination = boat.model.GetEmptyPosition();
      boat.GoAboard(this);
    }

    // 上岸时调用
    public void GoAshore(CoastController coast)
    {
      this.model.location = coast.location;
      this.model.isOnboard = false;
      this.transform.parent = null;

      this.nowDestination = coast.model.GetEmptyPosition();
      coast.GoAshore(this);
    }

    // 重置游戏
    public new void Reset()
    {
      // base.Reset();
      GoAshore((Director.GetInstance().currentSceneController as GameController).rightCoast);
      this.transform.position = this.nowDestination;

    }

    // 获取目的地
    public Vector3 GetDestination()
    {
      return nowDestination;
    }

    // 设置坐标
    public void SetPosition(Vector3 position)
    {
      transform.position = position;
    }

  }


}
