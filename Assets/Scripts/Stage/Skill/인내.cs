using DataTable;

public class 인내 : Skill_Passive
{
    private void Awake()
    {
        skill = Skill.GetList()[3];
    }

    public override void init()
    {
        GameManager.instance.Reduces = level * skill.sk_increase;
    }
}
