using System;

namespace lab22
{
    //ПОЧАТКОВА ІЄРАРХІЯ З ПОРУШЕННЯМ LSPS

    class Bird
    {
        
        public virtual void Fly()
        {
            Console.WriteLine("Bird is flying");
        }
    }

    class Penguin : Bird
    {
        // Пінгвін не літає = порушення контракту
        public override void Fly()
        {
            throw new NotImplementedException("Penguins cannot fly");
        }
    }

    class BirdClient
    {
        public static void MakeBirdFly(Bird bird)
        {
            bird.Fly();
        }
    }

    //АЛЬТЕРНАТИВНЕ РІШЕННЯ (LSP-СУМІСНЕ)

    interface IBird
    {
        void Move();
    }

    interface IFlyingBird
    {
        void Fly();
    }

    class Sparrow : IBird, IFlyingBird
    {
        public void Move()
        {
            Console.WriteLine("Sparrow is walking");
        }

        public void Fly()
        {
            Console.WriteLine("Sparrow is flying");
        }
    }

    class PenguinFixed : IBird
    {
        public void Move()
        {
            Console.WriteLine("Penguin is swimming");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" LSP violation demo ");
            Console.WriteLine();
            Bird bird1 = new Bird();
            BirdClient.MakeBirdFly(bird1);

            Bird bird2 = new Penguin();
            try
            {
                BirdClient.MakeBirdFly(bird2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("    LSP fixed version    ");
            Console.WriteLine();

            IBird sparrow = new Sparrow();
            sparrow.Move();

            IFlyingBird flyingSparrow = new Sparrow();
            flyingSparrow.Fly();

            IBird penguin = new PenguinFixed();
            penguin.Move();

            Console.WriteLine("Program finished correctly");
        }
    }
}
