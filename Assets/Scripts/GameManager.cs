using UnityEngine;

public static class GameManager
{
	public static Vector2Int FieldSize;

	public static bool IsGamePaused;

    public static float GetDamageMult(ElementEnum striker, ElementEnum target)
	{
		int elemetDist = target - striker;
		if (elemetDist < 0)
		{
			elemetDist += System.Enum.GetNames(typeof(ElementEnum)).Length;
		}
		if (elemetDist == 1 || elemetDist == 3)
		{
			return _damageMult;
		}
		if (elemetDist == 0)
		{
			return 1;
		}
		return 1 / _damageMult;
	}

    private static float _damageMult = 2;
}