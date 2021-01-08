using LifeSimulatorLibrary;

namespace LifeSimulatorConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Simulation simulation = new Simulation(1, 1, 10, 9);
			simulation.Start();
		}
	}
}
