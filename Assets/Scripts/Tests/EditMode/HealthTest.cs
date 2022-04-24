using NUnit.Framework;
using System;

public class HealthTest
{
	public class Constructor
	{
		[TestCase(12)]
		[TestCase(1)]
		public void CurrentPointsHasStartingValue(int startingPoints)
		{
			var health = new Health(startingPoints);
			Assert.That(health.CurrentPoints, Is.EqualTo(startingPoints));
		}

		[TestCase(12)]
		[TestCase(1)]
		public void FullPointsHasStartingValue(int startingPoints)
		{
			var health = new Health(startingPoints);
			Assert.That(health.FullPoints, Is.EqualTo(startingPoints));
		}

		[TestCase(0)]
		[TestCase(-1)]
		public void ThrowsError_WhenStartingPointsIsInvalid(int startingPoints)
		{
			Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>(),
			delegate
			{
				new Health(startingPoints);
			});
		}

		[Test]
		public void IsDeadIsFalse()
		{
			var health = new Health(12);
			Assert.That(health.IsDead, Is.False);
		}
	}

	public class TakeDamage
	{
		[Test]
		public void CurrentPointsDecrease()
		{
			var health = new Health(12);
			health.TakeDamage(1);
			Assert.That(health.CurrentPoints, Is.EqualTo(11));
		}

		[TestCase(1, 2, 2)]
		[TestCase(1, 2, 3)]
		[TestCase(1, 2, 22)]
		[TestCase(-21, 2, 23)]
		public void CurrentPoints_WhenStartingPoints_ThenDamagePoints(
			int currentPoints,
			int startingPoints,
			int damagePoints)
		{
			var health = new Health(startingPoints);
			health.TakeDamage(damagePoints);
			Assert.That(health.CurrentPoints, Is.EqualTo(currentPoints));
		}

		[TestCase(0)]
		[TestCase(-1)]
		public void ThrowsError_WhenDamagePointsIsInvalid(int damagePoints)
		{
			var health = new Health(12);
			Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>(),
			delegate
			{
				health.TakeDamage(damagePoints);
			});
		}

		[Test]
		public void IsDead_AfterTwoInvocations()
		{
			var health = new Health(10);
			health.TakeDamage(9);
			Assert.That(health.IsDead, Is.False);
			health.TakeDamage(1);
			Assert.That(health.IsDead, Is.True);
		}
	}

	public class Replenish
	{
		[TestCase(0)]
		[TestCase(-1)]
		public void ThrowsError_WhenReplenishPointsIsInvalid(int replenishPoints)
		{
			var health = new Health(12);
			Exception ex = Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>(),
			delegate
			{
				health.Replenish(replenishPoints);
			});
			Assert.That(ex.Message, Does.Match("invalid").IgnoreCase);
		}

		[TestCase(4, 4, 0, 1)]
		[TestCase(4, 4, 3, 4)]
		[TestCase(4, 4, 3, 3)]
		[TestCase(3, 4, 3, 2)]
		[TestCase(2, 4, 3, 1)]
		public void CurrentPoints_WhenStartingPoints_ThenDamagePoints_ThenReplenishPoints(
			int currentPoints,
			int startingPoints,
			int damagePoints,
			int replenishPoints)
		{
			var health = new Health(startingPoints);
			health.TakeDamage(damagePoints);
			health.Replenish(replenishPoints);
			Assert.That(health.CurrentPoints, Is.EqualTo(currentPoints));
		}
	}
}