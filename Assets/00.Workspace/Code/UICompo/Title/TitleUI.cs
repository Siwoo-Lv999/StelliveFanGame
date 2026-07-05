using Manager;
using UnityEngine;

namespace TitleUI {
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