using UnityEngine;

public static class HotKeys
{
    public static int InvertMouseWheel = -1;

    public static KeyCode Escape = KeyCode.Escape;

    public static KeyCode Attack = KeyCode.Mouse0;

    public static KeyCode[] Elements = new KeyCode[5];

    public static KeyCode Element0 = KeyCode.Alpha1;
    public static KeyCode Element1 = KeyCode.Alpha2;
    public static KeyCode Element2 = KeyCode.Alpha3;
    public static KeyCode Element3 = KeyCode.Alpha4;
    public static KeyCode Element4 = KeyCode.Alpha5;

    public static void Init()
	{
        Elements[0] = Element0;
        Elements[1] = Element1;
        Elements[2] = Element2;
        Elements[3] = Element3;
        Elements[4] = Element4;
    }
}