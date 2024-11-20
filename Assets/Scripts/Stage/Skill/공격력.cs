using DataTable;

public class 공격력 : Skill_Passive
{
    private void Awake()
    {
        skill = Skill.GetList()[7];
    }

    public override void init()
    {
        GameManager.instance.AttackRateUp(skill.sk_increase);
    }
}
