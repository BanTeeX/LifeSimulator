using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeSimulatorLibrary
{
	public class Prey : Animal
	{
		public Prey(Tile position, Gender gender) : base(position, gender)
		{
		}

		public override void GiveBirth()
		{
			Console.WriteLine($"Birth {GetType()}"); //########################################
			Prey newPrey = new Prey(Position, (Gender)Random.Next(2));
			onBirth(newPrey);
			Position.AddAnimal(newPrey);
		}
	}
}
