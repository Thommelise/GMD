using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
  public int currHealth;
  public int maksHP = 100;
  public ProgressBar pb;
  private void Start()
  {
    currHealth = maksHP;
    pb.BarValue = currHealth;
  }

  public void HurtPlayer(int damage)
  {
    currHealth -= damage;
    pb.BarValue = currHealth;
    if (currHealth == 0)
    {
      FindObjectOfType<GameManager>().EndGame();
    }
  }

  public void HealPlayer(int healAmount)
  {
    currHealth += healAmount;
    pb.BarValue = currHealth;
  }
}
