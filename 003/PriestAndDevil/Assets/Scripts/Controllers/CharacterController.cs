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

    // 上船时调用
    public void GoAboard(BoatController boat)
    {
      location = boat.location;
      model.isOnboard = true;
      transform.parent = boat.transform;

      SetDestination(boat.model.GetEmptyPosition());
      boat.GoAboard(this);
    }

    // 上岸时调用
    public void GoAshore(CoastController coast)
    {
      model.location = coast.location;
      model.isOnboard = false;
      transform.parent = null;

      SetDestination(coast.model.GetEmptyPosition());
      coast.GoAshore(this);
    }

    // 重置游戏
    public new void Reset()
    {
      base.Reset();
      GoAshore((Director.GetInstance().currentSceneController as GameController).rightCoast);
    }
  }
}
