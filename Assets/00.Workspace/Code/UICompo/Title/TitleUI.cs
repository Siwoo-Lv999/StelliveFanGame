using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UICompo.Title {
    public class TitleUI : MonoBehaviour {
        
        
        public void PressedStartBtn() {
            FadeManager.Instance.MoveScene("TestStageScene");
        }

        public void PressedSettingBtn() {
            
        }
        
        public void PressedQuitBtn() {
            
        }
    }
}