using DataTable;

public class 제압 : Skill_Passive
{
    private void Awake()
    {
        skill = Skill.GetList()[5];
    }

    public override void init()
    {
        GameManager.instance.Slow = level * skill.sk_increase;
    }
}
