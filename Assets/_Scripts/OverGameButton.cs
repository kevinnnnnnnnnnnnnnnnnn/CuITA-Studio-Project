using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CulTA
{
    public class OverGameButton : MonoBehaviour
    {
        public void OnClickedLeaveDream()
        {
            TransitionManager.instance.canTransition = true;
            TransitionManager.instance.Transition("GameScene4", "LeaveDreamScene", 0, 0);
            TransitionManager.instance.canTransition = false;
            
            GameManager.instance.isGameStart = false;
        }



        public void OnClickedExitDream()
        {
            TransitionManager.instance.canTransition = true;
            TransitionManager.instance.Transition("GameScene4", "ExitDreamScene", 0, 0);
            TransitionManager.instance.canTransition = false;
        }
    }
}
