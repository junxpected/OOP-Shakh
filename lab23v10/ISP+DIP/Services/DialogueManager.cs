using System;
using lab23.Interfaces;

namespace lab23.Services
{
    public class DialogueManager : ITalk
    {
        public void Talk()
        {
            Console.WriteLine("Hero talks with NPC.");
        }
    }
}