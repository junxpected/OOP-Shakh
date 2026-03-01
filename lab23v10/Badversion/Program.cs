using System;

namespace lab23
{
    class Program
    {
        static void Main(string[] args)
        {
            HeroAction hero = new HeroAction();

            hero.Attack();
            hero.Heal();
            hero.Talk();
        }
    }
}