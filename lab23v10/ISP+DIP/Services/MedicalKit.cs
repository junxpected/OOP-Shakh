using System;
using lab23.Interfaces;

namespace lab23.Services
{
    public class MedicalKit : IHeal
    {
        public void Heal()
        {
            Console.WriteLine("Hero heals himself!");
        }
    }
}