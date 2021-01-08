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
			List<Animal> temp = Position.Animals.Where(animal => animal is Prey).ToList();
			if (temp.Count() > 0)
			{
				Console.WriteLine($"Eat {GetType()}"); //########################################
				temp[0].Die();
			}
			else
			{
				Die();
			}
		}

		public override List<Animal> FindPartners()
		{
			return Position.Animals.Where(animal => animal is Predator && animal.IsAbleToReproduce && animal.Gender != Gender && animal != this).ToList();
		}

		public override void GiveBirth()
		{
			Console.WriteLine($"Birth {GetType()}"); //########################################
			Position.Animals.Add(new Predator(Position, (Gender)Random.Next(2)));
		}
	}
}
