using System.Collections.Generic;

namespace LifeSimulatorLibrary
{
	public class Tile
	{
		public int Id { get; set; }
		public List<Animal> Animals { get; set; } = new List<Animal>();
		public List<Tile> Neighbours { get; set; } = new List<Tile>();

		public Tile(int id)
		{
			Id = id;
		}
	}
}
