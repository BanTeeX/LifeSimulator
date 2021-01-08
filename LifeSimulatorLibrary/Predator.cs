using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeSimulatorLibrary
{
	public class Predator : Animal
	{
		public Predator(Tile position, Gender gender) : base(position, gender)
		{
		}

		public void Eat()
		{
			if (Position.Prey.Count() > 0)
			{
				Console.WriteLine($"Eat {GetType()}"); //########################################
				Position.Prey[0].Die();
			}
			else
			{
				Die();
			}
		}

		public override void GiveBirth()
		{
			Console.WriteLine($"Birth {GetType()}"); //########################################
			Predator predator = new Predator(Position, (Gender)Random.Next(2));
			onBirth(predator);
			Position.AddAnimal(predator);
		}
	}
}
