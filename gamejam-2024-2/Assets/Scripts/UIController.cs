using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController instance;
    public Slider estaminaSlide;

    void Awake() {
        instance = this;
    }

    public void UpdateEstamina(float atual, float max) {
        estaminaSlide.value = atual / max;
    }
}
