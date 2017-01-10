using UnityEngine;
using System.Collections;
using GameCore;

public class PlayerManager : Singleton<PlayerManager>
{
    private static GameObject mainPlayer;

    public static GameObject MainPlayer 
    {
        get {
            return mainPlayer;
        }
        set {
            mainPlayer = value;
        }
    }
}
