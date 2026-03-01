using System;

namespace lab23
{
    public class HeroAction
    {
        private WeaponSystem weaponSystem;
        private MedicalKit medicalKit;
        private DialogueManager dialogueManager;

        public HeroAction()
        {
            // Порушення DIP — створюємо залежності всередині класу
            weaponSystem = new WeaponSystem();
            medicalKit = new MedicalKit();
            dialogueManager = new DialogueManager();
        }

        public void Attack()
        {
            weaponSystem.Hit();
        }

        public void Heal()
        {
            medicalKit.UseKit();
        }

        public void Talk()
        {
            dialogueManager.StartDialogue();
        }
    }
}