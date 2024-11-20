using DataTable;

public class 금괴더미 : Skill_Passive
{
    private void Awake()
    {
        skill = Skill.GetList()[8];
    }

    public override void init()
    {
        GameManager.instance.GoldBoost = level * skill.sk_increase;
    }
}
