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
				tile.Neighbours.Add(Tiles[(id - width) % Tiles.Length]);
				tile.Neighbours.Add(Tiles[(id + width) % Tiles.Length]);
				tile.Neighbours.Add(Tiles[(id - width) % Tiles.Length]);
				tile.Neighbours.Add(Tiles[(id + width) % Tiles.Length]);
			}

			Animal newAnimal;
			Tile randomTile;
			for (int i = 0; i < amountOfPrey; i++)
			{
				randomTile = Tiles[Random.Next(Tiles.Length)];
				newAnimal = new Prey(randomTile, (Gender)Random.Next(2));
				randomTile.Animals.Add(newAnimal);
			}
			for (int i = 0; i < amountOfPredators; i++)
			{
				randomTile = Tiles[Random.Next(Tiles.Length)];
				newAnimal = new Predator(randomTile, (Gender)Random.Next(2));
				randomTile.Animals.Add(newAnimal);
			}
			UpdateListAnimals();
		}

		public void Tic()
		{
			foreach (Animal animal in Animals)
			{
				if (animal is Predator predator)
				{
					predator.Eat();
				}
			}
			UpdateListAnimals();
			foreach (Animal animal in Animals)
			{
				animal.Reproduce();
			}
			UpdateListAnimals();
			foreach (Animal animal in Animals)
			{
				animal.Move();
			}
		}

		public void Start()
		{
			while(Animals.Where(animal => animal is Predator).Count() > 0)
			{
				Tic();
			}
		}

		public void UpdateListAnimals()
		{
			Animals.Clear();
			foreach (Tile tile in Tiles)
			{
				Animals.AddRange(tile.Animals);
			}
		}
	}
}
