public static class SceneRoot
{
    private static JointPin selectedPin;

    public static void JointPinClicked(JointPin pin)
    {
        if (selectedPin != null)
        {
            selectedPin.SetSelection(false);
        }

        if (pin == selectedPin)
        {
            selectedPin = null;
        }
        else
        {
            selectedPin = pin;
            selectedPin.SetSelection(true);
        }
    }

    public static void JointHoleClicked(JointHole hole)
    {
        if (selectedPin != null)
        {
            selectedPin.SetSelection(false);
            selectedPin.MoveTo(hole.transform.position);
            selectedPin = null;
        }
    }
}
