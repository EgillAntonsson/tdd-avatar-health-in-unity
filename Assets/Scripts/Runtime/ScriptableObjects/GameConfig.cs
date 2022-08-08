using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "GameConfigInstance",
menuName = "Avatar Health/Create GameConfig Instance",
order = 1)]
public class GameConfig : ScriptableObject {
	public int StartingPoints = 12;
	public int PointsPerUnit = 4;
	public int MaxUnits = 30;
	public int MaxNegativePointsForInstantKillProtection = -20;

	private void OnValidate()
	{
		var validation = Validation.Validate(StartingPoints, 1);
		StartingPoints = ProcessValidation(validation, nameof(StartingPoints));

		validation = Validation.Validate(PointsPerUnit, 2);
		PointsPerUnit = ProcessValidation(validation, nameof(PointsPerUnit));

		validation = Validation.Validate(MaxNegativePointsForInstantKillProtection, Int32.MinValue, -1);
		MaxNegativePointsForInstantKillProtection = ProcessValidation(validation, nameof(MaxNegativePointsForInstantKillProtection));

		double lowestMaxUnits = Math.Ceiling((double)StartingPoints / (double)PointsPerUnit);
		validation = Validation.Validate(MaxUnits, (int)lowestMaxUnits);
		MaxUnits = ProcessValidation(validation, nameof(MaxUnits));
	}

	private int ProcessValidation((bool IsValid, int Value, string FailMessage) validation, string propertyName)
	{
		if (!validation.IsValid)
			{
				Debug.LogWarning(validation.FailMessage + $", for '{propertyName}'. Will set value to {validation.Value}.");
			}
			return validation.Value;
	}
}
