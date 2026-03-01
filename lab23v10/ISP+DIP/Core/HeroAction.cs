using lab23.Interfaces;

namespace lab23.Core
{
    public class HeroAction
    {
        private readonly IAttack attack;
        private readonly IHeal heal;
        private readonly ITalk talk;

        // DIP — залежності передаються через конструктор
        public HeroAction(IAttack attack, IHeal heal, ITalk talk)
        {
            this.attack = attack;
            this.heal = heal;
            this.talk = talk;
        }

        public void DoAttack()
        {
            attack.Attack();
        }

        public void DoHeal()
        {
            heal.Heal();
        }

        public void DoTalk()
        {
            talk.Talk();
        }
    }
}