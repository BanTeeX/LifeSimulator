using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeSimulatorLibrary
{
	public abstract class Animal
	{
		public Tile Position { get; set; }
		public Gender Gender { get; set; }
		public bool IsAbleToReproduce { get; set; } = true;

		protected Random Random { get; set; } = new Random();

		public Animal(Tile position, Gender gender)
		{
			Position = position;
			Gender = gender;
		}

		public void Move()
		{
			Console.WriteLine($"Move {GetType()}"); //###################################
			Tile newTile = Position.Neighbours[Random.Next(Position.Neighbours.Count)];
			newTile.Animals.Add(this);
			Position.Animals.Remove(this);
			Position = newTile;
			IsAbleToReproduce = true;
		}

		public void Die()
		{
			Console.WriteLine($"Die {GetType()}"); //#################################
			Position.Animals.Remove(this);
		}

		public abstract List<Animal> FindPartners();

		public abstract void GiveBirth();

		public void Reproduce()
		{
			List<Animal> temp = FindPartners();
			if (IsAbleToReproduce && temp.Count() > 0)
			{
				Console.WriteLine($"Reproduce {GetType()}"); //##########################
				IsAbleToReproduce = false;
				temp[0].IsAbleToReproduce = false;
				GiveBirth();
			}
		}
	}
}
