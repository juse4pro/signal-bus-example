using GameMath;

namespace GameMath.Test;

public class CombatMathTests
{
	[Theory]
	[InlineData(10, 0f, 1f, 10)]
	[InlineData(10, 1f, 1f, 20)]
	[InlineData(10, 0f, 0.5f, 5)]
	[InlineData(10, 1f, 0.5f, 10)]
	public void GetEffectiveDamage_ShouldReturnCorrectUnscaledDamage(int baseDamage, float damageMultiplier, float damageReductionScalar, int expectedDamage)
	{
		int result = CombatMath.GetEffectiveDamage(baseDamage, damageMultiplier, damageReductionScalar);
		Assert.Equal(expectedDamage, result);
	}


	[Fact]
	public void GetEffectiveDamage_ShouldFailWithWrongValues()
	{
		Assert.Throws<ArgumentOutOfRangeException>(
			() => CombatMath.GetEffectiveDamage(10, 1f, -1f)
		);
	}
}