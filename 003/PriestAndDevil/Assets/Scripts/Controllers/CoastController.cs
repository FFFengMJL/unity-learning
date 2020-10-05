using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PriestsAndDevils
{
  public class CoastController : MonoBehaviour
  {
    public Coast model = new Coast();
    public new string name { get { return model.name; } set { model.name = value; } }
    public Location location { get { return model.location; } set { model.location = value; } }

    // 上船时调用
    public void GoAboard(CharacterController character)
    {
      var characters = model.characters;
      for (int i = 0; i < characters.Length; ++i)
      {
        if (characters[i] != null &&
            characters[i].name == character.name)
        {
          characters[i] = null;
        }
      }
    }

    // 上岸时调用
    public void GoAshore(CharacterController character)
    {
      int index = model.GetEmptyIndex();
      model.characters[index] = character.model;
    }

    // 重置游戏时调用
    public void Reset()
    {
      model.characters = new Character[6];
    }
  }
}
