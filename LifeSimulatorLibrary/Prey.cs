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

		public override List<Animal> FindPartners()
		{
			return Position.Animals.Where(animal => animal is Prey && animal.IsAbleToReproduce && animal.Gender != Gender && animal != this).ToList();
		}

		public override void GiveBirth()
		{
			Console.WriteLine($"Birth {GetType()}"); //########################################
			Position.Animals.Add(new Prey(Position, (Gender)Random.Next(2)));
		}
	}
}
