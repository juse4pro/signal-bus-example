namespace GameMath;

public static class CombatMath
{
	/// <param name="baseDamage">The base damage.</param>
	/// <param name="extraDamageMultiplier">Every value above 0 will increase damage by percents.</param>
	/// <param name="damageReductionScalar">Combat result is getting scaled by this.</param>
	public static int GetEffectiveDamage(int baseDamage, float extraDamageMultiplier, float damageReductionScalar)
	{
		if (extraDamageMultiplier < 0f)
		{
			throw new ArgumentOutOfRangeException(nameof(extraDamageMultiplier), "Damage multiplier must be greater than or equal to 0.");
		}
		
		if (damageReductionScalar > 1f || damageReductionScalar < 0f)
		{
			throw new ArgumentOutOfRangeException(nameof(damageReductionScalar), "Damage reduction scalar must be less than or equal to 1.");
		}
		
		return (int)((float)baseDamage * (extraDamageMultiplier) * damageReductionScalar);
	}
}