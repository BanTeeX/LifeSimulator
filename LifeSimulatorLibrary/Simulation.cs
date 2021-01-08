using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeSimulatorLibrary
{
	public class Simulation
	{
		public int Width { get; set; }
		public int Height { get; set; }
		public int AmountOfPrey { get; set; }
		public int AmountOfPredators { get; set; }
		public Tile[] Tiles { get; set; }
		public List<Animal> Animals { get; set; } = new List<Animal>();
		public List<Animal> DeadAnimals { get; set; } = new List<Animal>();
		public List<Animal> BornAnimals { get; set; } = new List<Animal>();

		private Random Random { get; set; } = new Random();

		public Simulation(int width, int height, int amountOfPrey, int amountOfPredators)
		{
			Width = width;
			Height = height;
			AmountOfPrey = amountOfPrey;
			AmountOfPredators = amountOfPredators;

			Tiles = new Tile[width * height];
			for (int i = 0; i < Tiles.Length; i++)
			{
				Tiles[i] = new Tile(i);
			}
			int id;
			foreach (Tile tile in Tiles)
			{
				id = tile.Id + Tiles.Length;
				tile.Neighbours.Add(Tiles[(id - 1) % Tiles.Length]);
				tile.Neighbours.Add(Tiles[(id + 1) % Tiles.Length]);
				tile.Neighbours.Add(Tiles[(id - width) % Tiles.Length]);
				tile.Neighbours.Add(Tiles[(id + width) % Tiles.Length]);
			}

			Animal newAnimal;
			Tile randomTile;
			for (int i = 0; i < amountOfPrey; i++)
			{
				randomTile = Tiles[Random.Next(Tiles.Length)];
				newAnimal = new Prey(randomTile, (Gender)Random.Next(2))
				{
					onDeath = AddDeadAnimal,
					onBirth = AddBornAnimal
				};
				randomTile.AddAnimal(newAnimal);
				Animals.Add(newAnimal);
			}
			for (int i = 0; i < amountOfPredators; i++)
			{
				randomTile = Tiles[Random.Next(Tiles.Length)];
				newAnimal = new Predator(randomTile, (Gender)Random.Next(2))
				{
					onDeath = AddDeadAnimal,
					onBirth = AddBornAnimal
				};
				randomTile.AddAnimal(newAnimal);
				Animals.Add(newAnimal);
			}
		}

		public void Start()
		{
			while(true)
			{
				foreach (Animal animal in Animals)
				{
					if (animal is Predator predator)
					{
						predator.Eat();
					}
				}
				CollectDeadAnimals();
				if (Animals.Where(animal => animal is Predator).Count() == 0)
				{
					break;
				}
				foreach (Animal animal in Animals)
				{
					animal.Reproduce();
				}
				CollectBornAnimals();
				foreach (Animal animal in Animals)
				{
					animal.Move();
				}
			}
		}

		public void AddDeadAnimal(Animal animal)
		{
			DeadAnimals.Add(animal);
		}

		public void AddBornAnimal(Animal animal)
		{
			animal.onBirth = AddBornAnimal;
			animal.onDeath = AddDeadAnimal;
			BornAnimals.Add(animal);
		}

		public void CollectDeadAnimals()
		{
			foreach (Animal deadAnimal in DeadAnimals)
			{
				Animals.Remove(deadAnimal);
			}
			DeadAnimals.Clear();
		}

		public void CollectBornAnimals()
		{
			foreach (Animal bornAnimal in BornAnimals)
			{
				Animals.Add(bornAnimal);
			}
			BornAnimals.Clear();
		}
	}
}
