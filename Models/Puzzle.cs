using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSudoku.Models
{
	internal class Puzzle
	{
		public Board PlayerBoard { get; set; }
		public Board Solution { get; set; }
		public Difficulty Difficulty { get; set; }
	}

	internal enum Difficulty
	{
		Easy,
		Medium,
		Hard
	}
}
