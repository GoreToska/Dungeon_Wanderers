public interface IDamagable
{
	float HP { get; }
	void TakeDamage(float damage, bool ignoreDefence = false);
}

public interface IStamina
{
	float Stamina { get; }
	void TakeStaminaDamage(float damage, bool ignoreDefence = false);
	void RegenStamina(float value);
}