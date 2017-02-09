using UnityEngine;

namespace SmartTagsAndLayers
{
	public class Layer
	{
		public Layer(string _name)
		{
			name = _name;
		}

		protected string name = "Default";

		public string Name { get { return name; } }
		public int Mask { get { return LayerMask.GetMask(name); } }
		public int Get { get { return LayerMask.NameToLayer(name); } }
	}
}