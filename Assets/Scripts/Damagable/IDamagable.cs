public interface IDamagable
{
	float HP { get; }
	void TakeDamage(float damage, bool ignoreDefence = false);
}