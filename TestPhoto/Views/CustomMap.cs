using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace TestXamarinFirebase
{
	public class CustomMap : Map
	{
		public CustomCircle Circle { get; set; }
        public List<CustomPin> CustomPins { get; set; }
		public List<CustomCircle> CustomCircle { get; set; }


		public CustomMap()
		{
			CustomPins = new List<CustomPin>();
			CustomCircle = new List<CustomCircle>();
		}
	}
}
