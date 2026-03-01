using System;
using lab23.Interfaces;

namespace lab23.Services
{
    public class WeaponSystem : IAttack
    {
        public void Attack()
        {
            Console.WriteLine("Hero hits enemy with sword!");
        }
    }
}