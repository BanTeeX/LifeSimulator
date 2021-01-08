using System.Collections.Generic;
using System.Linq;

namespace LifeSimulatorLibrary
{
	public class Tile
	{
		public int Id { get; set; }
		public List<Prey> Prey { get; set; } = new List<Prey>();
		public List<Predator> Predators { get; set; } = new List<Predator>();
		public List<Tile> Neighbours { get; set; } = new List<Tile>();

		public Tile(int id)
		{
			Id = id;
		}

		public void AddAnimal(Animal animal)
		{
			if (animal is Prey prey)
			{
				Prey.Add(prey);
			}
			else
			{
				Predators.Add(animal as Predator);
			}
		}

		public void RemoveAnimal(Animal animal)
		{
			if (animal is Prey prey)
			{
				Prey.Remove(prey);
			}
			else
			{
				Predators.Remove(animal as Predator);
			}
		}

		public Animal FindPartner(Animal animal)
		{
			if (animal is Prey prey)
			{
				return Prey.FirstOrDefault(x => x.IsAbleToReproduce && x.Gender != prey.Gender && x != prey);
			}
			else
			{
				return Predators.FirstOrDefault(x => x.IsAbleToReproduce && x.Gender != animal.Gender && x != animal);
			}
		}
	}
}
