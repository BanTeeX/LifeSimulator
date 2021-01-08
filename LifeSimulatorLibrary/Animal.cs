using System;

namespace LifeSimulatorLibrary
{
	public enum Gender
	{
		Male,
		Female
	}

	public delegate void OnDeath(Animal animal);
	public delegate void OnBirth(Animal animal);

	public abstract class Animal
	{
		public Tile Position { get; set; }
		public Gender Gender { get; set; }
		public bool IsAbleToReproduce { get; set; } = true;
		public OnDeath onDeath;
		public OnBirth onBirth;

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
			newTile.AddAnimal(this);
			Position.RemoveAnimal(this);
			Position = newTile;
			IsAbleToReproduce = true;
		}

		public void Die()
		{
			Console.WriteLine($"Die {GetType()}"); //#################################
			onDeath(this);
			Position.RemoveAnimal(this);
		}

		public abstract void GiveBirth();

		public void Reproduce()
		{
			if (IsAbleToReproduce)
			{
				Animal temp = Position.FindPartner(this);
				if (temp != null)
				{
					Console.WriteLine($"Reproduce {GetType()}"); //##########################
					IsAbleToReproduce = false;
					temp.IsAbleToReproduce = false;
					GiveBirth();
				}
			}
		}
	}
}
