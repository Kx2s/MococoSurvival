using DataTable;

public class 숙련 : Skill_Passive
{
    private void Awake()
    {
        skill = Skill.GetList()[4];
    }

    public override void init()
    {
        GameManager.instance.Count = level * skill.sk_increase;
    }
}
