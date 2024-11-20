using DataTable;
public class 치명 : Skill_Passive
{
    private void Awake()
    {
        skill = Skill.GetList()[0];
    }

    public override void init()
    {
        GameManager.instance.Critical = skill.sk_increase * level;
    }
}
