public interface IHealable
{
	float HP { get; }
	void TakeHeal(float heal);
}