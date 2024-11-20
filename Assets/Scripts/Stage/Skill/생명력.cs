using DataTable;

public class 생명력 : Skill_Passive
{
    private void Awake()
    {
        skill = Skill.GetList()[6];
    }

    public override void init()
    {
        float persent = GameManager.instance.health / GameManager.instance.baseHealth;
        GameManager.instance.baseHealth = Character.Health * (100 + level * skill.sk_increase) / 100;
        GameManager.instance.health = persent * GameManager.instance.baseHealth;
    }
}
