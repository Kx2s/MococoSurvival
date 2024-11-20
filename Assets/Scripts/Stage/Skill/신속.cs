using DataTable;

public class 신속 : Skill_Passive
{
    private void Awake()
    {
        skill = Skill.GetList()[2];
    }

    public override void init()
    {
        GameManager.instance.SpeedRateUp(skill.sk_increase);
    }
}
