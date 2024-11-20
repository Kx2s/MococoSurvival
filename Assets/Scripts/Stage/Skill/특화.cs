using DataTable;

public class 특화 : Skill_Passive
{
    private void Awake()
    {
        skill = Skill.GetList()[1];
    }

    public override void init()
    {
        GameManager.instance.Range = skill.sk_increase * level;
    }
}
