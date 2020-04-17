using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene{
    Main,
    Lobby,
    InGame,
}

namespace TestProject{
    public class SceneManager : GameObjectSingleton<SceneManager>
    {
        // public
        public static string currentScene { get => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;}

        public static void MoveScene(Scene scene, bool uwerTransition = true){
            Transition.LoadLevel(scene.ToString(), 0.2f, Color.black);
        }

        public static void MoveScene(string sceneName, bool useTransition = true)
        {
            Transition.LoadLevel(sceneName, 0.2f, Color.black);
        }
    }
}