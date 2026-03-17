using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSudoku.Models
{
	internal class Cell
	{	
		public bool IsOriginal { get; set; }
		public List<int> Notes { get; set; } = new List<int>();
		public int? Value { get; set; } = null;
		public int Row { get; set; }
		public int Column { get; set; }
	}
}
