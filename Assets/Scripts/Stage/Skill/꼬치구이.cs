using DataTable;

public class 꼬치구이 : Skill_Passive
{
    private void Awake()
    {
        skill = Skill.GetList()[9];
    }

    public override void init()
    {
        GameManager.instance.ExpBoost = level * skill.sk_increase;
    }
}