using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringHelper
{
    public static int FIRE = Animator.StringToHash("Fire");
    public static int IDLE = Animator.StringToHash("Idle");
    public static int WALK = Animator.StringToHash("Walk");
    public static int RUN = Animator.StringToHash("Run");
    public static int ATTACK = Animator.StringToHash("Attack");
    public static int DEATH = Animator.StringToHash("Death");
    public static string HIGH_SCORE = "High Score: ";
    public static string CURRENT_SCORE = "Score: ";
    public static string KILLED_ENEMY_COUNT = "Killed Enemy Count: ";
    public static string LEVEL = "Level: ";
}
