using System;

public static class Validation
{
	public static (bool, int, string) Validate(int value, int lowestValidValue = Int32.MinValue, int highestValidValue = Int32.MaxValue)
	{
		string message = "";
		if (value >= lowestValidValue && value <= highestValidValue)
		{
			return (true, value, message);
		}

		message = $"Value {value} is invalid, it should be within the range of {lowestValidValue} and {highestValidValue}";
		int retValue = highestValidValue == Int32.MaxValue ? lowestValidValue : highestValidValue;
			return (false, retValue, message);
	}
}
