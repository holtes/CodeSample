using UnityEngine;

public class DayNight : MonoBehaviour {

    public Light sun;
    public AnimationCurve rotationX;
    public AnimationCurve rotationY;
    public AnimationCurve attenuation;
    public Gradient gradient;
    public Gradient ambientGradient;

    public void OnSliderChanged(float value){
        Vector3 angle = new Vector3(rotationX.Evaluate(value), rotationY.Evaluate(value), 0);
        sun.transform.localEulerAngles = angle;

        sun.color = gradient.Evaluate(value);
        sun.intensity = attenuation.Evaluate(value);
        RenderSettings.ambientLight = ambientGradient.Evaluate(value);
    }

}
