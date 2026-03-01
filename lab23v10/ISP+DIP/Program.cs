using lab23.Core;
using lab23.Services;


namespace lab23
{
    class Program
    {
        static void Main(string[] args)
        {
            var weapon = new WeaponSystem();
            var medkit = new MedicalKit();
            var dialogue = new DialogueManager();

            var hero = new HeroAction(weapon,medkit,dialogue);


            hero.DoAttack();
            hero.DoHeal();
            hero.DoTalk();




        }
    }
}