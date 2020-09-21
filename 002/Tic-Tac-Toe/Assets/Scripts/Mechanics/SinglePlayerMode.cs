using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe.Mechanics
{
  public class SinglePlayerMode : Basic
  {
    // ai 下一步需要走的坐标
    private int ai_x = -1;
    private int ai_y = -1;

    public override void AIMove()
    {
      ai_x = ai_y = -1;
      if (TryBlock()) { }
      else if (TryWin()) { }
      else
      {
        RandomMove();
      }
      SetHistory(ai_x, ai_y);
    }

    // 尝试赢
    private bool TryWin()
    {
      // AI is the Player 2.
      for (int i = 0; i < 3; ++i)
      {
        for (int j = 0; j < 3; ++j)
        {
          if (TryWinInCrossLine(i, j) || TryWinInRow(i, j) || TryWinInColumn(i, j))
          {
            return true;
          }
        }
      }
      return false;
    }

    // 尝试组织
    private bool TryBlock()
    {
      // AI is the Player 2.
      for (int i = 0; i < 3; ++i)
      {
        for (int j = 0; j < 3; ++j)
        {
          if (TryBlockInCrossLine(i, j) || TryBlockInRow(i, j) || TryBlockInColumn(i, j))
          {
            return true;
          }
        }
      }
      return false;
    }

    // 看斜线能否走，会顺带设置下一步的坐标
    private bool CheckInCrossLine(int i, int j, Player player)
    {
      if (i + j == 1 || history[i, j] != player) { return false; }
      if (i == 0)
      {
        if (j == 0 && history[1, 1] == player && history[2, 2] == Player.Unfinished)
        {
          ai_x = 2;
          ai_y = 2;
          return true;
        }
        if (j == 0 && history[2, 2] == player && history[1, 1] == Player.Unfinished)
        {
          ai_x = 1;
          ai_y = 1;
          return true;
        }
        if (j == 2 && history[1, 1] == player && history[0, 2] == Player.Unfinished)
        {
          ai_x = 0;
          ai_y = 2;
          return true;
        }
        if (j == 2 && history[0, 2] == player && history[1, 1] == Player.Unfinished)
        {
          ai_x = 1;
          ai_y = 1;
          return true;
        }
      }
      if (i == 1)
      {
        if (history[0, 0] == player && history[2, 2] == Player.Unfinished)
        {
          ai_x = 2;
          ai_y = 2;
          return true;
        }
        if (history[2, 2] == player && history[0, 0] == Player.Unfinished)
        {
          ai_x = 0;
          ai_y = 0;
          return true;
        }
        if (history[2, 0] == player && history[0, 2] == Player.Unfinished)
        {
          ai_x = 0;
          ai_y = 2;
          return true;
        }
        if (history[0, 2] == player && history[2, 0] == Player.Unfinished)
        {
          ai_x = 2;
          ai_y = 0;
          return true;
        }
      }
      if (i == 2)
      {
        if (j == 0 && history[1, 1] == player && history[2, 0] == Player.Unfinished)
        {
          ai_x = 2;
          ai_y = 0;
          return true;
        }
        if (j == 0 && history[2, 0] == player && history[1, 1] == Player.Unfinished)
        {
          ai_x = 1;
          ai_y = 1;
          return true;
        }
        if (j == 2 && history[1, 1] == player && history[0, 0] == Player.Unfinished)
        {
          ai_x = 0;
          ai_y = 0;
          return true;
        }
        if (j == 2 && history[0, 0] == player && history[1, 1] == Player.Unfinished)
        {
          ai_x = 1;
          ai_y = 1;
          return true;
        }
      }
      return false;
    }

    // 检查行是否能走，并且设置下一步坐标
    private bool CheckInRow(int i, int j, Player player)
    {
      if (history[i, j] != player) { return false; }
      if (j == 0)
      {
        if (history[i, 1] == player && history[i, 2] == Player.Unfinished)
        {
          ai_x = i;
          ai_y = 2;
          return true;
        }
        if (history[i, 2] == player && history[i, 1] == Player.Unfinished)
        {
          ai_x = i;
          ai_y = 1;
          return true;
        }
      }
      if (j == 1)
      {
        if (history[i, 0] == player && history[i, 2] == Player.Unfinished)
        {
          ai_x = i;
          ai_y = 2;
          return true;
        }
        if (history[i, 2] == player && history[i, 0] == Player.Unfinished)
        {
          ai_x = i;
          ai_y = 0;
          return true;
        }
      }
      if (j == 2)
      {
        if (history[i, 0] == player && history[i, 1] == Player.Unfinished)
        {
          ai_x = i;
          ai_y = 1;
          return true;
        }
        if (history[i, 1] == player && history[i, 0] == Player.Unfinished)
        {
          ai_x = i;
          ai_y = 0;
          return true;
        }
      }
      return false;
    }

    // 检查列是否能走，并且设置下一步坐标
    private bool CheckInColumn(int i, int j, Player player)
    {
      if (history[i, j] != player) { return false; }
      if (i == 0)
      {
        if (history[1, j] == player && history[2, j] == Player.Unfinished)
        {
          ai_x = 2;
          ai_y = j;
          return true;
        }
        if (history[2, j] == player && history[1, j] == Player.Unfinished)
        {
          ai_x = 1;
          ai_y = j;
          return true;
        }
      }
      if (i == 1)
      {
        if (history[0, j] == player && history[2, j] == Player.Unfinished)
        {
          ai_x = 2;
          ai_y = j;
          return true;
        }
        if (history[2, j] == player && history[0, j] == Player.Unfinished)
        {
          ai_x = 0;
          ai_y = j;
          return true;
        }
      }
      if (i == 2)
      {
        if (history[0, j] == player && history[1, j] == Player.Unfinished)
        {
          ai_x = 1;
          ai_y = j;
          return true;
        }
        if (history[1, j] == player && history[0, j] == Player.Unfinished)
        {
          ai_x = 0;
          ai_y = j;
          return true;
        }
      }
      return false;
    }


    // 检查斜线是否能赢
    private bool TryWinInCrossLine(int i, int j)
    {
      return CheckInCrossLine(i, j, Player.Second);
    }

    // 检查行是否能赢
    private bool TryWinInRow(int i, int j)
    {
      return CheckInRow(i, j, Player.Second);
    }

    // 检查列是否能赢
    private bool TryWinInColumn(int i, int j)
    {
      return CheckInColumn(i, j, Player.Second);
    }

    // 检查玩家斜线是否能赢，尝试阻止
    private bool TryBlockInCrossLine(int i, int j)
    {
      return CheckInCrossLine(i, j, Player.First);
    }

    // 检查玩家行是否能赢，尝试阻止
    private bool TryBlockInRow(int i, int j)
    {
      return CheckInRow(i, j, Player.First);
    }

    // 检查玩家列是否能赢，尝试阻止
    private bool TryBlockInColumn(int i, int j)
    {
      return CheckInColumn(i, j, Player.First);
    }

    // 随机走一步
    private void RandomMove()
    {
      List<int> row = new List<int>();
      List<int> col = new List<int>();
      int count = 0;
      for (int i = 0; i < 3; ++i)
      {
        for (int j = 0; j < 3; ++j)
        {
          if (history[i, j] == Player.Unfinished)
          {
            row.Add(i);
            col.Add(j);
            count++;
          }
        }
      }
      // Here count must be greater than 0.
      System.Random rand = new System.Random();
      int index = rand.Next(0, count);
      ai_x = row[index];
      ai_y = col[index];
    }
  }
}
